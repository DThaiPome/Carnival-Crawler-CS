using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.ConsoleDisplay
{
    public class FramedDisplayModel : IStringFrameModel
    {
        private IStringFrameModel model;
        private char frameChar;
        private int frameRows;
        private int frameCols;
        private int sizeRows;
        private int sizeCols;

        public FramedDisplayModel(IStringFrameModel model, char frameChar, (int, int) frameDim, (int, int) sizeDim)
        {
            this.model = model;
            this.frameChar = frameChar;
            this.frameRows = frameDim.Item1;
            this.frameCols = frameDim.Item2;
            this.sizeRows = sizeDim.Item1;
            this.sizeCols = sizeDim.Item2;
        }

        public FramedDisplayModel(IStringFrameModel model, char frameChar) : this(model, frameChar, (1, 1), model.GetDimensions()) { }

        public (int, int) GetDimensions()
        {
            (int, int) modelDim = this.model.GetDimensions();
            return (modelDim.Item1 + 1, modelDim.Item2 + 1);
        }

        public string GetString()
        {
            string[] modelLines = this.model.GetString().Split('\n');
            string[] lines = new string[modelLines.Length + (2 * this.frameRows)];
            for(int i = 0; i < frameRows; i++)
            {
                lines[i] = this.GetFrameCharLine(this.GetDimensions().Item2);
            }
            for(int i = frameRows; i < lines.Length - frameRows; i++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(this.GetFrameCharLine(this.frameCols));
                builder.Append(lines[i - this.frameRows]);
                builder.Append(this.GetFrameCharLine(this.frameCols));

                lines[i] = builder.ToString();
            }
            for (int i = lines.Length - this.frameRows; i < lines.Length; i++)
            {
                lines[i] = this.GetFrameCharLine(this.GetDimensions().Item2);
            }

            return String.Join("\n", lines);
        }

        private string GetFrameCharLine(int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int c = 0; c < length; c++)
            {
                builder.Append(frameChar);
            }
            return builder.ToString();
        }

        public void Update()
        {
            this.model.Update();
        }
    }
}
