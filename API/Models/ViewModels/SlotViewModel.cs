using System;
using API.Models.POCO;

namespace API.Models.ViewModels
{
    public class SlotViewModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public int Number { get; set; }

        public string SlotMessage { get; set; }

        public string ContentType { get; set; }
        public string ContentAsBase64 { get; set; }

        public DateTime EarliestDateOfAllowedOpeningTime { get; set; }
        public Nullable<DateTime> Opened { get; set; }

        public SlotViewModel(Slot slot)
        {
            this.Id = slot.Id;
            this.Active = slot.Active;
            this.Number = slot.Number;
            this.SlotMessage = slot.SlotMessage;
            this.ContentType = slot.ContentType;
            this.ContentAsBase64 = slot.ContentAsBase64;
            this.EarliestDateOfAllowedOpeningTime = slot.EarliestDateOfAllowedOpeningTime;
            this.Opened = slot.Opened;
        }
    }
}