﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [ApiController]
    public class CourseParticipantController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<CourseParticipantController> _logger;
        public CourseParticipantController(Context context, ILogger<CourseParticipantController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("[controller]/GetAll")]
        public APIResponse<IEnumerable<CourseParticipant>> GetAll()
        {
            APIResponse<IEnumerable<CourseParticipant>> response = new APIResponse<IEnumerable<CourseParticipant>>();
            var courseParticipants = _context.CourseParticipants.ToList();
            if (courseParticipants != null)
            {
                response.Success = true;
                response.Data = courseParticipants;
            }
            return response;
        }
        [HttpPost]
        [Route("[controller]/AddOrUpdate")]
        public APIResponse<CourseParticipant> AddOrUpdate(CourseParticipant courseParticipant)
        {
            APIResponse<CourseParticipant> response = new APIResponse<CourseParticipant>();
            response.Data = AddOrUpdateCourseParticipant(courseParticipant);
            response.Success = true;
            return response;
        }
        [HttpGet]
        [Route("[controller]/GetCourseParticipantsById/{Id}")]
        public APIResponse<IEnumerable<CourseParticipant>> GetCourseById(int Id)
        {
            APIResponse<IEnumerable<CourseParticipant>> response = new APIResponse<IEnumerable<CourseParticipant>>();
            response.Data = _context.CourseParticipants.Where(u => u.UserId == Id).ToList();

            response.Success = true;
            return response;
        }
        CourseParticipant AddOrUpdateCourseParticipant(CourseParticipant courseParticipant)
        {
            var existingCourseParticipant = _context.CourseParticipants.SingleOrDefault(cp => cp.CourseId == courseParticipant.CourseId && cp.UserId == courseParticipant.UserId);
            CourseParticipant newCourseParticipant = courseParticipant;
            if (existingCourseParticipant != null)
            {
                existingCourseParticipant.Grade = courseParticipant.Grade;
                existingCourseParticipant.Role = courseParticipant.Role;
                existingCourseParticipant.Status = courseParticipant.Status;
                newCourseParticipant = UpdateCourseParticipant(existingCourseParticipant);
            }
            else
            {
                newCourseParticipant = AddCourseParticipant(courseParticipant);
            }
            return newCourseParticipant;
        }
        CourseParticipant AddCourseParticipant(CourseParticipant courseParticipant)
        {
            _context.CourseParticipants.Add(courseParticipant);
            _context.SaveChanges();
            return courseParticipant;
        }
        CourseParticipant UpdateCourseParticipant(CourseParticipant courseParticipant)
        {
            _context.CourseParticipants.Update(courseParticipant);
            _context.SaveChanges();
            return courseParticipant;
        }
    }
}