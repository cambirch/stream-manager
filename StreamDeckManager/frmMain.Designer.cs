namespace StreamDeckManager
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.buttonConnectToDeck = new System.Windows.Forms.Button();
			this.buttonConnectToOBS = new System.Windows.Forms.Button();
			this.layoutButtons = new System.Windows.Forms.TableLayoutPanel();
			this.buttonTallyManager = new System.Windows.Forms.Button();
			this.treeTallies = new System.Windows.Forms.TreeView();
			this.tabsTallyDetails = new System.Windows.Forms.TabControl();
			this.tabTallies = new System.Windows.Forms.TabPage();
			this.tabRemotes = new System.Windows.Forms.TabPage();
			this.treeRemotes = new System.Windows.Forms.TreeView();
			this.tabsTallyDetails.SuspendLayout();
			this.tabTallies.SuspendLayout();
			this.tabRemotes.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonConnectToDeck
			// 
			this.buttonConnectToDeck.Location = new System.Drawing.Point(169, 12);
			this.buttonConnectToDeck.Name = "buttonConnectToDeck";
			this.buttonConnectToDeck.Size = new System.Drawing.Size(151, 23);
			this.buttonConnectToDeck.TabIndex = 2;
			this.buttonConnectToDeck.Text = "Connect to Stream Deck";
			this.buttonConnectToDeck.UseVisualStyleBackColor = true;
			this.buttonConnectToDeck.Click += new System.EventHandler(this.buttonConnectToDeck_Click);
			// 
			// buttonConnectToOBS
			// 
			this.buttonConnectToOBS.Location = new System.Drawing.Point(12, 12);
			this.buttonConnectToOBS.Name = "buttonConnectToOBS";
			this.buttonConnectToOBS.Size = new System.Drawing.Size(151, 23);
			this.buttonConnectToOBS.TabIndex = 3;
			this.buttonConnectToOBS.Text = "Connect to OBS";
			this.buttonConnectToOBS.UseVisualStyleBackColor = true;
			this.buttonConnectToOBS.Click += new System.EventHandler(this.buttonConnectToOBS_Click);
			// 
			// layoutButtons
			// 
			this.layoutButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.layoutButtons.ColumnCount = 2;
			this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutButtons.Location = new System.Drawing.Point(12, 41);
			this.layoutButtons.Name = "layoutButtons";
			this.layoutButtons.RowCount = 2;
			this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutButtons.Size = new System.Drawing.Size(920, 442);
			this.layoutButtons.TabIndex = 4;
			// 
			// buttonTallyManager
			// 
			this.buttonTallyManager.Enabled = false;
			this.buttonTallyManager.Location = new System.Drawing.Point(326, 12);
			this.buttonTallyManager.Name = "buttonTallyManager";
			this.buttonTallyManager.Size = new System.Drawing.Size(100, 23);
			this.buttonTallyManager.TabIndex = 5;
			this.buttonTallyManager.Text = "Tally Manager";
			this.buttonTallyManager.UseVisualStyleBackColor = true;
			this.buttonTallyManager.Click += new System.EventHandler(this.buttonTallyManager_Click);
			// 
			// treeTallies
			// 
			this.treeTallies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeTallies.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.treeTallies.Location = new System.Drawing.Point(3, 3);
			this.treeTallies.Name = "treeTallies";
			this.treeTallies.ShowLines = false;
			this.treeTallies.ShowPlusMinus = false;
			this.treeTallies.ShowRootLines = false;
			this.treeTallies.Size = new System.Drawing.Size(177, 460);
			this.treeTallies.TabIndex = 0;
			// 
			// tabsTallyDetails
			// 
			this.tabsTallyDetails.Controls.Add(this.tabTallies);
			this.tabsTallyDetails.Controls.Add(this.tabRemotes);
			this.tabsTallyDetails.Dock = System.Windows.Forms.DockStyle.Right;
			this.tabsTallyDetails.Location = new System.Drawing.Point(938, 0);
			this.tabsTallyDetails.Name = "tabsTallyDetails";
			this.tabsTallyDetails.SelectedIndex = 0;
			this.tabsTallyDetails.Size = new System.Drawing.Size(191, 494);
			this.tabsTallyDetails.TabIndex = 7;
			// 
			// tabTallies
			// 
			this.tabTallies.BackColor = System.Drawing.SystemColors.Control;
			this.tabTallies.Controls.Add(this.treeTallies);
			this.tabTallies.Location = new System.Drawing.Point(4, 24);
			this.tabTallies.Name = "tabTallies";
			this.tabTallies.Padding = new System.Windows.Forms.Padding(3);
			this.tabTallies.Size = new System.Drawing.Size(183, 466);
			this.tabTallies.TabIndex = 0;
			this.tabTallies.Text = "Tallies";
			// 
			// tabRemotes
			// 
			this.tabRemotes.BackColor = System.Drawing.SystemColors.Control;
			this.tabRemotes.Controls.Add(this.treeRemotes);
			this.tabRemotes.Location = new System.Drawing.Point(4, 24);
			this.tabRemotes.Name = "tabRemotes";
			this.tabRemotes.Padding = new System.Windows.Forms.Padding(3);
			this.tabRemotes.Size = new System.Drawing.Size(183, 466);
			this.tabRemotes.TabIndex = 1;
			this.tabRemotes.Text = "Remotes";
			// 
			// treeRemotes
			// 
			this.treeRemotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeRemotes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.treeRemotes.Location = new System.Drawing.Point(3, 3);
			this.treeRemotes.Name = "treeRemotes";
			this.treeRemotes.ShowLines = false;
			this.treeRemotes.ShowPlusMinus = false;
			this.treeRemotes.ShowRootLines = false;
			this.treeRemotes.Size = new System.Drawing.Size(177, 460);
			this.treeRemotes.TabIndex = 1;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1129, 494);
			this.Controls.Add(this.tabsTallyDetails);
			this.Controls.Add(this.buttonTallyManager);
			this.Controls.Add(this.layoutButtons);
			this.Controls.Add(this.buttonConnectToOBS);
			this.Controls.Add(this.buttonConnectToDeck);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Stream Deck Manager";
			this.Shown += new System.EventHandler(this.frmMain_Shown);
			this.tabsTallyDetails.ResumeLayout(false);
			this.tabTallies.ResumeLayout(false);
			this.tabRemotes.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonConnectToDeck;
        private System.Windows.Forms.Button buttonConnectToOBS;
        private System.Windows.Forms.TableLayoutPanel layoutButtons;
		private System.Windows.Forms.Button buttonTallyManager;
		private System.Windows.Forms.TreeView treeTallies;
		private System.Windows.Forms.TabControl tabsTallyDetails;
		private System.Windows.Forms.TabPage tabTallies;
		private System.Windows.Forms.TabPage tabRemotes;
		private System.Windows.Forms.TreeView treeRemotes;
	}
}

