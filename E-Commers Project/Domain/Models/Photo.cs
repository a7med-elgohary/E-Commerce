﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commers_Project.Domain.Models
{
    public class UserPhoto
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Url { get; set; } = string.Empty;
        // Navigation Properties
        public required User User { get; set; }

    }
}
