using System;
using RunningDiary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunningDiary.Fakes;

namespace RunningDiaryTests
{
    [TestClass]
    public class UserTests
    {
        User user;
        string testNick;
        string testPass;
        DateTime testDate;
        double testDistance;
        string testDuration;
        int testElevation;

        [TestInitialize]
        public void TestInitialize()
        {
            testNick = "roadRunner";
            testPass = "Coyot";
            testDate = new DateTime(2018, 03, 15);
            testDistance = 12.78;
            testDuration = "01:15:24";
            testElevation = 150;
            user = new User(testNick, testPass);
        }

        // LIST WORKOUT

        [TestMethod]
        public void ListWorkoutWithOneWorkout_ReturnsStringWithWorkout()
        {
            user.Activities.Add(new StubIWorkout() { DistanceGet = () => { return testDistance; } });
            StringAssert.Contains(user.ListWorkouts(), "12,78");
        }

        [TestCategory("Stubs")]
        [TestMethod]
        public void ListWorkoutWithTwoWorkouts_ReturnsStringWithWorkouts()
        {
            user.Activities.Add(new StubIWorkout() { DistanceGet = () => { return testDistance; } });
            user.Activities.Add(new StubIWorkout() { DistanceGet = () => { return 4.56; } });
            StringAssert.EndsWith(user.ListWorkouts(), "4,56 km\n");
        }

        [TestMethod]
        public void EmptyWorkoutList_ReturnsEmptyString()
        {
            StringAssert.Equals(user.ListWorkouts(), string.Empty);
        }

        // COUNT WORKOUTS

        [TestCategory("Stubs")]
        [TestMethod]
        public void CountWorkout_With2Runs()
        {
            user.Activities.Add(new StubIWorkout() { DistanceGet = () => { return 12.88; } });
            user.Activities.Add(new StubIWorkout() { DistanceGet = () => { return 4.56; } });
            StringAssert.Equals(user.CountWorkouts(), "You ran 2 times");
        }

        // SHOW WORKOUT

        [TestCategory("Stubs")]
        [TestMethod]
        public void ShowWorkout_ReturnsStringWithDetails()
        {
            user.Activities.Add(new StubIWorkout()
            {
                WorkoutDateGet = () => { return testDate; },
                DistanceGet = () => { return testDistance; },
                DurationGet = () => { return testDuration; },
                ElevationGet = () => { return testElevation; }
            });
            StringAssert.Contains(user.ShowWorkout(0), testDate.ToString("yyyy-MM-dd") + " | " + testDistance + " km | " + testDuration + " | " + testElevation);
        }

        [TestMethod]
        public void ShowNotExistWorkout_ReturnErrorMessage()
        {
            StringAssert.Contains(user.ShowWorkout(0), "Workout with this id don't exist.");
        }

        // SHOW TOTAL DISTANCE
        [TestCategory("Stubs")]
        [TestMethod]
        public void ShowTotalDistance_ReturnsString()
        {
            user.Activities.Add(new StubIWorkout() { DistanceGet = () => { return 15.22; } });
            user.Activities.Add(new StubIWorkout() { DistanceGet = () => { return 4.56; } });
            user.Activities.Add(new StubIWorkout() { DistanceGet = () => { return 10.10; } });

            StringAssert.Contains(user.ShowTotalDistance(), "29,88");
        }

        // DELETE WORKOUT 

        [TestCategory("Stubs")]
        [TestMethod]
        public void DeleteWorkout_ReturnsTrue()
        {
            var workout = new StubIWorkout();
            var workout2 = new StubIWorkout();
            user.Activities.Add(workout);
            user.Activities.Add(workout2);
            user.DeleteWorkout(0);
            CollectionAssert.DoesNotContain(user.Activities, workout);
        }


        [TestMethod]
        public void DeleteWorkout_ThatDoesntExist_ReturnFalse()
        {
            Assert.IsFalse(user.DeleteWorkout(0));
        }


        // DELETE ALL WORKOUTS
        [TestCategory("Stubs")]
        public void DeleteAllWorkouts_NewActivityList()
        {
            for (int i = 0; i < 20; i++)
                user.Activities.Add(new StubIWorkout());
            user.DeleteAllWorkouts();
            Assert.AreEqual(user.Activities.Count, 0);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            user = null;
            testNick = null;
            testPass = null;
            testDuration = null;
        }
    }
}
