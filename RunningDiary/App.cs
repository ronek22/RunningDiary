using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningDiary
{
    class App
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            User first = new User("ronek22", "aha");
            service.SignUp(first);
            Console.WriteLine("Ilosc userow: " + service.Users.Count);
            service.SignUp(first);
            Console.WriteLine("Proba dodania tego samego uzytkownika po raz drugi.\nIlosc userow: " + service.Users.Count);

            service.SignUp("michu", "123");
            service.SignUp("strus", "elo");
            Console.WriteLine("Dodanie 2 uzytkowników bezposrednio za pomoca metody SingUp.\nIlosc userow: " + service.Users.Count);

            service.RemoveUser(first);
            Console.WriteLine("Proba usuniecia pierwszego uzytkownika bezposrednio za pomoca obiektu:\nIlosc userow: " + service.Users.Count);

            Console.WriteLine("Proba dodania treningu dla micha");
            service.CreateWorkout("michu", new DateTime(2018, 02, 13), 11.5, "00:55:16", 50);
            service.CreateWorkout("strus", new DateTime(2018, 03, 15), 12, "01:05:00", 12);
            service.CreateWorkout("strus", new DateTime(2018, 03, 16), 5, "00:25:00", 20);

            Console.WriteLine("Wyswietlenie treningow micha");
            Console.WriteLine(service.GetUser("michu").ListWorkouts());

            IUser strus = service.LogIn("strus", "elo");
            Console.WriteLine("Wyswietlenie treningow strusia");
            Console.WriteLine(strus.ListWorkouts());
            Console.WriteLine("Wyswietlanie szczegółowe pierwszego treningu strusia");
            Console.WriteLine(strus.ShowWorkout(0));
            Console.WriteLine("Wyswietlenie łącznego dystansu przebiegniętego przez strusia");
            Console.WriteLine(strus.ShowTotalDistance());

            Console.Read();

        }
    }
}
