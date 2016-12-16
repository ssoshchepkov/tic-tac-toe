using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReactiveUI;
using TicTacToeWinForms.ViewModels;
using System.Reactive.Linq;

namespace TicTacToeWinForms.Controls
{
    /// <summary>
    /// Represents View class for a cell on game field
    /// </summary>
    public partial class CellControl : UserControl, IViewFor<CellViewModel>
    {
        /// <summary>
        /// Creates a new instance of <see cref="CellControl"/> with specified view model.
        /// </summary>
        /// <param name="vm">View model for this view.</param>
        public CellControl(CellViewModel vm)
        {
            InitializeComponent();
            VM = vm;

            // Binding button text to the cell marking symbol.
            this.OneWayBind(VM, x => x.Caption, x => x.button.Text);

            // Binding button click event to MarkCellCommand
            this.BindCommand(VM, x => x.MarkCellCommand, x => x.button);

            this.OneWayBind(VM, x => x.IsWinning, x => x.button.BackColor, winnig => winnig ? SystemColors.MenuHighlight : button.BackColor);
            
            // Setting decent background color for "disabled" control.
            button.Events().EnabledChanged.Subscribe(x => 
            {
                if(!button.Enabled) button.BackColor = SystemColors.ButtonHighlight;
            });
        }

        #region ViewModel
        public CellViewModel VM { get; private set; }

        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (CellViewModel)value; }
        }

        CellViewModel IViewFor<CellViewModel>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }
        #endregion ViewModel
    }
}
