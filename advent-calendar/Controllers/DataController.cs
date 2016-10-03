using System.Web.Mvc;

namespace advent_calendar_OLD.Controllers
{
    public class DataController : Controller
    {
        [HttpGet]
        [Authorize]
        public JsonResult HelloWorld()
        {
            var message = "Hello World";
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    
        //[HttpPost]
        //public JsonResult Register(UserViewModel u)
        //{
        //    string message = "";
        //    //Here we will save data to the database
        //    if (ModelState.IsValid)
        //    {
        //        using (var dc = new ApplicationDbContext())
        //        {
        //            //check username available
        //            try
        //            {

        //                var user = dc.ApplicationUsers.Where(a => a.Name.Equals(u.Name)).FirstOrDefault();
        //                if (user == null)
        //                {
        //                    var passHlp = new business.PasswordHelper( password: u.Password) {};
        //                    var newUser = new ApplicationUser
        //                    {
        //                        Active = false,
        //                        Email = u.Email,
        //                        PasswordHash = passHlp.PasswordHash()
        //                    };
        //                    //Save here
        //                    dc.ApplicationUsers.Add(newUser);
        //                    dc.SaveChanges();
        //                    message = "Success";
        //                }
        //                else
        //                {
        //                    message = "Username not available!";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                int i = 0;
        //                throw ex;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        message = "Failed!";
        //    }
        //    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}


    }
}