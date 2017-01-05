using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealEstateExample.Startup))]
namespace RealEstateExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
