using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class Token
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string JtiID { get; set; } = null!;
        public string token { get; set; } = null!;
        public bool IsUsed { get; set; } = false;
        public bool IsRevoked { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public string UserID { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
