using System;
using exerciseTrackerDataAccess.Context;
using exerciseTrackerDataAccess.Moldels;
using exerciseTrackerDataAccess.Repositories;

namespace exerciseTracker.Models
{
    public class ExerciseTrackerViewModel
    {
        private readonly ExerciseTrackerRepository _repo;
        public List<ExerciseTrackerModel> ExerciseList { get; set; }
        public ExerciseTrackerModel CurrentExercise { get; set; }

        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; } = "";

        public ExerciseTrackerViewModel(ExerciseTrackerDbContext context)
        {
            _repo = new ExerciseTrackerRepository(context);
            ExerciseList = GetAllExercise();
            CurrentExercise = ExerciseList.FirstOrDefault()!;
        }

        public ExerciseTrackerViewModel(ExerciseTrackerDbContext context, long exerciseId)
        {
            _repo = new ExerciseTrackerRepository(context);
            ExerciseList = GetAllExercise();
            CurrentExercise = exerciseId > 0 ? GetExercise(exerciseId) : new ExerciseTrackerModel();
        }

        public void SaveExercise(ExerciseTrackerModel exercise)
        {
            if (exercise.ID > 0)
            {
                _repo.Update(exercise);
            }
            else
            {
                exercise.ID = _repo.Create(exercise);
            }

            ExerciseList = GetAllExercise();
            CurrentExercise = GetExercise(exercise.ID);
        }

        public void RemoveExercise(long exerciseId)
        {
            _repo.Delete(exerciseId);
            ExerciseList = GetAllExercise();
            CurrentExercise = ExerciseList.FirstOrDefault()!;
        }

        public List<ExerciseTrackerModel> GetAllExercise()
        {
            return _repo.GetAllExercises();
        }

        public ExerciseTrackerModel GetExercise(long exerciseId)
        {
            return _repo.GetExerciseById(exerciseId);
        }
    
    }
}

