using System;

namespace advent_calendar.Models.ViewModels
{
    public class CalendarViewModel
    {
        public int Id { get; set; }
        public bool Active { get; private set; }

        public int Year { get; set; }

        public string ContentType { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }



        public CalendarViewModel(advent_calendar.Models.POCO.Calendar calendar)
        {
            this.Id = calendar.Id;
            this.Active = calendar.Active;
            this.Year = calendar.Year;
            this.ContentType = calendar.ContentType;
            this.Content = Convert.ToBase64String(calendar.Content);
            this.Name = calendar.Name;
        }

    }
}