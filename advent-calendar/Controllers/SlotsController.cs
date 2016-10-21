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
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
            return user;
        }
        private ApplicationUserRole CurrentLoggedInUserRole()
        {
            var currentLoggedInUser = CurrentLoggedInUser();
            var currentLoggedInUserRole = currentLoggedInUser.ApplicationUserRole;
            return currentLoggedInUserRole;
        }

        private Calendar CurrentCalendar(int currentCalendarId)
        {
            var currentCalendar = (from c in db.Calendars
                where c.Id == currentCalendarId
                select c).FirstOrDefault();
            return currentCalendar;
        }


        #region UploadSlots
        
        [System.Web.Mvc.HttpPost]
        public async Task<HttpResponseMessage> UploadSlot()
        {

            if (!(CurrentLoggedInUserRole().Name == ConfigurationManager.AppSettings["STRING_SUPER_ADMINISTRATOR"] ||
                CurrentLoggedInUserRole().Name == ConfigurationManager.AppSettings["STRING_USER_ADMINISTRATOR"]))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Lack of sufficient privileges to perform operation.");
            }

            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "Unsupported media type.");
            }
            //TODO
            // Read the file and form data.
            MultipartFormDataMemoryStreamProvider provider = new MultipartFormDataMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            // Extract the fields from the form data.
            var currentCalendarId = int.Parse( provider.FormData["currentCalendarId"]);

            var currentCalendar = CurrentCalendar( currentCalendarId);

            // Check if files are on the request.
            if (!provider.FileStreams.Any())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No file uploaded.");
            }
            
            foreach (KeyValuePair<string, Stream> file in provider.FileStreams)
            {
                string fileName = file.Key;
                Stream stream = file.Value;

                string contentType = fileName.Substring(fileName.LastIndexOf('.'));
                int slotNumber = 0;
                string slotMessage = "";

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

        private bool SaveToDB(System.IO.Stream sourceStream, string contentType, Calendar calendar, int slotNumber, string slotMessage)
        {
            bool saved = false;
            var adventCalendarMonth = 12;
            DateTime earliestDateOfAllowedOpeningTime = new DateTime(year:calendar.Year, month: adventCalendarMonth, day: slotNumber);

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
                //do we have any old values which should be updated?
                InactivatePossibleEarlierSlotWithSameNumber(calendar, slotNumber);
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
                                   where s.Active == activeStatusToSearchFor && s.Calendar == calendar
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

        #region GetCalendarByYearAndCurrentLoggedInUser
        
        public async Task<IHttpActionResult> GetCalendarByYearAndCurrentLoggedInUser(int year)
        {
            var currentLoggedInUser = CurrentLoggedInUser();

            var calendar = await (from c in db.Calendars
                                  from u in db.Users
                                  where (c.Year == year && c.Active == true) &&
                                  u.Id == currentLoggedInUser.Id
                                  select c).FirstOrDefaultAsync();

            if (calendar == null)
            {
                return NotFound();
            }

            return Ok(calendar);
        }

        #endregion

        /********************************************************************************************************************************************************/



        // GET: api/Slots
        public IQueryable<Slot> GetSlots()
        {
            return db.Slots;
        }

        // GET: api/Slots/5
        [ResponseType(typeof(Slot))]
        public async Task<IHttpActionResult> GetSlot(int id)
        {
            Slot slot = await db.Slots.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            return Ok(slot);
        }

        // PUT: api/Slots/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSlot(int id, Slot slot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != slot.Id)
            {
                return BadRequest();
            }

            db.Entry(slot).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(id))
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

        // POST: api/Slots
        [ResponseType(typeof(Slot))]
        public async Task<IHttpActionResult> PostSlot(Slot slot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Slots.Add(slot);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = slot.Id }, slot);
        }

        // DELETE: api/Slots/5
        [ResponseType(typeof(Slot))]
        public async Task<IHttpActionResult> DeleteSlot(int id)
        {
            Slot slot = await db.Slots.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            db.Slots.Remove(slot);
            await db.SaveChangesAsync();

            return Ok(slot);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SlotExists(int id)
        {
            return db.Slots.Count(e => e.Id == id) > 0;
        }
    }
}