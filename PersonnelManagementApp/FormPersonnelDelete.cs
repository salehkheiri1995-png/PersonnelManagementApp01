using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    public partial class FormPersonnelDelete : BaseThemedForm
    {
        private DbHelper db = new DbHelper();

        public FormPersonnelDelete()
        {
            InitializeComponent();
        }

        private void DeletePersonnel(bool cascadeDelete)
        {
            if (cbPersonnel.SelectedIndex < 0)
                return;

            try
            {
                int personnelId = (int)cbPersonnel.SelectedValue;
                string personnelName = cbPersonnel.SelectedItem.ToString();
                string query = "DELETE FROM Personnel WHERE PersonnelID = ?";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    new OleDbParameter("?", personnelId)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("پرسنل با موفقیت حذف شد!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // 🔴 آغاز رویداد تغییر دادها
                    DataChangeEventManager.OnPersonnelDeleted(personnelId, personnelName);
                    
                    LoadPersonnelList();
                    dgvPersonnelInfo.DataSource = null;
                }
                else
                {
                    MessageBox.Show("هیچ پرسنلی حذف نشد!", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در حذف پرسنل: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPersonnelList()
        {
            try
            {
                // بارگذاری لیست پرسنل در ComboBox
                DataTable personnelTable = db.ExecuteQuery("SELECT PersonnelID, FirstName, LastName FROM Personnel ORDER BY FirstName");
                cbPersonnel.DataSource = personnelTable;
                cbPersonnel.DisplayMember = "FirstName";
                cbPersonnel.ValueMember = "PersonnelID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در بارگذاری لیست پرسنل: " + ex.Message);
            }
        }
    }
}
