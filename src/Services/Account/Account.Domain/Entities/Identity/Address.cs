using System.ComponentModel.DataAnnotations;

namespace Account.Domain.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        [Required]
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}