using System.Linq;
using System.Web.Mvc;
using advent_calendar.Models;
namespace advent_calendar.Controllers
{
    public class DataController : Controller
    {
        [HttpPost]
        public JsonResult Register(model.POCO.User u)
        {
            string message = "";
            //Here we will save data to the database
            if (ModelState.IsValid)
            {
                using (var dc = new ApplicationDbContext())
                {
                    //check username available
                    var user = dc.Users.Where(a => a.Name.Equals(u.Name)).FirstOrDefault();
                    if (user == null)
                    {
                        //Save here
                        dc.Users.Add(u);
                        dc.SaveChanges();
                        message = "Success";
                    }
                    else
                    {
                        message = "Username not available!";
                    }
                }
            }
            else
            {
                message = "Failed!";
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}