using System;
using System.Collections.Generic;

namespace PersonnelManagementApp
{
    /// <summary>
    /// مدیریت رویدادهای تغییر داده‌های پرسنلی برای تازه‌سازی بی‌درنگ UI
    /// </summary>
    public static class DataChangeEventManager
    {
        // رویدادها
        public static event EventHandler<DataChangeEventArgs> PersonnelDeleted;
        public static event EventHandler<DataChangeEventArgs> PersonnelAdded;
        public static event EventHandler<DataChangeEventArgs> PersonnelUpdated;
        public static event EventHandler<EventArgs> DataRefreshRequested;

        /// <summary>
        /// آغاز حذف یک پرسنل
        /// </summary>
        public static void OnPersonnelDeleted(int personnelId, string name)
        {
            PersonnelDeleted?.Invoke(null, new DataChangeEventArgs
            {
                ChangeType = DataChangeType.Deleted,
                PersonnelID = personnelId,
                PersonnelName = name,
                Timestamp = DateTime.Now
            });
        }

        /// <summary>
        /// آغاز افزودن یک پرسنل جدید
        /// </summary>
        public static void OnPersonnelAdded(int personnelId, string name)
        {
            PersonnelAdded?.Invoke(null, new DataChangeEventArgs
            {
                ChangeType = DataChangeType.Added,
                PersonnelID = personnelId,
                PersonnelName = name,
                Timestamp = DateTime.Now
            });
        }

        /// <summary>
        /// آغاز به‌روزرسانی یک پرسنل
        /// </summary>
        public static void OnPersonnelUpdated(int personnelId, string name)
        {
            PersonnelUpdated?.Invoke(null, new DataChangeEventArgs
            {
                ChangeType = DataChangeType.Updated,
                PersonnelID = personnelId,
                PersonnelName = name,
                Timestamp = DateTime.Now
            });
        }

        /// <summary>
        /// درخواست تازه‌سازی کل داده‌ها
        /// </summary>
        public static void RequestDataRefresh()
        {
            DataRefreshRequested?.Invoke(null, EventArgs.Empty);
        }
    }

    /// <summary>
    /// اطلاعات تغییر داده‌ها
    /// </summary>
    public class DataChangeEventArgs : EventArgs
    {
        public DataChangeType ChangeType { get; set; }
        public int PersonnelID { get; set; }
        public string PersonnelName { get; set; }
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// نوع تغییر داده
    /// </summary>
    public enum DataChangeType
    {
        Deleted,
        Added,
        Updated
    }
}
