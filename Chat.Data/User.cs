using System.ComponentModel.DataAnnotations;

namespace Chat.Data
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Login { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Password { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}
