using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PersonnelManagementApp
{
    public partial class FormPersonnelDelete : BaseThemedForm
    {
        private DbHelper db = new DbHelper();
        private ComboBox cbPersonnel;
        private DataGridView dgvPersonnelInfo;

        public FormPersonnelDelete()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "حذف پرسنل";
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.RightToLeft = RightToLeft.Yes;

            // پس‌زمینه گرادیانت
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.LightCoral, Color.White, LinearGradientMode.Vertical))
            {
                this.BackgroundImage = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(this.BackgroundImage))
                {
                    g.FillRectangle(brush, this.ClientRectangle);
                }
            }

            // لیبل انتخاب پرسنل
            Label lblPersonnel = new Label
            {
                Text = "انتخاب پرسنل:",
                Location = new Point(50, 20),
                Size = new Size(150, 30),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };
            RegisterThemedControl(lblPersonnel);

            // ComboBox انتخاب پرسنل
            cbPersonnel = new ComboBox
            {
                Location = new Point(210, 22),
                Size = new Size(300, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize),
                Name = "cbPersonnel"
            };
            cbPersonnel.SelectedIndexChanged += CbPersonnel_SelectedIndexChanged;
            RegisterThemedControl(cbPersonnel);

            // DataGridView برای نمایش اطلاعات
            dgvPersonnelInfo = new DataGridView
            {
                Name = "dgvPersonnelInfo",
                Location = new Point(50, 60),
                Size = new Size(this.ClientSize.Width - 100, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AllowUserToAddRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 30,
                RowTemplate = { Height = 25 }
            };
            RegisterThemedControl(dgvPersonnelInfo);
            ApplyRoundedCorners(dgvPersonnelInfo, 10);

            // دکمه حذف
            Button btnDelete = new Button
            {
                Text = "حذف ارز ریز",
                Location = new Point(this.ClientSize.Width - 250, 470),
                Size = new Size(120, 40),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize),
                BackColor = Color.LightCoral,
                ForeColor = Color.White,
                Name = "btnDelete"
            };
            btnDelete.Click += BtnDelete_Click;
            ApplyRoundedCorners(btnDelete, 10);
            RegisterThemedControl(btnDelete);

            // دکمه حذف برابر برای رابطه ای موجود
            Button btnDeleteWithReferences = new Button
            {
                Text = "حذف حذاقانه",
                Location = new Point(this.ClientSize.Width - 380, 470),
                Size = new Size(120, 40),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Name = "btnDeleteAll"
            };
            btnDeleteWithReferences.Click += BtnDeleteWithReferences_Click;
            ApplyRoundedCorners(btnDeleteWithReferences, 10);
            RegisterThemedControl(btnDeleteWithReferences);

            // دکمه بازگشت
            Button btnBack = new Button
            {
                Text = "بازگشت",
                Location = new Point(50, 470),
                Size = new Size(120, 40),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Name = "btnCancel"
            };
            btnBack.Click += (s, e) => { this.Close(); };
            ApplyRoundedCorners(btnBack, 10);
            RegisterThemedControl(btnBack);

            this.Controls.Add(lblPersonnel);
            this.Controls.Add(cbPersonnel);
            this.Controls.Add(dgvPersonnelInfo);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnDeleteWithReferences);
            this.Controls.Add(btnBack);

            LoadPersonnelList();
        }

        private void LoadPersonnelList()
        {
            try
            {
                DataTable dt = db.ExecuteQuery("SELECT PersonnelID, FirstName + ' ' + LastName AS FullName FROM Personnel ORDER BY FirstName");
                cbPersonnel.DataSource = dt;
                cbPersonnel.DisplayMember = "FullName";
                cbPersonnel.ValueMember = "PersonnelID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در بارگذاری لیست پرسنل: " + ex.Message);
            }
        }

        private void CbPersonnel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPersonnel.SelectedIndex >= 0 && cbPersonnel.SelectedValue != null)
            {
                try
                {
                    int personnelId = (int)cbPersonnel.SelectedValue;
                    DisplayPersonnelDetails(personnelId);
                }
                catch
                {
                    // Ignore
                }
            }
        }

        private void DisplayPersonnelDetails(int personnelId)
        {
            try
            {
                string query = "SELECT PersonnelID, FirstName, LastName, FatherName, PersonnelNumber, NationalID, MobileNumber, BirthDate, HireDate, StartDateOperation, Description FROM Personnel WHERE PersonnelID = ?";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    new OleDbParameter("?", personnelId)
                };
                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    dgvPersonnelInfo.DataSource = dt;
                    foreach (DataGridViewColumn column in dgvPersonnelInfo.Columns)
                    {
                        column.Width = 150;
                        column.DefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در نمایش اطلاعات: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (cbPersonnel.SelectedIndex < 0)
            {
                MessageBox.Show("لطفاً یک پرسنل انتخاب کنید.", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string personnelName = cbPersonnel.SelectedItem.ToString();
            if (MessageBox.Show($"آیا مطمئن هستید که می‌خواهید این پرسنل \"{ personnelName }\" را حذف کنید?\n\nبر اساس رابطه‌های لحاظ نشده حذف خواهد شد.", "تأیید حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DeletePersonnel(true);
            }
        }

        private void BtnDeleteWithReferences_Click(object sender, EventArgs e)
        {
            if (cbPersonnel.SelectedIndex < 0)
            {
                MessageBox.Show("لطفاً یک پرسنل انتخاب کنید.", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string personnelName = cbPersonnel.SelectedItem.ToString();
            if (MessageBox.Show($"آیا مطمئن هستید که می‌خواهید \"{ personnelName }\" و تمام رابطه‌های مربوط را حذف کنید?\n\nاین کار برگشتناپذیر نیست!", "تأیید حذف حذاقانه", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                DeletePersonnel(false);
            }
        }

        private void DeletePersonnel(bool cascadeDelete)
        {
            if (cbPersonnel.SelectedIndex < 0)
                return;

            try
            {
                int personnelId = (int)cbPersonnel.SelectedValue;
                string query = "DELETE FROM Personnel WHERE PersonnelID = ?";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    new OleDbParameter("?", personnelId)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("پرسنل با موفقیت حذف شد!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ApplyRoundedCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }
    }
}
