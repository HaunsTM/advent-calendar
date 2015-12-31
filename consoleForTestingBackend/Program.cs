using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleForTestingBackend
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG

            try
            {
                
                    System.Data.Entity.Database.SetInitializer(new DefaultDataDBInitializer());
                    var db = new AdventCalendarContext();
                Console.WriteLine("Initializing database");
                db.Database.Initialize(true);
                /**/
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException.Message);
                Console.WriteLine(sqlException.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                throw;
            }
#endif
            Console.WriteLine("Done/succeeded in initializing database!");
            Console.ReadLine();
        }
    }
}
