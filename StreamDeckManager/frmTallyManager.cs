using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StreamDeckManager.Managers;
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

namespace StreamDeckManager
{
	public partial class frmTallyManager : Form
	{
		private readonly RemoteTallySettings _remoteTallySettings;
		private readonly IHostEnvironment _hostEnvironment;
		private readonly ObsManager _obsManager;

		private bool hasChanges = false;

		public frmTallyManager(
			IOptionsMonitor<RemoteTallySettings> remoteTallySettings,
			IHostEnvironment hostEnvironment,
			ObsManager obsManager
			)
		{
			(_remoteTallySettings, _hostEnvironment, _obsManager) = (remoteTallySettings.CurrentValue, hostEnvironment, obsManager);
			InitializeComponent();

			listRemoteDisplays.DisplayMember = "Name";
			listRemoteDisplays.DataSource = _remoteTallySettings.Displays;

			listTallies.DisplayMember = "Name";
			listTallies.DataSource = _remoteTallySettings.Tallies;

			UpdateSources();
			UpdateTallyLights();
		}

		private void UpdateTallyLights()
		{
			treeTallyLights.Nodes.Clear();
			foreach (var tally in _remoteTallySettings.Tallies)
			{
				treeTallyLights.Nodes.Add(new TreeNode
				{
					Name = tally.ID,
					Text = $"{tally.Name}  - [{tally.ID}]",
				});
			}
		}
		private void UpdateSources()
		{
			treeSetup.Nodes.Clear();

			var sources = _obsManager.SourcesList;
			foreach (var source in sources)
			{
				var node = new TreeNode
				{
					Name = source.Name,
					Text = $"{source.Name} [{source.Type}]",
				};

				var sourceTally = _remoteTallySettings.SourceTallies.Find(f => f.Name == source.Name);
				if (sourceTally is object)
				{
					foreach (var tallyID in sourceTally.TallyIDs)
					{
						var tallySource = _remoteTallySettings.Tallies.Find(f => f.ID == tallyID);
						if (tallySource is null) continue;

						var childNode = new TreeNode
						{
							Text = $"{tallySource.Name}  - [{tallySource.ID}]",
						};
						node.Nodes.Add(childNode);
					}
					node.Expand();
				}

				treeSetup.Nodes.Add(node);
			}
		}

		#region Remote Displays

		private void buttonAddDisplay_Click(object sender, EventArgs e)
		{
			var newTallyDisplay = new RemoteDisplay
			{
				Name = "New Tally Display",
				Description = string.Empty,
				IPAddress = "192.168.x.x",
			};

			_remoteTallySettings.Displays.Add(newTallyDisplay);

			listRemoteDisplays.DataSource = null;
			listRemoteDisplays.DisplayMember = "Name";
			listRemoteDisplays.DataSource = _remoteTallySettings.Displays;
			listRemoteDisplays.SelectedItem = newTallyDisplay;

			hasChanges = true;
		}

		private void buttonDeleteDisplay_Click(object sender, EventArgs e)
		{
			if (listRemoteDisplays.SelectedItem is null) return;

			_remoteTallySettings.Displays.Add((RemoteDisplay)listRemoteDisplays.SelectedItem);
			listRemoteDisplays.DataSource = null;
			listRemoteDisplays.DisplayMember = "Name";
			listRemoteDisplays.DataSource = _remoteTallySettings.Displays;

			hasChanges = true;
		}

		private void buttonSaveDisplay_Click(object sender, EventArgs e)
		{
			var selectedDisplay = (RemoteDisplay?)listRemoteDisplays.SelectedItem;
			if (selectedDisplay is null) return;

			selectedDisplay.Name = textDisplayName.Text;
			selectedDisplay.Description = textDisplayDescription.Text;
			selectedDisplay.IPAddress = textDisplayIPAddress.Text;

			listRemoteDisplays.DataSource = null;
			listRemoteDisplays.DisplayMember = "Name";
			listRemoteDisplays.DataSource = _remoteTallySettings.Displays;
			listRemoteDisplays.SelectedItem = selectedDisplay;

			hasChanges = true;
		}

		private void listRemoteDisplays_SelectedValueChanged(object sender, EventArgs e)
		{
			var selectedTally = (RemoteDisplay?)listRemoteDisplays.SelectedItem;

			if (selectedTally is null)
			{
				buttonDeleteDisplay.Enabled = buttonSaveDisplay.Enabled = groupRemoteDisplay.Enabled = false;
				return;
			}
			else
			{
				buttonDeleteDisplay.Enabled = buttonSaveDisplay.Enabled = groupRemoteDisplay.Enabled = true;
			}

			textDisplayName.Text = selectedTally.Name;
			textDisplayDescription.Text = selectedTally.Description;
			textDisplayIPAddress.Text = selectedTally.IPAddress;
		}

		#endregion Remote Displays

		#region Tallies

		private void buttonAddTally_Click(object sender, EventArgs e)
		{
			var newTally = new TallyLight
			{
				Name = "New Tally Light",
				Description = string.Empty,
				ID = "new",
				HasProgram = false,
				HasPreview = false,
			};

			_remoteTallySettings.Tallies.Add(newTally);

			listTallies.DataSource = null;
			listTallies.DisplayMember = "Name";
			listTallies.DataSource = _remoteTallySettings.Tallies;
			listTallies.SelectedItem = newTally;
			UpdateTallyLights();

			hasChanges = true;
		}

		private void buttonDeleteTally_Click(object sender, EventArgs e)
		{
			if (listTallies.SelectedItem is null) return;

			_remoteTallySettings.Tallies.Add((TallyLight)listTallies.SelectedItem);
			listTallies.DataSource = null;
			listTallies.DisplayMember = "Name";
			listTallies.DataSource = _remoteTallySettings.Tallies;
			UpdateTallyLights();

			hasChanges = true;
		}

		private void buttonSaveTally_Click(object sender, EventArgs e)
		{
			var selectedLight = (TallyLight?)listTallies.SelectedItem;
			if (selectedLight is null) return;

			selectedLight.Name = textTallyName.Text;
			selectedLight.Description = textTallyDescription.Text;
			selectedLight.ID = textTallyID.Text;
			selectedLight.HasProgram = checkTallyProgram.Checked;
			selectedLight.HasPreview = checkTallyPreview.Checked;

			listTallies.DataSource = null;
			listTallies.DisplayMember = "Name";
			listTallies.DataSource = _remoteTallySettings.Tallies;
			listTallies.SelectedItem = selectedLight;
			UpdateTallyLights();

			hasChanges = true;
		}

		private void listTallies_SelectedValueChanged(object sender, EventArgs e)
		{
			var selectedLight = (TallyLight?)listTallies.SelectedItem;

			if (selectedLight is null)
			{
				buttonAddTally.Enabled = buttonDeleteTally.Enabled = groupTallies.Enabled = false;
				return;
			}
			else
			{
				buttonAddTally.Enabled = buttonDeleteTally.Enabled = groupTallies.Enabled = true;
			}

			textTallyName.Text = selectedLight.Name;
			textTallyDescription.Text = selectedLight.Description;
			textTallyID.Text = selectedLight.ID;
			checkTallyProgram.Checked = selectedLight.HasProgram;
			checkTallyPreview.Checked = selectedLight.HasPreview;
		}

		#endregion Tallies

		private void frmTallyManager_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!hasChanges) return;
			UseWaitCursor = true;
			JObject fileRoot;

			// Load the json file
			using (var readStream = File.OpenText(Path.Combine(_hostEnvironment.ContentRootPath, "appsettings.json")))
			{
				using JsonTextReader reader = new JsonTextReader(readStream);
				fileRoot = (JObject)JToken.ReadFrom(reader);
			}

			// Find the key to write to
			JObject remoteTalliesObject = (JObject)fileRoot["RemoteTallies"]!;
			remoteTalliesObject.Replace(JObject.FromObject(_remoteTallySettings));

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
		}

		private void treeTallyLights_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;

			DoDragDrop(e.Item, DragDropEffects.Copy);
		}

		private void treeSetup_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.AllowedEffect;
		}

		private void treeSetup_DragOver(object sender, DragEventArgs e)
		{
			treeSetup.BeginUpdate();

			// Retrieve the client coordinates of the mouse position.
			Point targetPoint = treeSetup.PointToClient(new Point(e.X, e.Y));
			CleanTallySetupNodes();

			// Select the node at the mouse position.
			var node = treeSetup.GetNodeAt(targetPoint);
			if (node is object)
			{
				if (node.Parent is null)
				{
					node.BackColor = Color.Red;
					e.Effect = e.AllowedEffect;
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}

			treeSetup.EndUpdate();
		}
		private void CleanTallySetupNodes()
		{
			foreach (TreeNode nodeToClear in treeSetup.Nodes)
			{
				if (nodeToClear.BackColor != Color.Transparent)
				{
					nodeToClear.BackColor = Color.Transparent;
				}
			}
		}

		private void treeSetup_DragDrop(object sender, DragEventArgs e)
		{
			CleanTallySetupNodes();

			// Retrieve the client coordinates of the drop location.
			Point targetPoint = treeSetup.PointToClient(new Point(e.X, e.Y));

			// Retrieve the node at the drop location.
			TreeNode targetNode = treeSetup.GetNodeAt(targetPoint);
			if (targetNode is null) return;

			// Retrieve the node that was dragged.
			TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

			var s = _remoteTallySettings.SourceTallies.Find(f => f.Name == targetNode.Name);
			if (s is null)
			{
				s = new Source
				{
					Name = targetNode.Name,
					TallyIDs = new[] { draggedNode.Name },
				};
				_remoteTallySettings.SourceTallies.Add(s);
				hasChanges = true;
			}
			else
			{
				if (!s.TallyIDs.Contains(draggedNode.Name)) {
					s.TallyIDs = s.TallyIDs.Union(new[] { draggedNode.Name }).ToArray();
					hasChanges = true;
				}
			}

			UpdateSources();
		}

		private void treeSetup_DragLeave(object sender, EventArgs e)
		{
			treeSetup.BeginUpdate();
			CleanTallySetupNodes();
			treeSetup.EndUpdate();
		}
	}
}
