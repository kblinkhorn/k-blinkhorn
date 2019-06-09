using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    class Program
    {
        static void Main(string[] args)
        {

            int iMinAnswer = 1111;
            int iMaxAnswer = 6666;
            int iMinDigit = 1;
            int iMaxDigit = 6;

            int iNbrGuesses = 10;

            bool[] aGuess = { false, false, false, false };
            bool[] aAnswer = { false, false, false, false };

            bool wwcd = false;
            
            Console.WriteLine("Welcome to Mastermind!\n");
            Console.WriteLine("\nYou will have 10 chances to solve the game\nby selecting a 4 digit number where each digit is between 1 and 6.");
            Console.WriteLine("Press any key to begin....");
            Console.ReadKey();

            Console.Clear();

            //generate Random number
            int iRandomNbr = GenerateRandomNumber(); 
            
            while (iNbrGuesses > 0)
            {
                Console.WriteLine(string.Format("You have {0} guesses remaining. ", iNbrGuesses));

                int iGuess = 0;

                Console.WriteLine("Enter a 4 digit number with each number between 1 and 6  ie 2164: ");
                string sGuess = Console.ReadLine();

                //Check if guess is the correct format
                if (isGuessCorrectFormat(ref sGuess))
                {
                    Int32.TryParse(sGuess, out iGuess);

                    if (iGuess == iRandomNbr)
                    {
                        wwcd = true;
                    }

                    // check if digits in correct place
                    for (int i = 0; i < 4; i++)
                    {
                        aGuess[i] = false;
                        aAnswer[i] = false;
                    }
                    
                    int iGuessTemp = iGuess;
                    int iguessDgt = 0;
                    int iRandomTemp = iRandomNbr;
                    int irandDgt = 0;
                    int iCntInPos = 0;

                    for (int j = 0; j < 4; j++)
                    {
                        iguessDgt = iGuessTemp % 10;
                        iGuessTemp = iGuessTemp / 10;
                        irandDgt = iRandomTemp % 10;
                        iRandomTemp = iRandomTemp / 10;

                        if (iguessDgt == irandDgt)
                        {
                            aGuess[j] = true;
                            aAnswer[j] = true;
                            iCntInPos++;
                        }
                    }

                    string sResultInPos = null;

                    switch (iCntInPos)
                    {
                        case 0:
                            break;
                        case 1:
                            sResultInPos = "+";
                            break;
                        case 2:
                            sResultInPos = "++";
                            break;
                        case 3:
                            sResultInPos = "+++";
                            break;
                    }



                    //Check if digits are correct but out of place.
                    int iCntOutPos = 0;

                    iGuessTemp = iGuess;
                    iRandomTemp = iRandomNbr;

                    for (int k = 0; k < 4; k++)
                    {
                        iguessDgt = iGuessTemp % 10;
                        iGuessTemp = iGuessTemp / 10;
                        irandDgt = iRandomTemp % 10;
                        iRandomTemp = iRandomNbr;

                        if (aGuess[k] == false)
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                irandDgt = iRandomTemp % 10;
                                iRandomTemp = iRandomTemp / 10;
                                if (aAnswer[l] == false)
                                {
                                    if (iguessDgt == irandDgt)
                                    {
                                        iCntOutPos++;
                                        aGuess[k] = true;
                                        aAnswer[l] = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    string sResultOutPos = null;

                    switch (iCntOutPos)
                    {
                        case 0:
                            break;
                        case 1:
                            sResultOutPos = "-";
                            break;
                        case 2:
                            sResultOutPos = "--";
                            break;
                        case 3:
                            sResultOutPos = "---";
                            break;
                        case 4:
                            sResultOutPos = "----";
                            break;
                    }

                    string sResult = string.Format("Result: {0}{1}", sResultInPos, sResultOutPos);
                    Console.WriteLine("Results: " + sResult);
                    Console.WriteLine("---------------------");
                    iNbrGuesses--;
                }
                else
                    Console.WriteLine("You have entered some invalid characters. Make sure your input is between 1111 and 6666, with each digit between 1 and 6.");
            }
            if (wwcd)
            {
                Console.WriteLine("**********************************");
                Console.WriteLine("*                                *");
                Console.WriteLine("*         WINNER  WINNER         *");
                Console.WriteLine("*                                *");
                Console.WriteLine("*        CHICKEN DINNER!!!!      *");
                Console.WriteLine("*                                *");
                Console.WriteLine("**********************************");
            }
            else
            {
                Console.WriteLine("You have lost the game.");
                Console.WriteLine("The Answer was: " + iRandomNbr);
            }

            Console.WriteLine("\r\n\r\nThe Game has ended, press any key to exit");
            Console.ReadKey();
        }

        private static int GenerateRandomNumber()
        {
            string sRndAns = "";
            Random sRdm = new Random();
            for (int i = 0; i < 4; i++)
            {
                sRndAns = sRndAns + sRdm.Next(1, 6).ToString();
            }

            return Int32.Parse(sRndAns);
        }

        private static bool isGuessCorrectFormat(ref string sGuess)
        {
            int iGuess = 0;
            Int32.TryParse(sGuess, out iGuess);

            int[] iGuessArray = GetIntArray(iGuess);

            for (int i = 0; i < 4; i++)
            {
                int _dgt = iGuessArray[i];
                if (_dgt > 6 || _dgt < 1)
                {
                    Console.WriteLine("You have entered some invalid characters.\nPlease re-enter your guess and make sure it is between 1111 and 6666, with each digit between 1 and 6.");
                    Console.WriteLine("please re-enter your guess: ");
                    sGuess = Console.ReadLine();
                    if (isGuessCorrectFormat(ref sGuess))
                        return true;
                    return false;
                }
            }
            return true;
        }

        private static int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }
    }
}
