using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDiary.Model
{
    public class Diary
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }
    }
}
