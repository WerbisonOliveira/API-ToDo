using Microsoft.AspNet.Identity.EntityFramework;

namespace To_Do.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}