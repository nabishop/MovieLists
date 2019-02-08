using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBackend.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
