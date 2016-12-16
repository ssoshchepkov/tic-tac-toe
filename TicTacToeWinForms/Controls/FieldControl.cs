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
    public partial class FieldControl : UserControl, IViewFor<GameFieldViewModel>
    {
        private List<CellControl> _cells;

        public FieldControl(GameFieldViewModel viewModel)
        {
            InitializeComponent();

            VM = viewModel;

            _cells = new List<CellControl>(VM.FieldSize * VM.FieldSize);

            CellControl cell = null;

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

            this.OneWayBind(VM, x => x.CurrentTurn, x => x.turnLabel.Text);
            this.OneWayBind(VM, x => x.CurrentPlayerSymbol, x => x.playerLabel.Text);

            ResizeView();
        }

        private void ResizeView()
        {
            int dx = Width - ClientRectangle.Width;
            int dy = Height - ClientRectangle.Height;
            Width = Padding.Left + Settings.Default.ButtonControlSize.Width * VM.FieldSize + dx + Padding.Right;
            Height = Padding.Top + Padding.Bottom + Settings.Default.ButtonControlSize.Height * VM.FieldSize + dy + Settings.Default.InfoPanelSize.Height;
        }

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
    }
}
