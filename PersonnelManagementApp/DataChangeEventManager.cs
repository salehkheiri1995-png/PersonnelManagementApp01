using System;
using System.Collections.Generic;

namespace PersonnelManagementApp
{
    /// <summary>
    /// رویدادها مربوط به تغییرات دادهبانک
    /// </summary>
    public class DataChangeEventArgs : EventArgs
    {
        public int PersonnelID { get; set; }
        public string PersonnelName { get; set; }
        public DateTime Timestamp { get; set; }
        public string AdditionalInfo { get; set; }

        public DataChangeEventArgs()
        {
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// مدیر رویدادها برای تغییرات دادههای پرسنل
    /// این طبقه به عنوان یک static رویداد هاب عمل می‌کند
    /// </summary>
    public static class DataChangeEventManager
    {
        /// <summary>
        /// رویداد حذف اطلاعات پرسنل
        /// </summary>
        public static event EventHandler<DataChangeEventArgs> PersonnelDeleted;

        /// <summary>
        /// رویداد اضافه کردن اطلاعات پرسنل جدید
        /// </summary>
        public static event EventHandler<DataChangeEventArgs> PersonnelAdded;

        /// <summary>
        /// رویداد ویرایش اطلاعات پرسنل
        /// </summary>
        public static event EventHandler<DataChangeEventArgs> PersonnelUpdated;

        /// <summary>
        /// رویداد درخواست تازه‌سازی کلی دادهها
        /// </summary>
        public static event EventHandler DataRefreshRequested;

        /// <summary>
        /// فعال‌کردن رویداد حذف
        /// </summary>
        public static void RaisePersonnelDeleted(int personnelID, string personnelName)
        {
            PersonnelDeleted?.Invoke(null, new DataChangeEventArgs
            {
                PersonnelID = personnelID,
                PersonnelName = personnelName,
                AdditionalInfo = $"پرسنل '{personnelName}' حذف شد"
            });
        }

        /// <summary>
        /// فعال‌کردن رویداد اضافه‌کردن
        /// </summary>
        public static void RaisePersonnelAdded(int personnelID, string personnelName)
        {
            PersonnelAdded?.Invoke(null, new DataChangeEventArgs
            {
                PersonnelID = personnelID,
                PersonnelName = personnelName,
                AdditionalInfo = $"پرسنل جدید '{personnelName}' اضافه شد"
            });
        }

        /// <summary>
        /// فعال‌کردن رویداد بروز‌رسانی
        /// </summary>
        public static void RaisePersonnelUpdated(int personnelID, string personnelName)
        {
            PersonnelUpdated?.Invoke(null, new DataChangeEventArgs
            {
                PersonnelID = personnelID,
                PersonnelName = personnelName,
                AdditionalInfo = $"پرسنل '{personnelName}' بروز رسانی شد"
            });
        }

        /// <summary>
        /// فعال‌کردن رویداد تازه‌سازی
        /// </summary>
        public static void RaiseDataRefreshRequested()
        {
            DataRefreshRequested?.Invoke(null, EventArgs.Empty);
        }
    }
}
