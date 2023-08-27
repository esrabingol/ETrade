using Microsoft.AspNetCore.Identity;

namespace ETrade.Web.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        //ekstra olarak eklemek istediğim alanlar
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

    }
}
