﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelnessWebsite.Models
{
    public class DailyWorkout
    {
        public DailyWorkout()
        {     
        }
        public int UserID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public List<Workout> Workout { get; set; }
        public string? WorkoutType { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]

        public DateTime DateTime { get; set; }

    }
}
