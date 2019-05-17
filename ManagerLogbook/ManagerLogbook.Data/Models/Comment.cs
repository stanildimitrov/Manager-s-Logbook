﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLogbook.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string OriginalDescription { get; set; }

        [MaxLength(200)]
        public string EditedDesctiption { get; set; }

        [Required]
        [Range(0, 5,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
