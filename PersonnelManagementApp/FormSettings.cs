using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    public partial class FormSettings : Form
    {
        private readonly SettingsManager settingsManager;
        private Color selectedAddColor, selectedEditColor, selectedDeleteColor;
        private Color selectedSearchColor, selectedAnalyticsColor, selectedSettingsColor;
        private Color selectedPrimaryColor, selectedSecondaryColor;
        private Color selectedBackgroundColor, selectedTextColor;

        public FormSettings(SettingsManager settings)
        {
            settingsManager = settings;
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void InitializeComponent()
        {
            this.Text = "تنظیمات (Settings)";
            this.Size = new Size(700, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.RightToLeft = RightToLeft.Yes;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Font = new Font("Tahoma", 11);

            // Main TabControl
            TabControl mainTab = new TabControl
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10)
            };

            // Tab 1: Fonts
            TabPage fontPage = CreateFontTab();
            mainTab.TabPages.Add(fontPage);

            // Tab 2: Colors
            TabPage colorPage = CreateColorTab();
            mainTab.TabPages.Add(colorPage);

            // Tab 3: UI
            TabPage uiPage = CreateUITab();
            mainTab.TabPages.Add(uiPage);

            this.Controls.Add(mainTab);

            // Bottom buttons panel
            Panel buttonPanel = new Panel
            {
                Height = 50,
                Dock = DockStyle.Bottom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Button btnSave = new Button
            {
                Text = "ذخیره (Save)",
                Size = new Size(100, 35),
                Location = new Point(this.Width - 120, 8),
                BackColor = Color.LightGreen,
                ForeColor = Color.White,
                Font = new Font("Tahoma", 10, FontStyle.Bold)
            };
            btnSave.Click += (s, e) => BtnSave_Click();
            ApplyRoundedCorners(btnSave, 8);

            Button btnCancel = new Button
            {
                Text = "بازگشت (Cancel)",
                Size = new Size(100, 35),
                Location = new Point(this.Width - 230, 8),
                BackColor = Color.LightCoral,
                ForeColor = Color.White,
                Font = new Font("Tahoma", 10, FontStyle.Bold)
            };
            btnCancel.Click += (s, e) => this.Close();
            ApplyRoundedCorners(btnCancel, 8);

            Button btnReset = new Button
            {
                Text = "بازتنظیم (Reset)",
                Size = new Size(100, 35),
                Location = new Point(this.Width - 340, 8),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                Font = new Font("Tahoma", 10, FontStyle.Bold)
            };
            btnReset.Click += (s, e) => BtnReset_Click();
            ApplyRoundedCorners(btnReset, 8);

            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnReset);
            this.Controls.Add(buttonPanel);
        }

        private TabPage CreateFontTab()
        {
            TabPage tab = new TabPage { Text = "\u0641ونت (Fonts)" };
            int y = 15;

            // Primary Font
            tab.Controls.Add(CreateLabel("فونت اصلی (Primary Font):", 10, y));
            ComboBox cbPrimaryFont = new ComboBox
            {
                Location = new Point(10, y + 25),
                Size = new Size(300, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "cbPrimaryFont"
            };
            cbPrimaryFont.Items.AddRange(settingsManager.PersianFonts);
            cbPrimaryFont.SelectedItem = settingsManager.PrimaryFont;
            tab.Controls.Add(cbPrimaryFont);
            y += 60;

            // Primary Font Size
            tab.Controls.Add(CreateLabel(اندازه فونت اصلی (Size):", 10, y));
            NumericUpDown nudPrimarySize = new NumericUpDown
            {
                Location = new Point(10, y + 25),
                Size = new Size(100, 25),
                Minimum = 8,
                Maximum = 20,
                Value = settingsManager.PrimaryFontSize,
                Name = "nudPrimarySize"
            };
            tab.Controls.Add(nudPrimarySize);
            y += 60;

            // Title Font
            tab.Controls.Add(CreateLabel("فونت عنوان (Title Font):", 10, y));
            ComboBox cbTitleFont = new ComboBox
            {
                Location = new Point(10, y + 25),
                Size = new Size(300, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "cbTitleFont"
            };
            cbTitleFont.Items.AddRange(settingsManager.PersianFonts);
            cbTitleFont.SelectedItem = settingsManager.TitleFont;
            tab.Controls.Add(cbTitleFont);
            y += 60;

            // Title Font Size
            tab.Controls.Add(CreateLabel("اندازه فونت عنوان (Size):", 10, y));
            NumericUpDown nudTitleSize = new NumericUpDown
            {
                Location = new Point(10, y + 25),
                Size = new Size(100, 25),
                Minimum = 12,
                Maximum = 32,
                Value = settingsManager.TitleFontSize,
                Name = "nudTitleSize"
            };
            tab.Controls.Add(nudTitleSize);
            y += 60;

            // Button Font
            tab.Controls.Add(CreateLabel("فونت دكمه (Button Font):", 10, y));
            ComboBox cbButtonFont = new ComboBox
            {
                Location = new Point(10, y + 25),
                Size = new Size(300, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "cbButtonFont"
            };
            cbButtonFont.Items.AddRange(settingsManager.PersianFonts);
            cbButtonFont.SelectedItem = settingsManager.ButtonFont;
            tab.Controls.Add(cbButtonFont);
            y += 60;

            // Button Font Size
            tab.Controls.Add(CreateLabel("اندازه فونت دكمه (Size):", 10, y));
            NumericUpDown nudButtonSize = new NumericUpDown
            {
                Location = new Point(10, y + 25),
                Size = new Size(100, 25),
                Minimum = 10,
                Maximum = 16,
                Value = settingsManager.ButtonFontSize,
                Name = "nudButtonSize"
            };
            tab.Controls.Add(nudButtonSize);

            tab.Tag = new object[] { cbPrimaryFont, nudPrimarySize, cbTitleFont, nudTitleSize, cbButtonFont, nudButtonSize };
            return tab;
        }

        private TabPage CreateColorTab()
        {
            TabPage tab = new TabPage { Text = "رنگ (Colors)" };
            int y = 15;
            int x = 10;

            // Primary Color
            tab.Controls.Add(CreateLabel("رنگ اصلی (Primary):", x, y));
            Button btnPrimaryColor = CreateColorButton(settingsManager.PrimaryColor, x, y + 25, "رنگ اصلی");
            btnPrimaryColor.Click += (s, e) => {
                selectedPrimaryColor = ChooseColor(settingsManager.PrimaryColor);
                btnPrimaryColor.BackColor = selectedPrimaryColor;
            };
            tab.Controls.Add(btnPrimaryColor);
            y += 60;

            // Secondary Color
            tab.Controls.Add(CreateLabel("رنگ ثانوي (Secondary):", x, y));
            Button btnSecondaryColor = CreateColorButton(settingsManager.SecondaryColor, x, y + 25, "رنگ ثانوي");
            btnSecondaryColor.Click += (s, e) => {
                selectedSecondaryColor = ChooseColor(settingsManager.SecondaryColor);
                btnSecondaryColor.BackColor = selectedSecondaryColor;
            };
            tab.Controls.Add(btnSecondaryColor);
            y += 60;

            // Background Color
            tab.Controls.Add(CreateLabel("رنگ پسزمين (Background):", x, y));
            Button btnBackgroundColor = CreateColorButton(settingsManager.BackgroundColor, x, y + 25, "رنگ پسزمين");
            btnBackgroundColor.Click += (s, e) => {
                selectedBackgroundColor = ChooseColor(settingsManager.BackgroundColor);
                btnBackgroundColor.BackColor = selectedBackgroundColor;
            };
            tab.Controls.Add(btnBackgroundColor);
            y += 60;

            // Text Color
            tab.Controls.Add(CreateLabel("رنگ متن (Text):", x, y));
            Button btnTextColor = CreateColorButton(settingsManager.TextColor, x, y + 25, "رنگ متن");
            btnTextColor.Click += (s, e) => {
                selectedTextColor = ChooseColor(settingsManager.TextColor);
                btnTextColor.BackColor = selectedTextColor;
            };
            tab.Controls.Add(btnTextColor);

            // Button Colors Section
            y += 80;
            tab.Controls.Add(CreateLabel("رنگ‌های دكمه (Button Colors):", x, y, true));
            y += 40;

            // Add Color
            tab.Controls.Add(CreateLabel("رنگ ثبت (Add):", x, y));
            Button btnAddColor = CreateColorButton(settingsManager.ButtonAddColor, x, y + 25, "رنگ ثبت");
            btnAddColor.Click += (s, e) => {
                selectedAddColor = ChooseColor(settingsManager.ButtonAddColor);
                btnAddColor.BackColor = selectedAddColor;
            };
            tab.Controls.Add(btnAddColor);

            // Edit Color
            tab.Controls.Add(CreateLabel("رنگ ویرایش (Edit):", x + 200, y));
            Button btnEditColor = CreateColorButton(settingsManager.ButtonEditColor, x + 200, y + 25, "رنگ ویرایش");
            btnEditColor.Click += (s, e) => {
                selectedEditColor = ChooseColor(settingsManager.ButtonEditColor);
                btnEditColor.BackColor = selectedEditColor;
            };
            tab.Controls.Add(btnEditColor);

            y += 60;

            // Delete Color
            tab.Controls.Add(CreateLabel("رنگ حذف (Delete):", x, y));
            Button btnDeleteColor = CreateColorButton(settingsManager.ButtonDeleteColor, x, y + 25, "رنگ حذف");
            btnDeleteColor.Click += (s, e) => {
                selectedDeleteColor = ChooseColor(settingsManager.ButtonDeleteColor);
                btnDeleteColor.BackColor = selectedDeleteColor;
            };
            tab.Controls.Add(btnDeleteColor);

            // Search Color
            tab.Controls.Add(CreateLabel("رنگ جستجو (Search):", x + 200, y));
            Button btnSearchColor = CreateColorButton(settingsManager.ButtonSearchColor, x + 200, y + 25, "رنگ جستجو");
            btnSearchColor.Click += (s, e) => {
                selectedSearchColor = ChooseColor(settingsManager.ButtonSearchColor);
                btnSearchColor.BackColor = selectedSearchColor;
            };
            tab.Controls.Add(btnSearchColor);

            tab.Tag = new object[] { btnPrimaryColor, btnSecondaryColor, btnBackgroundColor, btnTextColor, btnAddColor, btnEditColor, btnDeleteColor, btnSearchColor };
            return tab;
        }

        private TabPage CreateUITab()
        {
            TabPage tab = new TabPage { Text = "UI" };
            int y = 15;

            // Button Corner Radius
            tab.Controls.Add(CreateLabel("بررسـی گوشه دكمه (Button Corner Radius):", 10, y));
            NumericUpDown nudRadius = new NumericUpDown
            {
                Location = new Point(10, y + 25),
                Size = new Size(100, 25),
                Minimum = 0,
                Maximum = 30,
                Value = settingsManager.ButtonCornerRadius,
                Name = "nudRadius"
            };
            tab.Controls.Add(nudRadius);
            y += 60;

            // RTL Layout
            CheckBox chkRTL = new CheckBox
            {
                Location = new Point(10, y),
                Size = new Size(300, 25),
                Text = "طرح راستبه چپ (Right-to-Left Layout)",
                Checked = settingsManager.RightToLeftLayout,
                Name = "chkRTL",
                Font = new Font("Tahoma", 11)
            };
            tab.Controls.Add(chkRTL);
            y += 40;

            // Dark Mode
            CheckBox chkDarkMode = new CheckBox
            {
                Location = new Point(10, y),
                Size = new Size(300, 25),
                Text = "حالت تاریك (Dark Mode)",
                Checked = settingsManager.EnableDarkMode,
                Name = "chkDarkMode",
                Font = new Font("Tahoma", 11)
            };
            tab.Controls.Add(chkDarkMode);

            tab.Tag = new object[] { nudRadius, chkRTL, chkDarkMode };
            return tab;
        }

        private Label CreateLabel(string text, int x, int y, bool isBold = false)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(400, 25),
                Font = new Font("Tahoma", isBold ? 12 : 10, isBold ? FontStyle.Bold : FontStyle.Regular)
            };
        }

        private Button CreateColorButton(Color color, int x, int y, string name)
        {
            Button btn = new Button
            {
                Location = new Point(x, y),
                Size = new Size(100, 30),
                BackColor = color,
                ForeColor = GetContrastColor(color),
                Text = برابر",
                Name = name
            };
            ApplyRoundedCorners(btn, 8);
            return btn;
        }

        private Color ChooseColor(Color currentColor)
        {
            ColorDialog cd = new ColorDialog { Color = currentColor };
            return cd.ShowDialog() == DialogResult.OK ? cd.Color : currentColor;
        }

        private Color GetContrastColor(Color color)
        {
            int brightness = (color.R * 299 + color.G * 587 + color.B * 114) / 1000;
            return brightness > 128 ? Color.Black : Color.White;
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private void LoadCurrentSettings()
        {
            // Settings are loaded into controls during CreateTab methods
        }

        private void BtnSave_Click()
        {
            try
            {
                // Get font settings
                TabPage fontTab = ((TabControl)this.Controls[0]).TabPages[0];
                var fontControls = (object[])fontTab.Tag;
                settingsManager.PrimaryFont = (fontControls[0] as ComboBox).SelectedItem.ToString();
                settingsManager.PrimaryFontSize = (int)(fontControls[1] as NumericUpDown).Value;
                settingsManager.TitleFont = (fontControls[2] as ComboBox).SelectedItem.ToString();
                settingsManager.TitleFontSize = (int)(fontControls[3] as NumericUpDown).Value;
                settingsManager.ButtonFont = (fontControls[4] as ComboBox).SelectedItem.ToString();
                settingsManager.ButtonFontSize = (int)(fontControls[5] as NumericUpDown).Value;

                // Save all settings
                settingsManager.SaveSettings();
                MessageBox.Show("تنظیمات با موفقیت ذخیره شدند.\nSettings saved successfully.",
                                "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReset_Click()
        {
            if (MessageBox.Show("آیا مطمئن اید که ميخواهید قطعي بازتنظیم کنید?\n\nAre you sure you want to reset?",
                            "تأیید", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                settingsManager.ResetToDefaults();
                this.Close();
            }
        }
    }
}
