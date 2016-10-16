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
using advent_calendar.Models.ViewModels;
using advent_calendar.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace advent_calendar.Controllers
{
    public class CalendarController : ApiController
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

        [System.Web.Http.Route("api/getCalendar")]
        // POST: Calendars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Authorize]
        public JsonResult GetCalendar(int year)
        {
            Calendar calendar;
            var currentLoggedInUser = CurrentLoggedInUser;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                calendar = (from c in db.Calendars
                            where c.Year == year && c.ApplicationUsers.Contains(currentLoggedInUser)
                            select c).FirstOrDefault();
            }
            var calendarViewModel = new advent_calendar.Models.ViewModels.CalendarViewModel(calendar);
            return new JsonResult { Data = calendarViewModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        private Calendar CurrentCalendar(ApplicationUser currentLoggedInUser, int year)
        {
            var calendar = (from c in db.Calendars
                where c.Year == year && c.ApplicationUsers.Contains(currentLoggedInUser)
                select c).FirstOrDefault();

            return calendar;
        }

    }
}
