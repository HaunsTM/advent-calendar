using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using advent_calendar.Models;
using advent_calendar.Models.POCO;
using advent_calendar.Models.ViewModels;
using advent_calendar.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace advent_calendar.Controllers
{
    [Authorize]
    public class SlotsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser CurrentLoggedInUser()
        {
            ApplicationUser user;
            var userId = Convert.ToInt32(User.Identity.GetUserId());

            user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
            return user;
        }

        private ApplicationUser CurrentLoggedInUsersUserAdministrator(ApplicationUser currentLoggedInUser)
        {
            var currentLoggedInUserRole = this.CurrentLoggedInUserRole(currentLoggedInUser);
            if (currentLoggedInUserRole.Name == ConfigurationManager.AppSettings["STANDARD_USER"])
            {
                return currentLoggedInUser.IsAdministratedBy;
            }
            else
            {
                return currentLoggedInUser;
            }
        }

        private ApplicationUserRole CurrentLoggedInUserRole(ApplicationUser currentLoggedInUser)
        {
            ApplicationUserRole currentLoggedInUserRole;
            
                currentLoggedInUserRole = (from cLIUR in db.ApplicationUserRoles
                    where cLIUR.Id == currentLoggedInUser.ApplicationUserRole.Id
                    select cLIUR).FirstOrDefault();
            currentLoggedInUserRole = currentLoggedInUser.ApplicationUserRole;
            return currentLoggedInUserRole;
        }

        private Calendar CurrentCalendar(int calendarYear, ApplicationUser calendarsUser)
        {
            bool activeStatusToSearchFor = true;
            Calendar currentCalendar = null;

            currentCalendar = (from c in db.Calendars
                from u in db.Users
                where (c.Year == calendarYear && c.Active == activeStatusToSearchFor) &&
                      u.Id == calendarsUser.Id
                select c).FirstOrDefault();
            return currentCalendar;

        }


        #region UploadSlots

        public async Task<HttpResponseMessage> UploadSlot()
        {
            var currentLoggedInUser = this.CurrentLoggedInUser();
            var currentLoggedInUserRole = this.CurrentLoggedInUserRole(currentLoggedInUser);

            if (!(currentLoggedInUserRole.Name == ConfigurationManager.AppSettings["SUPER_ADMINISTRATOR"] ||
                  currentLoggedInUserRole.Name == ConfigurationManager.AppSettings["USER_ADMINISTRATOR"]))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    "Lack of sufficient privileges to perform operation.");
            }

            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "Unsupported media type.");
            }
            // Read the file and form data.
            var provider = new MultipartFormDataMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            //// Extract the fields from the form data.
            /**/
            var calendarYear = int.Parse(provider.FormData["calendarYear"].ToString());
            var slotNumber = int.Parse(provider.FormData["slotNumber"].ToString());
            var slotMessage = provider.FormData["slotMessage"].ToString();

            var currentCalendar = CurrentCalendar(calendarYear, currentLoggedInUser);

            // Check if files are on the request.
            if (!provider.FileStreams.Any())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No file uploaded.");
            }

            foreach (KeyValuePair<string, Stream> file in provider.FileStreams)
            {
                string fileName = file.Key;
                Stream stream = file.Value;
                /**/
                string contentType = fileName.Substring(fileName.LastIndexOf('.'));
                //do we have any old values which should be updated?
                InactivatePossibleEarlierSlotWithSameNumber(currentCalendar, slotNumber);
                SaveToDB(stream, contentType, currentCalendar, slotNumber, slotMessage);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Successfully Uploaded: ");
        }

        private byte[] FileAsByteArray(System.IO.Stream sourceStream)
        {
            byte[] fileAsByteArray = null;
            using (var memoryStream = new MemoryStream())
            {
                sourceStream.CopyTo(memoryStream);
                fileAsByteArray = memoryStream.ToArray();
            }

            return fileAsByteArray;
        }

        private bool SaveToDB(System.IO.Stream sourceStream, string contentType, Calendar calendar, int slotNumber,
            string slotMessage)
        {
            bool saved = false;
            var adventCalendarMonth = 12;
            DateTime earliestDateOfAllowedOpeningTime = new DateTime(year: calendar.Year, month: adventCalendarMonth,
                day: slotNumber);

            var slotToSave = new Slot
            {
                Active = true,
                Content = FileAsByteArray(sourceStream),
                ContentType = contentType,
                Calendar = calendar,
                EarliestDateOfAllowedOpeningTime = earliestDateOfAllowedOpeningTime,
                Number = slotNumber,
                Opened = null,
                SlotMessage = slotMessage
            };

            if (ModelState.IsValid)
            {
                db.Slots.Add(slotToSave);
                db.SaveChanges();
                saved = true;
            }

            return saved;
        }

        private bool InactivatePossibleEarlierSlotWithSameNumber(Calendar calendar, int slotNumber)
        {
            bool done = false;
            bool activeStatusToSearchFor = true;
            bool activeStatusAfterUpdate = false;
            
                var stillActiveSlots = from s in db.Slots
                    where s.Active == activeStatusToSearchFor && s.Calendar.Id == calendar.Id
                    select s;

                foreach (var stillActiveSlot in stillActiveSlots)
                {
                    stillActiveSlot.Active = activeStatusAfterUpdate;
                }
                // Submit the changes to the database.
            try
            {
                db.SaveChanges();
                done = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return done;
        }


        #endregion


        /********************************************************************************************************************************************************/


        // GET: api/Slots/5
        [HttpGet]
        [ResponseType(typeof(SlotViewModel))]
        public async Task<IHttpActionResult> OpenSlot(int calendarYear, int slotNumber)
        {
            var currentLoggedInUser = this.CurrentLoggedInUser();
            var currentLoggedInUsersUserAdministrator = this.CurrentLoggedInUsersUserAdministrator(currentLoggedInUser);
            Calendar wantedCalendar;
            CalendarViewModel wantedCalendarViewModel;



            wantedCalendar = await (from wC in db.Calendars
                from u in db.Users
                where
                wC.Active == true && wC.Year == calendarYear &&
                u.Id == currentLoggedInUser.Id
                select wC).FirstOrDefaultAsync();
                

            if (wantedCalendar == null)
            {
                return Content(HttpStatusCode.NotFound, String.Format("Calendar, {0}, doesn't exist", calendarYear.ToString()));
            }
            else
            {
                Slot wantedSlot;
                wantedCalendarViewModel = new CalendarViewModel(wantedCalendar);
                //wantedCalendar = null;

                wantedSlot = await (from wS in db.Slots
                    where wS.Active == true && wS.Number == slotNumber && wS.Calendar.Id == wantedCalendarViewModel.Id
                    select wS).FirstOrDefaultAsync();
                        
                
                if (wantedSlot == null)
                {
                    return Content(HttpStatusCode.NotFound, String.Format("Slot {{calendar year: {0}; slot number: {1}}} doesn't exist", calendarYear.ToString(), slotNumber.ToString()));
                }
                else
                {
                    if (wantedSlot.EarliestDateOfAllowedOpeningTime > DateTime.Now)
                    {
                        return Content(HttpStatusCode.Forbidden, 
                            String.Format("The slot {{calendar year: {0}; slot number: {1}}} must not be opened before {2}!", 
                                calendarYear.ToString(), 
                                slotNumber.ToString(),
                                wantedSlot.EarliestDateOfAllowedOpeningTime.ToShortDateString()));
                    }
                    else
                    {
                        wantedSlot.Opened=DateTime.Now;
                        var wantedSlotViewModel = new SlotViewModel(wantedSlot);
                        await db.SaveChangesAsync();
                        wantedSlot = null;
                        return Ok(wantedSlotViewModel);
                    }
                }
            }
        }
    }
}