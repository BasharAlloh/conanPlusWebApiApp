using System;
using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.Models
{
    public class PromoVideo
    {
        [Key]
        public int VideoId { get; set; }

        // Optional for URL-based videos
        
        // Stores the file path if a video is uploaded
        public string VideoFilePath { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
