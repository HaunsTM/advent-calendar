using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(advent_calendar.Startup))]
namespace advent_calendar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
