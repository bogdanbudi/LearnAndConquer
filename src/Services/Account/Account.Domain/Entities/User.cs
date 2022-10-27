using Account.Domain.Common;

namespace Account.Domain.Entities
{
    public class User : EntityBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string GivenName { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }
    }
}
