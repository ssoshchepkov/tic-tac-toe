using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Models;
using TicTacToeWinForms.Controls;
using TicTacToeWinForms.Properties;
using TicTacToeWinForms.Services;
using TicTacToeWinForms.ViewModels;

namespace TicTacToeWinForms
{
    public partial class MainFormView : Form, IViewFor<MainViewModel>
    {
        private FieldControl _gameField;
 
        public MainFormView()
        {
            InitializeComponent();

            VM = new MainViewModel(new ApplicationService(), new MessageBoxService(), new GameSettings(Settings.Default.FieldSize));

            this.BindCommand(VM, x => x.ExitCommand, x => x.exitToolStripMenuItem);
            this.BindCommand(VM, x => x.NewGameCommand, x => x.newGameToolStripMenuItem);

            // Create a view for a new game
            this.WhenAnyValue(x => x.VM.GameField).Where(x => x != null).Subscribe(fieldVM => 
            {
                // clean-up view of a previous game
                if (_gameField != null)
                {
                    _gameField.Dispose();
                    Controls.Remove(_gameField);
                }
                _gameField = new FieldControl(fieldVM) { Parent = this, Top = mainMenu.Bottom + Padding.Top, Left = Padding.Left };  
            });

            ResizeWindow();
        }

        private void ResizeWindow()
        {
            int dx = Width - ClientRectangle.Width;
            int dy = Height - ClientRectangle.Height;
            Width = Padding.Left + Settings.Default.ButtonControlSize.Width * VM.Settings.FieldSize + dx + Padding.Right;
            Height = Padding.Top + Padding.Bottom + Settings.Default.ButtonControlSize.Height * VM.Settings.FieldSize + dy + Settings.Default.InfoPanelSize.Height + mainMenu.Bottom;
        }


        public MainViewModel VM { get; private set; }

        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (MainViewModel)value; }
        }

        MainViewModel IViewFor<MainViewModel>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }
    }
}
