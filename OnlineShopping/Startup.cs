using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OnlineShopping.Data.Context;
using OnlineShopping.Models;
using Owin;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(OnlineShopping.Startup))]
namespace OnlineShopping
{
    public partial class Startup
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultUser();
        }

        public void CreateDefaultUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));


            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(
                    new IdentityRole() { Name = "Admin" }
                    );

                var user = new ApplicationUser()
                {
                    Email = ConfigurationManager.AppSettings["Email"].ToString(),
                    UserName = "admin"
                };

                var result = userManager.Create(user,
                                                ConfigurationManager.AppSettings["Password"].ToString());
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
