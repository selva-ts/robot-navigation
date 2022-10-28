using RobotNavigation.NaivgationApp;

namespace RobotNavigation.NaivgationAppUnitTest
{
    [TestClass]
    public class MarsNavigationUnitTest
    {
        [TestMethod]
        public void Naviagate_With_Valid_Inputs()
        {
            // Arrange
            var grid = "5x5";
            var command = "FFRFLFLF";
            var expected = "1,4,West";

            var marsNavigation = new MarsNavigation(grid, command);

            // Act
            marsNavigation.NavigateRobot();

            // Assert
            string actual = marsNavigation.FinalPosition;
            Assert.AreEqual(expected, actual, "Robot navigation failed");
        }

        [TestMethod]
        public void Naviagate_With_Invalid_Inputs()
        {
            // Arrange
            var grid = "4x4";
            var command = "FRFFLFF";
            var expected = "3,3,North";

            var marsNavigation = new MarsNavigation(grid, command);

            // Act
            marsNavigation.NavigateRobot();

            // Assert
            string actual = marsNavigation.FinalPosition;
            Assert.AreNotEqual(expected, actual, "Robot navigation failed");
        }
       
        [TestMethod]
        public void Naviagate_Out_Of_Grid_Inputs()
        {
            // Arrange
            var grid = "3x3";
            var command = "FFRFLFLF";
            var expected = "1,3,West";

            var marsNavigation = new MarsNavigation(grid, command);

            // Act
            marsNavigation.NavigateRobot();

            // Assert
            string actual = marsNavigation.FinalPosition;
            Assert.AreEqual(expected, actual, "Robot navigation failed");
        }

        [TestMethod]
        public void Naviagate_Negative_Grid_Inputs()
        {
            // Arrange
            var grid = "4x4";
            var command = "LFFLFFF";
            var expected = "1,1,South";

            var marsNavigation = new MarsNavigation(grid, command);

            // Act
            marsNavigation.NavigateRobot();

            // Assert
            string actual = marsNavigation.FinalPosition;
            Assert.AreEqual(expected, actual, "Robot navigation failed");
        }

        [TestMethod]
        public void Navigate_InvalidGrid_ShouldThrowArgumentException()
        {
            // Arrange
            var grid = "5y5";
            var command = "FRFFLF";

            // Act and assert
            Assert.ThrowsException<ArgumentException>(() => new MarsNavigation(grid, command));
        }

        [TestMethod]
        public void Navigate_InvalidCommand_ShouldThrowArgumentException()
        {
            // Arrange
            var grid = "4x4";
            var command = "AYFFRFLFLF";

            // Act and assert
            Assert.ThrowsException<ArgumentException>(() => new MarsNavigation(grid, command));
        }

    }
}