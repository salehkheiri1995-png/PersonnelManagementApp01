using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    /// <summary>
    /// Base class for all forms that supports real-time theme changes
    /// All custom forms should inherit from this class
    /// </summary>
    public class BaseThemedForm : Form
    {
        protected SettingsManager settingsManager = SettingsManager.Instance;
        protected List<Control> themedControls = new List<Control>();

        public BaseThemedForm()
        {
            // Subscribe to theme changes
            SettingsManager.ThemeChanged += OnThemeChanged;
            ApplyInitialTheme();
        }

        /// <summary>
        /// Apply initial theme when form loads
        /// </summary>
        protected virtual void ApplyInitialTheme()
        {
            this.BackColor = settingsManager.BackgroundColor;
            this.RightToLeft = settingsManager.RightToLeftLayout ? RightToLeft.Yes : RightToLeft.No;
            this.ForeColor = settingsManager.TextColor;
            // NOTE: Do NOT override form font - let each form set its own
        }

        /// <summary>
        /// Called when theme settings change - override in derived classes
        /// </summary>
        protected virtual void OnThemeChanged(object sender, EventArgs e)
        {
            ApplyInitialTheme();
            RefreshAllControls();
        }

        /// <summary>
        /// Refresh all controls to apply new theme
        /// </summary>
        protected virtual void RefreshAllControls()
        {
            foreach (Control control in themedControls)
            {
                ApplyThemeToControl(control);
            }
        }

        /// <summary>
        /// Apply theme to a specific control
        /// IMPORTANT: This only applies COLORS and STYLES, not fonts
        /// Fonts are set in InitializeComponent and should NOT be changed
        /// </summary>
        protected virtual void ApplyThemeToControl(Control control)
        {
            if (control is Button btn)
            {
                ApplyButtonTheme(btn);
            }
            else if (control is Label lbl)
            {
                // Only apply color, NOT font
                lbl.ForeColor = settingsManager.TextColor;
            }
            else if (control is TextBox || control is ComboBox || control is ListBox || control is CheckedListBox)
            {
                // Only apply color, NOT font
                control.ForeColor = settingsManager.TextColor;
                control.BackColor = Color.White;
            }
            else if (control is DataGridView dgv)
            {
                // Only apply color, NOT font
                dgv.ForeColor = settingsManager.TextColor;
                dgv.BackgroundColor = settingsManager.BackgroundColor;
            }
        }

        /// <summary>
        /// Apply button theme based on button name/tag
        /// Changes only colors and style, keeps the font from InitializeComponent
        /// </summary>
        protected virtual void ApplyButtonTheme(Button btn)
        {
            // Keep the button's existing font - do NOT change it
            btn.ForeColor = Color.White;

            // Determine button color based on name or tag
            if (btn.Name.Contains("Add") || btn.Text.Contains("ثبت"))
                btn.BackColor = settingsManager.ButtonAddColor;
            else if (btn.Name.Contains("Edit") || btn.Text.Contains("ویرایش"))
                btn.BackColor = settingsManager.ButtonEditColor;
            else if (btn.Name.Contains("Delete") || btn.Text.Contains("حذف"))
                btn.BackColor = settingsManager.ButtonDeleteColor;
            else if (btn.Name.Contains("Search") || btn.Text.Contains("جستجو"))
                btn.BackColor = settingsManager.ButtonSearchColor;
            else if (btn.Name.Contains("Analytics") || btn.Text.Contains("تحلیل"))
                btn.BackColor = settingsManager.ButtonAnalyticsColor;
            else if (btn.Name.Contains("Settings") || btn.Text.Contains("تنظیمات"))
                btn.BackColor = settingsManager.ButtonSettingsColor;
            else if (btn.Name.Contains("Save") || btn.Text.Contains("ذخیره"))
                btn.BackColor = Color.LightGreen;
            else if (btn.Name.Contains("Cancel") || btn.Text.Contains("بازگشت"))
                btn.BackColor = Color.LightCoral;
            else if (btn.Name.Contains("Reset") || btn.Text.Contains("بازتنظیم"))
                btn.BackColor = Color.Orange;
            else
                btn.BackColor = settingsManager.PrimaryColor;

            ApplyRoundedCorners(btn, settingsManager.ButtonCornerRadius);
        }

        /// <summary>
        /// Apply rounded corners to a control
        /// </summary>
        protected void ApplyRoundedCorners(Control control, int radius)
        {
            if (radius <= 0) return;

            try
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
            catch { /* Ignore if region can't be applied */ }
        }

        /// <summary>
        /// Register a control for theme updates
        /// Only colors and styles will be updated, not fonts
        /// </summary>
        protected void RegisterThemedControl(Control control)
        {
            if (!themedControls.Contains(control))
            {
                themedControls.Add(control);
            }
        }

        /// <summary>
        /// Register multiple controls for theme updates
        /// </summary>
        protected void RegisterThemedControls(params Control[] controls)
        {
            foreach (var control in controls)
            {
                RegisterThemedControl(control);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unsubscribe from theme changes
                SettingsManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
}
