using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorsPaper
{
    public class TurnCollection
    {
        public List<Turn> Turns { get; set; }
        
        public TurnCollection(string[] names)
        {
            Turns = new List<Turn>();
            var i = 1;
            Turn prevTurn = null;
            foreach(var n in names)
            {
                var t = new Turn()
                {
                    Button = i,
                    Name = n,
                    Prev = prevTurn,
                };
                if (prevTurn != null)
                {
                    prevTurn.Next = t;
                }
                i++;
                prevTurn = t;
                Turns.Add(t);
            }
            Turns.Last().Next = Turns.First();
            Turns.First().Prev = Turns.Last();
        }

        public string GameResult (int compNumber, int playerNumber)
        {
            var compTurn = Turns.First(x => x.Button == compNumber);
            var playerTurn = Turns.First(x => x.Button == playerNumber);
            if (compTurn.Button == playerTurn.Button)
            {
                return "DRAW";
            }
            var half = Turns.Count / 2;
            Turn playerNext = null;
            for (var i = 0; i<half; i++)
            {
                if (playerNext==null)
                {
                    playerNext = playerTurn.Next;
                }
                else
                {
                    playerNext = playerNext.Next;
                }
                if (playerNext.Button==compTurn.Button)
                {
                    return "YOU LOSE!";
                }
            }

            return "YOU WIN!";

        }

        public string [][] GetTable()
        {
            var res = new List<string[]>();
            var header = new List<string>() { "PC/USER" };
            header.AddRange(Turns.Select(x => x.Name));
            res.Add(header.ToArray());
            foreach (var horTurn in Turns)
            {
                var row = new List<string>();
                row.Add(horTurn.Name);
                foreach (var vertTurn in Turns)
                {
                    row.Add(GameResult(horTurn.Button, vertTurn.Button));
                }
                res.Add(row.ToArray());
            }
            return res.ToArray();
        }

    }
}
