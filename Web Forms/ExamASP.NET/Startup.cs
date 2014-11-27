using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExamASP.NET.Startup))]
namespace ExamASP.NET
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
