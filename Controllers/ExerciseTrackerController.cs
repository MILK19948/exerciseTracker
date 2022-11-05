using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exerciseTracker.Models;
using exerciseTrackerDataAccess.Context;
using exerciseTrackerDataAccess.Moldels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exerciseTracker.Controllers
{
    public class ExerciseTrackerController : Controller
    {
        private readonly ExerciseTrackerDbContext _context;

        public ExerciseTrackerController(ExerciseTrackerDbContext context)
        {
            _context = context;
        }
        // GEt
        public IActionResult Index()
        {
            ExerciseTrackerViewModel model = new ExerciseTrackerViewModel(_context);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(long exerciseId, string FirstName, string SecondName, string WorkoutName, string WorkoutTime)
        {
            ExerciseTrackerViewModel model = new ExerciseTrackerViewModel(_context);

            ExerciseTrackerModel exercises = new(exerciseId, FirstName, SecondName, WorkoutName, WorkoutTime);

            model.SaveExercise(exercises);
            model.IsActionSuccess = true;
            model.ActionMessage = "Exercise has been saved successfully";

            return View(model);
        }

        public IActionResult Update(long id)
        {
            ExerciseTrackerViewModel model = new(_context, id);

            return View(model);
        }

        public IActionResult Delete(long id)
        {
           ExerciseTrackerViewModel model = new(_context);

            if (id > 0)
            {
                model.RemoveExercise(id);
                model.IsActionSuccess = true;
                model.ActionMessage = "Exercise has been deleted successfully";
            }

            return View("Index", model);
        }
    }
}
