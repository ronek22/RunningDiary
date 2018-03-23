using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningDiary
{
    public interface IWorkout
    {
        IUser Runner { get; }
        DateTime WorkoutDate { get; }
        double Distance { get; }
        string Duration { get; }
        int Elevation { get; }
        string Pace { get; }

        string CalculatePace(double distance, string time);
    }
    class Workout : IWorkout
    {
        public IUser Runner { get; private set; }
        public DateTime WorkoutDate { get; private set; }
        public double Distance { get; private set; }
        public string Duration { get; private set; }
        public int Elevation { get; private set; }
        public string Pace { get; private set; }

        public Workout(IUser runner, DateTime workoutDate, double distance, string duration, int elevation)
        {
            Runner = runner;
            WorkoutDate = workoutDate;
            Distance = distance;
            Duration = duration;
            Elevation = elevation;
            Pace = CalculatePace(distance, duration);
        }


        public string CalculatePace(double distance, string time)
        {
            double pace;
            if (!DateTime.TryParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                throw new ArgumentException("Wrong format of duration, should be in hh:mm:ss");

            TimeSpan duration = dt.TimeOfDay;
            pace = duration.TotalMinutes / distance;

            int minutes = (int)pace;
            int seconds = (int)((pace - minutes) * 60);

            if (seconds > 9)
                return String.Format("{0}:{1} min/km", minutes, seconds);
            else
                return String.Format("{0}:0{1} min/km", minutes, seconds);
        }
    }
}
