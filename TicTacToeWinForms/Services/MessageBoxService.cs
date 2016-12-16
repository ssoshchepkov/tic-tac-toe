using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeWinForms.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void Show(string message, string title)
        {
            MessageBox.Show(message, MessagesResource.GameOverPopupTitle);
        }
    }
}
