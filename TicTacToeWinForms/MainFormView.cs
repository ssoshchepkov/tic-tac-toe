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
using TicTacToe.Models.VictoryConditions;
using TicTacToeWinForms.Controls;
using TicTacToeWinForms.Properties;
using TicTacToeWinForms.Services;
using TicTacToeWinForms.ViewModels;

namespace TicTacToeWinForms
{
    /// <summary>
    /// Represents the main application window.
    /// </summary>
    public partial class MainFormView : Form, IViewFor<MainViewModel>
    {
        private FieldControl _gameField;

        /// <summary>
        /// Creates a new instance of <see cref="MainFormView"/>.
        /// </summary>
        public MainFormView()
        {
            InitializeComponent();

            var settings = new GameSettings(Settings.Default.FieldSize, new AllInLineVictoryConditions());
            settings.AddPlayer(new Player(Symbols.X)); // X goes first
            settings.AddPlayer(new Player(Symbols.O));

            VM = new MainViewModel(new WinFormsApplicationService(), new WinFormsMessageBoxService(), settings);

            // Binding 'Exit' menu element
            this.BindCommand(VM, x => x.ExitCommand, x => x.exitToolStripMenuItem);
            // Binding 'New game' menu element
            this.BindCommand(VM, x => x.NewGameCommand, x => x.newGameToolStripMenuItem);

            // Create a view for a new game
            this.WhenAnyValue(x => x.VM.GameField).Where(x => x != null).Subscribe(fieldVM => 
            {
                // clean-up the view of a previous game
                if (_gameField != null)
                {
                    _gameField.Dispose();
                    Controls.Remove(_gameField);
                }
                _gameField = new FieldControl(fieldVM) { Parent = this, Top = mainMenu.Bottom + Padding.Top, Left = Padding.Left };  
            });

            ResizeWindow();
        }

        /// <summary>
        /// Recalculates width and height of this window depending on the size of game field.
        /// </summary>
        private void ResizeWindow()
        {
            int dx = Width - ClientRectangle.Width;
            int dy = Height - ClientRectangle.Height;
            Width = Padding.Left + Settings.Default.ButtonControlSize.Width * VM.Settings.FieldSize + dx + Padding.Right;
            Height = Padding.Top + Padding.Bottom + Settings.Default.ButtonControlSize.Height * VM.Settings.FieldSize + dy + Settings.Default.InfoPanelSize.Height + mainMenu.Bottom;
        }

        #region ViewModel
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
        #endregion ViewModel
    }
}
