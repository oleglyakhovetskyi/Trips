using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataProject.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("IdentityDb") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
    public class ApplicationUser : IdentityUser
    {
        public int Year { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationUser()
        {
        }
    }
}