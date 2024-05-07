namespace WebAPI.Models
{
    public class Token
    {
        public int TokenId { get; set; }

        public string Tokenkey { get; set; }

        public bool Status { get; set; }

        public int PersonId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Exp { get; set; }
    }
}
