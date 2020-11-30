using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StreamDeckManager.Actions;
using StreamDeckManager.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static StreamDeckManager.UtilityFunctions;

namespace StreamDeckManager
{
	public partial class frmConfigureButton : Form
	{
		private readonly int _keyIndex;
		private readonly IOptionsMonitor<StreamDeckSettings> _streamDeckSettings;
		private readonly ActionFactory _actionFactory;
		private readonly IHostEnvironment _hostEnvironment;

		private readonly List<ActionSettings> actions;
		public frmConfigureButton(
			int keyIndex,
			IOptionsMonitor<StreamDeckSettings> streamDeckSettings,
			ActionFactory actionFactory,
			IHostEnvironment hostEnvironment
		)
		{
			(_keyIndex, _streamDeckSettings, _actionFactory, _hostEnvironment) = (keyIndex, streamDeckSettings, actionFactory, hostEnvironment);
			InitializeComponent();

			comboActionType.Items.AddRange(actionFactory.AvailableActions().Cast<object>().ToArray());
			groupConfigureAction.Enabled = false;

			var keySettings = streamDeckSettings.CurrentValue.Keys.Find(f => f.KeyNumber == keyIndex);
			if (keySettings is object)
			{
				buttonDemo.Text = keySettings.Text;
				buttonDemo.BackColor = ColorFromHex(keySettings.Color);
				buttonDemo.ForeColor = TextColorFromBackground(buttonDemo.BackColor);

				textButtonText.Text = keySettings.Text;
				textButtonColor.Text = keySettings.Color;

				actions = keySettings.Actions;
			}
			else
			{
				actions = new List<ActionSettings>();
			}

			listActions.DisplayMember = "Name";
			listActions.DataSource = actions;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			JObject fileRoot;

			// Load the json file
			using (var readStream = File.OpenText(Path.Combine(_hostEnvironment.ContentRootPath, "appsettings.json"))) {
				using JsonTextReader reader = new JsonTextReader(readStream);
				fileRoot = (JObject)JToken.ReadFrom(reader);
			}

			// Find the key to write to
			JArray keysArray = (JArray)fileRoot["StreamDeck"]!["Keys"]!;
			JObject? keyToWrite = keysArray.FirstOrDefault(element => element.Value<int>("KeyNumber") == _keyIndex) as JObject;

			if (keyToWrite is object)
			{
				keyToWrite["Text"] = textButtonText.Text;
				keyToWrite["Color"] = textButtonColor.Text;

				JArray actionsArray = new JArray();
				foreach (ActionSettings action in listActions.Items)
				{
					actionsArray.Add(new JObject
					{
						{"Name", action!.Name},
						{"Props", JObject.FromObject(action.Props) }
					});
				}
				keyToWrite["Actions"] = actionsArray;
			}

			using (var writeStream = File.CreateText(Path.Combine(_hostEnvironment.ContentRootPath, "appsettings.json")))
			{
				JsonSerializer serializer = new JsonSerializer
				{
					Formatting = Formatting.Indented,
				};

				//serialize object directly into file stream
				serializer.Serialize(writeStream, fileRoot);
			}

			UseWaitCursor = false;
			Close();
		}

		private void textButtonText_TextChanged(object sender, EventArgs e)
		{
			buttonDemo.Text = textButtonText.Text;
		}

		private void textButtonColor_TextChanged(object sender, EventArgs e)
		{
			try
			{
				buttonDemo.BackColor = ColorFromHex(textButtonColor.Text);
				buttonDemo.ForeColor = TextColorFromBackground(buttonDemo.BackColor);
			}
			catch { }
		}

		private void listActions_SelectedValueChanged(object sender, EventArgs e)
		{
			var selectedAction = (ActionSettings?)listActions.SelectedItem;

			if (selectedAction is null)
			{
				groupConfigureAction.Enabled = false;
				buttonDeleteAction.Enabled = false;
				return;
			}
			else
			{
				groupConfigureAction.Enabled = true;
				buttonDeleteAction.Enabled = true;
			}

			comboActionType.SelectedItem = selectedAction.Name;

			var t = _actionFactory.PropertiesForActionType(selectedAction.Name);

			layoutProperties.SuspendLayout();

			layoutProperties.ColumnCount = 2;
			layoutProperties.RowCount = t.Length + 1;

			layoutProperties.Controls.Clear();
			layoutProperties.ColumnStyles.Clear();
			layoutProperties.RowStyles.Clear();

			layoutProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36));
			layoutProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 - 36));

			for (int i = 0; i < t.Length; i++)
			{
				layoutProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29));
			}
			layoutProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100));

			for (int i = 0; i < t.Length; i++)
			{
				var label = new Label();
				label.Text = t[i].DisplayText;
				label.TextAlign = ContentAlignment.MiddleRight;
				label.Dock = DockStyle.Fill;

				var textbox = new TextBox();
				textbox.Dock = DockStyle.Fill;
				int itemIndex = i;
				textbox.Tag = new Action<string>((value) => {
					selectedAction.Props[t[itemIndex].Property] = value;
				});
				textbox.Text = selectedAction.Props.FirstOrDefault(f => f.Key == t[i].Property).Value ?? string.Empty;
				textbox.TextChanged += Textbox_TextChanged;

				layoutProperties.Controls.Add(label, 0, i);
				layoutProperties.Controls.Add(textbox, 1, i);
			}

			layoutProperties.ResumeLayout();
		}

		private void Textbox_TextChanged(object? sender, EventArgs e)
		{
			var textbox = (TextBox)sender!;
			((Action<string>)textbox.Tag).Invoke(textbox.Text);
		}

		private void buttonAddAction_Click(object sender, EventArgs e)
		{
			string firstAction = _actionFactory.AvailableActions().First();
			var properties = _actionFactory.PropertiesForActionType(firstAction);

			var newItem = new ActionSettings
			{
				Name = firstAction,
				Props = properties.ToDictionary(v => v.Property, v => v.DefaultValue.ToString() ?? string.Empty)
			};
			actions.Add(newItem);

			listActions.DataSource = null;
			listActions.DataSource = actions;
			listActions.SelectedItem = newItem;
			listActions.DisplayMember = "Name";
		}

		private void buttonDeleteAction_Click(object sender, EventArgs e)
		{
			actions.Remove((ActionSettings)listActions.SelectedItem);

			listActions.DataSource = null;
			listActions.DataSource = actions;
			listActions.DisplayMember = "Name";
		}

		private void comboActionType_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedAction = listActions.SelectedItem as ActionSettings;
			if (selectedAction is null) return;
			if (selectedAction.Name == (string)comboActionType.SelectedItem) return;

			selectedAction.Name = (string)comboActionType.SelectedItem;

			listActions.DataSource = null;
			listActions.DisplayMember = "Name";
			listActions.DataSource = actions;

			listActions_SelectedValueChanged(null, EventArgs.Empty);
		}
	}
}
