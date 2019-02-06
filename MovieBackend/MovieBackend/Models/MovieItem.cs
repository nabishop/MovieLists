namespace MovieBackend.Models
{
    public class MovieItem
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int Movie_ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public bool Watched { get; set; }
        public float Rating { get; set; }
    }
}
