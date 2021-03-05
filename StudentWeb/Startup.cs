using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentWeb.Startup))]
namespace StudentWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
