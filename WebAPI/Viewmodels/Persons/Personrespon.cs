namespace WebAPI.Viewmodels.Persons
{
    public class Personrespon
    {
        public int PersonId { get; set; }

        public string PersonCode { get; set; }

        public string Password { get; set; }

        public string PersonName { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string AccessToken { get; set; }

        public DateTime CreateDate { get; set; }    
    }
}
