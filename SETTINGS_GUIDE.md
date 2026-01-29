# Settings Guide - Theme & Font Customization

## Overview

The Personnel Management Application now includes a comprehensive **Settings** feature that allows you to customize:

- 🔤 **Fonts** - Change primary, title, and button fonts
- 📏 **Font Sizes** - Adjust text size for better readability
- 🎨 **Colors** - Customize button and UI colors
- ⚙️ **UI Options** - Configure button corner radius and layout

---

## Accessing Settings

### From Main Menu

1. **Launch the application**
2. **Click the "تنظیمات" (Settings) button** on the main menu
3. The Settings dialog will open with multiple tabs

---

## Settings Tabs

### 1. **فونت (Fonts) Tab**

Customize all text fonts in the application.

#### Available Options:

**Primary Font (فونت اصلی)**
- Used for: Regular text, labels, data grid
- Recommended fonts:
  - ✅ **Tahoma** (Best for Persian)
  - ✅ **B Nazanin** (Beautiful Persian font)
  - ✅ **B Mitra** (Clean Persian font)
  - ✅ **B Titr** (Bold Persian font)
  - ✅ **Segoe UI** (Modern, multilingual)
  - Arial, Verdana
- Size range: 8pt - 20pt
- Default: Tahoma, 11pt

**Title Font (فونت عنوان)**
- Used for: Main window title, section headers
- Same fonts available as Primary Font
- Size range: 12pt - 32pt
- Default: Tahoma, 20pt
- Recommended: Bold style

**Button Font (فونت دکمه)**
- Used for: All button text
- Same fonts available
- Size range: 10pt - 16pt
- Default: Tahoma, 12pt

#### How to Change Fonts:

1. **Select Font:**
   - Click dropdown menu for font name
   - Choose from available Persian fonts

2. **Adjust Size:**
   - Use spinner buttons (▲▼) to increase/decrease
   - Or click field and type number directly

3. **Apply Changes:**
   - Click "ذخیره (Save)" button
   - Changes take effect immediately

---

### 2. **رنگ (Colors) Tab**

Customize UI and button colors for your preference.

#### Theme Colors:

**Primary Color (رنگ اصلی)**
- Used for: Primary UI elements
- Default: Teal (RGB: 33, 128, 141)

**Secondary Color (رنگ ثانویه)**
- Used for: Secondary UI elements
- Default: Brown (RGB: 94, 82, 64)

**Background Color (رنگ پسزمین)**
- Used for: Form backgrounds
- Default: AliceBlue (RGB: 240, 248, 255)

**Text Color (رنگ متن)**
- Used for: All text content
- Default: Dark Slate (RGB: 19, 52, 59)

#### Button Colors:

| Button | Default Color | RGB Value |
|--------|---------------|----------|
| **Add (ثبت)** | Light Blue | (173, 216, 230) |
| **Edit (ویرایش)** | Light Green | (144, 238, 144) |
| **Delete (حذف)** | Light Coral | (240, 128, 128) |
| **Search (جستجو)** | Orange | (255, 165, 0) |
| **Analytics (تحلیل)** | Steel Blue | (70, 130, 180) |
| **Settings (تنظیمات)** | Medium Purple | (147, 112, 219) |

#### How to Change Colors:

1. **Click Color Button:**
   - Click any colored button next to color name

2. **Choose Color:**
   - Standard Windows color picker opens
   - Select desired color
   - Click OK

3. **Button Background Updates:**
   - Preview shows new color immediately

4. **Save Changes:**
   - Click "ذخیره (Save)" to persist

#### Color Picker Tips:

- **Custom Colors:** Use "تعریف رنگ‌های سفارشی" tab for precise RGB values
- **RGB Values:** Enter exact RGB numbers for consistency
- **Hex Values:** Some apps support HEX (e.g., #FF5733)
- **Test First:** Apply and check before saving

---

### 3. **UI Tab**

Configure visual appearance and layout options.

#### Button Corner Radius (برریسی گوشه دکمه)
- **Purpose:** Makes button corners rounded vs sharp
- **Range:** 0 (sharp) to 30 (very rounded)
- **Default:** 15
- **Recommended:** 10-20 for modern look
- **Note:** Changes visual appearance only

#### Right-to-Left Layout (طرح راست‌به‌چپ)
- **Purpose:** Enable RTL layout for Persian/Arabic text
- **Default:** ☑️ Checked (Enabled)
- **When to disable:** If using English-only mode
- **Note:** Affects menu alignment and text direction

#### Dark Mode (حالت تاریک)
- **Purpose:** Switch to dark theme for reduced eye strain
- **Default:** ☐ Unchecked (Disabled)
- **When to use:** Evening work sessions
- **Note:** Currently placeholder for future implementation

---

## Saving Settings

### Where Settings Are Stored

Settings are saved in an XML file:

```
AppSettings.xml
Location: Application installation directory
Format: Human-readable XML
```

### Example AppSettings.xml:

```xml
<?xml version="1.0" encoding="utf-8"?>
<AppSettings>
  <Fonts>
    <PrimaryFont>B Nazanin</PrimaryFont>
    <PrimaryFontSize>12</PrimaryFontSize>
    <TitleFont>B Titr</TitleFont>
    <TitleFontSize>22</TitleFontSize>
    <ButtonFont>Tahoma</ButtonFont>
    <ButtonFontSize>12</ButtonFontSize>
  </Fonts>
  <Colors>
    <PrimaryColor>33,128,141</PrimaryColor>
    <ButtonAddColor>173,216,230</ButtonAddColor>
    <!-- ... more colors ... -->
  </Colors>
  <UI>
    <ButtonCornerRadius>15</ButtonCornerRadius>
    <RightToLeftLayout>true</RightToLeftLayout>
    <EnableDarkMode>false</EnableDarkMode>
  </UI>
</AppSettings>
```

### How Settings Are Applied

1. **On Application Start:**
   - Reads AppSettings.xml
   - Loads all saved settings
   - Applies to all forms

2. **Changes Take Effect:**
   - After clicking "ذخیره" (Save)
   - Some changes visible immediately
   - Application restart recommended for full effect

3. **If File Missing:**
   - Default settings are used
   - New AppSettings.xml created

---

## Reset to Defaults

### Reset Button

**In Settings Dialog:**
1. Click **"بازتنظیم (Reset)"** button
2. Confirm the action when prompted
3. Settings return to original defaults
4. AppSettings.xml is recreated

### Default Settings

```
Primary Font:     Tahoma, 11pt
Title Font:       Tahoma, 20pt, Bold
Button Font:      Tahoma, 12pt, Bold
Corner Radius:    15px
RTL Layout:       Enabled
Dark Mode:        Disabled
```

---

## Persian Font Recommendations

### Best Fonts for Persian Text

1. **Tahoma** ⭐⭐⭐⭐⭐
   - Default Windows font
   - Excellent Persian support
   - Good at all sizes
   - Pre-installed on most systems

2. **B Nazanin** ⭐⭐⭐⭐⭐
   - Beautiful, elegant font
   - Excellent readability
   - Persian-optimized
   - Slightly larger at same point size

3. **B Mitra** ⭐⭐⭐⭐
   - Modern, clean design
   - Professional appearance
   - Good for data tables

4. **B Titr** ⭐⭐⭐⭐
   - Bold, striking font
   - Perfect for titles
   - Headers stand out well

### Font Pairing Examples

**Professional Look:**
```
Title:    B Titr, 22pt, Bold
Primary:  B Nazanin, 11pt
Button:   Tahoma, 12pt, Bold
```

**Clean & Modern:**
```
Title:    Segoe UI, 20pt, Bold
Primary:  Tahoma, 11pt
Button:   Tahoma, 12pt, Bold
```

**Traditional:**
```
Title:    B Nazanin, 22pt, Bold
Primary:  B Nazanin, 12pt
Button:   B Mitra, 12pt, Bold
```

---

## Color Schemes

### Predefined Schemes (Recommended)

**Light Blue (Default)**
```
Primary:    Teal (33, 128, 141)
Background: AliceBlue (240, 248, 255)
Buttons:    Various pastels
```

**Professional**
```
Primary:    Dark Blue (25, 50, 100)
Background: White (255, 255, 255)
Buttons:    Muted colors
```

**Warm Tones**
```
Primary:    Brown (139, 69, 19)
Background: Cream (245, 245, 220)
Buttons:    Warm colors
```

---

## Troubleshooting

### Font Not Appearing

**Problem:** Selected font doesn't display correctly

**Solutions:**
1. Font may not be installed - choose different font
2. Restart application for full effect
3. Reset to defaults if corrupted
4. Stick with Tahoma (always available)

### Colors Not Saving

**Problem:** Colors revert after restart

**Solutions:**
1. Ensure "ذخیره" (Save) button was clicked
2. Check if AppSettings.xml exists in app folder
3. Verify write permissions to app directory
4. Check Windows Event Log for errors

### Text Too Small/Large

**Problem:** Text size unreadable

**Solutions:**
1. Adjust Primary Font Size
2. Increase to at least 11pt for readability
3. Match Title Font Size (20pt typical)
4. Use B Nazanin font (appears larger at same size)

---

## Advanced Usage

### Manual AppSettings.xml Editing

**For advanced users only:**

1. Navigate to application folder
2. Open AppSettings.xml with text editor
3. Modify RGB values directly:
   ```xml
   <ButtonAddColor>200,150,100</ButtonAddColor>
   ```
4. Save file
5. Restart application

**RGB Value Format:**
```
RGB (Red, Green, Blue)
Each value: 0-255
Example: (255, 0, 0) = Red
```

### Backup Settings

**To backup current settings:**

1. Copy `AppSettings.xml` to safe location
2. Name it `AppSettings_Backup.xml`
3. Can restore by copying back

---

## FAQ

### Q: Do I need to restart the application for changes?

**A:** Some changes (fonts, colors) apply immediately, but full restart is recommended for consistency.

### Q: Can I share settings with others?

**A:** Yes! Copy your `AppSettings.xml` to another user's application folder.

### Q: Will settings survive Windows updates?

**A:** If app folder is preserved, yes. If reinstalling app, backup AppSettings.xml first.

### Q: Can I revert a single setting?

**A:** Use Reset button for all defaults, then reapply specific changes. Or manually edit AppSettings.xml.

### Q: What if font file is corrupted?

**A:** Choose different font. If persistent, reset to defaults.

---

## Support

For issues or suggestions:

1. Check this guide first
2. Verify AppSettings.xml exists
3. Try resetting to defaults
4. Contact support with:
   - Operating System
   - Font being used
   - Exact error message

---

**Last Updated:** January 29, 2026

**Version:** 1.1.0 - Settings Feature
