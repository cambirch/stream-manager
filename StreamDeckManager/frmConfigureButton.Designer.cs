namespace StreamDeckManager
{
	partial class frmConfigureButton
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
			System.Windows.Forms.Label labelText;
			System.Windows.Forms.Label labelColor;
			this.buttonDemo = new System.Windows.Forms.Button();
			this.textButtonText = new System.Windows.Forms.TextBox();
			this.textButtonColor = new System.Windows.Forms.TextBox();
			this.listActions = new System.Windows.Forms.ListBox();
			this.groupConfigureAction = new System.Windows.Forms.GroupBox();
			this.layoutProperties = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.comboActionType = new System.Windows.Forms.ComboBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonAddAction = new System.Windows.Forms.Button();
			this.buttonDeleteAction = new System.Windows.Forms.Button();
			labelText = new System.Windows.Forms.Label();
			labelColor = new System.Windows.Forms.Label();
			this.groupConfigureAction.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelText
			// 
			labelText.AutoSize = true;
			labelText.Location = new System.Drawing.Point(12, 115);
			labelText.Name = "labelText";
			labelText.Size = new System.Drawing.Size(31, 15);
			labelText.TabIndex = 1;
			labelText.Text = "Text:";
			// 
			// labelColor
			// 
			labelColor.AutoSize = true;
			labelColor.Location = new System.Drawing.Point(12, 159);
			labelColor.Name = "labelColor";
			labelColor.Size = new System.Drawing.Size(39, 15);
			labelColor.TabIndex = 3;
			labelColor.Text = "Color:";
			// 
			// buttonDemo
			// 
			this.buttonDemo.Location = new System.Drawing.Point(12, 12);
			this.buttonDemo.Name = "buttonDemo";
			this.buttonDemo.Size = new System.Drawing.Size(100, 100);
			this.buttonDemo.TabIndex = 0;
			this.buttonDemo.Text = "Button Text";
			this.buttonDemo.UseVisualStyleBackColor = true;
			// 
			// textButtonText
			// 
			this.textButtonText.Location = new System.Drawing.Point(12, 133);
			this.textButtonText.Name = "textButtonText";
			this.textButtonText.Size = new System.Drawing.Size(100, 23);
			this.textButtonText.TabIndex = 2;
			this.textButtonText.TextChanged += new System.EventHandler(this.textButtonText_TextChanged);
			// 
			// textButtonColor
			// 
			this.textButtonColor.Location = new System.Drawing.Point(12, 177);
			this.textButtonColor.Name = "textButtonColor";
			this.textButtonColor.Size = new System.Drawing.Size(100, 23);
			this.textButtonColor.TabIndex = 4;
			this.textButtonColor.TextChanged += new System.EventHandler(this.textButtonColor_TextChanged);
			// 
			// listActions
			// 
			this.listActions.FormattingEnabled = true;
			this.listActions.ItemHeight = 15;
			this.listActions.Location = new System.Drawing.Point(133, 42);
			this.listActions.Name = "listActions";
			this.listActions.Size = new System.Drawing.Size(164, 169);
			this.listActions.TabIndex = 5;
			this.listActions.SelectedValueChanged += new System.EventHandler(this.listActions_SelectedValueChanged);
			// 
			// groupConfigureAction
			// 
			this.groupConfigureAction.Controls.Add(this.layoutProperties);
			this.groupConfigureAction.Controls.Add(this.flowLayoutPanel1);
			this.groupConfigureAction.Location = new System.Drawing.Point(303, 12);
			this.groupConfigureAction.Name = "groupConfigureAction";
			this.groupConfigureAction.Size = new System.Drawing.Size(290, 227);
			this.groupConfigureAction.TabIndex = 6;
			this.groupConfigureAction.TabStop = false;
			this.groupConfigureAction.Text = "Configure Action";
			// 
			// layoutProperties
			// 
			this.layoutProperties.ColumnCount = 2;
			this.layoutProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.54618F));
			this.layoutProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.45382F));
			this.layoutProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutProperties.Location = new System.Drawing.Point(3, 49);
			this.layoutProperties.Name = "layoutProperties";
			this.layoutProperties.RowCount = 2;
			this.layoutProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutProperties.Size = new System.Drawing.Size(284, 175);
			this.layoutProperties.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.label1);
			this.flowLayoutPanel1.Controls.Add(this.comboActionType);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 19);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(284, 30);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 29);
			this.label1.TabIndex = 0;
			this.label1.Text = "Action Type:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboActionType
			// 
			this.comboActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboActionType.FormattingEnabled = true;
			this.comboActionType.Location = new System.Drawing.Point(81, 3);
			this.comboActionType.Name = "comboActionType";
			this.comboActionType.Size = new System.Drawing.Size(200, 23);
			this.comboActionType.TabIndex = 1;
			this.comboActionType.SelectedIndexChanged += new System.EventHandler(this.comboActionType_SelectedIndexChanged);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(12, 217);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(285, 23);
			this.buttonSave.TabIndex = 7;
			this.buttonSave.Text = "Save Changes";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonAddAction
			// 
			this.buttonAddAction.Location = new System.Drawing.Point(133, 12);
			this.buttonAddAction.Name = "buttonAddAction";
			this.buttonAddAction.Size = new System.Drawing.Size(75, 23);
			this.buttonAddAction.TabIndex = 8;
			this.buttonAddAction.Text = "Add Action";
			this.buttonAddAction.UseVisualStyleBackColor = true;
			this.buttonAddAction.Click += new System.EventHandler(this.buttonAddAction_Click);
			// 
			// buttonDeleteAction
			// 
			this.buttonDeleteAction.Location = new System.Drawing.Point(222, 12);
			this.buttonDeleteAction.Name = "buttonDeleteAction";
			this.buttonDeleteAction.Size = new System.Drawing.Size(75, 23);
			this.buttonDeleteAction.TabIndex = 9;
			this.buttonDeleteAction.Text = "Delete";
			this.buttonDeleteAction.UseVisualStyleBackColor = true;
			this.buttonDeleteAction.Click += new System.EventHandler(this.buttonDeleteAction_Click);
			// 
			// frmConfigureButton
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(605, 251);
			this.Controls.Add(this.buttonDeleteAction);
			this.Controls.Add(this.buttonAddAction);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.groupConfigureAction);
			this.Controls.Add(this.listActions);
			this.Controls.Add(this.textButtonColor);
			this.Controls.Add(labelColor);
			this.Controls.Add(this.textButtonText);
			this.Controls.Add(labelText);
			this.Controls.Add(this.buttonDemo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmConfigureButton";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Configure Button";
			this.groupConfigureAction.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonDemo;
		private System.Windows.Forms.Label labelText;
		private System.Windows.Forms.TextBox textButtonText;
		private System.Windows.Forms.Label labelColor;
		private System.Windows.Forms.TextBox textButtonColor;
		private System.Windows.Forms.ListBox listActions;
		private System.Windows.Forms.GroupBox groupConfigureAction;
		private System.Windows.Forms.TableLayoutPanel layoutProperties;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonAddAction;
		private System.Windows.Forms.Button buttonDeleteAction;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboActionType;
	}
}