﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class Rating
    {

        // Primary key
        [Key]
        public int Id { get; set; }

        // Foreign key
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        // Navigation property
        public virtual Restaurant Restaurant { get; set; }

        [Required, Range(0,10)]
        public double FoodScore { get; set; }
        [Required, Range(0, 10)]
        public double CleanlinessScore { get; set; }
        [Required, Range(0, 10)]
        public double EnvironmentScore { get; set; }

        public double AverageRating
        {
            get
            {
                var totalScore = FoodScore + CleanlinessScore + EnvironmentScore;
                return totalScore / 3;
            }
        }

    }
}