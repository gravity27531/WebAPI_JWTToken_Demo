using System.Reflection.Metadata.Ecma335;

namespace WebAPI.Viewmodels.Persons
{
    public class PersonResult
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public Personrespon Personrespons { get; set; }
    }
}
