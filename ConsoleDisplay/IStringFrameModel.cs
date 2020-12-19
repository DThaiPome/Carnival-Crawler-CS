using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.ConsoleDisplay
{
    /// <summary>
    /// An object that outputs text to display in a ConsoleDisplay.
    /// </summary>
    public interface IStringFrameModel
    {
        /// <summary>
        /// Update the display (next frame).
        /// </summary>
        void Update();

        /// <summary>
        /// Get the text to be displayed.
        /// </summary>
        /// <returns>the current display frame.</returns>
        string GetString();

        /// <summary>
        /// Get the height an width of a model.
        /// </summary>
        /// <returns>(rows, columns)</returns>
        (int, int) GetDimensions();
    }
}
