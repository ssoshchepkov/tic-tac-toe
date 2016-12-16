using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWinForms.Services
{
    public interface IMessageBoxService
    {
        void Show(string message, string title);
    }
}
