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
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using API.Models;
using API.Models.POCO;
using API.Providers;
using Microsoft.AspNet.Identity;

namespace API.Controllers
{
    //[Authorize]
    [EnableCors("*", "*", "*")]
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

            if (!(currentLoggedInUserRole.Name == ConfigurationManager.AppSettings["STRING_SUPER_ADMINISTRATOR"] ||
                  currentLoggedInUserRole.Name == ConfigurationManager.AppSettings["STRING_USER_ADMINISTRATOR"]))
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
            await Request.Content.ReadAsMultipartAsync(provider);

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
                //do we have any old values which should be updated?
                InactivatePossibleEarlierCalendarByYear(calendarToSave.Year, currentLoggedInUser);
                db.Calendars.Add(calendarToSave);
                db.SaveChanges();
                saved = true;
            }
            return saved;
        }


        private bool InactivatePossibleEarlierCalendarByYear(int calendarYear, ApplicationUser currentLoggedInUser)
        {
            bool done = false;
            bool activeStatusToSearchFor = true;
            bool activeStatusAfterUpdate = false;

            var earlierCalendars = from c in db.Calendars
                from u in db.Users
                where (c.Year == calendarYear && c.Active == activeStatusToSearchFor) &&
                      u.Id == currentLoggedInUser.Id
                select c;

            foreach (var earlierCalendar in earlierCalendars)
            {
                earlierCalendar.Active = activeStatusAfterUpdate;
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