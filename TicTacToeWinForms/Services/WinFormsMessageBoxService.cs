using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeWinForms.Services
{
    /// <summary>
    /// WinForms implementation of IMessageBoxService
    /// </summary>
    public class WinFormsMessageBoxService : IMessageBoxService
    {
        /// <summary>
        /// Creates a message box.
        /// </summary>
        /// <param name="message">The text to display in the message box.</param>
        /// <param name="title">The text to display in the title bar of the message box.</param>
        public void Show(string message, string title)
        {
            MessageBox.Show(message, title);
        }
    }
}
