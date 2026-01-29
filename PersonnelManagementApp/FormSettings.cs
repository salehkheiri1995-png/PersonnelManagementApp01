using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    public partial class FormSettings : BaseThemedForm
    {
        private Color selectedAddColor, selectedEditColor, selectedDeleteColor;
        private Color selectedSearchColor, selectedAnalyticsColor, selectedSettingsColor;
        private Color selectedPrimaryColor, selectedSecondaryColor;
        private Color selectedBackgroundColor, selectedTextColor;

        public FormSettings()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void InitializeComponent()
        {
            this.Text = "تنظیمات (Settings)";
            this.Size = new Size(700, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.RightToLeft = settingsManager.RightToLeftLayout ? RightToLeft.Yes : RightToLeft.No;
            this.BackColor = settingsManager.BackgroundColor;
            this.Font = new Font("Tahoma", 11);

            // Main TabControl
            TabControl mainTab = new TabControl
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10)
            };
            RegisterThemedControl(mainTab);

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
                Font = new Font("Tahoma", 10, FontStyle.Bold),
                Name = "btnSave"
            };
            btnSave.Click += (s, e) => BtnSave_Click();
            ApplyRoundedCorners(btnSave, 8);
            RegisterThemedControl(btnSave);

            Button btnCancel = new Button
            {
                Text = "بازگشت (Cancel)",
                Size = new Size(100, 35),
                Location = new Point(this.Width - 230, 8),
                BackColor = Color.LightCoral,
                ForeColor = Color.White,
                Font = new Font("Tahoma", 10, FontStyle.Bold),
                Name = "btnCancel"
            };
            btnCancel.Click += (s, e) => this.Close();
            ApplyRoundedCorners(btnCancel, 8);
            RegisterThemedControl(btnCancel);

            Button btnReset = new Button
            {
                Text = "بازتنظیم (Reset)",
                Size = new Size(100, 35),
                Location = new Point(this.Width - 340, 8),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                Font = new Font("Tahoma", 10, FontStyle.Bold),
                Name = "btnReset"
            };
            btnReset.Click += (s, e) => BtnReset_Click();
            ApplyRoundedCorners(btnReset, 8);
            RegisterThemedControl(btnReset);

            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnReset);
            this.Controls.Add(buttonPanel);
        }

        private TabPage CreateFontTab()
        {
            TabPage tab = new TabPage { Text = "فونت (Fonts)" };
            int y = 15;

            // Primary Font
            Label lbl1 = CreateLabel("فونت اصلی (Primary Font):", 10, y);
            tab.Controls.Add(lbl1);
            RegisterThemedControl(lbl1);

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
            RegisterThemedControl(cbPrimaryFont);
            y += 60;

            // Primary Font Size
            Label lbl2 = CreateLabel("اندازه فونت اصلی (Size):", 10, y);
            tab.Controls.Add(lbl2);
            RegisterThemedControl(lbl2);

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
            RegisterThemedControl(nudPrimarySize);
            y += 60;

            // Title Font
            Label lbl3 = CreateLabel("فونت عنوان (Title Font):", 10, y);
            tab.Controls.Add(lbl3);
            RegisterThemedControl(lbl3);

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
            RegisterThemedControl(cbTitleFont);
            y += 60;

            // Title Font Size
            Label lbl4 = CreateLabel("اندازه فونت عنوان (Size):", 10, y);
            tab.Controls.Add(lbl4);
            RegisterThemedControl(lbl4);

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
            RegisterThemedControl(nudTitleSize);
            y += 60;

            // Button Font
            Label lbl5 = CreateLabel("فونت دکمه (Button Font):", 10, y);
            tab.Controls.Add(lbl5);
            RegisterThemedControl(lbl5);

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
            RegisterThemedControl(cbButtonFont);
            y += 60;

            // Button Font Size
            Label lbl6 = CreateLabel("اندازه فونت دکمه (Size):", 10, y);
            tab.Controls.Add(lbl6);
            RegisterThemedControl(lbl6);

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
            RegisterThemedControl(nudButtonSize);

            tab.Tag = new object[] { cbPrimaryFont, nudPrimarySize, cbTitleFont, nudTitleSize, cbButtonFont, nudButtonSize };
            return tab;
        }

        private TabPage CreateColorTab()
        {
            TabPage tab = new TabPage { Text = "رنگ (Colors)" };
            int y = 15;
            int x = 10;

            // Primary Color
            Label lblPrimary = CreateLabel("رنگ اصلی (Primary):", x, y);
            tab.Controls.Add(lblPrimary);
            RegisterThemedControl(lblPrimary);

            Button btnPrimaryColor = CreateColorButton(settingsManager.PrimaryColor, x, y + 25, "رنگ اصلی");
            btnPrimaryColor.Click += (s, e) => {
                selectedPrimaryColor = ChooseColor(settingsManager.PrimaryColor);
                btnPrimaryColor.BackColor = selectedPrimaryColor;
            };
            tab.Controls.Add(btnPrimaryColor);
            RegisterThemedControl(btnPrimaryColor);
            y += 60;

            // Secondary Color
            Label lblSecondary = CreateLabel("رنگ ثانویه (Secondary):", x, y);
            tab.Controls.Add(lblSecondary);
            RegisterThemedControl(lblSecondary);

            Button btnSecondaryColor = CreateColorButton(settingsManager.SecondaryColor, x, y + 25, "رنگ ثانویه");
            btnSecondaryColor.Click += (s, e) => {
                selectedSecondaryColor = ChooseColor(settingsManager.SecondaryColor);
                btnSecondaryColor.BackColor = selectedSecondaryColor;
            };
            tab.Controls.Add(btnSecondaryColor);
            RegisterThemedControl(btnSecondaryColor);
            y += 60;

            // Background Color
            Label lblBackground = CreateLabel("رنگ پسزمینه (Background):", x, y);
            tab.Controls.Add(lblBackground);
            RegisterThemedControl(lblBackground);

            Button btnBackgroundColor = CreateColorButton(settingsManager.BackgroundColor, x, y + 25, "رنگ پسزمینه");
            btnBackgroundColor.Click += (s, e) => {
                selectedBackgroundColor = ChooseColor(settingsManager.BackgroundColor);
                btnBackgroundColor.BackColor = selectedBackgroundColor;
            };
            tab.Controls.Add(btnBackgroundColor);
            RegisterThemedControl(btnBackgroundColor);
            y += 60;

            // Text Color
            Label lblText = CreateLabel("رنگ متن (Text):", x, y);
            tab.Controls.Add(lblText);
            RegisterThemedControl(lblText);

            Button btnTextColor = CreateColorButton(settingsManager.TextColor, x, y + 25, "رنگ متن");
            btnTextColor.Click += (s, e) => {
                selectedTextColor = ChooseColor(settingsManager.TextColor);
                btnTextColor.BackColor = selectedTextColor;
            };
            tab.Controls.Add(btnTextColor);
            RegisterThemedControl(btnTextColor);

            // Button Colors Section
            y += 80;
            Label lblButtonColors = CreateLabel("رنگ‌های دکمه (Button Colors):", x, y, true);
            tab.Controls.Add(lblButtonColors);
            RegisterThemedControl(lblButtonColors);
            y += 40;

            // Add Color
            Label lblAdd = CreateLabel("رنگ ثبت (Add):", x, y);
            tab.Controls.Add(lblAdd);
            RegisterThemedControl(lblAdd);

            Button btnAddColor = CreateColorButton(settingsManager.ButtonAddColor, x, y + 25, "رنگ ثبت");
            btnAddColor.Click += (s, e) => {
                selectedAddColor = ChooseColor(settingsManager.ButtonAddColor);
                btnAddColor.BackColor = selectedAddColor;
            };
            tab.Controls.Add(btnAddColor);
            RegisterThemedControl(btnAddColor);

            // Edit Color
            Label lblEdit = CreateLabel("رنگ ویرایش (Edit):", x + 200, y);
            tab.Controls.Add(lblEdit);
            RegisterThemedControl(lblEdit);

            Button btnEditColor = CreateColorButton(settingsManager.ButtonEditColor, x + 200, y + 25, "رنگ ویرایش");
            btnEditColor.Click += (s, e) => {
                selectedEditColor = ChooseColor(settingsManager.ButtonEditColor);
                btnEditColor.BackColor = selectedEditColor;
            };
            tab.Controls.Add(btnEditColor);
            RegisterThemedControl(btnEditColor);

            y += 60;

            // Delete Color
            Label lblDelete = CreateLabel("رنگ حذف (Delete):", x, y);
            tab.Controls.Add(lblDelete);
            RegisterThemedControl(lblDelete);

            Button btnDeleteColor = CreateColorButton(settingsManager.ButtonDeleteColor, x, y + 25, "رنگ حذف");
            btnDeleteColor.Click += (s, e) => {
                selectedDeleteColor = ChooseColor(settingsManager.ButtonDeleteColor);
                btnDeleteColor.BackColor = selectedDeleteColor;
            };
            tab.Controls.Add(btnDeleteColor);
            RegisterThemedControl(btnDeleteColor);

            // Search Color
            Label lblSearch = CreateLabel("رنگ جستجو (Search):", x + 200, y);
            tab.Controls.Add(lblSearch);
            RegisterThemedControl(lblSearch);

            Button btnSearchColor = CreateColorButton(settingsManager.ButtonSearchColor, x + 200, y + 25, "رنگ جستجو");
            btnSearchColor.Click += (s, e) => {
                selectedSearchColor = ChooseColor(settingsManager.ButtonSearchColor);
                btnSearchColor.BackColor = selectedSearchColor;
            };
            tab.Controls.Add(btnSearchColor);
            RegisterThemedControl(btnSearchColor);

            tab.Tag = new object[] { btnPrimaryColor, btnSecondaryColor, btnBackgroundColor, btnTextColor, btnAddColor, btnEditColor, btnDeleteColor, btnSearchColor };
            return tab;
        }

        private TabPage CreateUITab()
        {
            TabPage tab = new TabPage { Text = "UI" };
            int y = 15;

            // Button Corner Radius
            Label lblRadius = CreateLabel("بررسی گوشه دکمه (Button Corner Radius):", 10, y);
            tab.Controls.Add(lblRadius);
            RegisterThemedControl(lblRadius);

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
            RegisterThemedControl(nudRadius);
            y += 60;

            // RTL Layout
            CheckBox chkRTL = new CheckBox
            {
                Location = new Point(10, y),
                Size = new Size(300, 25),
                Text = "طرح راست‌به‌چپ (Right-to-Left Layout)",
                Checked = settingsManager.RightToLeftLayout,
                Name = "chkRTL",
                Font = new Font("Tahoma", 11)
            };
            tab.Controls.Add(chkRTL);
            RegisterThemedControl(chkRTL);
            y += 40;

            // Dark Mode
            CheckBox chkDarkMode = new CheckBox
            {
                Location = new Point(10, y),
                Size = new Size(300, 25),
                Text = "حالت تاریک (Dark Mode)",
                Checked = settingsManager.EnableDarkMode,
                Name = "chkDarkMode",
                Font = new Font("Tahoma", 11)
            };
            tab.Controls.Add(chkDarkMode);
            RegisterThemedControl(chkDarkMode);

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
                Font = new Font("Tahoma", isBold ? 12 : 10, isBold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = settingsManager.TextColor
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
                Text = "انتخاب",
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

        private void LoadCurrentSettings()
        {
            // Settings are loaded into controls during CreateTab methods
        }

        private void BtnSave_Click()
        {
            try
            {
                // Get font settings
                TabControl mainTab = (TabControl)this.Controls[0];
                TabPage fontTab = mainTab.TabPages[0];
                var fontControls = (object[])fontTab.Tag;

                settingsManager.PrimaryFont = (fontControls[0] as ComboBox).SelectedItem.ToString();
                settingsManager.PrimaryFontSize = (int)(fontControls[1] as NumericUpDown).Value;
                settingsManager.TitleFont = (fontControls[2] as ComboBox).SelectedItem.ToString();
                settingsManager.TitleFontSize = (int)(fontControls[3] as NumericUpDown).Value;
                settingsManager.ButtonFont = (fontControls[4] as ComboBox).SelectedItem.ToString();
                settingsManager.ButtonFontSize = (int)(fontControls[5] as NumericUpDown).Value;

                // Get color settings
                TabPage colorTab = mainTab.TabPages[1];
                if (selectedPrimaryColor != Color.Empty) settingsManager.PrimaryColor = selectedPrimaryColor;
                if (selectedSecondaryColor != Color.Empty) settingsManager.SecondaryColor = selectedSecondaryColor;
                if (selectedBackgroundColor != Color.Empty) settingsManager.BackgroundColor = selectedBackgroundColor;
                if (selectedTextColor != Color.Empty) settingsManager.TextColor = selectedTextColor;
                if (selectedAddColor != Color.Empty) settingsManager.ButtonAddColor = selectedAddColor;
                if (selectedEditColor != Color.Empty) settingsManager.ButtonEditColor = selectedEditColor;
                if (selectedDeleteColor != Color.Empty) settingsManager.ButtonDeleteColor = selectedDeleteColor;
                if (selectedSearchColor != Color.Empty) settingsManager.ButtonSearchColor = selectedSearchColor;

                // Get UI settings
                TabPage uiTab = mainTab.TabPages[2];
                var uiControls = (object[])uiTab.Tag;
                settingsManager.ButtonCornerRadius = (int)(uiControls[0] as NumericUpDown).Value;
                settingsManager.RightToLeftLayout = (uiControls[1] as CheckBox).Checked;
                settingsManager.EnableDarkMode = (uiControls[2] as CheckBox).Checked;

                // Save to file
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
            if (MessageBox.Show("آیا مطمئن اید که می‌خواهید قطعی بازتنظیم کنید?\n\nAre you sure you want to reset?",
                            "تأیید", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                settingsManager.ResetToDefaults();
                this.Close();
            }
        }
    }
}
