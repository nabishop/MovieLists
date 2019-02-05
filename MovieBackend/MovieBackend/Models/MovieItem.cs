namespace MovieBackend.Models
{
    public class MovieItem
    {
        [System.ComponentModel.DataAnnotations.Key]

        public string Name { get; set; }
        public string Vibe { get; set; }
        public string DateAdded { get; set; }
        public int Song_ID { get; set; }
        public int User_ID { get; set; }
    }
}
