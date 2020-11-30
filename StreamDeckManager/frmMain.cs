using OBSWebsocketDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StreamDeckSharp;
using OpenMacroBoard.SDK;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using StreamDeckManager.Settings;
using Microsoft.Extensions.Configuration;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Bmp;
using StreamDeckManager.Managers;
using Microsoft.Extensions.DependencyInjection;
using StreamDeckManager.Actions;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Net;
using System.Net.Sockets;
using Websocket.Client;
using System.Windows.Interop;

namespace StreamDeckManager
{
	partial class frmMain : Form
	{
		protected readonly ObsManager _obsManager;
		protected readonly ButtonsManager _buttonsManager;
		protected readonly IServiceProvider _serviceProvider;
		private readonly IOptionsMonitor<RemoteTallySettings> _remoteTallySettings;
		private readonly TallyManager _tallyManager;

		public frmMain(
			ObsManager obsManager,
			ButtonsManager buttonsManager,
			IOptionsMonitor<StreamDeckSettings> streamDeckSettings,
			IOptionsMonitor<RemoteTallySettings> remoteTallySettings,
			TallyManager tallyManager,
			IServiceProvider serviceProvider
		)
		{
			(_obsManager, _buttonsManager, _serviceProvider, _remoteTallySettings, _tallyManager)
				= (obsManager, buttonsManager, serviceProvider, remoteTallySettings, tallyManager);
			InitializeComponent();

			_obsManager.OnConnectionStateChanged += _obsManager_OnConnectionStateChanged;
			_obsManager.OnEvent += _obsManager_OnEvent;
			_buttonsManager.OnConnectionStateChanged += _buttonsManager_OnConnectionStateChanged;

			_tallyManager.OnTallyChange += _tallyManager_OnTallyChange;
			_tallyManager.OnRemoteConnectionChanged += _tallyManager_OnRemoteConnectionChanged;

			// Display the buttons
			SetupButtons();
			streamDeckSettings.OnChange(async _ =>
			{
				await Task.Delay(50);
				// Since this is UI work, ensure it actually happens on the UI thread
				this.Invoke(new Action(SetupButtons));
			});

			//_obs = new OBSWebsocket();
			//_obs.Connected += onConnect;
			//_obs.Disconnected += onDisconnect;

			//_obs.SceneChanged += onSceneChange;
			//_obs.SceneCollectionChanged += onSceneColChange;
			//_obs.ProfileChanged += onProfileChange;
			//_obs.TransitionChanged += onTransitionChange;
			//_obs.TransitionDurationChanged += onTransitionDurationChange;

			//_obs.StreamingStateChanged += onStreamingStateChange;
			//_obs.RecordingStateChanged += onRecordingStateChange;

			//_obs.StreamStatus += onStreamData;

			//Open the Stream Deck device
			//_deck = StreamDeck.OpenDevice();
			//_deck.KeyStateChanged += _deck_KeyStateChanged;

			// Setup correct initial UI State
			Task.Run(async () =>
			{
				await Task.Delay(300);

				_obsManager_OnConnectionStateChanged(null, EventArgs.Empty);
				_buttonsManager_OnConnectionStateChanged(null, EventArgs.Empty);
			});
		}

		private void _tallyManager_OnRemoteConnectionChanged(object? sender, TallyDisplayEventArgs e)
		{
			foreach (TreeNode? node in treeRemotes.Nodes)
			{
				if (node is null) continue;

				if (node.Name == e.Display.Name)
				{
					node.BackColor = e.IsConnected
						? System.Drawing.Color.Green
						: System.Drawing.Color.Red;
				}
			}
		}

		private void _tallyManager_OnTallyChange(object? sender, IReadOnlyDictionary<string, int> e)
		{
			foreach (TreeNode? node in treeTallies.Nodes)
			{
				if (node is null) continue;

				e.TryGetValue($"{node.Name}-program", out var programValue);
				e.TryGetValue($"{node.Name}-preview", out var previewValue);

				if (programValue != 0)
				{
					node.BackColor = System.Drawing.Color.Red;
				}
				else if (previewValue != 0)
				{
					node.BackColor = System.Drawing.Color.Green;
				}
				else
				{
					node.BackColor = System.Drawing.Color.Transparent;
				}
			}
		}

		private void _buttonsManager_OnConnectionStateChanged(object? sender, EventArgs e)
		{
			buttonConnectToDeck.Invoke(new Action(() =>
			{
				buttonConnectToDeck.Text = _buttonsManager.IsConnected
					? "Disconnect from Deck"
					: "Connect to Deck";
				buttonConnectToDeck.Enabled = true;
			}));
		}

		private void _obsManager_OnEvent(object? sender, EventArgs e) => _buttonsManager.UpdateUI();

		private void SetupButtons()
		{
			UseWaitCursor = true;
			layoutButtons.SuspendLayout();

			layoutButtons.ColumnCount = _buttonsManager.Width;
			layoutButtons.RowCount = _buttonsManager.Height;

			layoutButtons.Controls.Clear();
			layoutButtons.ColumnStyles.Clear();
			layoutButtons.RowStyles.Clear();

			for (int i = 0; i < _buttonsManager.Width; i++)
			{
				this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / _buttonsManager.Width));
			}

			for (int i = 0; i < _buttonsManager.Width; i++)
			{
				this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / _buttonsManager.Height));
			}

			for (int x = 0; x < _buttonsManager.Width; x++)
			{
				for (int y = 0; y < _buttonsManager.Height; y++)
				{
					var button = new System.Windows.Forms.Button();
					button.Dock = DockStyle.Fill;
					button.Text = $"{x} {y}";
					button.MouseUp += Button_MouseUp;
					button.Tag = y * _buttonsManager.Width + x;

					layoutButtons.Controls.Add(button, x, y);
					_buttonsManager.SetUIButton(button, x, y);
				}
			}

			treeTallies.Nodes.Clear();

			foreach (var tally in _remoteTallySettings.CurrentValue.Tallies)
			{
				var treeNode = new TreeNode
				{
					Name = tally.ID,
					Text = tally.Name
				};
				treeTallies.Nodes.Add(treeNode);
			}

			treeRemotes.Nodes.Clear();
			foreach (var display in _remoteTallySettings.CurrentValue.Displays)
			{
				var treeNode = new TreeNode
				{
					Name = display.Name,
					Text = $"{display.Name} [{display.Description}]"
				};
				treeRemotes.Nodes.Add(treeNode);
			}

			layoutButtons.ResumeLayout();
			UseWaitCursor = false;
		}

		private void Button_MouseUp(object sender, MouseEventArgs e)
		{
			var btn = (Button)sender!;
			if (e.Button != MouseButtons.Right) return;

			var form = new frmConfigureButton(
				(int)btn.Tag,
				_serviceProvider.GetRequiredService<IOptionsMonitor<StreamDeckSettings>>(),
				_serviceProvider.GetRequiredService<ActionFactory>(),
				_serviceProvider.GetRequiredService<IHostEnvironment>()
				);
			form.ShowDialog(this);
		}

		private void _obsManager_OnConnectionStateChanged(object? sender, EventArgs e)
		{
			buttonConnectToOBS.Invoke(new Action(() =>
			{
				buttonConnectToOBS.Text = _obsManager.IsConnected
					? "Disconnect from OBS"
					: "Connect to OBS";
				buttonConnectToOBS.Enabled = true;

				buttonTallyManager.Enabled = _obsManager.IsConnected;
			}));
		}

		//private void onConnect(object? sender, EventArgs e)
		//{
		//    var scenes = _obs.ListScenes();

		//    treeView1.Nodes.Clear();
		//    foreach (var scene in scenes)
		//    {
		//        var node = new TreeNode(scene.Name);
		//        foreach (var item in scene.Items)
		//        {
		//            node.Nodes.Add(item.SourceName);
		//        }

		//        treeView1.Nodes.Add(node);
		//    }
		//}

		private void frmMain_Shown(object sender, EventArgs e)
		{
			//_deck.SetBrightness(100);

			//foreach (var key in _streamDeckSettings.CurrentValue.Keys)
			//{
			//    var pixel = Color.ParseHex(key.Color);
			//    var keyPicture = CreateTextImage(key.Text, false, pixel);

			//    _deck.SetKeyBitmap(key.KeyNumber, keyPicture);
			//}

			////Send the bitmap informaton to the device
			//for (int i = 0; i < _deck.Keys.Count; i++)
			//    _deck.SetKeyBitmap(i, rowColors[i % 4]);

			//var bit = CreateTextImage("Fade .25s", false, Color.Purple);
			//_deck.SetKeyBitmap(0, bit);
		}

		private void _deck_KeyStateChanged(object? sender, OpenMacroBoard.SDK.KeyEventArgs e)
		{
			if (!e.IsDown) return;

			//var pressedKey = _streamDeckSettings.CurrentValue.Keys.FirstOrDefault(f => f.KeyNumber == e.Key);
			//if (pressedKey is null) return;

			//if (pressedKey.ObsAction is object)
			//{
			//    var props = JObject.Parse(pressedKey.ObsAction.Props);
			//    _obs.SendRequest(pressedKey.ObsAction.ActionName, props);
			//}

			//if (e.Key == 0 && e.IsDown)
			//{
			//    _obs.SendRequest("TransitionToProgram", JObject.FromObject(new
			//    {
			//        duration = 1000,
			//        name = "Fade"
			//    }));
			//}

			//if (e.Key == 8 && e.IsDown)
			//{
			//    var props = new JObject();
			//    props.Add("scene-name", "Camera 2");
			//    _obs.SendRequest("SetPreviewScene", props);
			//}
			//if (e.Key == 16 && e.IsDown)
			//{
			//    var props = new JObject();
			//    props.Add("scene-name", "Camera 3");
			//    _obs.SendRequest("SetPreviewScene", props);
			//}

			//if (e.Key == 1 && e.IsDown)
			//{
			//    var props = new JObject();
			//    props.Add("scene-name", "Overlay Elements");
			//    props.Add("item", "BLogo");
			//    props.Add("visible", "false");

			//    _obs.SendRequest("SetSceneItemProperties", props);
			//}
		}

		private KeyBitmap CreateTextImage(string text, bool selected, Color background)
		{
			using var memoryStream = new System.IO.MemoryStream();
			using (var img = new Image<Rgb24>(100, 100))
			{
				PathBuilder pathBuilder = new PathBuilder();
				pathBuilder.SetOrigin(new PointF(0, 0));
				pathBuilder.AddLine(0, 70, 100, 70);

				IPath path = pathBuilder.Build();

				// For production application we would recomend you create a FontCollection
				// singleton and manually install the ttf fonts yourself as using SystemFonts
				// can be expensive and you risk font existing or not existing on a deployment
				// by deployment basis.
				var font = SystemFonts.CreateFont("Arial", 15, FontStyle.Regular);

				var textGraphicsOptions = new TextGraphicsOptions() // draw the text along the path wrapping at the end of the line
				{
					TextOptions = {
						WrapTextWidth = path.Length,
						HorizontalAlignment = SixLabors.Fonts.HorizontalAlignment.Center,
					}
				};

				// lets generate the text as a set of vectors drawn along the path


				var glyphs = TextBuilder.GenerateGlyphs(text, path, new RendererOptions(font, textGraphicsOptions.TextOptions.DpiX, textGraphicsOptions.TextOptions.DpiY)
				{
					HorizontalAlignment = textGraphicsOptions.TextOptions.HorizontalAlignment,
					TabWidth = textGraphicsOptions.TextOptions.TabWidth,
					VerticalAlignment = textGraphicsOptions.TextOptions.VerticalAlignment,
					WrappingWidth = textGraphicsOptions.TextOptions.WrapTextWidth,
					ApplyKerning = textGraphicsOptions.TextOptions.ApplyKerning
				});

				Color backColor = background;
				if (!selected)
				{
					var pixel = background.ToPixel<Rgb24>();
					var systemColor = System.Drawing.Color.FromArgb(pixel.R, pixel.G, pixel.G);
					var darkened = ControlPaint.Dark(systemColor);
					backColor = Color.FromRgb(darkened.R, darkened.G, darkened.B);
				}

				Color textColor = Color.Black;
				var pixel2 = backColor.ToPixel<Rgb24>();
				System.Drawing.Color sys = System.Drawing.Color.FromArgb(pixel2.R, pixel2.G, pixel2.B);
				if (sys.GetBrightness() > .1)
				{
					textColor = Color.Black;
				}
				else
				{
					textColor = Color.White;
				}

				img.Mutate(ctx => ctx
					.Fill(backColor) // white background image
					//.Draw(Color.Gray, 3, path) // draw the path so we can see what the text is supposed to be following
					.Fill(textColor, glyphs));

				img.SaveAsBmp(memoryStream);
			}
			memoryStream.Position = 0;

			return KeyBitmap.Create.FromStream(memoryStream);
		}

		private void buttonConnectToOBS_Click(object sender, EventArgs e)
		{
			if (_obsManager.IsConnecting) return;

			if (!_obsManager.IsConnected)
			{
				_obsManager.Connect();
				if (!_obsManager.IsConnected)
				{
					buttonConnectToOBS.Text = "Working...";
					buttonConnectToOBS.Enabled = false;
				}
			}
			else
			{

			}
		}

		private void buttonConnectToDeck_Click(object sender, EventArgs e)
		{
			if (!_buttonsManager.IsConnected)
			{
				try
				{
					_buttonsManager.Connect();
				}
				catch { }
			}
			else
			{
				try
				{
					_buttonsManager.Disconnect();
				}
				catch { }
			}
		}

		//private async void btnFindTally_Click(object sender, EventArgs e)
		//{
		//	var client = new HttpClient();
		//	var ip = GetLocalIPAddress();

		//	var byt = ip.GetAddressBytes();

		//	btnFindTally.Text = "finding...";
		//	for (var i = 1; i < 255; i++)
		//	{
		//		try
		//		{
		//			var resp = await client.GetAsync($"http://{byt[0]}.{byt[1]}.{byt[2]}.{i}");
		//			if (resp.IsSuccessStatusCode)
		//			{
		//				var str = await resp.Content.ReadAsStringAsync();
		//				if (str.Contains("ESP-Tally"))
		//				{
		//					lblTally.Invoke((Action)(() =>
		//					{
		//						lblTally.Text = $"http://{byt[0]}.{byt[1]}.{byt[2]}.{i}";
		//					}));
		//					return;
		//				}
		//			}
		//		} catch
		//		{
		//			// Ignore
		//		}
		//	}
		//	btnFindTally.Invoke((Action)(() =>
		//	{
		//		btnFindTally.Text = "done";
		//	}));
		//}

		//public static IPAddress GetLocalIPAddress()
		//{
		//	var host = Dns.GetHostEntry(Dns.GetHostName());
		//	foreach (var ip in host.AddressList)
		//	{
		//		if (ip.AddressFamily == AddressFamily.InterNetwork)
		//		{
		//			return ip;
		//		}
		//	}
		//	throw new Exception("No network adapters with an IPv4 address in the system!");
		//}

		private void buttonTallyManager_Click(object sender, EventArgs e)
		{
			var configureTallies = _serviceProvider.GetRequiredService<frmTallyManager>();
			configureTallies.ShowDialog(this);
		}
	}
}
