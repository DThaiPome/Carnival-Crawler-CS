using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.ConsoleDisplay
{
    public class ConsoleDisplayWindow : IConsoleDisplay
    {
        IStringFrameModel model;
        int startRow;
        int startCol;
        int endRow;
        int endCol;

        public ConsoleDisplayWindow(IStringFrameModel model, int startRow, int startCol, int endRow, int endCol)
        {
            this.model = model;
            this.startRow = startRow;
            this.startCol = startCol;
            this.endRow = endRow;
            this.endCol = endCol;
        }

        public void Draw()
        {
            string modelText = this.model.GetString();
            string[] lines = modelText.Split('\n');

            for(int row = startRow; row < this.endRow 
                && row - startRow < lines.Length
                && row < Console.WindowHeight; row++)
            {
                string line = lines[row - startRow];
                StringBuilder builder = new StringBuilder(lines[row - startRow]);
                for(int i = line.Length; i < this.endCol - this.startCol
                    && i < Console.WindowWidth - this.startCol; i++)
                {
                    builder.Append(" ");
                }

                string lineToPrint = builder.ToString().Substring(0, Math.Min(Console.WindowWidth, this.endCol - this.startCol));
                if (this.startCol < Console.WindowWidth)
                {
                    Console.SetCursorPosition(this.startCol, row);
                    Console.Write(lineToPrint);
                }
            }

            Console.WriteLine();
        }
    }
}
