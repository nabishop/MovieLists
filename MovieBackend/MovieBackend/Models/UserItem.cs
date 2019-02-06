namespace MovieBackend.Models
{
    public class UserItem
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
