# Personnel Management Application 👥

<div dir="rtl">

## 📄 نمای کلی

**Personnel Management Application** یک اپلیکیشن ویندوز فرمز تحت شبکه است که برای مدیریت اطلاعات پرسنل در سازمان‌های زیرساخت الکتریکی ایران طراحی شده است.

این سیستم امکان:
- ✅ **ثبت پرسنل جدید** با اطلاعات جامع
- ✅ **ویرایش اطلاعات** پرسنل موجود
- ✅ **جستجو و فیلتر** پیشرفته
- ✅ **تحلیل داده‌های آماری** و گزارش‌گیری
- ✅ **صادرات به CSV** برای تحلیل بیشتر

</div>

## 🎯 Features

### Core Functionality

1. **Personnel Management** 👨‍💼
   - Register new personnel with comprehensive data entry
   - Edit existing personnel records
   - Delete records with confirmation
   - Search and filter by multiple criteria (Name, ID, Number)

2. **Data Management** 📊
   - Hierarchical location management (Province → City → District)
   - Department and job classification
   - Employment contract tracking
   - Education and certification records
   - Work shift assignment

3. **Analytics & Reporting** 📈
   - Personnel distribution by department
   - Gender statistics
   - Contract type analysis
   - Job level distribution
   - Location-based analytics
   - Export data to CSV format

4. **User Interface** 🎨
   - Right-to-Left (RTL) layout for Persian language
   - Intuitive menu-based navigation
   - Color-coded action buttons
   - Responsive dialog windows
   - Gradient background design

## 🛠️ Tech Stack

| Component | Technology |
|-----------|------------|
| **Platform** | Windows Forms (.NET Framework) |
| **Language** | C# 7.0+ |
| **Database** | Microsoft Access (.accdb) |
| **Data Access** | OLE DB (OleDbConnection) |
| **IDE** | Visual Studio 2019+ |
| **Target Framework** | .NET Framework 4.7.2+ |

## 📋 Requirements

### System Requirements
- **OS:** Windows 7 or higher
- **.NET Framework:** 4.7.2 or higher
- **RAM:** 2GB minimum
- **Disk Space:** 100MB for application and database
- **Microsoft Access Database Engine:** Required for .accdb support

### Microsoft Access Database Engine
If you don't have Access installed, download and install:
- [Access Runtime (32-bit)](https://www.microsoft.com/en-us/download/details.aspx?id=13255)
- [Access Runtime (64-bit)](https://www.microsoft.com/en-us/download/details.aspx?id=51155)

Or use the installed version of Access if available.

## 📦 Installation

### Option 1: Build from Source

1. **Clone the repository:**
   ```bash
   git clone https://github.com/salehkheiri1995-png/PersonnelManagementApp01.git
   cd PersonnelManagementApp01
   ```

2. **Open in Visual Studio:**
   - Open `PersonnelManagementApp.sln` in Visual Studio 2019 or later

3. **Restore NuGet Packages:**
   ```bash
   nuget restore
   ```

4. **Build the Solution:**
   ```bash
   dotnet build
   ```

5. **Run the Application:**
   - Press `F5` or click **Start Debugging**

### Option 2: Release Build

1. Build in Release configuration
2. Navigate to `bin/Release/` folder
3. Run `PersonnelManagementApp.exe`

### Initial Setup

1. **First Run:**
   - Application will prompt you to select the database file (`MyDatabase.accdb`)
   - The path will be saved in `dbconfig.ini` for future use

2. **Database Path:**
   - Default location: Application installation directory
   - Can be changed by selecting a different file on startup

## 🚀 Usage

### Main Menu Options

```
┌──────────────────────────────────┐
│   سیستم مدیریت پرسنل              │
│  (Personnel Management System)    │
├──────────────────────────────────┤
│  [1] ثبت پرسنل جدید              │
│      Register New Personnel       │
├──────────────────────────────────┤
│  [2] ویرایش پرسنل                 │
│      Edit Personnel               │
├──────────────────────────────────┤
│  [3] حذف پرسنل                    │
│      Delete Personnel             │
├──────────────────────────────────┤
│  [4] جستجوی پرسنل                 │
│      Search Personnel             │
├──────────────────────────────────┤
│  [5] تحلیل داده‌های پرسنل         │
│      Analytics Dashboard          │
├──────────────────────────────────┤
│  [6] خروج                        │
│      Exit                         │
└──────────────────────────────────┘
```

### Workflow Examples

#### 1. Register New Personnel

1. Click **"ثبت پرسنل جدید"** (Register New Personnel)
2. Fill in personnel information:
   - Personal details (Name, National ID, etc.)
   - Location (Province, City, District)
   - Employment information (Department, Company, Position)
   - Education details (Degree, Field)
   - Work arrangements (Shift, Contract Type)
3. Click **Save**
4. Confirmation message will appear

#### 2. Search Personnel

1. Click **"جستجوی پرسنل"** (Search Personnel)
2. Enter search term (Name, National ID, or Personnel Number)
3. View results in table
4. Export results to CSV if needed

#### 3. View Analytics

1. Click **"تحلیل داده‌های پرسنل"** (Analytics)
2. View statistical summaries and charts
3. Analyze distribution by various criteria

## 📂 Project Structure

```
PersonnelManagementApp01/
├── PersonnelManagementApp/
│   ├── DbHelper.cs                    # Database operations
│   ├── GlobalConstants.cs             # Configuration & constants
│   ├── Program.cs                     # Application entry point
│   ├── MainForm.cs                    # Main window UI
│   ├── FormPersonnelRegister.cs       # Register form
│   ├── FormPersonnelEdit.cs           # Edit form
│   ├── FormPersonnelDelete.cs         # Delete form
│   ├── FormPersonnelSearch.cs         # Search form
│   ├── FormPersonnelAnalytics.cs      # Analytics form
│   ├── MyDatabase.accdb               # Database file
│   └── PersonnelManagementApp.csproj  # Project file
├── PersonnelManagementApp.sln         # Solution file
├── README.md                          # This file
├── ARCHITECTURE.md                    # Architecture documentation
├── .gitignore                         # Git ignore patterns
└── .gitattributes                     # Git attributes
```

## 🗄️ Database Information

### Main Tables

1. **Personnel** - Employee records
   - PersonnelID, FirstName, LastName
   - NationalID, PersonnelNumber
   - Department, Company, Position
   - Contact information
   - Employment status

2. **Posts** - Electrical infrastructure posts
   - PostID, PostName, Location
   - Voltage Level, Capacity
   - Equipment inventory

3. **Reference Tables**
   - Provinces, Cities, Districts
   - OperationDepartments, TransferAffairs
   - Companies, JobLevels, Degrees
   - WorkShift, Gender, ContractType

### Connection String

```csharp
Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;
```

## ⚙️ Configuration

### GlobalConstants.cs

All application-wide constants are defined in `GlobalConstants.cs`:

```csharp
// Application Info
AppName = "سیستم مدیریت پرسنل"
AppVersion = "1.0.0"

// UI Theme
Colors.BtnAddColor = Color.LightBlue
Colors.BtnEditColor = Color.LightGreen
Colors.BtnDeleteColor = Color.LightCoral

// Validation Rules
Validation.MinPersonnelNameLength = 2
Validation.NationalIDLength = 10

// Error Messages (Persian)
ErrorMessages.DatabaseNotFound
ErrorMessages.ConnectionFailed
ErrorMessages.InvalidInput
```

### Database Configuration (dbconfig.ini)

```ini
DatabasePath=C:\path\to\MyDatabase.accdb
LastUpdated=2026-01-29 10:30:45
```

## 🔐 Security

1. **SQL Injection Prevention**
   - All queries use parameterized statements
   - OleDbParameter for safe data binding

2. **Error Handling**
   - Try-catch blocks for all database operations
   - User-friendly error messages
   - Detailed error logging

3. **Database Access**
   - Connection testing functionality
   - Automatic path saving for reliability

## 🐛 Troubleshooting

### Issue: "Database not found" error

**Solution:**
1. Ensure `MyDatabase.accdb` exists in the application folder
2. On first run, select the correct database file
3. Check `dbconfig.ini` for correct path

### Issue: "Microsoft.ACE.OLEDB.12.0 provider not registered"

**Solution:**
1. Install Microsoft Access Database Engine (see Requirements)
2. Ensure matching architecture (32-bit or 64-bit)
3. Restart the application after installation

### Issue: Connection timeout or slowness

**Solutions:**
1. Check database file integrity
2. Defragment the Access database
3. Reduce dataset size if filtering specific records
4. Ensure sufficient disk space

## 📊 Data Export

The application supports exporting data to CSV format:

1. Perform a search or view analytics
2. Click the **Export** button
3. Choose save location
4. File will be saved with UTF-8 encoding
5. Open in Excel for further analysis

## 🔄 Version History

### Version 1.0.0 (Current)
- Initial release
- Core CRUD operations
- Search and filter functionality
- Analytics dashboard
- CSV export
- Farsi language support

## 🚀 Future Enhancements

- [ ] User authentication & role-based access
- [ ] Audit logging for all operations
- [ ] Batch import with validation
- [ ] Advanced filtering and sorting
- [ ] Data backup/restore functionality
- [ ] SQL Server migration
- [ ] WPF/Modern UI update
- [ ] Unit testing framework
- [ ] Multi-language support (English, Arabic)
- [ ] Dark mode theme
- [ ] Export to Excel and PDF

## 📝 Contributing

Contributions are welcome! Please:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/YourFeature`)
3. Commit changes (`git commit -m 'Add YourFeature'`)
4. Push to branch (`git push origin feature/YourFeature`)
5. Open a Pull Request

## 📄 License

This project is provided as-is for educational and organizational use.

## 👨‍💼 Author

**Saleh Kheirv**
- GitHub: [@salehkheiri1995-png](https://github.com/salehkheiri1995-png)

## 📞 Contact & Support

For issues, questions, or suggestions:
1. Create an [Issue](https://github.com/salehkheiri1995-png/PersonnelManagementApp01/issues)
2. Check [ARCHITECTURE.md](./ARCHITECTURE.md) for detailed technical documentation

## 📚 Documentation

- [ARCHITECTURE.md](./ARCHITECTURE.md) - Detailed project architecture and design
- [GlobalConstants.cs](./PersonnelManagementApp/GlobalConstants.cs) - Configuration reference

---

<div align="center">

**Last Updated:** January 29, 2026

**Status:** ✅ Active Development

If you find this project helpful, please consider starring ⭐ it!

</div>

</div>
