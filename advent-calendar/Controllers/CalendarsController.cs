using System;
using System.Collections.Generic;
using System.Configuration;
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
using advent_calendar.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace advent_calendar.Controllers
{
    [Authorize]
    public class CalendarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUser CurrentLoggedInUser()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
            return user;
        }

        private ApplicationUserRole CurrentLoggedInUserRole(ApplicationUser currentLoggedInUser)
        {
            var currentLoggedInUserRole = currentLoggedInUser.ApplicationUserRole;
            return currentLoggedInUserRole;
        }

        #region UploadCalendar

        [System.Web.Mvc.HttpPost]
        public async Task<HttpResponseMessage> UploadCalendar()
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
            MultipartFormDataMemoryStreamProvider provider = new MultipartFormDataMemoryStreamProvider();
            try
            {

            await Request.Content.ReadAsMultipartAsync(provider);
            }
            catch (Exception ex)
            {
                int i = 0;
                throw;
            }

            // Extract the fields from the form data.
            string calendarName = provider.FormData["calendarName"];
            string calendarYear = provider.FormData["calendarYear"];


            // Check if files are on the request.
            if (!provider.FileStreams.Any())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No file uploaded.");
            }

            foreach (KeyValuePair<string, Stream> file in provider.FileStreams)
            {
                string fileName = file.Key;
                Stream stream = file.Value;
                
                // Do something with the uploaded file
                //UploadManager.Upload(stream, fileName, uploadType, description);
                SaveToDB(currentLoggedInUser, stream, fileName.Substring(fileName.LastIndexOf('.') + 1), calendarName,
                    int.Parse(calendarYear));
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

        private bool SaveToDB(ApplicationUser currentLoggedInUser, System.IO.Stream sourceStream, string contentType,
            string name, int year)
        {
            bool saved = false;
            var calendarToSave = new Calendar
            {
                Active = true,
                ApplicationUsers = new List<ApplicationUser> {currentLoggedInUser},
                Content = FileAsByteArray(sourceStream),
                ContentType = contentType,
                Name = name,
                Year = year
            };
            if (ModelState.IsValid)
            {
                db.Calendars.Add(calendarToSave);
                db.SaveChanges();
                //do we have any old values which should be updated?
                InactivatePossibleEarlierCalendarByYearAndUpdatePossibleSlots(calendarToSave.Year, currentLoggedInUser, calendarToSave);
                saved = true;
            }
            return saved;
        }


        private bool InactivatePossibleEarlierCalendarByYearAndUpdatePossibleSlots(int calendarYear, ApplicationUser currentLoggedInUser, Calendar newCalendarThatShouldBeActive)
        {
            bool done = false;
            bool activeStatusAfterUpdate = false;
            try
            {
                var currentLoggedInUsersEarlierCalendarsThatShouldBeInactive =
                    db.Calendars
                    .Where(cal => cal.ApplicationUsers.All(y => y.Id == currentLoggedInUser.Id)) //get all calendars by the current logged in user
                    .Where(cal => cal.Id != newCalendarThatShouldBeActive.Id) //exclude the calendar that should be active
                    .ToList();
                

                foreach (var earlierCalendar in currentLoggedInUsersEarlierCalendarsThatShouldBeInactive)
                {
                    earlierCalendar.Active = activeStatusAfterUpdate;

                    var earlierCalendarsSlots = (from s in db.Slots
                        where s.Calendar.Id == earlierCalendar.Id
                        select s).ToList();

                    foreach (var slot in earlierCalendarsSlots)
                    {
                        //tie existing slots to the new calendar
                        slot.Calendar = newCalendarThatShouldBeActive;
                    }
                }
                // Submit the changes to the database.

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

        #region GetCalendarByYearAndCurrentLoggedInUser

        public async Task<IHttpActionResult> GetCalendarByYearAndCurrentLoggedInUser(int year)
        {
            var currentLoggedInUser = CurrentLoggedInUser();

            var calendar = await 
                db.Calendars
                    .Where(cal => cal.ApplicationUsers.All(y => y.Id == currentLoggedInUser.Id)) //get all calendars by the current logged in user
                    .Where(cal => cal.Active ==true) //make sure to get an active one
                    .FirstOrDefaultAsync();

            if (calendar == null)
            {
                return NotFound();
            }

            var vmCalendar = new Models.ViewModels.CalendarViewModel(calendar);
            return Ok(vmCalendar);
            
        }

        #endregion

        /********************************************************************************/







        // GET: api/Calendars
        public IQueryable<Calendar> GetCalendars()
        {
            return db.Calendars;
        }

        // GET: api/Calendars/5
        [ResponseType(typeof(Calendar))]
        public async Task<IHttpActionResult> GetCalendar(int id)
        {
            Calendar calendar = await db.Calendars.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            return Ok(calendar);
        }

        // PUT: api/Calendars/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCalendar(int id, Calendar calendar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calendar.Id)
            {
                return BadRequest();
            }

            db.Entry(calendar).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Calendars
        [ResponseType(typeof(Calendar))]
        public async Task<IHttpActionResult> PostCalendar(Calendar calendar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Calendars.Add(calendar);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = calendar.Id }, calendar);
        }

        // DELETE: api/Calendars/5
        [ResponseType(typeof(Calendar))]
        public async Task<IHttpActionResult> DeleteCalendar(int id)
        {
            Calendar calendar = await db.Calendars.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            db.Calendars.Remove(calendar);
            await db.SaveChangesAsync();

            return Ok(calendar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CalendarExists(int id)
        {
            return db.Calendars.Count(e => e.Id == id) > 0;
        }
    }
}