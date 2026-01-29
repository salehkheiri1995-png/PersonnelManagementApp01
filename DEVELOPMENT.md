# Development Guide - Personnel Management Application

## 🛠️ Development Setup

### Prerequisites

1. **Visual Studio 2019 or Higher**
   - Community, Professional, or Enterprise edition
   - Workloads: Desktop Development with C#

2. **.NET Framework 4.7.2 or Higher**
   - Check: Visual Studio Installer → More → .NET Framework Developer Pack

3. **Microsoft Access Database Engine**
   - 32-bit: Download from [Microsoft](https://www.microsoft.com/en-us/download/details.aspx?id=13255)
   - 64-bit: Download from [Microsoft](https://www.microsoft.com/en-us/download/details.aspx?id=51155)

4. **Git**
   - Command line or GitHub Desktop

### Initial Setup

1. **Clone Repository:**
   ```bash
   git clone https://github.com/salehkheiri1995-png/PersonnelManagementApp01.git
   cd PersonnelManagementApp01
   ```

2. **Open Solution in Visual Studio:**
   ```bash
   start PersonnelManagementApp.sln
   ```

3. **Restore Dependencies:**
   - Visual Studio will automatically restore NuGet packages
   - If not, right-click Solution → Restore NuGet Packages

4. **Build Solution:**
   - Press `Ctrl+Shift+B` or Build → Build Solution
   - Ensure no build errors appear

5. **Run Application:**
   - Press `F5` or Debug → Start Debugging
   - On first run, select database file location

## 📁 Project Structure Deep Dive

### Core Layers

```
UI Layer (Forms)
    ↓
Business Logic Layer (EventHandlers, Validation)
    ↓
Data Access Layer (DbHelper)
    ↓
Database Layer (MS Access)
```

### Adding a New Feature

#### Example: Add "Export to Excel" Feature

1. **Add to DbHelper.cs:**
   ```csharp
   public void ExportToExcel(DataTable dt, string filePath)
   {
       // Implementation using EPPlus or similar
       // Add using statement: using OfficeOpenXml;
   }
   ```

2. **Create Button in FormPersonnelSearch.cs:**
   ```csharp
   Button btnExportExcel = new Button
   {
       Text = "Export to Excel",
       Location = new Point(10, 450),
       Size = new Size(120, 30)
   };
   btnExportExcel.Click += BtnExportExcel_Click;
   this.Controls.Add(btnExportExcel);
   ```

3. **Implement Handler:**
   ```csharp
   private void BtnExportExcel_Click(object sender, EventArgs e)
   {
       SaveFileDialog sfd = new SaveFileDialog
       {
           Filter = "Excel Files (*.xlsx)|*.xlsx",
           DefaultExt = ".xlsx"
       };
       
       if (sfd.ShowDialog() == DialogResult.OK)
       {
           dbHelper.ExportToExcel(gridPersonnel.DataSource as DataTable, sfd.FileName);
       }
   }
   ```

4. **Update GlobalConstants.cs:**
   ```csharp
   public static class SuccessMessages
   {
       public const string ExcelExportSuccessful = "خروجی اکسل با موفقیت انجام شد.";
   }
   ```

5. **Test the feature thoroughly**
6. **Commit changes:**
   ```bash
   git add .
   git commit -m "feat: Add Excel export functionality"
   git push origin feature/excel-export
   ```

## 🧪 Testing

### Manual Testing Checklist

Before committing code, test:

- [ ] Application launches without errors
- [ ] Database connection works
- [ ] All buttons navigate to correct forms
- [ ] New/Edit/Delete operations complete
- [ ] Search returns correct results
- [ ] Data validation works
- [ ] Error messages display properly
- [ ] Data persists after restart
- [ ] RTL layout displays correctly
- [ ] No memory leaks (check Task Manager)

### Database Testing

1. **Test with Empty Database:**
   - Run with fresh database
   - Verify error handling

2. **Test with Large Dataset:**
   - Add 1000+ records
   - Check performance
   - Verify search speed

3. **Concurrent Access:**
   - Open multiple instances
   - Verify no corruption

## 📝 Code Style Guidelines

### Naming Conventions

```csharp
// Classes
public class FormPersonnelRegister

// Methods
public void ExecuteQuery()
private void InitializeComponent()

// Variables
string firstName;
int personnelCount;
DataTable dtPersonnel;

// Constants
public const string DatabaseNotFound = "...";
public const int MaxNameLength = 100;

// Private fields
private readonly DbHelper dbHelper;
private DataTable _currentData;
```

### Code Organization

1. **Class Structure:**
   ```csharp
   public class MyClass
   {
       // Constants
       private const int MaxSize = 100;
       
       // Fields
       private readonly DbHelper dbHelper;
       private string _currentName;
       
       // Properties
       public string CurrentName { get; set; }
       
       // Constructor
       public MyClass(DbHelper helper)
       {
           dbHelper = helper;
       }
       
       // Public methods
       public void DoSomething()
       {
       }
       
       // Private methods
       private void ValidateInput(string input)
       {
       }
   }
   ```

2. **Comment Guidelines:**
   ```csharp
   /// <summary>
   /// Registers a new personnel in the system
   /// </summary>
   /// <param name="firstName">Personnel first name</param>
   /// <param name="lastName">Personnel last name</param>
   /// <returns>PersonnelID of newly registered person</returns>
   public int RegisterPersonnel(string firstName, string lastName)
   {
       // Validate inputs
       if (string.IsNullOrWhiteSpace(firstName))
           throw new ArgumentException("First name cannot be empty");
       
       // Insert into database
       string query = "INSERT INTO Personnel (FirstName, LastName) VALUES (?, ?)";
       // ...
   }
   ```

## 🔧 Common Development Tasks

### Adding a New Form

1. **Create New Class:**
   - Right-click Project → Add → Windows Form
   - Name it `FormYourFeature.cs`

2. **Add to MainForm:**
   ```csharp
   Button btnYourFeature = new Button { /*...*/ };
   btnYourFeature.Click += (s, e) => new FormYourFeature().ShowDialog();
   this.Controls.Add(btnYourFeature);
   ```

3. **Implement Form Logic:**
   ```csharp
   public partial class FormYourFeature : Form
   {
       private readonly DbHelper dbHelper;
       
       public FormYourFeature()
       {
           InitializeComponent();
           dbHelper = new DbHelper();
       }
       
       // Your implementation
   }
   ```

### Adding Database Query

1. **Create Method in DbHelper.cs:**
   ```csharp
   public DataTable? GetPersonnelByDepartment(int deptID)
   {
       string query = "SELECT * FROM Personnel WHERE DeptID = ? ORDER BY FirstName";
       OleDbParameter[] parameters = new OleDbParameter[] 
       {
           new OleDbParameter("?", deptID)
       };
       return ExecuteQuery(query, parameters);
   }
   ```

2. **Use in Form:**
   ```csharp
   DataTable? results = dbHelper.GetPersonnelByDepartment(deptID);
   if (results != null)
   {
       dataGridView1.DataSource = results;
   }
   ```

### Updating Configuration

1. **Add Constant to GlobalConstants.cs:**
   ```csharp
   public const int DefaultRowsPerPage = 50;
   ```

2. **Use in Application:**
   ```csharp
   int pageSize = GlobalConstants.DefaultRowsPerPage;
   ```

## 🐛 Debugging

### Debug Mode

1. **Set Breakpoints:**
   - Click left margin of code line
   - Red circle appears

2. **Watch Window:**
   - Debug → Windows → Watch → Watch 1
   - Add variable names

3. **Immediate Window:**
   - Debug → Windows → Immediate
   - Execute code during break

4. **Call Stack:**
   - Debug → Windows → Call Stack
   - See method call sequence

### Common Issues

**Issue: "Object reference not set to an instance"**
```csharp
// Bad
DataTable dt = dbHelper.ExecuteQuery(query);
dt.Rows[0]["Name"]; // Fails if dt is null

// Good
DataTable? dt = dbHelper.ExecuteQuery(query);
if (dt?.Rows.Count > 0)
{
    string name = dt.Rows[0]["Name"].ToString();
}
```

**Issue: "Connection string not valid"**
- Check dbconfig.ini exists
- Verify database path is correct
- Ensure Access Engine is installed

## 📦 Building Release

### Build Steps

1. **Set Build Configuration to Release:**
   - Configuration Manager → Active Solution Configuration → Release

2. **Build Solution:**
   ```
   Build → Build Solution
   ```

3. **Find Output:**
   - Navigate to `bin/Release/`
   - Copy all files
   - Create installer if needed

### Versioning

Update version in:
1. `GlobalConstants.cs` - `AppVersion`
2. `PersonnelManagementApp.csproj` - `<Version>` tag (if present)
3. `README.md` - Version History section

## 🔄 Git Workflow

### Branch Naming

```
feature/feature-name          # New features
fix/bug-description           # Bug fixes
docs/documentation-update     # Documentation
refactor/code-improvement     # Code refactoring
```

### Commit Message Format

```
type(scope): subject

body (optional)

footer (optional)

# Examples:
feat(personnel): Add bulk import functionality
fix(database): Resolve connection timeout issue
docs(readme): Update installation instructions
refactor(dbhelper): Optimize query performance
```

### Creating Pull Request

1. Create branch: `git checkout -b feature/your-feature`
2. Make changes and commit
3. Push: `git push origin feature/your-feature`
4. Create PR on GitHub
5. Ensure CI passes
6. Request review
7. Merge when approved

## 📚 Resources

- [Microsoft Learn - C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Windows Forms Documentation](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
- [OLE DB Documentation](https://learn.microsoft.com/en-us/sql/connect/oledb/)
- [Git Documentation](https://git-scm.com/doc)

## ✅ Pre-Commit Checklist

Before committing code:

- [ ] Code compiles without errors
- [ ] No compiler warnings
- [ ] Follows code style guidelines
- [ ] All methods have XML comments
- [ ] Code tested thoroughly
- [ ] No hardcoded values (use GlobalConstants)
- [ ] Error handling implemented
- [ ] Database queries use parameters
- [ ] Commit message is descriptive
- [ ] No sensitive data in commit

## 📞 Getting Help

1. **Check Documentation:**
   - README.md
   - ARCHITECTURE.md
   - Code comments

2. **Create an Issue:**
   - Describe problem
   - Include error messages
   - Provide reproduction steps

3. **Ask Questions:**
   - Discussion board (if available)
   - Email project maintainer

---

**Last Updated:** January 29, 2026

**Happy Coding!** 🚀
