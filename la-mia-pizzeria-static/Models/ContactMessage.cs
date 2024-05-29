using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("contactmessage")]

    public class ContactMessage
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public ContactMessage() { }

        public ContactMessage(int id, string email, string message)
        {
            Id = id;
            Email = email;
            Message = message;
        }
    }
}
