using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWinForms.Services
{
    /// <summary>
    /// This interface hides platform-specific implementation of an application life-cycle management.
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Close the application.
        /// </summary>
        void Exit();
    }
}
