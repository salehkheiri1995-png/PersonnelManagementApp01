using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    /// <summary>
    /// Manages application settings including fonts, colors, and UI theme
    /// Provides event notifications for real-time theme updates across all forms
    /// </summary>
    public class SettingsManager
    {
        private static SettingsManager _instance;
        private readonly string settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppSettings.xml");
        private XDocument settingsDoc;

        // Event: Fired when settings change
        public static event EventHandler ThemeChanged;

        // Font Settings
        private string _primaryFont = "Tahoma";
        private int _primaryFontSize = 11;
        private string _titleFont = "Tahoma";
        private int _titleFontSize = 20;
        private string _buttonFont = "Tahoma";
        private int _buttonFontSize = 12;

        public string PrimaryFont
        {
            get => _primaryFont;
            set
            {
                if (_primaryFont != value)
                {
                    _primaryFont = value;
                    RaiseThemeChanged();
                }
            }
        }

        public int PrimaryFontSize
        {
            get => _primaryFontSize;
            set
            {
                if (_primaryFontSize != value)
                {
                    _primaryFontSize = value;
                    RaiseThemeChanged();
                }
            }
        }

        public string TitleFont
        {
            get => _titleFont;
            set
            {
                if (_titleFont != value)
                {
                    _titleFont = value;
                    RaiseThemeChanged();
                }
            }
        }

        public int TitleFontSize
        {
            get => _titleFontSize;
            set
            {
                if (_titleFontSize != value)
                {
                    _titleFontSize = value;
                    RaiseThemeChanged();
                }
            }
        }

        public string ButtonFont
        {
            get => _buttonFont;
            set
            {
                if (_buttonFont != value)
                {
                    _buttonFont = value;
                    RaiseThemeChanged();
                }
            }
        }

        public int ButtonFontSize
        {
            get => _buttonFontSize;
            set
            {
                if (_buttonFontSize != value)
                {
                    _buttonFontSize = value;
                    RaiseThemeChanged();
                }
            }
        }

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
        private Color _primaryColor = Color.FromArgb(33, 128, 141);
        private Color _secondaryColor = Color.FromArgb(94, 82, 64);
        private Color _backgroundColor = Color.FromArgb(240, 248, 255);
        private Color _textColor = Color.FromArgb(19, 52, 59);
        private Color _buttonAddColor = Color.LightBlue;
        private Color _buttonEditColor = Color.LightGreen;
        private Color _buttonDeleteColor = Color.LightCoral;
        private Color _buttonSearchColor = Color.Orange;
        private Color _buttonAnalyticsColor = Color.SteelBlue;
        private Color _buttonSettingsColor = Color.MediumPurple;

        public Color PrimaryColor
        {
            get => _primaryColor;
            set
            {
                if (_primaryColor != value)
                {
                    _primaryColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color SecondaryColor
        {
            get => _secondaryColor;
            set
            {
                if (_secondaryColor != value)
                {
                    _secondaryColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color TextColor
        {
            get => _textColor;
            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color ButtonAddColor
        {
            get => _buttonAddColor;
            set
            {
                if (_buttonAddColor != value)
                {
                    _buttonAddColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color ButtonEditColor
        {
            get => _buttonEditColor;
            set
            {
                if (_buttonEditColor != value)
                {
                    _buttonEditColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color ButtonDeleteColor
        {
            get => _buttonDeleteColor;
            set
            {
                if (_buttonDeleteColor != value)
                {
                    _buttonDeleteColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color ButtonSearchColor
        {
            get => _buttonSearchColor;
            set
            {
                if (_buttonSearchColor != value)
                {
                    _buttonSearchColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color ButtonAnalyticsColor
        {
            get => _buttonAnalyticsColor;
            set
            {
                if (_buttonAnalyticsColor != value)
                {
                    _buttonAnalyticsColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        public Color ButtonSettingsColor
        {
            get => _buttonSettingsColor;
            set
            {
                if (_buttonSettingsColor != value)
                {
                    _buttonSettingsColor = value;
                    RaiseThemeChanged();
                }
            }
        }

        // UI Settings
        private int _buttonCornerRadius = 15;
        private bool _rightToLeftLayout = true;
        private bool _enableDarkMode = false;

        public int ButtonCornerRadius
        {
            get => _buttonCornerRadius;
            set
            {
                if (_buttonCornerRadius != value)
                {
                    _buttonCornerRadius = value;
                    RaiseThemeChanged();
                }
            }
        }

        public bool RightToLeftLayout
        {
            get => _rightToLeftLayout;
            set
            {
                if (_rightToLeftLayout != value)
                {
                    _rightToLeftLayout = value;
                    RaiseThemeChanged();
                }
            }
        }

        public bool EnableDarkMode
        {
            get => _enableDarkMode;
            set
            {
                if (_enableDarkMode != value)
                {
                    _enableDarkMode = value;
                    RaiseThemeChanged();
                }
            }
        }

        // Singleton Pattern
        public static SettingsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SettingsManager();
                }
                return _instance;
            }
        }

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
                MessageBox.Show($"خطا در بارگذاری تنظیمات: {ex.Message}\nUsing defaults.",
                                "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    _primaryFont = fonts.Element("PrimaryFont")?.Value ?? _primaryFont;
                    _primaryFontSize = int.TryParse(fonts.Element("PrimaryFontSize")?.Value, out var pfs) ? pfs : _primaryFontSize;
                    _titleFont = fonts.Element("TitleFont")?.Value ?? _titleFont;
                    _titleFontSize = int.TryParse(fonts.Element("TitleFontSize")?.Value, out var tfs) ? tfs : _titleFontSize;
                    _buttonFont = fonts.Element("ButtonFont")?.Value ?? _buttonFont;
                    _buttonFontSize = int.TryParse(fonts.Element("ButtonFontSize")?.Value, out var bfs) ? bfs : _buttonFontSize;
                }

                // Color Settings
                var colors = root.Element("Colors");
                if (colors != null)
                {
                    _primaryColor = ParseColor(colors.Element("PrimaryColor")?.Value, _primaryColor);
                    _secondaryColor = ParseColor(colors.Element("SecondaryColor")?.Value, _secondaryColor);
                    _backgroundColor = ParseColor(colors.Element("BackgroundColor")?.Value, _backgroundColor);
                    _textColor = ParseColor(colors.Element("TextColor")?.Value, _textColor);
                    _buttonAddColor = ParseColor(colors.Element("ButtonAddColor")?.Value, _buttonAddColor);
                    _buttonEditColor = ParseColor(colors.Element("ButtonEditColor")?.Value, _buttonEditColor);
                    _buttonDeleteColor = ParseColor(colors.Element("ButtonDeleteColor")?.Value, _buttonDeleteColor);
                    _buttonSearchColor = ParseColor(colors.Element("ButtonSearchColor")?.Value, _buttonSearchColor);
                    _buttonAnalyticsColor = ParseColor(colors.Element("ButtonAnalyticsColor")?.Value, _buttonAnalyticsColor);
                    _buttonSettingsColor = ParseColor(colors.Element("ButtonSettingsColor")?.Value, _buttonSettingsColor);
                }

                // UI Settings
                var ui = root.Element("UI");
                if (ui != null)
                {
                    _buttonCornerRadius = int.TryParse(ui.Element("ButtonCornerRadius")?.Value, out var bcr) ? bcr : _buttonCornerRadius;
                    _rightToLeftLayout = bool.TryParse(ui.Element("RightToLeftLayout")?.Value, out var rtl) ? rtl : _rightToLeftLayout;
                    _enableDarkMode = bool.TryParse(ui.Element("EnableDarkMode")?.Value, out var dm) ? dm : _enableDarkMode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در تجزیه تنظیمات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        new XElement("PrimaryFont", _primaryFont),
                        new XElement("PrimaryFontSize", _primaryFontSize),
                        new XElement("TitleFont", _titleFont),
                        new XElement("TitleFontSize", _titleFontSize),
                        new XElement("ButtonFont", _buttonFont),
                        new XElement("ButtonFontSize", _buttonFontSize)
                    ),
                    new XElement("Colors",
                        new XElement("PrimaryColor", ColorToString(_primaryColor)),
                        new XElement("SecondaryColor", ColorToString(_secondaryColor)),
                        new XElement("BackgroundColor", ColorToString(_backgroundColor)),
                        new XElement("TextColor", ColorToString(_textColor)),
                        new XElement("ButtonAddColor", ColorToString(_buttonAddColor)),
                        new XElement("ButtonEditColor", ColorToString(_buttonEditColor)),
                        new XElement("ButtonDeleteColor", ColorToString(_buttonDeleteColor)),
                        new XElement("ButtonSearchColor", ColorToString(_buttonSearchColor)),
                        new XElement("ButtonAnalyticsColor", ColorToString(_buttonAnalyticsColor)),
                        new XElement("ButtonSettingsColor", ColorToString(_buttonSettingsColor))
                    ),
                    new XElement("UI",
                        new XElement("ButtonCornerRadius", _buttonCornerRadius),
                        new XElement("RightToLeftLayout", _rightToLeftLayout),
                        new XElement("EnableDarkMode", _enableDarkMode)
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
                        new XElement("PrimaryFont", _primaryFont),
                        new XElement("PrimaryFontSize", _primaryFontSize),
                        new XElement("TitleFont", _titleFont),
                        new XElement("TitleFontSize", _titleFontSize),
                        new XElement("ButtonFont", _buttonFont),
                        new XElement("ButtonFontSize", _buttonFontSize)
                    ),
                    new XElement("Colors",
                        new XElement("PrimaryColor", ColorToString(_primaryColor)),
                        new XElement("SecondaryColor", ColorToString(_secondaryColor)),
                        new XElement("BackgroundColor", ColorToString(_backgroundColor)),
                        new XElement("TextColor", ColorToString(_textColor)),
                        new XElement("ButtonAddColor", ColorToString(_buttonAddColor)),
                        new XElement("ButtonEditColor", ColorToString(_buttonEditColor)),
                        new XElement("ButtonDeleteColor", ColorToString(_buttonDeleteColor)),
                        new XElement("ButtonSearchColor", ColorToString(_buttonSearchColor)),
                        new XElement("ButtonAnalyticsColor", ColorToString(_buttonAnalyticsColor)),
                        new XElement("ButtonSettingsColor", ColorToString(_buttonSettingsColor))
                    ),
                    new XElement("UI",
                        new XElement("ButtonCornerRadius", _buttonCornerRadius),
                        new XElement("RightToLeftLayout", _rightToLeftLayout),
                        new XElement("EnableDarkMode", _enableDarkMode)
                    )
                );

                new XDocument(root).Save(settingsFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره تنظیمات: {ex.Message}",
                                "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset to default settings
        /// </summary>
        public void ResetToDefaults()
        {
            _primaryFont = "Tahoma";
            _primaryFontSize = 11;
            _titleFont = "Tahoma";
            _titleFontSize = 20;
            _buttonFont = "Tahoma";
            _buttonFontSize = 12;

            _primaryColor = Color.FromArgb(33, 128, 141);
            _secondaryColor = Color.FromArgb(94, 82, 64);
            _backgroundColor = Color.FromArgb(240, 248, 255);
            _textColor = Color.FromArgb(19, 52, 59);
            _buttonAddColor = Color.LightBlue;
            _buttonEditColor = Color.LightGreen;
            _buttonDeleteColor = Color.LightCoral;
            _buttonSearchColor = Color.Orange;
            _buttonAnalyticsColor = Color.SteelBlue;
            _buttonSettingsColor = Color.MediumPurple;

            _buttonCornerRadius = 15;
            _rightToLeftLayout = true;
            _enableDarkMode = false;

            SaveSettings();
            RaiseThemeChanged();
        }

        /// <summary>
        /// Raise theme changed event to notify all forms
        /// </summary>
        private void RaiseThemeChanged()
        {
            ThemeChanged?.Invoke(this, EventArgs.Empty);
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
                return new Font(_primaryFont, _primaryFontSize, style);
            }
            catch
            {
                return new Font("Tahoma", _primaryFontSize, style);
            }
        }

        public Font GetTitleFont(FontStyle style = FontStyle.Bold)
        {
            try
            {
                return new Font(_titleFont, _titleFontSize, style);
            }
            catch
            {
                return new Font("Tahoma", _titleFontSize, style);
            }
        }

        public Font GetButtonFont(FontStyle style = FontStyle.Bold)
        {
            try
            {
                return new Font(_buttonFont, _buttonFontSize, style);
            }
            catch
            {
                return new Font("Tahoma", _buttonFontSize, style);
            }
        }
    }
}
