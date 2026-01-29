using System;
using System.Drawing;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    /// <summary>
    /// Manages application settings including fonts, colors, and UI theme
    /// </summary>
    public class SettingsManager
    {
        private readonly string settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppSettings.xml");
        private XDocument settingsDoc;

        // Font Settings
        public string PrimaryFont { get; set; } = "Tahoma";
        public int PrimaryFontSize { get; set; } = 11;
        public string TitleFont { get; set; } = "Tahoma";
        public int TitleFontSize { get; set; } = 20;
        public string ButtonFont { get; set; } = "Tahoma";
        public int ButtonFontSize { get; set; } = 12;

        // Persian Fonts (Recommended)
        public string[] PersianFonts = new string[]
        {
            "Tahoma",
            "B Nazanin",
            "B Mitra",
            "B Titr",
            "Segoe UI",
            "Arial",
            "Verdana",
            "Times New Roman"
        };

        // Color Settings
        public Color PrimaryColor { get; set; } = Color.FromArgb(33, 128, 141); // Teal
        public Color SecondaryColor { get; set; } = Color.FromArgb(94, 82, 64); // Brown
        public Color BackgroundColor { get; set; } = Color.FromArgb(240, 248, 255); // AliceBlue
        public Color TextColor { get; set; } = Color.FromArgb(19, 52, 59); // Dark Slate
        public Color ButtonAddColor { get; set; } = Color.LightBlue;
        public Color ButtonEditColor { get; set; } = Color.LightGreen;
        public Color ButtonDeleteColor { get; set; } = Color.LightCoral;
        public Color ButtonSearchColor { get; set; } = Color.Orange;
        public Color ButtonAnalyticsColor { get; set; } = Color.SteelBlue;
        public Color ButtonSettingsColor { get; set; } = Color.MediumPurple;

        // UI Settings
        public int ButtonCornerRadius { get; set; } = 15;
        public bool RightToLeftLayout { get; set; } = true;
        public bool EnableDarkMode { get; set; } = false;

        public SettingsManager()
        {
            LoadSettings();
        }

        /// <summary>
        /// Load settings from XML file or create default
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                if (File.Exists(settingsFile))
                {
                    settingsDoc = XDocument.Load(settingsFile);
                    ParseSettings();
                }
                else
                {
                    CreateDefaultSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}\nUsing defaults.",
                                "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CreateDefaultSettings();
            }
        }

        /// <summary>
        /// Parse XML settings into properties
        /// </summary>
        private void ParseSettings()
        {
            try
            {
                var root = settingsDoc.Root;
                if (root == null) return;

                // Font Settings
                var fonts = root.Element("Fonts");
                if (fonts != null)
                {
                    PrimaryFont = fonts.Element("PrimaryFont")?.Value ?? PrimaryFont;
                    PrimaryFontSize = int.TryParse(fonts.Element("PrimaryFontSize")?.Value, out var pfs) ? pfs : PrimaryFontSize;
                    TitleFont = fonts.Element("TitleFont")?.Value ?? TitleFont;
                    TitleFontSize = int.TryParse(fonts.Element("TitleFontSize")?.Value, out var tfs) ? tfs : TitleFontSize;
                    ButtonFont = fonts.Element("ButtonFont")?.Value ?? ButtonFont;
                    ButtonFontSize = int.TryParse(fonts.Element("ButtonFontSize")?.Value, out var bfs) ? bfs : ButtonFontSize;
                }

                // Color Settings
                var colors = root.Element("Colors");
                if (colors != null)
                {
                    PrimaryColor = ParseColor(colors.Element("PrimaryColor")?.Value, PrimaryColor);
                    SecondaryColor = ParseColor(colors.Element("SecondaryColor")?.Value, SecondaryColor);
                    BackgroundColor = ParseColor(colors.Element("BackgroundColor")?.Value, BackgroundColor);
                    TextColor = ParseColor(colors.Element("TextColor")?.Value, TextColor);
                    ButtonAddColor = ParseColor(colors.Element("ButtonAddColor")?.Value, ButtonAddColor);
                    ButtonEditColor = ParseColor(colors.Element("ButtonEditColor")?.Value, ButtonEditColor);
                    ButtonDeleteColor = ParseColor(colors.Element("ButtonDeleteColor")?.Value, ButtonDeleteColor);
                    ButtonSearchColor = ParseColor(colors.Element("ButtonSearchColor")?.Value, ButtonSearchColor);
                    ButtonAnalyticsColor = ParseColor(colors.Element("ButtonAnalyticsColor")?.Value, ButtonAnalyticsColor);
                    ButtonSettingsColor = ParseColor(colors.Element("ButtonSettingsColor")?.Value, ButtonSettingsColor);
                }

                // UI Settings
                var ui = root.Element("UI");
                if (ui != null)
                {
                    ButtonCornerRadius = int.TryParse(ui.Element("ButtonCornerRadius")?.Value, out var bcr) ? bcr : ButtonCornerRadius;
                    RightToLeftLayout = bool.TryParse(ui.Element("RightToLeftLayout")?.Value, out var rtl) ? rtl : RightToLeftLayout;
                    EnableDarkMode = bool.TryParse(ui.Element("EnableDarkMode")?.Value, out var dm) ? dm : EnableDarkMode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing settings: {ex.Message}", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Create and save default settings
        /// </summary>
        private void CreateDefaultSettings()
        {
            settingsDoc = new XDocument(
                new XElement("AppSettings",
                    new XElement("Fonts",
                        new XElement("PrimaryFont", PrimaryFont),
                        new XElement("PrimaryFontSize", PrimaryFontSize),
                        new XElement("TitleFont", TitleFont),
                        new XElement("TitleFontSize", TitleFontSize),
                        new XElement("ButtonFont", ButtonFont),
                        new XElement("ButtonFontSize", ButtonFontSize)
                    ),
                    new XElement("Colors",
                        new XElement("PrimaryColor", ColorToString(PrimaryColor)),
                        new XElement("SecondaryColor", ColorToString(SecondaryColor)),
                        new XElement("BackgroundColor", ColorToString(BackgroundColor)),
                        new XElement("TextColor", ColorToString(TextColor)),
                        new XElement("ButtonAddColor", ColorToString(ButtonAddColor)),
                        new XElement("ButtonEditColor", ColorToString(ButtonEditColor)),
                        new XElement("ButtonDeleteColor", ColorToString(ButtonDeleteColor)),
                        new XElement("ButtonSearchColor", ColorToString(ButtonSearchColor)),
                        new XElement("ButtonAnalyticsColor", ColorToString(ButtonAnalyticsColor)),
                        new XElement("ButtonSettingsColor", ColorToString(ButtonSettingsColor))
                    ),
                    new XElement("UI",
                        new XElement("ButtonCornerRadius", ButtonCornerRadius),
                        new XElement("RightToLeftLayout", RightToLeftLayout),
                        new XElement("EnableDarkMode", EnableDarkMode)
                    )
                )
            );
            SaveSettings();
        }

        /// <summary>
        /// Save current settings to XML file
        /// </summary>
        public void SaveSettings()
        {
            try
            {
                var root = new XElement("AppSettings",
                    new XElement("Fonts",
                        new XElement("PrimaryFont", PrimaryFont),
                        new XElement("PrimaryFontSize", PrimaryFontSize),
                        new XElement("TitleFont", TitleFont),
                        new XElement("TitleFontSize", TitleFontSize),
                        new XElement("ButtonFont", ButtonFont),
                        new XElement("ButtonFontSize", ButtonFontSize)
                    ),
                    new XElement("Colors",
                        new XElement("PrimaryColor", ColorToString(PrimaryColor)),
                        new XElement("SecondaryColor", ColorToString(SecondaryColor)),
                        new XElement("BackgroundColor", ColorToString(BackgroundColor)),
                        new XElement("TextColor", ColorToString(TextColor)),
                        new XElement("ButtonAddColor", ColorToString(ButtonAddColor)),
                        new XElement("ButtonEditColor", ColorToString(ButtonEditColor)),
                        new XElement("ButtonDeleteColor", ColorToString(ButtonDeleteColor)),
                        new XElement("ButtonSearchColor", ColorToString(ButtonSearchColor)),
                        new XElement("ButtonAnalyticsColor", ColorToString(ButtonAnalyticsColor)),
                        new XElement("ButtonSettingsColor", ColorToString(ButtonSettingsColor))
                    ),
                    new XElement("UI",
                        new XElement("ButtonCornerRadius", ButtonCornerRadius),
                        new XElement("RightToLeftLayout", RightToLeftLayout),
                        new XElement("EnableDarkMode", EnableDarkMode)
                    )
                );

                new XDocument(root).Save(settingsFile);
                MessageBox.Show("تنظیمات با موفقیت ذخیره شدند.\n(Settings saved successfully.)",
                                "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره تنظیمات: {ex.Message}\n(Error saving settings: {ex.Message})",
                                "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset to default settings
        /// </summary>
        public void ResetToDefaults()
        {
            PrimaryFont = "Tahoma";
            PrimaryFontSize = 11;
            TitleFont = "Tahoma";
            TitleFontSize = 20;
            ButtonFont = "Tahoma";
            ButtonFontSize = 12;

            PrimaryColor = Color.FromArgb(33, 128, 141);
            SecondaryColor = Color.FromArgb(94, 82, 64);
            BackgroundColor = Color.FromArgb(240, 248, 255);
            TextColor = Color.FromArgb(19, 52, 59);
            ButtonAddColor = Color.LightBlue;
            ButtonEditColor = Color.LightGreen;
            ButtonDeleteColor = Color.LightCoral;
            ButtonSearchColor = Color.Orange;
            ButtonAnalyticsColor = Color.SteelBlue;
            ButtonSettingsColor = Color.MediumPurple;

            ButtonCornerRadius = 15;
            RightToLeftLayout = true;
            EnableDarkMode = false;

            SaveSettings();
        }

        /// <summary>
        /// Convert Color to RGB string format
        /// </summary>
        private string ColorToString(Color color)
        {
            return $"{color.R},{color.G},{color.B}";
        }

        /// <summary>
        /// Parse RGB string to Color
        /// </summary>
        private Color ParseColor(string colorString, Color defaultColor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(colorString))
                    return defaultColor;

                var parts = colorString.Split(',');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int r) &&
                    int.TryParse(parts[1], out int g) &&
                    int.TryParse(parts[2], out int b))
                {
                    return Color.FromArgb(r, g, b);
                }
            }
            catch { }

            return defaultColor;
        }

        /// <summary>
        /// Get font with current settings
        /// </summary>
        public Font GetPrimaryFont(FontStyle style = FontStyle.Regular)
        {
            try
            {
                return new Font(PrimaryFont, PrimaryFontSize, style);
            }
            catch
            {
                return new Font("Tahoma", PrimaryFontSize, style);
            }
        }

        public Font GetTitleFont(FontStyle style = FontStyle.Bold)
        {
            try
            {
                return new Font(TitleFont, TitleFontSize, style);
            }
            catch
            {
                return new Font("Tahoma", TitleFontSize, style);
            }
        }

        public Font GetButtonFont(FontStyle style = FontStyle.Bold)
        {
            try
            {
                return new Font(ButtonFont, ButtonFontSize, style);
            }
            catch
            {
                return new Font("Tahoma", ButtonFontSize, style);
            }
        }
    }
}
