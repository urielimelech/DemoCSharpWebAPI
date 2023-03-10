using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebAPI.Data.Objects
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] Password { get; set; }
        public string Email{ get; set; }

        public User(string email, byte[] password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
