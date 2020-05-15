using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hw_4_22_PeopleAjaxProject.Models;
using hw_4_22_PeopleAjaxProject.data;

namespace hw_4_22_PeopleAjaxProject.Controllers
{
    public class HomeController : Controller
    {
        private PersonManager _mng = new PersonManager();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPeople()
        {
            return Json(_mng.GetPeople());
        }
        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            _mng.AddPerson(person);
            return Json('0');
        }
        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            _mng.UpdatePerson(person);
            return Json('0');
        }
        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            _mng.DeletePerson(id);
            return Json('0');
        }
    }
}
