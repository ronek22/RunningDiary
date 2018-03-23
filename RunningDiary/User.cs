using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningDiary
{

    public interface IUser
    {
        string Nick { get; }
        string Password { get; }
        List<IWorkout> Activities { get; }
        string CountWorkouts();
        string ListWorkouts();
        string ShowWorkout(int i);
        string ShowTotalDistance();
        bool DeleteWorkout(int i);
        void DeleteAllWorkouts();
    }
    public class User : IUser
    {
        public string Nick { get; private set; }

        public string Password { get; private set; }

        public List<IWorkout> Activities { get; private set; }

        public User(string nick, string password)
        {
            Nick = nick;
            Password = password;
            Activities = new List<IWorkout>();
        }

        public string CountWorkouts()
        {
            return String.Format("You ran {0} times", Activities.Count);
        }

        public void DeleteAllWorkouts()
        {
            Activities = new List<IWorkout>();
        }

        public bool DeleteWorkout(int i)
        {
            if (i < Activities.Count)
            {
                Activities.RemoveAt(i);
                return true;
            }
            else return false;
        }

        public string ListWorkouts()
        {
            int total = Activities.Count;
            if (total == 0)
                return string.Empty;
            else
            {
                string message = "";
                for(int i = 0; i < total; i++)
                {
                    message += String.Format("{0}: {1} | {2} km\n", i, Activities[i].WorkoutDate.ToString("yyyy-MM-dd"), Activities[i].Distance);
                }
                return message;
            }
        }

        public string ShowWorkout(int i)
        {
            if (i < Activities.Count)
                return String.Format("{0} | {1} km | {2} | {3} | {4}", Activities[i].WorkoutDate.ToString("yyyy-MM-dd"), Activities[i].Distance,
                    Activities[i].Duration, Activities[i].Elevation, Activities[i].Pace);
            else
                return "Workout with this id don't exist.";
        }

        public string ShowTotalDistance()
        {
            int total = Activities.Count;
            if (total == 0)
                return string.Empty;
            else
            {
                double totalDistance = 0;
                for (int i = 0; i < total; i++)
                {
                    totalDistance += Activities[i].Distance;
                }
                return "Total distance: " + totalDistance + " km.";
            }
        }
    }
}
