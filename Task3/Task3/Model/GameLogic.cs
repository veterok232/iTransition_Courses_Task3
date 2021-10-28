using System;
using System.Collections.Generic;
using Task3.Helper;

namespace Task3.Model
{
    public static class GameLogic
    {
        public static int computerMoveIndex { get; private set; }

        public static int userMoveIndex { get; private set; }
        
        public static byte[] secretKey { get; private set; }

        public static string hmac { get; private set; }

        public static List<string> moves { get; private set; }

        public static void CreateGameRools(string[] args)
        {
            moves = new List<string>(args);
        }

        public static bool CheckUserMove(int moveIndex)
        {
            return moveIndex - 1 < moves.Count;
        }

        public static string GetMoveString(int moveIndex)
        {
            return moveIndex < moves.Count ? moves[moveIndex] : null;
        }

        public static void MakeComputerMove()
        {
            HMACGenerator hmacGenerator = new HMACGenerator();
            secretKey = hmacGenerator.GenerateRandomKey(16);
            computerMoveIndex = Math.Abs(BitConverter.ToInt32(hmacGenerator.GenerateRandomKey(sizeof(int)), 0)) % (moves.Count);
            hmac = hmacGenerator.CalculateHMAC(secretKey, $"{moves[computerMoveIndex]}");
        }

        public static bool MakeUserMove(int userMove)
        {
            userMove--;
            if ((userMove >= 0) && (userMove < moves.Count))
            {
                userMoveIndex = userMove;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool? CheckGameResult(int computerMoveIndex, int userMoveIndex)
        {
            if (userMoveIndex == computerMoveIndex)
            {
                return null;
            }
            int h = moves.Count / 2;
            int delta = userMoveIndex - computerMoveIndex;
            return delta > 0 ? delta <= h : delta < -h;
        }

        public static bool? CheckGameResult()
        {
            return CheckGameResult(GameLogic.computerMoveIndex, GameLogic.userMoveIndex);
        }
    }
}
