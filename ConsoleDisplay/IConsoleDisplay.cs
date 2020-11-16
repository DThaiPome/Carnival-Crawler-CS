using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.ConsoleDisplay
{
    /// <summary>
    /// Displays text in a constant location within the console.
    /// </summary>
    public interface IConsoleDisplay
    {
        /// <summary>
        /// Display the current frame of text.
        /// </summary>
        void Draw();
    }
}
