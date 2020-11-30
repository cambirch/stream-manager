using Microsoft.Extensions.Options;
using OpenMacroBoard.SDK;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using StreamDeckManager.Actions;
using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Settings;
using StreamDeckSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Managers
{
    public class ButtonsManager : IDisposable
    {
        private readonly IOptionsMonitor<StreamDeckSettings> _streamDeckSettings;
        private readonly ActionFactory _actionFactory;
        private ButtonReference[,] _buttons;
        private IStreamDeckBoard? _deck;

        public event EventHandler OnConnectionStateChanged;

        public ButtonsManager(IOptionsMonitor<StreamDeckSettings> streamDeckSettings, ActionFactory actionFactory)
        {
            (_streamDeckSettings, _actionFactory) = (streamDeckSettings, actionFactory);

            // Initialize all the buttons
            BuildButtons();

            if (_streamDeckSettings.CurrentValue.AutoConnect)
			{
                try
				{
                    Connect();
				}
                catch { }
			}

            streamDeckSettings.OnChange(async _ =>
            {
                await Task.Delay(10);
                BuildButtons();
            });
        }
        private void BuildButtons()
		{
            _buttons = new ButtonReference[_streamDeckSettings.CurrentValue.Height, _streamDeckSettings.CurrentValue.Width];
            for (int x = 0; x < _streamDeckSettings.CurrentValue.Width; x++)
            {
                for (int y = 0; y < _streamDeckSettings.CurrentValue.Height; y++)
                {
                    _buttons[y, x] = new ButtonReference(this, x, y);
                }
            }
        }

        public void Dispose()
		{
            _deck?.Dispose();
		}

        public int Width => _streamDeckSettings.CurrentValue.Width;
        public int Height => _streamDeckSettings.CurrentValue.Height;

        public void SetUIButton(System.Windows.Forms.Button button, int column, int row) => _buttons[row, column].UIButton = button;

        public void UpdateUI(bool force = false)
        {
            foreach (var button in _buttons)
            {
                button.SetUIState(force);
            }
        }

        public void Connect()
		{
            try
			{
                _deck = StreamDeck.OpenDevice();
                _deck.SetBrightness(100);
                _deck.KeyStateChanged += _deck_KeyStateChanged;

				_deck.ConnectionStateChanged += _deck_ConnectionStateChanged;
                OnConnectionStateChanged?.Invoke(null, EventArgs.Empty);
                UpdateUI(true);
            }
            catch
			{
                _deck = null;
			}
        }
        public void Disconnect()
		{
            _deck?.Dispose();
            _deck = null;

            OnConnectionStateChanged?.Invoke(null, EventArgs.Empty);
		}

        private void _deck_ConnectionStateChanged(object? sender, ConnectionEventArgs e) => OnConnectionStateChanged?.Invoke(sender, e);

		public bool IsConnected => _deck?.IsConnected ?? false;

		private void _deck_KeyStateChanged(object? sender, OpenMacroBoard.SDK.KeyEventArgs e)
		{
            if (!e.IsDown) return;

            int row = (int)(e.Key / Width);
            int column = e.Key % Width;

            _buttons[row, column].Trigger();
		}

		private class ButtonReference
        {
            private readonly ButtonsManager _parent;
            private IList<IAction> Actions;

            public ButtonReference(ButtonsManager parent, int column, int row)
            {
                (_parent, Row, Column, KeyNumber) = (parent, row, column, (row * parent._streamDeckSettings.CurrentValue.Width) + column);

                var key = Key;
                if (key is object)
                {
                    Color = System.Drawing.Color.FromArgb(int.Parse("FF" + key.Color, System.Globalization.NumberStyles.AllowHexSpecifier));
                    ActiveColor = System.Windows.Forms.ControlPaint.Light(Color);
                }
                Actions = key?.Actions?.Select(action => _parent._actionFactory.CreateAction(action.Name, action.Props))?.ToArray() ?? Array.Empty<IAction>();
            }

            private KeySettings? Key => _parent._streamDeckSettings.CurrentValue.Keys.Find(f => f.KeyNumber == KeyNumber);

            private System.Windows.Forms.Button _uiButton;
            public System.Windows.Forms.Button UIButton {
                get => _uiButton;
                set
                {
                    _uiButton = value;
                    _uiButton.Click += _uiButton_Click;

                    SetUIState();
                }
            }

            private void _uiButton_Click(object? sender, EventArgs e) => Trigger();

            public void Trigger()
			{
                foreach (var action in Actions)
                {
                    action.Trigger();
                }
            }

            public int Column { get; }
            public int Row { get; }
            public int KeyNumber { get; }


            public System.Drawing.Color Color { get; set; }
            public System.Drawing.Color TextColor => Color.GetBrightness() > .4 ? System.Drawing.Color.Black : System.Drawing.Color.White;

            public System.Drawing.Color ActiveColor { get; set; }
            public System.Drawing.Color ActiveTextColor => ActiveColor.GetBrightness() > .4 ? System.Drawing.Color.Black : System.Drawing.Color.White;

			public System.Drawing.Color CurrentBackColor { get; set; }
			public System.Drawing.Color CurrentTextColor { get; set; }
			public string CurrentText { get; set; }

			public void SetUIState(bool force = false)
            {
                bool? isActive = Actions.FirstOfType<IActionHasActive>()?.IsActive;
                string? text = Actions.Where(f => f is IActionCustomText cText && cText.UseCustomText).Cast<IActionCustomText>()?.FirstOrDefault()?.Text;
                void SetUI()
                {
                    if (isActive == true)
                    {
                        _uiButton.BackColor = CurrentBackColor = ActiveColor;
                        _uiButton.ForeColor = CurrentTextColor = ActiveTextColor;
                    }
                    else if (isActive == false)
                    {
                        _uiButton.BackColor = CurrentBackColor = Color;
                        _uiButton.ForeColor = CurrentTextColor = TextColor;
                    }

                    _uiButton.Text = CurrentText = text ?? Key?.Text ?? string.Empty;
                }
                void UpdateDeck()
				{
                    if (
                        // Changed active color
                        (isActive == true && (CurrentBackColor != ActiveColor || CurrentTextColor != ActiveTextColor))
                        // Changed in-active color
                        || (isActive == false && (CurrentBackColor != Color || CurrentTextColor != TextColor))
                        // Changed text
                        || (CurrentText != (text ?? Key?.Text ?? string.Empty))
                        || force
                    )
					{
                        // Changed UI
                        var graphic = _parent.CreateTextImage(
                            text: text ?? Key?.Text ?? string.Empty,
                            background: isActive == true ? ActiveColor : Color,
                            textColor: isActive == true ? ActiveTextColor : TextColor);
                        _parent._deck.SetKeyBitmap(KeyNumber, graphic);
					}
				}

                if (_parent._deck is object) UpdateDeck();
                if (_uiButton is object)
				{
                    if (_uiButton.InvokeRequired)
                    {
                        _uiButton.BeginInvoke(new Action(SetUI));
                    }
                    else
                    {
                        SetUI();
                    }
                }
            }
        }

        private OpenMacroBoard.SDK.KeyBitmap CreateTextImage(
            string text,
            System.Drawing.Color background,
            System.Drawing.Color textColor
            )
        {
            const int width = 72;
            const int height = 72;


            using var memoryStream = new System.IO.MemoryStream();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgb24>(width, height))
            {
                var pathBuilder = new SixLabors.ImageSharp.Drawing.PathBuilder();
                pathBuilder.SetOrigin(new PointF(0, 0));
                pathBuilder.AddLine(0, height / 3 * 2, width, height / 3 * 2);

                var path = pathBuilder.Build();

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



                Color drawBackColor = Color.FromRgb(background.R, background.G, background.B);
                Color drawTextColor = Color.FromRgb(textColor.R, textColor.G, textColor.B);

                img.Mutate(ctx => ctx
                    .Fill(drawBackColor) // white background image
                                     //.Draw(Color.Gray, 3, path) // draw the path so we can see what the text is supposed to be following
                    .Fill(drawTextColor, glyphs));

                img.SaveAsBmp(memoryStream);
            }
            memoryStream.Position = 0;

            return OpenMacroBoard.SDK.KeyBitmap.Create.FromStream(memoryStream);
        }

	}

    public static class UtilFunctions {
        [return: MaybeNull()]
        public static T FirstOfType<T>(this IEnumerable<object> items) => (T)items.FirstOrDefault(f => f is T);
    }
}
