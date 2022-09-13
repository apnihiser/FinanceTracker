using FinanceTracker.DataAccess.Identity;
using FinanceTracker.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.DataAccess.Utility;


namespace FinanceTracker.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initalize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (_db.Roles.Any(x => x.Name == Helper.Admin)) return;


            _roleManager.CreateAsync(new IdentityRole(Helper.Admin)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Name = "Admin Spark"

            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser? user = _db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");

            if (user is not null)
            {
                _userManager.AddToRoleAsync(user, Helper.Admin).GetAwaiter().GetResult();
            }
        }
    }
}