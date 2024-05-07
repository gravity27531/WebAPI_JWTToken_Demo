namespace WebAPI.Viewmodels.GenrateToken
{
    public class UsersPerson
    {
        public int PersonId { get; set; }
        public string? PersonCode { get; set; }

        public string? Password { get; set; }

        public string? PersonName { get; set; }

        public int? RoleId { get; set; }

        public string Name { get; set; } = null!;

    }
}
