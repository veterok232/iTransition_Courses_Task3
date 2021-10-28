using System;
using System.Linq;
using ASCIITableGenerator;
using Task3.Model;
using Task3.Helper;

namespace Task3
{
    class Program
    {
        const string ERROR_USER_INPUT_MESSAGE = "Incorrect input! Please input your turn again..";
       
        const string ERROR_INCORRECT_ARGUMENTS = "Incorrect command line arguments! It should be > 1 non-repeat odd arguments..";
        
        const string WIN_MESSAGE = "You win!";
        
        const string LOSE_MESSAGE = "You lose!";
        
        const string DRAW_MESSAGE = "Draw!";

        public static void ShowMenu()
        {
            Console.WriteLine("Available moves:");
            int i = 0;
            foreach (var move in GameLogic.moves)
            {
                Console.WriteLine($"{++i} - {move}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }

        public static bool IsArgumentsCorrect(string[] args)
        {
            return !(
                (args.Length < 3) || 
                (args.Length % 2 == 0) || 
                (args.Distinct().ToArray().Length != args.Length)
        );
        }

        public static bool IsUserInputCorrect(string input)
        {
            return (int.TryParse(input, out _) && GameLogic.CheckUserMove(Convert.ToInt32(input))) 
                || (input == "?");
        }

        public static bool CommandController(string userCommand)
        {
            switch (userCommand)
            {
                case "0":
                    return false;
                case "?":
                    ASCIITable helpTable = HelpTableGenerator.Create(GameLogic.moves);
                    Console.WriteLine(helpTable.GetAsString());
                    return false;
                default:
                    {
                        GameLogic.MakeUserMove(Convert.ToInt32(userCommand));
                        Console.WriteLine($"Your move: {GameLogic.GetMoveString(Convert.ToInt32(userCommand) - 1)}");
                        return true;
                    }
            }
        }
        
        static void Main(string[] args)
        {
            if (!IsArgumentsCorrect(args))
            {
                Console.WriteLine(ERROR_INCORRECT_ARGUMENTS);
                return;
            }
            GameLogic.CreateGameRools(args);
            GameLogic.MakeComputerMove();
            Console.WriteLine($"HMAC: {GameLogic.hmac}");
            ShowMenu();
            string userInput;
            Console.Write("Enter your move: ");
            while (!IsUserInputCorrect(userInput = Console.ReadLine()))
            {
                Console.WriteLine(ERROR_USER_INPUT_MESSAGE);
                Console.Write("Enter your move: ");
            }
            if (!CommandController(userInput))
            {
                Console.WriteLine("Exit..");
                return;
            }
            Console.WriteLine($"Computer move: {GameLogic.GetMoveString(Convert.ToInt32(GameLogic.computerMoveIndex))}");
            bool? isUserWin;
            if ((isUserWin = GameLogic.CheckGameResult()) == null)
            {
                Console.WriteLine(DRAW_MESSAGE);
            }
            else
            {
                Console.WriteLine(isUserWin == true ? WIN_MESSAGE: LOSE_MESSAGE);
            }
            Console.WriteLine($"HMAC key: {HMACGenerator.ConvertKeyToHex(GameLogic.secretKey)}");
            Console.ReadLine();
        }
    }
}
