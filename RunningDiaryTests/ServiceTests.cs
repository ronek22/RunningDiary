using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunningDiary;
using RunningDiary.Fakes;

namespace RunningDiaryTests
{
    [TestClass]
    public class ServiceTests
    {

        Service service;
        string testNick;
        string testNick2;
        string testPass;
        DateTime testDate;
        double testDistance;
        string testDuration;
        int testElevation;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            service = new Service();
            testNick = "RoadRunner";
            testNick2 = "Prefontaine";
            testPass = "1Mile";
            testDate = new DateTime(2018, 03, 15);
            testDistance = 12.78;
            testDuration = "01:15:24";
            testElevation = 150;
        }


        // SIGN_UP
        [TestMethod]
        public void SingUp_WithStrings()
        {
            Assert.IsTrue(service.SignUp(testNick, testPass));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SingUp_WithEmptyNick()
        {
            service.SignUp(string.Empty, testPass);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SingUp_WithEmptyPassword()
        {
            service.SignUp(testNick, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SingUp_WithEmptyNickAndPassword()
        {
            service.SignUp(string.Empty, string.Empty);
        }


        [TestCategory("Stubs")]
        [TestMethod]
        public void SignUp_WithUserObject()
        {
            var user = new StubIUser()
            {
                NickGet = () => { return testNick; },
                PasswordGet = () => { return testPass; }
            };
            service.SignUp(user);
            CollectionAssert.Contains(service.Users, user);
        }

        [TestMethod]
        public void SingUpWithRepeatedNick()
        {
            for (int i = 0; i < 10; i++)
                service.SignUp(testNick, testPass);
            Assert.AreEqual(1, service.Users.Count);
        }

        [TestMethod]
        public void SingUpMultipleNicks()
        {
            for(int i = 0; i < 20; i++)
                service.SignUp(testNick2 + i, testPass);
            Assert.AreEqual(20, service.Users.Count);
        }

        [TestCategory("Stubs")]
        [TestMethod]
        public void SingUpMultipleUsers()
        {
            for (int i = 0; i < 10; i++)
            {
                string nick = testNick + i;
                var user = new StubIUser() { NickGet = () => { return nick; } };
                service.SignUp(user);
            }
            Assert.AreEqual(10, service.Users.Count);
        }

        // GET_USER
        [TestCategory("Stubs")]
        [TestMethod]
        public void GetDoesntExistUser_ReturnsNull()
        {
            var user = new StubIUser() { NickGet = () => { return testNick; } };
            Assert.AreEqual(null, service.GetUser(user.NickGet()));
        }

        [TestCategory("Stubs")]
        [TestMethod]
        public void GetExistUser_ReturnUser()
        {
            var user = new StubIUser() { NickGet = () => { return testNick; } };
            service.SignUp(user);
            Assert.AreEqual(user, service.GetUser(user.NickGet()));
        }

        // USER EXIST

        [TestCategory("Stubs")]
        [TestMethod]
        public void UserExist_UserInList()
        {
            var user = new StubIUser()
            {
                NickGet = () => { return testNick; },
                PasswordGet = () => { return testPass; }
            };
            service.SignUp(user);
            Assert.IsTrue(service.UserExists(user));
        }

        [TestCategory("Stubs")]
        [TestMethod]
        public void UserExist_UserNotInList()
        {
            var user = new StubIUser()
            {
                NickGet = () => { return testNick; },
                PasswordGet = () => { return testPass; }
            };
            Assert.IsFalse(service.UserExists(user));
        }

        [TestMethod]
        public void UserExist_NickInList()
        {
            service.SignUp(testNick, testPass);
            Assert.IsTrue(service.UserExists(testNick));
        }

        [TestMethod]
        public void UserExist_NickNotInList()
        {
            Assert.IsFalse(service.UserExists(testNick));
        }

        [TestMethod]
        public void UserExist_NullNick()
        {
            string nullStr = null;
            Assert.IsFalse(service.UserExists(nullStr));
        }

        // LOG IN
        [TestCategory("Stubs")]
        [TestMethod]
        public void LogIn_UserExists_ReturnsUser()
        {
            var user = new StubIUser()
            {
                NickGet = () => { return testNick; },
                PasswordGet = () => { return testPass; }
            };
            service.SignUp(user);
            Assert.AreEqual(user, service.LogIn(testNick, testPass));
        }

        [TestCategory("Stubs")]
        [TestMethod]
        public void LogIn_UserDoesNotExist_ReturnsNull()
        {
            var user = new StubIUser()
            {
                NickGet = () => { return testNick; },
                PasswordGet = () => { return testPass; }
            };
            Assert.AreEqual(null, service.LogIn(testNick, testPass));
        }

        // REMOVE USER

        [TestMethod]
        public void RemoveUser_ExistingNick()
        {
            service.SignUp(testNick, testPass);
            Assert.IsTrue(service.RemoveUser(testNick));
        }

        [TestCategory("Stubs")]
        [TestMethod]
        public void RemoveUser_ExistingUserObject()
        {
            var user = new StubIUser()
            {
                NickGet = () => { return testNick; },
                PasswordGet = () => { return testPass; },
            };
            service.SignUp(user);
            service.RemoveUser(user);
            CollectionAssert.DoesNotContain(service.Users, user);
        }

        [TestCategory("Stubs")]
        [TestMethod]
        public void RemoveUser_WithSameData_ButDifferentObject()
        {
            var user = new StubIUser()
            {
                NickGet = () => { return testNick; },
                PasswordGet = () => { return testPass; },
            };
            service.SignUp(testNick, testPass);
            Assert.IsFalse(service.RemoveUser(user));
        }

        [TestMethod]
        public void RemoveUser_NonExistingNickname_ReturnsFalse()
        {
            Assert.IsFalse(service.RemoveUser(testNick));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveUser_NullUser_ThrowsException()
        {
            IUser nullUser = null;
            service.RemoveUser(nullUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveUser_NullString_ThrowsException()
        {
            string nullString = null;
            service.RemoveUser(nullString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveUser_EmptyString_ThrowsException()
        {
            service.RemoveUser(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateWorkoutWithEmptyDuration_ExistingNick()
        {
            service.SignUp(testNick, testPass);
            service.CreateWorkout(testNick, testDate, testDistance, string.Empty, testElevation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateWorkoutWithNullDuration_ExistingNick()
        {
            string nullStr = null;
            service.SignUp(testNick, testPass);
            service.CreateWorkout(testNick, testDate, testDistance, nullStr, testElevation);
        }

        [TestMethod]
        public void CreateWorkout_WithNick()
        {
            service.SignUp(testNick, testPass);
            service.CreateWorkout(testNick, testDate, testDistance, testDuration, testElevation);
            Assert.AreEqual(1, service.Workouts.Count);
        }

        [TestCategory("Data-Driven")]
        [TestMethod, DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
    "|DataDirectory|\\AddWorkouts.csv", "AddWorkouts#csv",
            DataAccessMethod.Sequential), DeploymentItem("AddWorkouts.csv")]
        public void AddUser_AddCorrectUserFromData_ReturnsTrue()
        {
            service.SignUp("ronek22", testPass);
            string nick = TestContext.DataRow["nick"].ToString();
            DateTime date = DateTime.ParseExact(TestContext.DataRow["date"].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            Double.TryParse(TestContext.DataRow["distance"].ToString(), out double distance);
            string duration = TestContext.DataRow["duration"].ToString();
            Int32.TryParse(TestContext.DataRow["elevation"].ToString(), out int elevation);

            service.CreateWorkout(nick, date, distance, duration, elevation);
            StringAssert.Contains(service.GetUser(nick).ListWorkouts(), distance.ToString());
        }
    }
}
