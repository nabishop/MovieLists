namespace MovieBackend.Models
{
    public class UserItem
    {
        [System.ComponentModel.DataAnnotations.Key]

        public string Name { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
    }
}
