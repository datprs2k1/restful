using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class User : IdentityUser
    {
        public User()
        {
            Tokens = new HashSet<Token>();
        }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;

        public ICollection<Token> Tokens { get; set; }
    }
}
