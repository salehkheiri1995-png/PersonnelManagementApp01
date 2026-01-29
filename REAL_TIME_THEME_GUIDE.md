# Real-Time Theme Updates - Implementation Guide

## Overview

تمام فرم‌های برنامه اکنون می‌توانند **Real-Time** تغییرات تنظیمات را اعمال کنند بدون نیاز به بسته و دوباره باز کردن برنامه!

---

## Architecture

### 🏗️ ساختار

```
SettingsManager (Singleton)
        ↓
   ThemeChanged Event
        ↓
BaseThemedForm (Base Class)
        ↓
   All Custom Forms
   ├─ FormPersonnelRegister
   ├─ FormPersonnelEdit
   ├─ FormPersonnelDelete
   ├─ FormPersonnelSearch
   ├─ FormPersonnelAnalytics
   └─ FormSettings
```

---

## Components

### 1. SettingsManager (Singleton Pattern)

**فایل:** `SettingsManager.cs`

**ویژگی‌ها:**
- ✅ Singleton instance (یک نسخه برای کل برنامه)
- ✅ Event system: `ThemeChanged` event
- ✅ Property change detection
- ✅ Automatic event raising

**استفاده:**
```csharp
// Get singleton instance
var settings = SettingsManager.Instance;

// Subscribe to theme changes
SettingsManager.ThemeChanged += MyForm_OnThemeChanged;

// Change property (automatically raises event)
settings.PrimaryFont = "B Nazanin";
```

### 2. BaseThemedForm (Base Class)

**فایل:** `BaseThemedForm.cs`

**مسئولیت‌ها:**
- ✅ Subscribe to `ThemeChanged` event
- ✅ Apply theme to form on load
- ✅ Refresh all controls when theme changes
- ✅ Apply theme-specific logic to controls
- ✅ Unsubscribe from events on dispose

**Override شده methods:**
```csharp
protected virtual void ApplyInitialTheme()      // Load theme on form start
protected virtual void OnThemeChanged()           // React to theme changes
protected virtual void RefreshAllControls()       // Update all controls
protected virtual void ApplyThemeToControl()      // Apply theme to single control
```

**Helper methods:**
```csharp
protected void RegisterThemedControl(Control control)              // Register one control
protected void RegisterThemedControls(params Control[] controls)   // Register multiple
protected void ApplyButtonTheme(Button btn)                        // Auto-color buttons
protected void ApplyRoundedCorners(Control control, int radius)   // Corner radius
```

---

## How to Implement in Your Forms

### Step 1: Change Inheritance

**Before:**
```csharp
public partial class FormPersonnelRegister : Form
{
    // ...
}
```

**After:**
```csharp
public partial class FormPersonnelRegister : BaseThemedForm
{
    // ... inherits all theme support automatically
}
```

### Step 2: Initialize SettingsManager

**Before:**
```csharp
public FormPersonnelRegister()
{
    InitializeComponent();
}
```

**After:**
```csharp
// No change needed! BaseThemedForm handles it
public FormPersonnelRegister()
{
    InitializeComponent();
}
```

**Why?** BaseThemedForm constructor:
- ✅ Automatically subscribes to `ThemeChanged` event
- ✅ Applies initial theme on form load
- ✅ No manual initialization needed

### Step 3: Register Themed Controls

After creating controls, register them:

```csharp
private void InitializeComponent()
{
    // ... create controls ...

    Button btnSubmit = new Button { /* ... */ };
    Label lblName = new Label { /* ... */ };
    TextBox txtName = new TextBox { /* ... */ };
    DataGridView dgvData = new DataGridView { /* ... */ };

    // Register individual controls
    RegisterThemedControl(btnSubmit);
    RegisterThemedControl(lblName);
    
    // Or register multiple at once
    RegisterThemedControls(txtName, dgvData);

    // Add to form
    this.Controls.Add(btnSubmit);
    this.Controls.Add(lblName);
    // ... etc
}
```

### Step 4: Button Naming Convention (Optional)

Buttons are automatically colored based on their name:

```csharp
Button btn = new Button
{
    Name = "btnAdd",        // → ButtonAddColor
    // OR
    Name = "btnEdit",       // → ButtonEditColor
    Name = "btnDelete",     // → ButtonDeleteColor
    Name = "btnSearch",     // → ButtonSearchColor
    Name = "btnSave",       // → Color.LightGreen
    Name = "btnCancel",     // → Color.LightCoral
};
```

---

## Theme Change Flow

### Step-by-step Process

```
1. User changes font size in Settings
   ↓
2. Settings dialog:
   settingsManager.PrimaryFontSize = 14
   ↓
3. SettingsManager property setter:
   if (value changed) → RaiseThemeChanged()
   ↓
4. ThemeChanged event fired:
   SettingsManager.ThemeChanged?.Invoke(this, EventArgs.Empty)
   ↓
5. All subscribed forms receive event:
   OnThemeChanged(object sender, EventArgs e)
   ↓
6. BaseThemedForm responds:
   - ApplyInitialTheme()  → Update form background, font, etc.
   - RefreshAllControls() → Update all registered controls
   ↓
7. Controls update their appearance:
   - Buttons change color and font
   - Labels apply new font
   - TextBoxes apply new font
   - DataGridView applies new theme
   ↓
8. UI updates INSTANTLY on screen ✨
```

---

## Real-Time Examples

### Example 1: Change Primary Font

```
Settings Form:
  User selects "B Nazanin" from dropdown
  Clicks Save

Settings Dialog:
  settingsManager.PrimaryFont = "B Nazanin"
  settingsManager.SaveSettings()
  Close form

All Open Forms:
  ← ThemeChanged event received
  Apply "B Nazanin" to all labels, textboxes, etc.
  UI updates IMMEDIATELY ⚡

Result:
  ✅ FormPersonnelRegister shows new font
  ✅ FormPersonnelSearch shows new font
  ✅ FormPersonnelAnalytics shows new font
  (No need to close and reopen!)
```

### Example 2: Change Button Color

```
Settings Form:
  User clicks color button for "Add"
  Selects new green color
  Clicks Save

All Forms with Add buttons:
  ← ThemeChanged event
  Buttons with name containing "Add" change color
  UI updates in real-time

Result:
  ✅ All "Add" buttons across app are new color
  ✅ Changes take effect immediately
```

### Example 3: Toggle RTL Layout

```
Settings Form:
  User unchecks "Right-to-Left Layout"
  Clicks Save

All Forms:
  ← ThemeChanged event
  this.RightToLeft = RightToLeft.No
  All controls re-layout from left-to-right
  UI flips immediately

Result:
  ✅ All forms switch to LTR instantly
  ✅ Text direction changes
  ✅ Alignment adjusts automatically
```

---

## Migration Checklist

### For Each Form That Needs Theme Support:

- [ ] Change `public class FormXXX : Form` → `public class FormXXX : BaseThemedForm`
- [ ] Remove manual `InitializeComponent()` calls for basic theme setup
- [ ] **After creating controls**, call `RegisterThemedControl(control)` or `RegisterThemedControls(...)`
- [ ] Follow button naming convention (optional but recommended)
- [ ] Remove `this.BackColor = Color...` (handled by BaseThemedForm)
- [ ] Remove `this.Font = new Font(...)` (handled by BaseThemedForm)
- [ ] Remove `this.RightToLeft = ...` (handled by BaseThemedForm)
- [ ] Test that controls theme properly when you change settings

### Before Migration:
```csharp
public partial class FormPersonnelRegister : Form
{
    public FormPersonnelRegister()
    {
        InitializeComponent();
        this.BackColor = Color.FromArgb(240, 248, 255);
        this.Font = new Font("Tahoma", 11);
        this.RightToLeft = RightToLeft.Yes;
    }
}
```

### After Migration:
```csharp
public partial class FormPersonnelRegister : BaseThemedForm
{
    public FormPersonnelRegister()
    {
        InitializeComponent();
        // Theme is automatically applied by BaseThemedForm!
    }

    private void InitializeComponent()
    {
        // ... create controls ...
        
        Button btnAdd = new Button { /* ... */ };
        RegisterThemedControl(btnAdd);
        this.Controls.Add(btnAdd);
        
        // More controls...
    }
}
```

---

## Testing Theme Changes

### Manual Testing Steps

1. **Start Application**
   ```
   F5 (Debug)
   Application launches
   ```

2. **Open Multiple Forms**
   ```
   Click "ثبت پرسنل جدید" (Register)
   Click "جستجو" (Search)
   Click "تحلیل" (Analytics)
   Leave all 3 forms open
   ```

3. **Change Theme Settings**
   ```
   Click "تنظیمات" (Settings) on main menu
   Change font to "B Nazanin"
   Change button Add color to bright red
   Change primary font size to 14
   Click "ذخیره" (Save)
   Settings dialog closes
   ```

4. **Verify Real-Time Updates**
   ```
   ✅ FormPersonnelRegister shows new font immediately
   ✅ Add button is now red
   ✅ All text is larger (14pt)
   
   ✅ FormPersonnelSearch shows same changes
   ✅ FormPersonnelAnalytics shows same changes
   
   ✅ Main menu also updated
   ```

### Automated Testing (Optional)

```csharp
[TestMethod]
public void TestRealTimeThemeChange()
{
    // Open form
    var form = new FormPersonnelRegister();
    form.Show();

    // Change theme
    SettingsManager.Instance.PrimaryFont = "B Nazanin";

    // Wait for event
    System.Threading.Thread.Sleep(100);

    // Verify form updated
    Assert.AreEqual("B Nazanin", form.Font.Name);
}
```

---

## Common Issues & Solutions

### Issue 1: Controls Not Updating

**Problem:** Changed settings but controls look the same

**Solution:** Make sure you registered the control:
```csharp
// ❌ Wrong - Control not registered
Button btn = new Button { /* ... */ };
this.Controls.Add(btn);

// ✅ Correct - Control registered
Button btn = new Button { /* ... */ };
RegisterThemedControl(btn);
this.Controls.Add(btn);
```

### Issue 2: Theme Not Applied on Load

**Problem:** Form doesn't have correct theme when first opened

**Solution:** Make sure you inherit from BaseThemedForm:
```csharp
// ❌ Wrong
public class MyForm : Form { }

// ✅ Correct
public class MyForm : BaseThemedForm { }
```

### Issue 3: Buttons Have Wrong Colors

**Problem:** Buttons aren't getting the right theme color

**Solution:** Use correct button names or check the naming logic:
```csharp
// ✅ Good - Name matches logic
Button btn = new Button { Name = "btnAdd" };     // Gets ButtonAddColor
Button btn = new Button { Name = "btnEdit" };    // Gets ButtonEditColor

// ⚠️ Check - May not match
Button btn = new Button { Name = "btnCustom" };  // Gets default PrimaryColor
```

### Issue 4: Event Not Firing

**Problem:** Change settings but nothing happens

**Solution:** Check that SettingsManager is using Singleton:
```csharp
// ❌ Wrong - Creates new instance each time
var settings = new SettingsManager();

// ✅ Correct - Uses singleton
var settings = SettingsManager.Instance;
```

---

## Forms to Migrate

### Priority 1 (Must Have)
- [x] MainForm.cs
- [ ] FormPersonnelRegister.cs
- [ ] FormPersonnelEdit.cs
- [ ] FormPersonnelDelete.cs
- [ ] FormPersonnelSearch.cs
- [ ] FormPersonnelAnalytics.cs

### Priority 2 (Nice to Have)
- [ ] Any other custom dialogs
- [ ] Any other forms in project

---

## Code Examples

### Complete Minimal Example

```csharp
using System;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    public partial class FormPersonnelRegister : BaseThemedForm  // ← Inherit from BaseThemedForm
    {
        public FormPersonnelRegister()
        {
            InitializeComponent();
            // Theme is automatically applied!
        }

        private void InitializeComponent()
        {
            this.Text = "ثبت پرسنل جدید";
            this.Size = new System.Drawing.Size(600, 400);

            // Create controls
            Label lblName = new Label { Text = "نام:", Location = new System.Drawing.Point(10, 10) };
            TextBox txtName = new TextBox { Location = new System.Drawing.Point(10, 35) };
            Button btnSave = new Button { Text = "ذخیره", Name = "btnSave", Location = new System.Drawing.Point(10, 100) };

            // Register them
            RegisterThemedControls(lblName, txtName, btnSave);

            // Add to form
            this.Controls.AddRange(new Control[] { lblName, txtName, btnSave });
        }
    }
}
```

### Complete Full Example

See `FormSettings.cs` for full implementation with all features.

---

## Summary

| Feature | Before | After |
|---------|--------|-------|
| Theme changes | Restart app | Real-time ⚡ |
| Font update | Manual | Automatic ✓ |
| Color change | Dialog close needed | Instant ✓ |
| RTL toggle | Restart app | Instant ✓ |
| Code duplication | Much | Minimal ✓ |
| Maintenance | Hard | Easy ✓ |

---

**Last Updated:** January 29, 2026

**Version:** 2.0 - Real-Time Theme System
