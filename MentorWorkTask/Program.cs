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

                    drawToConsole(layerTwo,input);
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

            for (int i = 1; i <= brickNumber; i++)
            {
                numbersOfBricks[i - 1] = i;
            }

            for (int r = 0; r < layerTwo.GetLength(0); r++)
            {
                if (layerTwoAddedBricks == layerOne.Length / 2)
                {
                    break;
                }
                for (int c = 0; c < layerTwo.GetLength(1); c++)
                {
                    if (layerTwo[r, c] == 0)
                    {
                        currentNumber = layerOne[r, c];

                        if (c != layerTwo.GetLength(1) - 1)
                        {
                            nextColNumber = layerOne[r, c + 1];

                            if (numbersOfBricks[number] == currentNumber)
                            {
                                if (numbersOfBricks[number] == nextColNumber)
                                {
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    layerTwo[r + 1, c] = numbersOfBricks[number];
                                    number++;
                                    layerTwoAddedBricks++;
                                }
                                else
                                {
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    layerTwo[r, c + 1] = numbersOfBricks[number];
                                    number++;
                                    c++;
                                    layerTwoAddedBricks++;
                                }
                            }
                            else
                            {
                                if (r != layerOne.GetLength(0) - 1)
                                {
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    layerTwo[r + 1, c] = numbersOfBricks[number];
                                    number++;
                                    layerTwoAddedBricks++;
                                }
                                else
                                {
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    layerTwo[r, c + 1] = numbersOfBricks[number];
                                    c++;
                                    number++;
                                }
                            }
                        }
                        else
                        {
                            if (r != layerOne.GetLength(0) - 1)
                            {
                                nextRawNumber = layerOne[r + 1, c];
                                if (numbersOfBricks[number] != nextRawNumber)
                                {
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    layerTwo[r + 1, c] = numbersOfBricks[number];
                                    layerTwoAddedBricks++;
                                    number++;
                                }
                                else if (numbersOfBricks[number] != currentNumber)
                                {
                                    layerTwo[r, c] = numbersOfBricks[number];
                                    layerTwo[r + 1, c] = numbersOfBricks[number];
                                    layerTwoAddedBricks++;
                                    number++;
                                }
                                else
                                {
                                    layerTwo[r, c] = numbersOfBricks[number - 1];
                                    layerTwo[r + 1, c] = numbersOfBricks[number - 1];

                                    layerTwo[r, c - 1] = numbersOfBricks[number];
                                    layerTwo[r + 1, c - 1] = numbersOfBricks[number];
                                    number++;
                                }
                            }
                        }
                    }
                }
            }
            return layerTwo;
        }

        static void drawToConsole(int[,] layerTwo, int[] input)
        {
            for (int raw = 0; raw < layerTwo.GetLength(0); raw++)
            {
                if (raw == 0)
                {
                    for (int i = 0; i < (input[1] * 2) + 1; i++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }
                if (raw > 0)
                {
                    for (int i = 1; i < (input[1] * 2) + 2; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (layerTwo[raw - 1, (i / 2) - 1] == layerTwo[raw, (i / 2) - 1])
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

                for (int col = 0; col < layerTwo.GetLength(1); col++)
                {
                    //check is this last column
                    if (col == layerTwo.GetLength(1) - 1)
                    {
                        if (layerTwo[raw, col] == layerTwo[raw, col - 1])
                        {
                            Console.Write(" " + layerTwo[raw, col] + "*");
                        }
                        else
                        {
                            Console.Write("*" + layerTwo[raw, col] + "*");
                        }
                    }
                    else if (col == 0)
                    {
                        Console.Write("*" + layerTwo[raw, col]);
                    }
                    else
                    {
                        if (layerTwo[raw, col] == layerTwo[raw, col - 1])
                        {
                            Console.Write(" " + layerTwo[raw, col]);
                        }
                        else if (layerTwo[raw, col] != layerTwo[raw, col - 1])
                        {
                            Console.Write("*" + layerTwo[raw, col]);
                        }
                    }
                }
                Console.WriteLine();

                if (raw == layerTwo.GetLength(0) - 1)
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
