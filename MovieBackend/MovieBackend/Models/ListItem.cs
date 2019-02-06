using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBackend.Models
{
    public class ListItem
    {
        [System.ComponentModel.DataAnnotations.Key]

        public string Name { get; set; }
        public string DateAdded { get; set; }
        public int Movie_ID { get; set; }
        public int User_ID { get; set; }
    }
}
