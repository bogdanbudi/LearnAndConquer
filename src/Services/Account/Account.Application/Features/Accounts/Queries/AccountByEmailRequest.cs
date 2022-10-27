namespace Account.Application.Features.Accounts.Queries
{
    public  class AccountByEmailRequest
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string GivenName { get; set; }

        public string Email { get; set; }
    }
}
