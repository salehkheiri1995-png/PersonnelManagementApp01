using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.Drawing;
using System.Collections.Generic;

namespace PersonnelManagementApp
{
    public partial class FormPersonnelAnalytics : BaseThemedForm
    {
        private DbHelper dbHelper;
        private SettingsManager settingsManager;

        public FormPersonnelAnalytics()
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            settingsManager = SettingsManager.Instance;
            ApplyThemedFont();
        }

        private void FormPersonnelAnalytics_Load(object sender, EventArgs e)
        {
            LoadAnalyticsData();
            ApplyThemedFont();
        }

        private void ApplyThemedFont()
        {
            Font themedFont = new Font(settingsManager.PrimaryFont, settingsManager.PrimaryFontSize);
            this.Font = themedFont;
            
            foreach (Control control in this.Controls)
            {
                control.Font = themedFont;
            }
        }

        private void LoadAnalyticsData()
        {
            try
            {
                // بارگذاری داده های تحلیلی پرسنلی
                string query = "SELECT COUNT(*) FROM Personnel";
                object result = dbHelper.ExecuteScalar(query);
                
                if (result != null)
                {
                    MessageBox.Show($"تعداد کل پرسنل: {result}", "اطلاعات تحلیلی", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری داده ها: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowPersonnelDetails(DataGridView dgv)
        {
            dgv.CellClick += (s, e) =>
            {
                if (e.ColumnIndex == dgv.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    try
                    {
                        int personnelID = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["PersonnelID"].Value);
                        
                        FormPersonnelEdit editForm = new FormPersonnelEdit();
                        editForm.txtPersonnelID.Text = personnelID.ToString();
                        editForm.BtnLoad_Click(null, EventArgs.Empty);
                        editForm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"خطا: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
        }
    }
}