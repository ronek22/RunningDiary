using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningDiary
{
    public class Service
    {
        public List<IUser> Users { get; private set; }
        public List<IWorkout> Workouts { get; private set; }

        public Service()
        {
            Users = new List<IUser>();
            Workouts = new List<IWorkout>();
        }

        public IUser GetUser(string nick)
        {
            return Users.Find(u => u.Nick == nick);
        }

        public bool UserExists(string nick)
        {
            if (GetUser(nick) != null)
                return true;
            return false;
        }

        public bool UserExists(IUser user)
        {
            if (Users.Contains(user))
                return true;
            return false;
        }

        public bool SignUp(string nick, string password)
        {
            if (String.IsNullOrEmpty(nick) || String.IsNullOrEmpty(password))
                throw new ArgumentNullException();
            if (!UserExists(nick))
            {
                Users.Add(new User(nick, password));
                return true;
            }
            else
                return false;
        }

        public bool SignUp(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (!UserExists(user.Nick))
            {
                Users.Add(user);
                return true;
            }
            else
                return false;
        }

        public IUser LogIn(string nick, string password)
        {
            return Users.Find(u => u.Nick == nick && u.Password == password);
        }

        public bool RemoveUser(string nickname)
        {
            if (String.IsNullOrEmpty(nickname))
                throw new ArgumentNullException();
            if (UserExists(nickname))
            {
                Users.Remove(GetUser(nickname));
                return true;
            }
            else
                return false;
        }

        public bool RemoveUser(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (UserExists(user))
            {
                Users.Remove(user);
                return true;
            }
            else
                return false;
        }

        public void CreateWorkout(string nick, DateTime workoutDate, double distance, string duration, int elevation)
        {
            if (String.IsNullOrEmpty(duration))
                throw new ArgumentNullException();
            var runner = GetUser(nick);
            var workout = new Workout(runner, workoutDate, distance, duration, elevation);
            runner.Activities.Add(workout);
            Workouts.Add(workout);
        }


    }
}
