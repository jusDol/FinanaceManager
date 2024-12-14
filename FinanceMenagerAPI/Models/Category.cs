﻿using System.ComponentModel.DataAnnotations;

namespace FinanceMenagerAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
