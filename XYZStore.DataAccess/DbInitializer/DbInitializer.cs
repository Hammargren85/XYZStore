using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XYZStore.Models;
using XYZStore.Utility;

namespace XYZStore.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {		
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
            //apply migrations if they are not applied
            try
			{
				if(_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch(Exception ex)
			{

			}
				//create roles if they are not created
			if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_User_Comp)).GetAwaiter().GetResult();

				//if roles are not created, then we will create admin user as well

				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "AdminoUser@outlook.com",
					Email = "AdminoUser@outlook.com",
					Name = "Admin Master",
					PhoneNumber = "121212131313",
					StreetAddress = "TownStreet",
					State = "SWE",
					PostalCode = "60666",
					City = "Town"
				}, "Admin123%").GetAwaiter().GetResult();
				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "AdminoUser@outlook.com");
			

				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
			}
			return;
		}
	}
}
