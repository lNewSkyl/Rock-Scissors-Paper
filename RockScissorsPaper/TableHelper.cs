using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorsPaper
{
    public static class TableHelper
    {
        public static void Draw(string[][] table)
        {
            var columnsWidths = new List<int>();
            for (var i = 0; i < table[0].Length; i++)
            {
                columnsWidths.Add(table.Max(x => x[i].Length) + 2);
            }

            var tableWidths = columnsWidths.Sum(x => x) + columnsWidths.Count() + 1;
            PrintLine(tableWidths);
            foreach (var row in table)
            {
                PrintRow(row, columnsWidths.ToArray());
                PrintLine(tableWidths);
            }
        }

        static void PrintLine(int tableWidth)
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(string[] columns, int[] columnsWidths)
        {
            string row = "|";
            for (var i = 0; i < columns.Length; i++)
            {
                var maxGap = columnsWidths[i] - columns[i].Length;
                var leftGap = maxGap / 2;
                var rightGap = maxGap - leftGap;
                row += new string(' ', leftGap);
                row += columns[i];
                row += new string(' ', rightGap);
                row += '|';
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
