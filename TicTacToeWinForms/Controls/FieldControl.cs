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
using TicTacToe.Models;
using TicTacToeWinForms.Properties;

namespace TicTacToeWinForms.Controls
{
    /// <summary>
    /// Represents View class for a game field
    /// </summary>
    public partial class FieldControl : UserControl, IViewFor<GameFieldViewModel>
    {
        /// <summary>
        /// Contains views for all cell of this field.
        /// </summary>
        private List<CellControl> _cells;

        /// <summary>
        /// Creates a new instance of <see cref="FieldControl"/> with specified view model.
        /// </summary>
        /// <param name="viewModel">View model for this view.</param>
        public FieldControl(GameFieldViewModel viewModel)
        {
            InitializeComponent();

            VM = viewModel;

            _cells = new List<CellControl>(VM.FieldSize * VM.FieldSize);

            CellControl cell = null;

            // creating views for all cells
            for (int row = 0; row < VM.FieldSize; row += 1)
            {
                for (int col = 0; col < VM.FieldSize; col += 1)
                {
                    cell = new CellControl(VM.Cells[col, row]) { Parent = this };
                    cell.Left = Padding.Left + col * cell.Width;
                    cell.Top = Padding.Top + row * cell.Height + headerPanel.Bottom;
                    _cells.Add(cell);
                }
            }

            // Binding turn indicator to ViewModel
            this.OneWayBind(VM, x => x.CurrentTurn, x => x.turnLabel.Text);
            // Binding current player indicator to ViewModel
            this.OneWayBind(VM, x => x.CurrentPlayerSymbol, x => x.playerLabel.Text);

            ResizeView();
        }

        /// <summary>
        /// Recalculates width and height of this control depending on the size of game field.
        /// </summary>
        private void ResizeView()
        {
            int dx = Width - ClientRectangle.Width;
            int dy = Height - ClientRectangle.Height;
            Width = Padding.Left + Settings.Default.ButtonControlSize.Width * VM.FieldSize + dx + Padding.Right;
            Height = Padding.Top + Padding.Bottom + Settings.Default.ButtonControlSize.Height * VM.FieldSize + dy + Settings.Default.InfoPanelSize.Height;
        }

        #region ViewModel
        public GameFieldViewModel VM { get; private set; }

        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (GameFieldViewModel)value; }
        }

        GameFieldViewModel IViewFor<GameFieldViewModel>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }
        #endregion ViewModel
    }
}
