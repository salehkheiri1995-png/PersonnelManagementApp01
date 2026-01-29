using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Drawing;

namespace PersonnelManagementApp
{
    public partial class FormPersonnelDelete : BaseThemedForm
    {
        private DbHelper db = new DbHelper();
        private ComboBox cbPersonnel;
        private DataGridView dgvPersonnelInfo;
        private Button btnDelete;
        private Button btnCancel;
        private Label lblPersonnel;
        private CheckBox chkCascadeDelete;

        public FormPersonnelDelete()
        {
            InitializeComponent();
            BuildUI();
        }

        private void InitializeComponent()
        {
            // اینیشیلائزیشن کمپوننٹس
            this.SuspendLayout();
            
            this.Text = "حذف پرسنل";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.RightToLeft = RightToLeft.Yes;
            this.BackColor = Color.White;
            
            this.ResumeLayout(false);
        }

        private void BuildUI()
        {
            // پینل بالا برای انتخاب پرسنل
            Panel pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(240, 248, 255),
                BorderStyle = BorderStyle.FixedSingle
            };
            RegisterThemedControl(pnlTop);

            // لیبل
            lblPersonnel = new Label
            {
                Text = "انتخاب پرسنل برای حذف:",
                Location = new Point(20, 15),
                Size = new Size(200, 25),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204)
            };
            pnlTop.Controls.Add(lblPersonnel);
            RegisterThemedControl(lblPersonnel);

            // ComboBox برای انتخاب پرسنل
            cbPersonnel = new ComboBox
            {
                Location = new Point(20, 45),
                Size = new Size(300, 30),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White
            };
            pnlTop.Controls.Add(cbPersonnel);
            RegisterThemedControl(cbPersonnel);

            // CheckBox برای حذف پیوندی
            chkCascadeDelete = new CheckBox
            {
                Text = "حذف تمام داده‌های مرتبط",
                Location = new Point(330, 48),
                Size = new Size(250, 25),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize),
                Checked = false
            };
            pnlTop.Controls.Add(chkCascadeDelete);
            RegisterThemedControl(chkCascadeDelete);

            // دکمه حذف
            btnDelete = new Button
            {
                Text = "🗑️ حذف",
                Location = new Point(600, 45),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.None
            };
            btnDelete.Click += BtnDelete_Click;
            pnlTop.Controls.Add(btnDelete);
            ApplyRoundedCorners(btnDelete, 8);
            RegisterThemedControl(btnDelete);

            // دکمه لغو
            btnCancel = new Button
            {
                Text = "❌ لغو",
                Location = new Point(720, 45),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.Click += (s, e) => this.Close();
            pnlTop.Controls.Add(btnCancel);
            ApplyRoundedCorners(btnCancel, 8);
            RegisterThemedControl(btnCancel);

            this.Controls.Add(pnlTop);

            // DataGridView برای نمایش اطلاعات پرسنل
            dgvPersonnelInfo = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                ReadOnly = true,
                RightToLeft = RightToLeft.Yes,
                BackgroundColor = Color.White,
                EnableHeadersVisualStyles = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };
            dgvPersonnelInfo.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgvPersonnelInfo.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPersonnelInfo.ColumnHeadersDefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold);
            dgvPersonnelInfo.ColumnHeadersHeight = 35;
            dgvPersonnelInfo.DefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize);
            dgvPersonnelInfo.DefaultCellStyle.BackColor = Color.White;
            dgvPersonnelInfo.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
            
            this.Controls.Add(dgvPersonnelInfo);
            RegisterThemedControl(dgvPersonnelInfo);

            // بارگذاری لیست پرسنل
            LoadPersonnelList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (cbPersonnel.SelectedIndex < 0)
            {
                MessageBox.Show("لطفاً یک پرسنل انتخاب کنید!", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "آیا از حذف این پرسنل اطمینان دارید؟",
                "تأیید حذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                DeletePersonnel(chkCascadeDelete.Checked);
            }
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
                    MessageBox.Show("✅ پرسنل با موفقیت حذف شد!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // 🔴 رویدادتغییر دادها
                    DataChangeEventManager.RaisePersonnelDeleted(personnelId, personnelName);
                    
                    LoadPersonnelList();
                    dgvPersonnelInfo.DataSource = null;
                }
                else
                {
                    MessageBox.Show("❌ هیچ پرسنلی حذف نشد!", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در حذف پرسنل: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPersonnelList()
        {
            try
            {
                // بارگذاری لیست پرسنل در ComboBox
                DataTable personnelTable = db.ExecuteQuery(
                    @"SELECT PersonnelID, (FirstName + ' ' + LastName) as FullName 
                      FROM Personnel 
                      ORDER BY FirstName"
                );
                
                cbPersonnel.DataSource = personnelTable;
                cbPersonnel.DisplayMember = "FullName";
                cbPersonnel.ValueMember = "PersonnelID";
                
                if (cbPersonnel.SelectedIndex >= 0)
                {
                    ShowPersonnelDetails();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در بارگذاری لیست پرسنل: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowPersonnelDetails()
        {
            try
            {
                if (cbPersonnel.SelectedIndex < 0)
                    return;

                int personnelId = (int)cbPersonnel.SelectedValue;
                string query = @"
                    SELECT p.*, 
                           pr.ProvinceName, c.CityName, d.DeptName, pn.PostName,
                           g.GenderName, deg.DegreeName, co.CompanyName
                    FROM Personnel p
                    LEFT JOIN Province pr ON p.ProvinceID = pr.ProvinceID
                    LEFT JOIN City c ON p.CityID = c.CityID
                    LEFT JOIN OperationDepartments d ON p.DeptID = d.DeptID
                    LEFT JOIN PostNames pn ON p.PostNameID = pn.PostNameID
                    LEFT JOIN Gender g ON p.GenderID = g.GenderID
                    LEFT JOIN Degree deg ON p.DegreeID = deg.DegreeID
                    LEFT JOIN Company co ON p.CompanyID = co.CompanyID
                    WHERE p.PersonnelID = ?
                ";
                
                OleDbParameter[] parameters = new OleDbParameter[] 
                { 
                    new OleDbParameter("?", personnelId) 
                };

                DataTable dt = db.ExecuteQuery(query, parameters);
                
                if (dt.Rows.Count > 0)
                {
                    dgvPersonnelInfo.DataSource = dt;
                    // تنظیم عرض ستون‌ها
                    foreach (DataGridViewColumn col in dgvPersonnelInfo.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        if (col.Width > 300)
                            col.Width = 300;
                    }
                }
                else
                {
                    dgvPersonnelInfo.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در نمایش جزئیات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            cbPersonnel.SelectedIndexChanged += (s, evt) => ShowPersonnelDetails();
        }
    }
}
