using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Data.Objects
{
    public class HashingSalt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public byte[]? salt { get; set; }
        public HashingSalt(byte[] salt) {
            this.salt = salt;
        }
    }
}
