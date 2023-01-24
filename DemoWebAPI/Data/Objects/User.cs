namespace AlbarWebAPI.Data.Objects
{
    public class User
    {
        public byte[] Password { get; set; } = new byte[32];
        public byte[] Salt { get; set; } = new byte[32];
        public int Id { get; set; }
        public string Email{ get; set; }

        public User(string email, byte[] salt, byte[] password)
        {
            this.Salt = salt;
            this.Email = email;
            this.Password = password;
        }
    }
}
