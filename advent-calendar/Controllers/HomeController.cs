using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using advent_calendar.Models;
using advent_calendar.Models.Interfaces;
using advent_calendar.Repository;

namespace advent_calendar.Controllers
{
    public class HomeController : Controller
    {
        #region Private member variables...

        private Repository.SlotRepository _slotRepository;

        #endregion

        public HomeController()
        {
            var adventCalendarContext = new ApplicationDbContext();
            _slotRepository = new SlotRepository(adventCalendarContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public JsonResult GiveMeSlotContent(string HTML_Id)
        {
            return null;
            /*
            //find correstonding slotcontent in database
            var currentSlot = _slotRepository.GetBy(HTML_Id);
            
            if (currentSlot != null)
            {
                var currentSlotNumber = currentSlot.Number;

                //is it allowed to open the slot?
                if (SlotCanBeOpened(currentSlot))
                {//yes, do nothing! :-)
                }
                else
                {
                    //a bit too early
                    var dateDiff = Math.Ceiling((currentSlot.EarliestDateOfAllowedOpeningTime - new DateTime(2015, 12, 31)).TotalDays);
                    currentSlot = new Slot { Active = false, Number = currentSlotNumber,  ImageURL = "0.gif", SlotMessage = String.Format("Aja baja... Den här luckan får du öppna först om {0} dagar!", dateDiff.ToString())}; ;
                }
            }
            else
            {
                currentSlot = new Slot { Active = false, ImageURL = "0.gif", SlotMessage = "Hmmm... Något gick fel! Klickade du verkligen på en lucka?"};
            }
            return Json(currentSlot);
            */
        }

        private bool SlotCanBeOpened(ISlot currentSlot)
        {
            var slotCanBeOpened = new DateTime(2015,12,31) >= currentSlot.EarliestDateOfAllowedOpeningTime;
            return slotCanBeOpened;
        }
    }
}