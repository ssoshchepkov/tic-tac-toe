using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeWinForms.Services
{
    /// <summary>
    /// WinForms implementation of IApplicationService
    /// </summary>
    public class WinFormsApplicationService : IApplicationService
    {
        /// <summary>
        /// Close the application.
        /// </summary>
        public void Exit()
        {
            Application.Exit();
        }
    }
}
