using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaTimeProj.DataAccess.Data;
using TeaTimeProj.Models;
using TeaTimeProj.Utility;

namespace TeaTimeProj.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        // Dpendency Injection for Services 
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }


        public void Initialize()
        {
            try
            {
                // check if there are any pending migrations
                // if yes, apply them
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }

            //check if the roles are already created
            // if no, then create them, including the admin role and all the other roles
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                // Create roles
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Manager)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();

                // if roles are not created, then we will create a super user who will maintain the website
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Administrater",
                    PhoneNumber = "0911111111",
                    Address = "test address 123",
                    EmailConfirmed = true,
                },"Admin123*").GetAwaiter().GetResult();
                // Assign Admin user to the Admin role
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
                // Assign to the role
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();


            }
            return;

        }
    }
}
