﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AssignmentController : AppController
    {
        [HttpPost]
        [Route("[controller]/AddCourseAssignment")]
        public async Task<IActionResult> AddCourseAssignment(Assignment assignment, int courseId)
        {
            string message = await GetUser();
            if (!ModelState.IsValid)
            {
                return View();
            }
            assignment.CourseId = courseId;
            var response = await APIAddAssignment(assignment);

            return RedirectToAction("GetCourseById", "Course", new { Id = assignment.CourseId });
        }
        [HttpGet]
        [Route("[controller]/CourseAssignments/{courseid}")]
        public async Task<IActionResult> CourseAssignments(int courseId)
        {
            string message = await GetUser();
            if (User != null)
            {
                IEnumerable<Assignment> files = new List<Assignment>();
                var response = await APIGetAssignmentByCourseId(courseId);
                if (response.Data != null)
                {
                    files = response.Data.Select(a => new Assignment()
                    {
                        Id = a.Id,
                        CourseId = courseId,
                        Name = a.Name,
                        Deadline = a.Deadline,
                        Description = a.Description
                    });
                    return View(files);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}