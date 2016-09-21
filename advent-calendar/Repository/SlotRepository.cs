using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using advent_calendar.Models;
using advent_calendar.Models.POCO;

namespace advent_calendar.Repository
{
    public class SlotRepository
    {
        //http://www.codeproject.com/Articles/631668/Learning-MVC-Part-Repository-Pattern-in-MVC-App

        private ApplicationDbContext _context;
        private bool _disposed = false;

        public SlotRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Slot GetBy(string HTML_Id)
        {
            Regex regex = new Regex(@".*_(?<slotNumber>\d*)");

            Match match = regex.Match(HTML_Id);

            if (match.Success)
            {
                var slotNumber = int.Parse(match.Groups["slotNumber"].Value);
                return GetBy(slotNumber);
            }
            else
            {
                throw new Exception("Invalid HTML id: " + HTML_Id);
            }
            
        }

        public Slot GetBy(int slotNumber)
        {
            
            using (var aCC = new ApplicationDbContext())
            {
                var entities = aCC.Database.SqlQuery<Slot>("select * from dbo.Slots where Number = @slotNumber", new SqlParameter("@slotNumber", slotNumber)).ToList();
                var count = entities.Count;
            }

            var entity = (from slots in _context.Slots
                          where slots.Number == slotNumber
                          select slots).FirstOrDefault();

            return entity;
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}