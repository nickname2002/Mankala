using Xunit;
using Mancala;

namespace MancalaTest
{
    public class PitTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(9999)]
        [InlineData(100000)]
        public void TestFillPit(int stonesToInsert)
        {
            // Arrange
            Pit pit = new HomePit(0);

            //Act 
            pit.Fill(stonesToInsert);

            //Arrange
            Assert.Equal(pit.StonesAmount, stonesToInsert);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(5, 6)]
        [InlineData(99, 100)]
        [InlineData(9999, 10000)]
        public void TestAddSingleStone(int starting, int expected)
        {
            // Arrange
            Pit pit = new HomePit(0);
            pit.Fill(starting);

            //Act 
            pit.AddStone();

            //Arrange
            Assert.Equal(expected, pit.StonesAmount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(99)]
        [InlineData(9999)]
        public void TestRemovingStones(int starting)
        {
            // Arrange
            Pit pit = new HomePit(0);
            pit.Fill(starting);

            //Act 
            pit.RemoveStones();

            //Arrange
            Assert.Equal(0, pit.StonesAmount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(99)]
        [InlineData(9999)]
        public void TestGetStones(int starting)
        {
            // Arrange
            Pit pit = new HomePit(0);
            pit.Fill(starting);

            //Act 
            int actual = pit.GetStones();

            //Arrange
            Assert.Equal(actual, pit.StonesAmount);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(500, false)]
        [InlineData(9999, false)]
        public void TestPitEmpty(int starting, bool expected)
        {
            // Arrange
            Pit pit = new HomePit(0);
            pit.Fill(starting);

            //Act 
            bool actual = pit.IsEmpty();

            //Arrange
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(50, 50, false)]
        [InlineData(99, 99, false)]
        [InlineData(140, 140, true)]
        [InlineData(179, 179, true)]
        [InlineData(181, 181, false)]
        public void TestClickAccuracy(int screenX, int screenY, bool expected)
        {
            // Arrange
            Pit pit = new PlayPit(1);
            pit.ScreenLoc = new System.Drawing.Point(100, 100);

            // Act
            bool actual = pit.Clicked(new System.Drawing.Point(screenX, screenY));

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
