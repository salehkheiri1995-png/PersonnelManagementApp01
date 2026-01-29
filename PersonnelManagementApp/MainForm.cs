using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PersonnelManagementApp
{
    public partial class MainForm : Form
    {
        private readonly SettingsManager settingsManager;

        public MainForm()
        {
            settingsManager = new SettingsManager();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "مدیریت پرسنل";
            this.WindowState = FormWindowState.Maximized;
            this.RightToLeft = RightToLeft.Yes;
            this.BackColor = settingsManager.BackgroundColor;

            // پسزمینه گرادیانت
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, settingsManager.ButtonAddColor, Color.White, LinearGradientMode.Vertical))
            {
                this.BackgroundImage = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(this.BackgroundImage))
                {
                    g.FillRectangle(brush, this.ClientRectangle);
                }
            }

            int centerX = (this.ClientSize.Width - 300) / 2;
            int centerY = (this.ClientSize.Height - 450) / 2;

            // سرتیتر
            Label lblTitle = new Label
            {
                Text = "سیستم مدیریت پرسنل",
                Location = new Point(centerX, centerY - 80),
                Size = new Size(300, 50),
                Font = settingsManager.GetTitleFont(FontStyle.Bold),
                ForeColor = Color.Navy,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitle);

            // دکمه ثبت پرسنل جدید
            Button btnAdd = new Button
            {
                Text = "ثبت پرسنل جدید",
                Location = new Point(centerX, centerY),
                Size = new Size(300, 50),
                Font = settingsManager.GetButtonFont(FontStyle.Bold),
                BackColor = settingsManager.ButtonAddColor,
                ForeColor = Color.White
            };
            ApplyRoundedCorners(btnAdd, settingsManager.ButtonCornerRadius);
            btnAdd.Click += (s, e) => new FormPersonnelRegister().ShowDialog();
            this.Controls.Add(btnAdd);

            // دکمه ویرایش پرسنل
            Button btnEdit = new Button
            {
                Text = "ویرایش پرسنل",
                Location = new Point(centerX, centerY + 60),
                Size = new Size(300, 50),
                Font = settingsManager.GetButtonFont(FontStyle.Bold),
                BackColor = settingsManager.ButtonEditColor,
                ForeColor = Color.White
            };
            ApplyRoundedCorners(btnEdit, settingsManager.ButtonCornerRadius);
            btnEdit.Click += (s, e) => new FormPersonnelEdit().ShowDialog();
            this.Controls.Add(btnEdit);

            // دکمه حذف پرسنل
            Button btnDelete = new Button
            {
                Text = "حذف پرسنل",
                Location = new Point(centerX, centerY + 120),
                Size = new Size(300, 50),
                Font = settingsManager.GetButtonFont(FontStyle.Bold),
                BackColor = settingsManager.ButtonDeleteColor,
                ForeColor = Color.White
            };
            ApplyRoundedCorners(btnDelete, settingsManager.ButtonCornerRadius);
            btnDelete.Click += (s, e) => new FormPersonnelDelete().ShowDialog();
            this.Controls.Add(btnDelete);

            // دکمه جستجوی پرسنل
            Button btnSearch = new Button
            {
                Text = "جستجوی پرسنل",
                Location = new Point(centerX, centerY + 180),
                Size = new Size(300, 50),
                Font = settingsManager.GetButtonFont(FontStyle.Bold),
                BackColor = settingsManager.ButtonSearchColor,
                ForeColor = Color.White
            };
            ApplyRoundedCorners(btnSearch, settingsManager.ButtonCornerRadius);
            btnSearch.Click += (s, e) => new FormPersonnelSearch().ShowDialog();
            this.Controls.Add(btnSearch);

            // دکمه تحلیل داده‌های پرسنل
            Button btnAnalytics = new Button
            {
                Text = "تحلیل داده‌های پرسنل",
                Location = new Point(centerX, centerY + 240),
                Size = new Size(300, 50),
                Font = settingsManager.GetButtonFont(FontStyle.Bold),
                BackColor = settingsManager.ButtonAnalyticsColor,
                ForeColor = Color.White
            };
            ApplyRoundedCorners(btnAnalytics, settingsManager.ButtonCornerRadius);
            btnAnalytics.Click += (s, e) => new FormPersonnelAnalytics().ShowDialog();
            this.Controls.Add(btnAnalytics);

            // دکمه تنظیمات
            Button btnSettings = new Button
            {
                Text = "تنظیمات",
                Location = new Point(centerX, centerY + 300),
                Size = new Size(300, 50),
                Font = settingsManager.GetButtonFont(FontStyle.Bold),
                BackColor = settingsManager.ButtonSettingsColor,
                ForeColor = Color.White
            };
            ApplyRoundedCorners(btnSettings, settingsManager.ButtonCornerRadius);
            btnSettings.Click += (s, e) => 
            {
                FormSettings settingsForm = new FormSettings(settingsManager);
                settingsForm.ShowDialog();
            };
            this.Controls.Add(btnSettings);

            // دکمه خروج
            Button btnExit = new Button
            {
                Text = "خروج",
                Location = new Point(centerX, centerY + 360),
                Size = new Size(300, 50),
                Font = settingsManager.GetButtonFont(FontStyle.Bold),
                BackColor = Color.Gray,
                ForeColor = Color.White
            };
            ApplyRoundedCorners(btnExit, settingsManager.ButtonCornerRadius);
            btnExit.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnExit);
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
    }
}
