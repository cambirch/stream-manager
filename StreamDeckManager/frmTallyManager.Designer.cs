
namespace StreamDeckManager
{
	partial class frmTallyManager
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabSetup = new System.Windows.Forms.TabPage();
			this.treeTallyLights = new System.Windows.Forms.TreeView();
			this.treeSetup = new System.Windows.Forms.TreeView();
			this.tabTallys = new System.Windows.Forms.TabPage();
			this.buttonDeleteDisplay = new System.Windows.Forms.Button();
			this.buttonAddDisplay = new System.Windows.Forms.Button();
			this.buttonSaveDisplay = new System.Windows.Forms.Button();
			this.groupRemoteDisplay = new System.Windows.Forms.GroupBox();
			this.layoutRemoteDisplays = new System.Windows.Forms.TableLayoutPanel();
			this.textDisplayName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textDisplayDescription = new System.Windows.Forms.TextBox();
			this.textDisplayIPAddress = new System.Windows.Forms.TextBox();
			this.listRemoteDisplays = new System.Windows.Forms.ListBox();
			this.tabTallies = new System.Windows.Forms.TabPage();
			this.buttonDeleteTally = new System.Windows.Forms.Button();
			this.buttonAddTally = new System.Windows.Forms.Button();
			this.buttonSaveTally = new System.Windows.Forms.Button();
			this.groupTallies = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.textTallyName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textTallyDescription = new System.Windows.Forms.TextBox();
			this.textTallyID = new System.Windows.Forms.TextBox();
			this.checkTallyProgram = new System.Windows.Forms.CheckBox();
			this.checkTallyPreview = new System.Windows.Forms.CheckBox();
			this.listTallies = new System.Windows.Forms.ListBox();
			this.tabControl1.SuspendLayout();
			this.tabSetup.SuspendLayout();
			this.tabTallys.SuspendLayout();
			this.groupRemoteDisplay.SuspendLayout();
			this.layoutRemoteDisplays.SuspendLayout();
			this.tabTallies.SuspendLayout();
			this.groupTallies.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabSetup);
			this.tabControl1.Controls.Add(this.tabTallys);
			this.tabControl1.Controls.Add(this.tabTallies);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(776, 426);
			this.tabControl1.TabIndex = 0;
			// 
			// tabSetup
			// 
			this.tabSetup.BackColor = System.Drawing.SystemColors.Control;
			this.tabSetup.Controls.Add(this.treeTallyLights);
			this.tabSetup.Controls.Add(this.treeSetup);
			this.tabSetup.Location = new System.Drawing.Point(4, 24);
			this.tabSetup.Name = "tabSetup";
			this.tabSetup.Padding = new System.Windows.Forms.Padding(3);
			this.tabSetup.Size = new System.Drawing.Size(768, 398);
			this.tabSetup.TabIndex = 0;
			this.tabSetup.Text = "Setup";
			// 
			// treeTallyLights
			// 
			this.treeTallyLights.Dock = System.Windows.Forms.DockStyle.Right;
			this.treeTallyLights.FullRowSelect = true;
			this.treeTallyLights.Location = new System.Drawing.Point(557, 3);
			this.treeTallyLights.Name = "treeTallyLights";
			this.treeTallyLights.ShowLines = false;
			this.treeTallyLights.ShowPlusMinus = false;
			this.treeTallyLights.ShowRootLines = false;
			this.treeTallyLights.Size = new System.Drawing.Size(208, 392);
			this.treeTallyLights.TabIndex = 1;
			this.treeTallyLights.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeTallyLights_ItemDrag);
			// 
			// treeSetup
			// 
			this.treeSetup.AllowDrop = true;
			this.treeSetup.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeSetup.Location = new System.Drawing.Point(3, 3);
			this.treeSetup.Name = "treeSetup";
			this.treeSetup.Size = new System.Drawing.Size(337, 392);
			this.treeSetup.TabIndex = 0;
			this.treeSetup.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeSetup_DragDrop);
			this.treeSetup.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeSetup_DragEnter);
			this.treeSetup.DragOver += new System.Windows.Forms.DragEventHandler(this.treeSetup_DragOver);
			this.treeSetup.DragLeave += new System.EventHandler(this.treeSetup_DragLeave);
			// 
			// tabTallys
			// 
			this.tabTallys.BackColor = System.Drawing.SystemColors.Control;
			this.tabTallys.Controls.Add(this.buttonDeleteDisplay);
			this.tabTallys.Controls.Add(this.buttonAddDisplay);
			this.tabTallys.Controls.Add(this.buttonSaveDisplay);
			this.tabTallys.Controls.Add(this.groupRemoteDisplay);
			this.tabTallys.Controls.Add(this.listRemoteDisplays);
			this.tabTallys.Location = new System.Drawing.Point(4, 24);
			this.tabTallys.Name = "tabTallys";
			this.tabTallys.Padding = new System.Windows.Forms.Padding(3);
			this.tabTallys.Size = new System.Drawing.Size(768, 398);
			this.tabTallys.TabIndex = 1;
			this.tabTallys.Text = "Remote Displays";
			// 
			// buttonDeleteDisplay
			// 
			this.buttonDeleteDisplay.Location = new System.Drawing.Point(116, 4);
			this.buttonDeleteDisplay.Name = "buttonDeleteDisplay";
			this.buttonDeleteDisplay.Size = new System.Drawing.Size(112, 23);
			this.buttonDeleteDisplay.TabIndex = 4;
			this.buttonDeleteDisplay.Text = "Delete Display";
			this.buttonDeleteDisplay.UseVisualStyleBackColor = true;
			this.buttonDeleteDisplay.Click += new System.EventHandler(this.buttonDeleteDisplay_Click);
			// 
			// buttonAddDisplay
			// 
			this.buttonAddDisplay.Location = new System.Drawing.Point(3, 3);
			this.buttonAddDisplay.Name = "buttonAddDisplay";
			this.buttonAddDisplay.Size = new System.Drawing.Size(107, 23);
			this.buttonAddDisplay.TabIndex = 3;
			this.buttonAddDisplay.Text = "Add Display";
			this.buttonAddDisplay.UseVisualStyleBackColor = true;
			this.buttonAddDisplay.Click += new System.EventHandler(this.buttonAddDisplay_Click);
			// 
			// buttonSaveDisplay
			// 
			this.buttonSaveDisplay.Location = new System.Drawing.Point(234, 372);
			this.buttonSaveDisplay.Name = "buttonSaveDisplay";
			this.buttonSaveDisplay.Size = new System.Drawing.Size(191, 23);
			this.buttonSaveDisplay.TabIndex = 2;
			this.buttonSaveDisplay.Text = "Save Display";
			this.buttonSaveDisplay.UseVisualStyleBackColor = true;
			this.buttonSaveDisplay.Click += new System.EventHandler(this.buttonSaveDisplay_Click);
			// 
			// groupRemoteDisplay
			// 
			this.groupRemoteDisplay.Controls.Add(this.layoutRemoteDisplays);
			this.groupRemoteDisplay.Location = new System.Drawing.Point(237, 8);
			this.groupRemoteDisplay.Name = "groupRemoteDisplay";
			this.groupRemoteDisplay.Size = new System.Drawing.Size(517, 358);
			this.groupRemoteDisplay.TabIndex = 1;
			this.groupRemoteDisplay.TabStop = false;
			this.groupRemoteDisplay.Text = "Selected Remote Display";
			// 
			// layoutRemoteDisplays
			// 
			this.layoutRemoteDisplays.ColumnCount = 2;
			this.layoutRemoteDisplays.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.73767F));
			this.layoutRemoteDisplays.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.26233F));
			this.layoutRemoteDisplays.Controls.Add(this.textDisplayName, 1, 0);
			this.layoutRemoteDisplays.Controls.Add(this.label1, 0, 0);
			this.layoutRemoteDisplays.Controls.Add(this.label2, 0, 1);
			this.layoutRemoteDisplays.Controls.Add(this.label3, 0, 2);
			this.layoutRemoteDisplays.Controls.Add(this.textDisplayDescription, 1, 1);
			this.layoutRemoteDisplays.Controls.Add(this.textDisplayIPAddress, 1, 2);
			this.layoutRemoteDisplays.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutRemoteDisplays.Location = new System.Drawing.Point(3, 19);
			this.layoutRemoteDisplays.Name = "layoutRemoteDisplays";
			this.layoutRemoteDisplays.RowCount = 4;
			this.layoutRemoteDisplays.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.layoutRemoteDisplays.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.layoutRemoteDisplays.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.layoutRemoteDisplays.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.layoutRemoteDisplays.Size = new System.Drawing.Size(511, 336);
			this.layoutRemoteDisplays.TabIndex = 0;
			// 
			// textDisplayName
			// 
			this.textDisplayName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textDisplayName.Location = new System.Drawing.Point(98, 3);
			this.textDisplayName.Name = "textDisplayName";
			this.textDisplayName.Size = new System.Drawing.Size(410, 23);
			this.textDisplayName.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Name:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Description:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 15);
			this.label3.TabIndex = 3;
			this.label3.Text = "IP Address:";
			// 
			// textDisplayDescription
			// 
			this.textDisplayDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textDisplayDescription.Location = new System.Drawing.Point(98, 33);
			this.textDisplayDescription.Name = "textDisplayDescription";
			this.textDisplayDescription.Size = new System.Drawing.Size(410, 23);
			this.textDisplayDescription.TabIndex = 4;
			// 
			// textDisplayIPAddress
			// 
			this.textDisplayIPAddress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textDisplayIPAddress.Location = new System.Drawing.Point(98, 63);
			this.textDisplayIPAddress.Name = "textDisplayIPAddress";
			this.textDisplayIPAddress.Size = new System.Drawing.Size(410, 23);
			this.textDisplayIPAddress.TabIndex = 5;
			// 
			// listRemoteDisplays
			// 
			this.listRemoteDisplays.FormattingEnabled = true;
			this.listRemoteDisplays.ItemHeight = 15;
			this.listRemoteDisplays.Location = new System.Drawing.Point(3, 33);
			this.listRemoteDisplays.Name = "listRemoteDisplays";
			this.listRemoteDisplays.Size = new System.Drawing.Size(225, 364);
			this.listRemoteDisplays.TabIndex = 0;
			this.listRemoteDisplays.SelectedValueChanged += new System.EventHandler(this.listRemoteDisplays_SelectedValueChanged);
			// 
			// tabTallies
			// 
			this.tabTallies.BackColor = System.Drawing.SystemColors.Control;
			this.tabTallies.Controls.Add(this.buttonDeleteTally);
			this.tabTallies.Controls.Add(this.buttonAddTally);
			this.tabTallies.Controls.Add(this.buttonSaveTally);
			this.tabTallies.Controls.Add(this.groupTallies);
			this.tabTallies.Controls.Add(this.listTallies);
			this.tabTallies.Location = new System.Drawing.Point(4, 24);
			this.tabTallies.Name = "tabTallies";
			this.tabTallies.Padding = new System.Windows.Forms.Padding(3);
			this.tabTallies.Size = new System.Drawing.Size(768, 398);
			this.tabTallies.TabIndex = 2;
			this.tabTallies.Text = "Tally Lights";
			// 
			// buttonDeleteTally
			// 
			this.buttonDeleteTally.Location = new System.Drawing.Point(116, 4);
			this.buttonDeleteTally.Name = "buttonDeleteTally";
			this.buttonDeleteTally.Size = new System.Drawing.Size(112, 23);
			this.buttonDeleteTally.TabIndex = 9;
			this.buttonDeleteTally.Text = "Delete Tally";
			this.buttonDeleteTally.UseVisualStyleBackColor = true;
			this.buttonDeleteTally.Click += new System.EventHandler(this.buttonDeleteTally_Click);
			// 
			// buttonAddTally
			// 
			this.buttonAddTally.Location = new System.Drawing.Point(3, 3);
			this.buttonAddTally.Name = "buttonAddTally";
			this.buttonAddTally.Size = new System.Drawing.Size(107, 23);
			this.buttonAddTally.TabIndex = 8;
			this.buttonAddTally.Text = "Add Tally";
			this.buttonAddTally.UseVisualStyleBackColor = true;
			this.buttonAddTally.Click += new System.EventHandler(this.buttonAddTally_Click);
			// 
			// buttonSaveTally
			// 
			this.buttonSaveTally.Location = new System.Drawing.Point(234, 372);
			this.buttonSaveTally.Name = "buttonSaveTally";
			this.buttonSaveTally.Size = new System.Drawing.Size(191, 23);
			this.buttonSaveTally.TabIndex = 7;
			this.buttonSaveTally.Text = "Save Tally";
			this.buttonSaveTally.UseVisualStyleBackColor = true;
			this.buttonSaveTally.Click += new System.EventHandler(this.buttonSaveTally_Click);
			// 
			// groupTallies
			// 
			this.groupTallies.Controls.Add(this.tableLayoutPanel1);
			this.groupTallies.Location = new System.Drawing.Point(237, 8);
			this.groupTallies.Name = "groupTallies";
			this.groupTallies.Size = new System.Drawing.Size(517, 358);
			this.groupTallies.TabIndex = 6;
			this.groupTallies.TabStop = false;
			this.groupTallies.Text = "Selected Tally";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.73767F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.26233F));
			this.tableLayoutPanel1.Controls.Add(this.textTallyName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.textTallyDescription, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.textTallyID, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.checkTallyProgram, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.checkTallyPreview, 1, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(511, 336);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// textTallyName
			// 
			this.textTallyName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textTallyName.Location = new System.Drawing.Point(98, 3);
			this.textTallyName.Name = "textTallyName";
			this.textTallyName.Size = new System.Drawing.Size(410, 23);
			this.textTallyName.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(42, 15);
			this.label4.TabIndex = 1;
			this.label4.Text = "Name:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 30);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 15);
			this.label5.TabIndex = 2;
			this.label5.Text = "Description:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 60);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(47, 15);
			this.label6.TabIndex = 3;
			this.label6.Text = "Tally ID:";
			// 
			// textTallyDescription
			// 
			this.textTallyDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textTallyDescription.Location = new System.Drawing.Point(98, 33);
			this.textTallyDescription.Name = "textTallyDescription";
			this.textTallyDescription.Size = new System.Drawing.Size(410, 23);
			this.textTallyDescription.TabIndex = 4;
			// 
			// textTallyID
			// 
			this.textTallyID.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textTallyID.Location = new System.Drawing.Point(98, 63);
			this.textTallyID.Name = "textTallyID";
			this.textTallyID.Size = new System.Drawing.Size(410, 23);
			this.textTallyID.TabIndex = 5;
			// 
			// checkTallyProgram
			// 
			this.checkTallyProgram.AutoSize = true;
			this.checkTallyProgram.Location = new System.Drawing.Point(98, 93);
			this.checkTallyProgram.Name = "checkTallyProgram";
			this.checkTallyProgram.Size = new System.Drawing.Size(189, 19);
			this.checkTallyProgram.TabIndex = 8;
			this.checkTallyProgram.Text = "Has Program (Light when Live)";
			this.checkTallyProgram.UseVisualStyleBackColor = true;
			// 
			// checkTallyPreview
			// 
			this.checkTallyPreview.AutoSize = true;
			this.checkTallyPreview.Location = new System.Drawing.Point(98, 123);
			this.checkTallyPreview.Name = "checkTallyPreview";
			this.checkTallyPreview.Size = new System.Drawing.Size(195, 19);
			this.checkTallyPreview.TabIndex = 9;
			this.checkTallyPreview.Text = "Has Preview (Light when Ready)";
			this.checkTallyPreview.UseVisualStyleBackColor = true;
			// 
			// listTallies
			// 
			this.listTallies.FormattingEnabled = true;
			this.listTallies.ItemHeight = 15;
			this.listTallies.Location = new System.Drawing.Point(3, 33);
			this.listTallies.Name = "listTallies";
			this.listTallies.Size = new System.Drawing.Size(225, 364);
			this.listTallies.TabIndex = 5;
			this.listTallies.SelectedValueChanged += new System.EventHandler(this.listTallies_SelectedValueChanged);
			// 
			// frmTallyManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmTallyManager";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tally Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTallyManager_FormClosing);
			this.tabControl1.ResumeLayout(false);
			this.tabSetup.ResumeLayout(false);
			this.tabTallys.ResumeLayout(false);
			this.groupRemoteDisplay.ResumeLayout(false);
			this.layoutRemoteDisplays.ResumeLayout(false);
			this.layoutRemoteDisplays.PerformLayout();
			this.tabTallies.ResumeLayout(false);
			this.groupTallies.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabSetup;
		private System.Windows.Forms.TabPage tabTallys;
		private System.Windows.Forms.Button buttonDeleteDisplay;
		private System.Windows.Forms.Button buttonAddDisplay;
		private System.Windows.Forms.Button buttonSaveDisplay;
		private System.Windows.Forms.GroupBox groupRemoteDisplay;
		private System.Windows.Forms.TableLayoutPanel layoutRemoteDisplays;
		private System.Windows.Forms.TextBox textDisplayName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textDisplayDescription;
		private System.Windows.Forms.TextBox textDisplayIPAddress;
		private System.Windows.Forms.ListBox listRemoteDisplays;
		private System.Windows.Forms.TabPage tabTallies;
		private System.Windows.Forms.Button buttonDeleteTally;
		private System.Windows.Forms.Button buttonAddTally;
		private System.Windows.Forms.Button buttonSaveTally;
		private System.Windows.Forms.GroupBox groupTallies;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox textTallyName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textTallyDescription;
		private System.Windows.Forms.TextBox textTallyID;
		private System.Windows.Forms.CheckBox checkTallyProgram;
		private System.Windows.Forms.CheckBox checkTallyPreview;
		private System.Windows.Forms.ListBox listTallies;
		private System.Windows.Forms.TreeView treeSetup;
		private System.Windows.Forms.TreeView treeTallyLights;
	}
}