using AzWebApplication.Models;
using AzWebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzWebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService _course_service;

        public CourseController(CourseService _svc)
        {
            _course_service = _svc;
        }

        // The Index method is used to get a list of courses and return it to the view
        public IActionResult Index()
        {
            IEnumerable<Course> _course_list = _course_service.GetCourses().GetAwaiter().GetResult();
            return View(_course_list);
        }
    }
}
