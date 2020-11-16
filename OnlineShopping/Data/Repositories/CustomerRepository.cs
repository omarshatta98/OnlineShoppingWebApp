using OnlineShopping.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace OnlineShopping.Data.Repositories
{
    public class CustomerRepository : Repository<ApplicationUser>
    {
        UserManager<ApplicationUser> userManager;

        public CustomerRepository()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }
        public bool Update(EditViewModel profile)
        {
            ApplicationUser currentUser = GetById(profile.Id);
            userManager.ChangePassword(profile.Id, profile.CurrentPassword, profile.NewPassword);
            currentUser.Name = profile.Name;
            currentUser.UserName = profile.UserName;
            currentUser.Email = profile.Email;
            currentUser.Country = profile.Country;
            currentUser.City = profile.City;
            currentUser.PhoneNumber = profile.PhoneNumber;

            _context.Entry(currentUser).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }
    }
}