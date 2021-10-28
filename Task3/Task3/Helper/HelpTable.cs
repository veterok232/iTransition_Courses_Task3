using System.Collections.Generic;
using Task3.Model;
using ASCIITableGenerator;

namespace Task3.Helper
{
    public static class HelpTableGenerator
    {
        public static ASCIITable Create(List<string> moves)
        {
            List<string[]> lines = new List<string[]>();
            List<string> header = new List<string>();
            header.Add(" PC \\ User ");
            header.AddRange(moves);
            lines.Add(header.ToArray());
            for (int i = 0; i < moves.Count; i++)
            {
                List<string> line = new List<string>();
                line.Add(moves[i]);
                for (int j = 0; j < moves.Count; j++)
                {
                    bool? gameResult = GameLogic.CheckGameResult(i, j);
                    if (gameResult == null)
                    {
                        line.Add(" DRAW ");
                    }
                    else
                    {
                        line.Add(gameResult == true ? " WIN " : " LOSE ");
                    }
                }
                lines.Add(line.ToArray());
            }
            return new ASCIITable(lines);
        }
    }
}
