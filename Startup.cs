using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SupplyMangement.Models;

[assembly: OwinStartupAttribute(typeof(SupplyMangement.Startup))]
namespace SupplyMangement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }

        public void CreateUserAndRoles ()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if(!roleManager.RoleExists("Administrator"))
            {
                //create default Admin Role
                var role = new IdentityRole("Administrator");
                roleManager.Create(role);

                //create deafult admin
                var user = new ApplicationUser();
                user.UserName = "abedijay@gmail.com";
                user.Email = "abedijay@gmail.com";
                string pwd = "Password@2018";

                var newUser = userManager.Create(user, pwd);

                //make abedijay@gmail.com to be Administrator in role table 
                if (newUser.Succeeded )
                {
                    userManager.AddToRole(user.Id, "Administrator");
                } 




            }
        }


    }
}
