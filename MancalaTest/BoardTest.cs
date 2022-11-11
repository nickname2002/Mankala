using Xunit;
using Mancala;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

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

        private static Board CreateFakeBoard(int leftHomePit, int[] upper, int[] bottom, int rightHomePit)
        {
            WariFactory factory = new WariFactory();

            Board b = new Board(upper.Length, 5);
            Player p1 = factory.CreatePlayer(b, PlayerID.P1);
            Player p2 = factory.CreatePlayer(b, PlayerID.P2);

            // Create left home pit
            b.HomePitLeft.RemoveStones();
            b.HomePitLeft.Fill(leftHomePit);

            // Create upper row
            for (int i = 0; i < upper.Length; i++)
            {
                Pit cPit = b.GetPit(i + 1);
                cPit.RemoveStones();
                cPit.Fill(upper[i]);
            }

            // Create bottom row
            for (int i = 0; i < bottom.Length; i++)
            {
                Pit cPit = b.GetPit(i + upper.Length);
                cPit.RemoveStones();
                cPit.Fill(bottom[i]);
            }

            // Create right home pit
            b.HomePitRight.RemoveStones();
            b.HomePitRight.Fill(rightHomePit);

            return b;
        }
    }
}