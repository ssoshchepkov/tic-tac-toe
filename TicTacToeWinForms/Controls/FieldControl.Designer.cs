namespace TicTacToeWinForms.Controls
{
    partial class FieldControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FieldControl));
            this.headerPanel = new System.Windows.Forms.Panel();
            this.playerLabel = new System.Windows.Forms.Label();
            this.playerCaptionlabel = new System.Windows.Forms.Label();
            this.turnLabel = new System.Windows.Forms.Label();
            this.turnCaptionLabel = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.playerLabel);
            this.headerPanel.Controls.Add(this.playerCaptionlabel);
            this.headerPanel.Controls.Add(this.turnLabel);
            this.headerPanel.Controls.Add(this.turnCaptionLabel);
            this.headerPanel.DataBindings.Add(new System.Windows.Forms.Binding("Size", global::TicTacToeWinForms.Properties.Settings.Default, "InfoPanelSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.headerPanel, "headerPanel");
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = global::TicTacToeWinForms.Properties.Settings.Default.InfoPanelSize;
            // 
            // playerLabel
            // 
            resources.ApplyResources(this.playerLabel, "playerLabel");
            this.playerLabel.Name = "playerLabel";
            // 
            // playerCaptionlabel
            // 
            resources.ApplyResources(this.playerCaptionlabel, "playerCaptionlabel");
            this.playerCaptionlabel.Name = "playerCaptionlabel";
            // 
            // turnLabel
            // 
            resources.ApplyResources(this.turnLabel, "turnLabel");
            this.turnLabel.Name = "turnLabel";
            // 
            // turnCaptionLabel
            // 
            resources.ApplyResources(this.turnCaptionLabel, "turnCaptionLabel");
            this.turnCaptionLabel.Name = "turnCaptionLabel";
            // 
            // FieldControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.headerPanel);
            this.Name = "FieldControl";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Label turnCaptionLabel;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Label playerCaptionlabel;
    }
}
