# Personnel Management Application - Architecture Documentation

## 📋 Project Overview

**Personnel Management Application** is a Windows Forms desktop application built with C# .NET Framework designed for managing personnel information in electrical infrastructure organizations in Iran.

**Current Version:** 1.0.0

## 🏗️ Architecture

### Technology Stack
- **Platform:** Windows Forms (.NET Framework)
- **Language:** C# 7.0+
- **Database:** Microsoft Access (.accdb)
- **Data Access:** OLE DB (OleDbConnection)
- **UI Framework:** Windows Forms

### Project Structure

```
PersonnelManagementApp01/
├── PersonnelManagementApp/          # Main project folder
│   ├── DbHelper.cs                  # Database connection & operations helper
│   ├── GlobalConstants.cs           # Centralized constants & configuration
│   ├── Program.cs                   # Application entry point
│   ├── MainForm.cs                  # Main window UI
│   ├── FormPersonnelRegister.cs     # Register new personnel
│   ├── FormPersonnelEdit.cs         # Edit personnel information
│   ├── FormPersonnelDelete.cs       # Delete personnel
│   ├── FormPersonnelSearch.cs       # Search & filter personnel
│   ├── FormPersonnelAnalytics.cs    # Data analytics & reporting
│   ├── MyDatabase.accdb             # Access database file
│   ├── PersonnelManagementApp.csproj # Project file
│   └── [Designer files]             # Auto-generated UI designer files
├── PersonnelManagementApp.sln       # Visual Studio solution file
├── .gitignore                       # Git ignore patterns
├── .gitattributes                   # Git attributes
├── README.md                        # Project readme
└── ARCHITECTURE.md                  # This file
```

## 🔄 Data Flow

```
UI Forms (MainForm)
    ↓
Form Event Handlers
    ↓
DbHelper Methods
    ↓
OleDbConnection & OleDbCommand
    ↓
MyDatabase.accdb (MS Access)
```

## 🗂️ Module Descriptions

### 1. **DbHelper.cs** (Database Abstraction Layer)
Core database operations helper class.

**Key Responsibilities:**
- Database connection management
- Configuration file handling (dbconfig.ini)
- Query execution (SELECT, INSERT, UPDATE, DELETE)
- Data export (CSV format)
- Database path configuration

**Main Methods:**
- `ExecuteQuery(string query, params)` - Returns DataTable
- `ExecuteNonQuery(string query, params)` - INSERT/UPDATE/DELETE operations
- `TestConnection()` - Verify database connectivity
- `SearchByPersonnel()` - Advanced personnel search
- `GetCitiesByProvince()`, `GetDeptsByAffair()` - Hierarchical lookups
- `ExportToCsv()` - Export data to CSV format

**Database Connection:**
- Uses OLE DB provider for MS Access (.accdb)
- Connection string: `Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}`
- Auto-saves database path in dbconfig.ini for future use

### 2. **GlobalConstants.cs** (Configuration & Constants)
Centralized configuration and constant definitions.

**Contains:**
- Application metadata (name, version, author)
- UI theme colors and dimensions
- Validation rules
- Error and success messages (Persian)
- Database configuration defaults

### 3. **MainForm.cs** (Main Window)
Application entry point and navigation hub.

**Features:**
- Gradient background (LightBlue → White)
- 6 main action buttons:
  1. Register New Personnel (ثبت پرسنل جدید)
  2. Edit Personnel (ویرایش پرسنل)
  3. Delete Personnel (حذف پرسنل)
  4. Search Personnel (جستجوی پرسنل)
  5. Analytics Dashboard (تحلیل داده‌های پرسنل)
  6. Exit Application (خروج)
- RTL (Right-to-Left) support for Persian UI
- Rounded button corners (15px radius)
- Maximized window state by default

### 4. **FormPersonnelRegister.cs**
UIfor registering new personnel with comprehensive data entry fields.

**Related Tables:**
- Personnel (main data)
- Provinces, Cities, Districts (location hierarchy)
- Companies, JobLevels, Degrees (employment details)
- WorkShift, ContractType, Gender (personnel characteristics)

### 5. **FormPersonnelEdit.cs**
UIfor updating existing personnel information.

### 6. **FormPersonnelDelete.cs**
UIfor removing personnel records with confirmation.

### 7. **FormPersonnelSearch.cs**
UIfor searching and filtering personnel by multiple criteria.
- Supports search by: Name, Personnel Number, National ID
- Displays results in DataGridView
- Allows export to CSV

### 8. **FormPersonnelAnalytics.cs**
Data analytics and visualization module.

**Features:**
- Statistical analysis of personnel data
- Department-wise distribution
- Gender distribution
- Contract type analysis
- Personnel count by location

## 🗄️ Database Schema Overview

### Main Tables:

**Personnel**
- PersonnelID (PK)
- FirstName, LastName, NationalID, PersonnelNumber
- ProvinceID, CityID, DeptID, DistrictID
- WorkShiftID, GenderID, ContractTypeID, JobLevelID
- CompanyID, DegreeID, DegreeFieldID
- MainJobTitle, CurrentActivity, StatusID

**Posts**
- PostID (PK)
- PostName, Voltage Level, Location hierarchy
- Capacity, Equipment details
- Geographic coordinates (Longitude, Latitude)

**Reference Tables:**
- Provinces, Cities, Districts (geographic hierarchy)
- OperationDepartments, TransferAffairs
- PostTypes, Voltages, Standards
- WorkShift, Gender, ContractType, JobLevel, Company, Degree

## 🔐 Security Considerations

1. **Database Access:**
   - Uses parameterized queries to prevent SQL injection
   - OLE DB parameters for safe query execution

2. **Configuration:**
   - Database path stored in local config file (dbconfig.ini)
   - User prompted to select database on first run

3. **Error Handling:**
   - Try-catch blocks for database operations
   - User-friendly error messages
   - Connection test functionality

## 📊 UI/UX Design

**Theme:**
- Right-to-Left (RTL) layout for Persian language
- Color-coded buttons for different operations:
  - Blue (Register/Analytics)
  - Green (Edit)
  - Red (Delete)
  - Orange (Search)
  - Gray (Exit)
- Rounded corners on buttons (15px)
- Gradient background on main form

**Fonts:**
- Primary: Tahoma (Persian support)
- Sizes: 20px (Title), 12px (Buttons), 11px (Default)

## 🔄 Workflow Examples

### Registering New Personnel:
1. User clicks "ثبت پرسنل جدید" on MainForm
2. FormPersonnelRegister opens
3. User fills in all personnel details
4. Form validates input
5. DbHelper.ExecuteNonQuery() inserts into Personnel table
6. Success message shown
7. Form closes

### Searching Personnel:
1. User clicks "جستجوی پرسنل"
2. FormPersonnelSearch opens
3. User enters search term (name, number, ID)
4. DbHelper.SearchByPersonnel() returns matching records
5. Results displayed in DataGridView
6. User can export results to CSV

## 🚀 Performance Optimizations

1. **Data Caching:**
   - Reference data (provinces, cities) cached on form load
   - Reduces database queries

2. **Parameterized Queries:**
   - Prevents SQL injection
   - Better query plan caching by DB engine

3. **Connection Pooling:**
   - Each operation opens and closes connection
   - OLE DB handles connection pooling

## 📝 Code Style Guidelines

- **Language:** Farsi (Persian) comments and UI strings
- **Naming Convention:** PascalCase for classes/methods, camelCase for variables
- **Comments:** Extensive comments for complex logic
- **Error Messages:** User-friendly Persian messages

## 🔮 Future Enhancements

1. **Architecture Improvements:**
   - Implement Repository Pattern
   - Add Dependency Injection
   - Create separate Data Access Layer

2. **Features:**
   - User authentication & role-based access
   - Audit logging for all operations
   - Batch import/export with validation
   - Advanced filtering and sorting
   - Data backup/restore functionality

3. **Technology:**
   - Migrate to SQL Server (from MS Access)
   - Modernize UI (WPF or WinUI)
   - Add unit testing framework
   - Implement async/await for DB operations

4. **User Experience:**
   - Multi-language support (English, Arabic, etc.)
   - Dark mode option
   - Customizable themes
   - Export to Excel/PDF
   - Print previews

## 📞 Support & Contribution

For issues or suggestions, please create an issue in the repository.

---

**Last Updated:** January 29, 2026
**Maintained by:** Electrical Company System
