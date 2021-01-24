using System;
using System.Collections.Generic;
using System.Linq;

namespace MentorWorkTask
{
    public class Program
    {
        static void Main()
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            if (inputValidation(input))
            {
                //initialization first matrix for first layer bricks
                int[,] layerOne = new int[input[0], input[1]];
                //initialization second matrix for second layer bricks
                int[,] layerTwo = new int[input[0], input[1]];


                //add bricks to matrix
                addBricksInMatrix(layerOne);

                //validate brick size  
                if (brickSizeValidation(layerOne))
                {
                    layerTwoCreating(layerTwo, layerOne);

                    drawingToConsole(layerTwo,input);
                }
                else
                {
                    Console.WriteLine("`-1` no solution exists");
                }
            }
            else
            {
                Console.WriteLine("`-1` no solution exists");
            }
        }

        static bool inputValidation(int[] input)
        {
            bool NumbersCountValidation = input.Length == 2;
            bool NumbersRangeValidation = input[0] < 100 && input[1] < 100;
            bool NumbersIsEvenValidation = input[0] % 2 == 0 && input[1] % 2 == 0;

            if (NumbersCountValidation && NumbersRangeValidation && NumbersIsEvenValidation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool brickSizeValidation(int[,] layerOne)
        {
            int[] allBricks = new int[layerOne.GetLength(0) * layerOne.GetLength(1)];
            var brickSize = new Dictionary<int, int>();
            var position = 0;

            for (int i = 0; i < layerOne.GetLength(0); i++)
            {
                for (int x = 0; x < layerOne.GetLength(1); x++)
                {
                    allBricks[position] = layerOne[i, x];
                    position++;
                }
            }

            foreach (var halfBrick in allBricks)
            {
                if (brickSize.ContainsKey(halfBrick))
                {
                    brickSize[halfBrick]++;
                }
                else
                {
                    brickSize[halfBrick] = 1;
                }
            }

            bool validation = true;

            foreach (var item in brickSize)
            {
                if (item.Value != 2)
                {
                    validation = false;
                }
            }

            return validation;
        }

        static int[,] addBricksInMatrix(int[,] layerOne)
        {
            for (int i = 0; i < layerOne.GetLength(0); i++)
            {
                var column = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                // validate numbers of column
                if (column.Length != layerOne.GetLength(1))
                {
                    break;
                }


                for (int col = 0; col < layerOne.GetLength(1); col++)
                {
                    layerOne[i, col] = column[col];
                }
            }

            return layerOne;
        }

        static int[,] layerTwoCreating(int[,] layerTwo, int[,] layerOne)
        {
            var number = 0;
            var layerTwoAddedBricks = 0;
            var currentNumber = 0;
            var nextColNumber = 0;
            var nextRawNumber = 0;

            var brickNumber = layerOne.Length / 2;

            var numbersOfBricks = new int[brickNumber];

            //take number of bricks
            for (int i = 1; i <= brickNumber; i++)
            {
                numbersOfBricks[i - 1] = i;
            }
            //begin to fill the second layer 
            for (int r = 0; r < layerTwo.GetLength(0); r++)
            {
                if (layerTwoAddedBricks == layerOne.Length / 2)
                {
                    break;
                }
                //fill second layer and check where to put the bricks
                for (int c = 0; c < layerTwo.GetLength(1); c++)
                {
                    //checks if a brick is already placed in the given position 
                    if (layerTwo[r, c] == 0)
                    {
                        //take current number of first layer
                        currentNumber = layerOne[r, c];

                        //is this is last column
                        if (c != layerTwo.GetLength(1) - 1)
                        {
                            //take next column number
                            nextColNumber = layerOne[r, c + 1];

                            //checks whether the current brick of the given position is the same as in the first layer
                            if (numbersOfBricks[number] == currentNumber)
                            {
                                //checks if the brick in the next position is the same
                                if (numbersOfBricks[number] == nextColNumber)
                                {
                                    //add halfbrick at current position 
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    // add next halfbrick in opposite direction , because the next one is the same
                                    layerTwo[r + 1, c] = numbersOfBricks[number];
                                    //take next brick
                                    number++; 
                                    //some my tests
                                    layerTwoAddedBricks++;
                                }
                                else
                                {
                                    //add halfbrick at current position 
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    // add next halfbrick in next position
                                    layerTwo[r, c + 1] = numbersOfBricks[number];
                                    //take next brick
                                    number++;
                                    //skips the next column, because we already add second half of brick there
                                    c++;
                                    layerTwoAddedBricks++;
                                }
                            }
                            else
                            {
                                //check is this is last row
                                if (r != layerOne.GetLength(0) - 1)
                                {
                                    //put current halfbrick at the current position
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    //put the second half of the brick on the bottom row
                                    layerTwo[r + 1, c] = numbersOfBricks[number];
                                    //take next brick
                                    number++;
                                    //some validation
                                    layerTwoAddedBricks++;
                                }
                                else
                                {
                                    //put current halfbrick at the current position
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    //put the second half of the brick on the next column
                                    layerTwo[r, c + 1] = numbersOfBricks[number];
                                    //skips the next column, because we already add second half of brick there
                                    c++;
                                    number++;
                                }
                            }
                        }
                        else
                        {
                            if (r != layerOne.GetLength(0) - 1)
                            {
                                //take the bottom number
                                nextRawNumber = layerOne[r + 1, c];
                                //if the bottom number is different from the current one we can put the brick vertically
                                if (numbersOfBricks[number] != nextRawNumber)
                                {
                                    //put half of brick in the current position
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    //put the next half in the bottom position
                                    layerTwo[r + 1, c] = numbersOfBricks[number];
                                    layerTwoAddedBricks++;
                                    //take next brick
                                    number++;
                                }
                                //if the number at the current position is different from the current one we can put the brick vertically
                                else if (numbersOfBricks[number] != currentNumber)
                                {
                                    //put half of brick in the current position
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    //put the next half in the bottom position
                                    layerTwo[r + 1, c] = numbersOfBricks[number];
                                    layerTwoAddedBricks++;
                                    //take next brick
                                    number++;
                                }
                                else
                                {
                                    //if the number of the given position is the same as the current one, and the number of the next row at the current position is the same 
                                    //swap the previus brick with the current one 

                                    //take the previous  brick and put it in the current position
                                    layerTwo[r, c] = numbersOfBricks[number - 1];
                                    layerTwo[r + 1, c] = numbersOfBricks[number - 1];
                                    //put the current brick in the position of previous  one
                                    layerTwo[r, c - 1] = numbersOfBricks[number];
                                    layerTwo[r + 1, c - 1] = numbersOfBricks[number];
                                    //take next brick
                                    number++;
                                }
                            }
                        }
                    }
                }
            }
            return layerTwo;
        }

        static void drawingToConsole(int[,] layerTwo, int[] input)
        {
            for (int row = 0; row < layerTwo.GetLength(0); row++)
            {
                //drawing first line of *
                if (row == 0)
                {
                    for (int i = 0; i < (input[1] * 2) + 1; i++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }
               
                
                if (row > 0)
                {
                    //check if brick is vertical don't draw * between parts of brick
                    for (int i = 1; i < (input[1] * 2) + 2; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (layerTwo[row - 1, (i / 2) - 1] == layerTwo[row, (i / 2) - 1])
                            {
                                Console.Write(" ");
                            }
                            else
                            {
                                Console.Write("*");
                            }
                        }
                        else
                        {
                            Console.Write("*");
                        }
                    }
                    Console.WriteLine();
                }
                //drawing bricks and * between them
                for (int col = 0; col < layerTwo.GetLength(1); col++)
                {
                    //check is this last column
                    if (col == layerTwo.GetLength(1) - 1)
                    {
                        //if brick is horisontal draw a (" "-white space) between the two halves
                        if (layerTwo[row, col] == layerTwo[row, col - 1])
                        {
                            Console.Write(" " + layerTwo[row, col] + "*");
                        }
                        else
                        {
                            Console.Write("*" + layerTwo[row, col] + "*");
                        }
                    }
                    else if (col == 0)
                    {
                        Console.Write("*" + layerTwo[row, col]);
                    }
                    else
                    {
                        if (layerTwo[row, col] == layerTwo[row, col - 1])
                        {
                            Console.Write(" " + layerTwo[row, col]);
                        }
                        else if (layerTwo[row, col] != layerTwo[row, col - 1])
                        {
                            Console.Write("*" + layerTwo[row, col]);
                        }
                    }
                }
                Console.WriteLine();

                //drawing last line of *
                if (row == layerTwo.GetLength(0) - 1)
                {
                    for (int i = 0; i < (input[1] * 2) + 1; i++)
                    {
                        Console.Write("*");
                    }
                }
            }
            Console.WriteLine();
        }

    }
}
