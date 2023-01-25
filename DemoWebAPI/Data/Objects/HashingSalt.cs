namespace DemoWebApi.Data.Objects
{
    public class HashingSalt
    {
        public byte[]? salt { get; set; }
        public int id { get; set; }
        public HashingSalt(byte[] salt) {
            this.salt = salt;
        }
    }
}
