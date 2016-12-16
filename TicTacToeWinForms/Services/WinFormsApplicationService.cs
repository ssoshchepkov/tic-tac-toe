using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeWinForms.Services
{
    public class WinFormsApplicationService : IApplicationService
    {
        public void Exit()
        {
            Application.Exit();
        }
    }
}
