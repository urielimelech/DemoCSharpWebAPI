namespace DemoWebAPI.Data.Objects
{
    public class User
    {
        public byte[] Password { get; set; }
        public int Id { get; set; }
        public string Email{ get; set; }

        public User(string email, byte[] password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
