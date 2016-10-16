using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using advent_calendar.Models;
using advent_calendar.Models.POCO;
using advent_calendar.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace advent_calendar.Controllers
{
    public class CreateCalendarController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUser CurrentLoggedInUser
        {
            get
            {
                var userId = Convert.ToInt32(User.Identity.GetUserId());
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(userId);
                return user;
            }
        }

        [System.Web.Http.Route("api/uploadCalendar")]
        // POST: Calendars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        public async Task<HttpResponseMessage> UploadCalendar()
        {
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

            IList<string> uploadedFiles = new List<string>();
            foreach (KeyValuePair<string, Stream> file in provider.FileStreams)
            {
                string fileName = file.Key;
                Stream stream = file.Value;

                // Do something with the uploaded file
                //UploadManager.Upload(stream, fileName, uploadType, description);
                SaveToDB(CurrentLoggedInUser, stream, fileName.Substring(fileName.LastIndexOf('.') + 1), calendarName,
                    int.Parse( calendarYear));
                // Keep track of the filename for the response
                uploadedFiles.Add(fileName);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Successfully Uploaded: " + string.Join(", ", uploadedFiles));
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

        
        private bool SaveToDB(ApplicationUser currentLoggedInUser, System.IO.Stream sourceStream, string contentType, string name, int year)
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
                saved = true;
            }
            return saved;
        }

    }
}
