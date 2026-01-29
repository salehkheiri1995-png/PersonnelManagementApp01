using System;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    public partial class FormPersonnelAnalytics : BaseThemedForm
    {
        /// <summary>
        /// ثبت گوش‌دهندگان رویدادها هنگام بارگذاری فرم
        /// </summary>
        private void SubscribeToDataChangeEvents()
        {
            // Subscribe to delete event
            DataChangeEventManager.PersonnelDeleted += OnPersonnelDeleted;
            
            // Subscribe to add event
            DataChangeEventManager.PersonnelAdded += OnPersonnelAdded;
            
            // Subscribe to update event
            DataChangeEventManager.PersonnelUpdated += OnPersonnelUpdated;
            
            // Subscribe to refresh request
            DataChangeEventManager.DataRefreshRequested += OnDataRefreshRequested;
        }

        /// <summary>
        /// Unsubscribe from all events when closing form
        /// </summary>
        private void UnsubscribeFromDataChangeEvents()
        {
            DataChangeEventManager.PersonnelDeleted -= OnPersonnelDeleted;
            DataChangeEventManager.PersonnelAdded -= OnPersonnelAdded;
            DataChangeEventManager.PersonnelUpdated -= OnPersonnelUpdated;
            DataChangeEventManager.DataRefreshRequested -= OnDataRefreshRequested;
        }

        /// <summary>
        /// هنگام حذف یک پرسنل
        /// </summary>
        private void OnPersonnelDeleted(object sender, DataChangeEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<DataChangeEventArgs>(OnPersonnelDeleted), sender, e);
                return;
            }

            try
            {
                // بارگذاری مجدد دادهها
                if (analyticsModel.LoadData(new DbHelper()))
                {
                    // تازه‌سازی تمام نمودارها
                    RefreshAllCharts();
                    
                    // نمایش پیام
                    Console.WriteLine($"[آپدیت] پرسنل {e.PersonnelName} (ID: {e.PersonnelID}) حذف شد. نمودارها بروز شدند.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در بروز‌رسانی بعد از حذف: {ex.Message}", "خطا");
            }
        }

        /// <summary>
        /// هنگام افزودن یک پرسنل جدید
        /// </summary>
        private void OnPersonnelAdded(object sender, DataChangeEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<DataChangeEventArgs>(OnPersonnelAdded), sender, e);
                return;
            }

            try
            {
                // بارگذاری مجدد دادهها
                if (analyticsModel.LoadData(new DbHelper()))
                {
                    // بارگذاری مجدد فیلترها
                    LoadFilterOptions();
                    
                    // تازه‌سازی تمام نمودارها
                    RefreshAllCharts();
                    
                    // نمایش پیام
                    Console.WriteLine($"[آپدیت] پرسنل جدید {e.PersonnelName} (ID: {e.PersonnelID}) اضافه شد. نمودارها بروز شدند.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در بروز‌رسانی بعد از اضافه کردن: {ex.Message}", "خطا");
            }
        }

        /// <summary>
        /// هنگام ویرایش اطلاعات یک پرسنل
        /// </summary>
        private void OnPersonnelUpdated(object sender, DataChangeEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<DataChangeEventArgs>(OnPersonnelUpdated), sender, e);
                return;
            }

            try
            {
                // بارگذاری مجدد دادهها
                if (analyticsModel.LoadData(new DbHelper()))
                {
                    // بارگذاری مجدد فیلترها (در صورتی که اطلاعات تغیر یافته است)
                    LoadFilterOptions();
                    
                    // تازه‌سازی تمام نمودارها
                    RefreshAllCharts();
                    
                    // نمایش پیام
                    Console.WriteLine($"[آپدیت] پرسنل {e.PersonnelName} (ID: {e.PersonnelID}) بروز شد. نمودارها تازه شدند.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در بروز‌رسانی بعد از تغییر: {ex.Message}", "خطا");
            }
        }

        /// <summary>
        /// هنگام درخواست تازه‌سازی کلی دادهها
        /// </summary>
        private void OnDataRefreshRequested(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnDataRefreshRequested), sender, e);
                return;
            }

            try
            {
                // بارگذاری مجدد کل دادهها
                LoadData();
                
                // نمایش پیام
                Console.WriteLine("[آپدیت] درخواست تازه‌سازی کل دادهها دریافت شد.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در تازه‌سازی دادهها: {ex.Message}", "خطا");
            }
        }
    }
}
