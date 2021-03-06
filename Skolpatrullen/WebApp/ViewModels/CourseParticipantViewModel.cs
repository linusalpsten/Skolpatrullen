﻿using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib;

namespace WebApp.ViewModels
{
    public class CourseParticipantViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public Roles Role { get; set; }
        public string Grade { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public bool isSchoolAdmin { get; set; }
        public bool isSuperUser { get; set; }
        public bool isTeacher { get; set; }

        public List<string> Grades = new List<string>
    {
      null,"IG","G","VG","MVG","A","B","C","D","E","F"
    };

        public User User { get; set; }
        public Course Course { get; set; }
        public CourseParticipant ToCourseParticipant()
        {
            return new CourseParticipant
            {
                UserId = this.UserId,
                CourseId = this.CourseId,
                Role = this.Role,
                Grade = this.Grade,
                Status = this.Status
            };
        }
    }
}
