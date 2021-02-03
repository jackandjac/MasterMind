using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind
{
    class Master
    {
        public const string STR_MASTER_MIND = "================================== Master Mind Game ==================================";
        public const string STR_INVALID_NUMBERS = "Input is invalid, the numbers you entered have to be greater than 0 and less or equal to 6, and separated by space";
        public const string STR_REPLAY_OR_QUIT = "Do you want to play this game again? Press 'R' to re-play or Press any to quit...";
        public const string STR_RETRY_OR_QUIT = "Press 'R' to retry or Press any key to quit...";
        public const string STR_GET_STARTED = "All right, let's get play the Master Mind Game.";
        public const string STR_NUMBER_GENERATED = "Number Generated :" + " [?] [?] [?] [?] ";
        public const string STR_INPUT_PROMPT = "Please enter your Guess:";
        public const string STR_THE_CODE = "The real code is:";
        public const string STR_FAILED = "Unfortunately, you didn't break the code, do you want to try again?";
        public const string STR_CONGRAT = "Congratulation, you successfully break the code. ";
        public const string STR_TEMP_RESULT = "You still have {0} time left.";       
        public const string STR_ROUND = "===================================    Round {0}    ===================================";

        private Random rand = new Random();
        private int[] target;
        public Master()
        {
            this.play();
        }

        private int[] gameNumberGenerator()
        {
            int[] nums = new int[4];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = rand.Next(1, 6);
            }
            return nums;
        }

        private void play()
        {
            //print the header
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(STR_MASTER_MIND);

            while (true)
            {
                //print the starting info
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(STR_GET_STARTED);
                Console.WriteLine(STR_NUMBER_GENERATED);
                
                //generate the game code that player needs to break
                target = gameNumberGenerator();
                bool win = false;
                for (int i = 0; i < 10; i++)
                {
                    //display the round info
                    Console.WriteLine(STR_ROUND, i + 1);

                    //get the hint based on the user input
                    string[] res = this.judgeUserInput(this.getUserInput(), target);

                    win = isWinner(res);

                    if (win)
                    {
                        //if user win the print the congratulation info.
                        Console.WriteLine(STR_CONGRAT);
                        //ask if user want to play the game again?
                        if (rePlay())
                        {
                            break;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        printRes(res, 10-i-1);
                    }    
                }

                if (!win)
                {
                    //if user failed to break the code disclose the code
                    Console.WriteLine(STR_THE_CODE);
                    Console.ForegroundColor = ConsoleColor.Green;
                    for (int i = 0; i < target.Length; i++)
                    {
                        
                        Console.Write(" [" + target[i] + "] ");
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(STR_FAILED);
                if (rePlay())
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        /// <summary>
        /// print the hint based on the user input
        /// </summary>
        /// <param name="digits">the user input result</param>
        /// <param name="round">the round that user currently at</param>
        private void printRes(string[] digits, int round)
        {
            for(int i = 0; i < digits.Length; i++)
            {
                if(digits[i].Equals("+"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(digits[i]);
                Console.Write("  ");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            if(round>0)
                Console.WriteLine(STR_TEMP_RESULT, round);
        }

        /// <summary>
        /// Ask user if they want to play the game again
        /// </summary>
        /// <returns></returns>
        private bool rePlay()
        {
            Console.WriteLine(STR_REPLAY_OR_QUIT);
            string line = Console.ReadLine().Trim();
            if(String.Equals(line,"r", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check if user win the game.
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        private bool isWinner(string[] digits)
        {
            for(int i = 0; i < digits.Length; i++)
            {
                if (!digits[i].Equals("+"))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Compare the user input with the generated numbers to generate the hint
        /// </summary>
        /// <param name="source">The user input number</param>
        /// <param name="target">The generated </param>
        /// <returns>the hint for user to use for the next round of the game</returns>
        private string[] judgeUserInput(int[] source, int[] target )
        {
            string[] digits = {"","","",""};
            int k = 0;
            for(int i = 0; i < source.Length; i++)
            {
                if(source[i]== target[i])
                {
                    digits[k++] = "+";
                    source[i] = 0;
                }
            }
            for(int i = 0; i < source.Length; i++)
            {
                if (source[i] == 0)
                    continue;

                for(int j = 0; j < target.Length; j++)
                {
                    if(source[i] == target[j])
                    {
                        digits[k++] = "-";
                        break;
                    }
                }
            }
            return digits;
        }
        /// <summary>
        /// Get the user input numbers from Command line
        /// </summary>
        /// <returns></returns>
        private int[] getUserInput()
        {
            int[] nums=null;
            while (true)
            {
                Console.WriteLine(STR_INPUT_PROMPT);
                string line = Console.ReadLine().Trim();
                nums = converter(line.Split(" "));
                if (nums == null)
                {
                    Console.WriteLine(STR_INVALID_NUMBERS);
                    Console.WriteLine(STR_RETRY_OR_QUIT);
                    if (rePlay())
                    {
                        continue;
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                }
                else
                {
                    break;
                }
            }
            return nums;
        }
        /// <summary>
        /// Convert the user input string into numbers
        /// </summary>
        /// <param name="numbers">the User input numbers which need to convert to int</param>
        /// <returns></returns>
        private int[] converter(string[] numbers)
        {
            int[] nums = new int[numbers.Length];
            for(int i = 0; i < numbers.Length; i++)
            {
                int temp = 0;
                if(Int32.TryParse(numbers[i],out temp))
                {    
                    //validate the user input numbers
                    if(temp >0 && temp <= 6)
                    {
                        nums[i] = temp;
                    }
                    else
                    {
                        return null;
                    }
                    
                }
                else
                {
                    return null;
                }
            }

            return nums;
        }
    }
}
