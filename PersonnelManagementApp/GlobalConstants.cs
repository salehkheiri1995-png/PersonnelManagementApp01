using System;
using System.Drawing;

namespace PersonnelManagementApp
{
    /// <summary>
    /// Global constants and configuration for Personnel Management Application
    /// </summary>
    public static class GlobalConstants
    {
        // Application Information
        public const string AppName = "سیستم مدیریت پرسنل";
        public const string AppVersion = "1.0.0";
        public const string AppAuthor = "Electrical Company System";
        public const string AppDescription = "Windows Forms application for managing personnel in electrical infrastructure";

        // Database Configuration
        public const string DatabaseFileName = "MyDatabase.accdb";
        public const string DatabaseConfigFile = "dbconfig.ini";
        
        // UI Constants
        public const string DefaultFont = "Tahoma";
        public const int DefaultFontSize = 11;

        // Theme Colors
        public static class Colors
        {
            // Button Colors
            public static Color BtnAddColor = Color.LightBlue;
            public static Color BtnEditColor = Color.LightGreen;
            public static Color BtnDeleteColor = Color.LightCoral;
            public static Color BtnSearchColor = Color.Orange;
            public static Color BtnAnalyticsColor = Color.SteelBlue;
            public static Color BtnExitColor = Color.Gray;
            public static Color BtnPrimaryText = Color.White;

            // Form Background
            public static Color FormBackground = Color.FromArgb(240, 248, 255);
            public static Color BackgroundGradientStart = Color.LightBlue;
            public static Color BackgroundGradientEnd = Color.White;

            // Text Colors
            public static Color TitleColor = Color.Navy;
            public static Color TextPrimary = Color.Black;
            public static Color TextSecondary = Color.DarkGray;
        }

        // Button Dimensions
        public const int ButtonWidth = 300;
        public const int ButtonHeight = 50;
        public const int ButtonSpacing = 60;
        public const int ButtonCornerRadius = 15;

        // Title Style
        public const int TitleFontSize = 20;
        public const int ButtonFontSize = 12;

        // Window State
        public const bool StartMaximized = true;
        public const bool RightToLeftLayout = true;

        // Validation
        public static class Validation
        {
            public const int MinPersonnelNameLength = 2;
            public const int MaxPersonnelNameLength = 100;
            public const int NationalIDLength = 10;
            public const int PersonnelNumberLength = 10;
        }

        // Database Queries
        public static class DefaultQueries
        {
            public const string PersonnelTableName = "Personnel";
            public const string PostsTableName = "Posts";
        }

        // Error Messages
        public static class ErrorMessages
        {
            public const string DatabaseNotFound = "پایگاه داده یافت نشد. لطفاً مسیر پایگاه داده را انتخاب کنید.";
            public const string ConnectionFailed = "اتصال به پایگاه داده ناموفق بود.";
            public const string InvalidInput = "ورودی نامعتبر است.";
            public const string OperationFailed = "عملیات ناموفق بود.";
            public const string NoDataFound = "هیچ داده‌ای یافت نشد.";
        }

        // Success Messages
        public static class SuccessMessages
        {
            public const string OperationSuccessful = "عملیات با موفقیت انجام شد.";
            public const string DataAdded = "داده جدید با موفقیت اضافه شد.";
            public const string DataUpdated = "داده با موفقیت بروزرسانی شد.";
            public const string DataDeleted = "داده با موفقیت حذف شد.";
            public const string ExportSuccessful = "صادرات با موفقیت انجام شد.";
        }
    }
}
