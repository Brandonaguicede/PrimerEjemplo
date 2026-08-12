using System.ComponentModel.DataAnnotations;

namespace HackerRank1.Entities.Models
{
    public class Library
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
    }
}
