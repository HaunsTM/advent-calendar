using API.Models.POCO;

namespace API.Models.ViewModels
{
    public class CalendarViewModel
    {
        public int Id { get; set; }
        public bool Active { get; private set; }

        public int Year { get; set; }

        public string ContentType { get; set; }
        public string ContentAsBase64 { get; set; }
        public string Name { get; set; }



        public CalendarViewModel(Calendar calendar)
        {
            this.Id = calendar.Id;
            this.Active = calendar.Active;
            this.Year = calendar.Year;
            this.ContentType = calendar.ContentType;
            this.ContentAsBase64 = calendar.ContentAsBase64;
            this.Name = calendar.Name;
        }

    }
}