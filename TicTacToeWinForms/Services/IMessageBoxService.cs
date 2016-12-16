using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWinForms.Services
{
    /// <summary>
    /// This interface hides platform-specific implementation of message boxes.
    /// </summary>
    public interface IMessageBoxService
    {
        /// <summary>
        /// Creates a message box.
        /// </summary>
        /// <param name="message">The text to display in the message box.</param>
        /// <param name="title">The text to display in the title bar of the message box.</param>
        void Show(string message, string title);
    }
}
