using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using advent_calendar.Models;
using advent_calendar.Models.POCO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace advent_calendar.Controllers
{
    public class CalendarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUser CurrentLoggedInUser()
        {
                var userId = Convert.ToInt32(User.Identity.GetUserId());
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(userId);
                return user;
        }




        [Authorize]
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