using _08042019_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_08042019_MVC.Startup))]
namespace _08042019_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUser();
        }

        private void createRolesandUser()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();

            var roleMAnager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            if (!roleMAnager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleMAnager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "cenelif@gmail.com";
                user.Email = "cenelif@gmail.com";

                string sifre = "123Elif.";
                var kullanici = userManager.Create(user, sifre);

                if (kullanici.Succeeded)
                {
                    var sonuc = userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleMAnager.RoleExists("Uye"))
            {
                var role = new IdentityRole();
                role.Name = "Uye";
                roleMAnager.Create(role);
            }
            
   
        }
    }
}
