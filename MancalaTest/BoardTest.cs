using Xunit;
using Mancala;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace MancalaTest
{
    public class BoardTest
    {
        /* Test whether a missclick, out of range of the pits */
        [Theory]
        [InlineData(0, 0)]
        [InlineData(550, 0)]
        [InlineData(700, 500)]
        public void ClickOutOfPitRangeValidity(int mouseX, int mouseY)
        {
            //Arrange 
            Board b = new Board(6, 5);

            //Act 
            Point mouseLoc = new Point(mouseX, mouseY);
            Pit? clickedPit = b.ClickPit(mouseLoc);

            //Assert 
            Assert.Null(clickedPit);
        }

        // TODO: Test GetPlayPits
        
        /* Check whether the correct opposing pit of a play pit is calculated */
        [Theory]
        [InlineData(1, 9)]
        [InlineData(10, 2)]
        [InlineData(3, 11)]
        [InlineData(12, 4)]
        [InlineData(5, 13)]
        [InlineData(14, 6)]
        [InlineData(7, 15)]
        public void CheckOpposingPitValidity(int index, int expectedIndex)
        {
            // Arrange 
            Board b = new Board(8, 5);

            // Act
            Pit opposingPit = b.OpposingPit(b.GetPit(index));
            int actualIndex = opposingPit.IndexInList;

            // Assert
            Assert.Equal(expectedIndex, actualIndex);
        }

        // TODO: Transfer to homepit
        
        /* Check whether the correct pit is returned at a certain index */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GetPitValidity(int index)
        {
            // Arrange
            Board b = new Board(6, 4);

            // Act
            Pit? fetchedPit = b.GetPit(index);

            // Assert
            Assert.Equal(fetchedPit.IndexInList, index);
        }
    }
}