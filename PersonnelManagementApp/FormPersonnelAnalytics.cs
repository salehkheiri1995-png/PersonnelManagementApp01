using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PersonnelManagementApp
{
    public partial class FormPersonnelAnalytics : BaseThemedForm
    {
        private readonly DbHelper dbHelper;
        private readonly TabControl tabControl;
        private readonly AnalyticsDataModel analyticsModel;

        // تمام نمودارها
        private readonly Chart chartDepartmentPie;
        private readonly Chart chartPositionPie;
        private readonly Chart chartGenderPie;
        private readonly Chart chartJobLevelPie;
        private readonly Chart chartContractTypePie;
        private readonly Chart chartProvincePie;
        private readonly Chart chartEducationPie;
        private readonly Chart chartCompanyPie;
        private readonly Chart chartWorkShiftPie;

        private readonly DataGridView dgvPersonnelStats;
        private readonly DataGridView dgvDepartmentDetails;
        private readonly DataGridView dgvPositionDetails;

        // فیلترها
        private readonly CheckedListBox clbProvincesFilter;
        private readonly CheckedListBox clbCitiesFilter;
        private readonly CheckedListBox clbAffairsFilter;
        private readonly CheckedListBox clbDepartmentsFilter;
        private readonly CheckedListBox clbDistrictsFilter;
        private readonly CheckedListBox clbPositionsFilter;
        private readonly CheckedListBox clbEducationFilter;
        private readonly CheckedListBox clbJobLevelFilter;
        private readonly CheckedListBox clbContractTypeFilter;
        private readonly CheckedListBox clbCompanyFilter;
        private readonly CheckedListBox clbWorkShiftFilter;
        private readonly CheckedListBox clbGenderFilter;

        private readonly Button btnClearFilters;
        private readonly Label lblFilterInfo;

        // فیلتر تاریخ استخدام
        private DateTimePicker dtpHireDateFrom;
        private DateTimePicker dtpHireDateTo;
        private CheckBox chkHireDateFilter;

        public FormPersonnelAnalytics()
        {
            dbHelper = new DbHelper();
            analyticsModel = new AnalyticsDataModel();
            tabControl = new TabControl();

            chartDepartmentPie = new Chart();
            chartPositionPie = new Chart();
            chartGenderPie = new Chart();
            chartJobLevelPie = new Chart();
            chartContractTypePie = new Chart();
            chartProvincePie = new Chart();
            chartEducationPie = new Chart();
            chartCompanyPie = new Chart();
            chartWorkShiftPie = new Chart();

            dgvPersonnelStats = new DataGridView();
            dgvDepartmentDetails = new DataGridView();
            dgvPositionDetails = new DataGridView();

            clbProvincesFilter = new CheckedListBox();
            clbCitiesFilter = new CheckedListBox();
            clbAffairsFilter = new CheckedListBox();
            clbDepartmentsFilter = new CheckedListBox();
            clbDistrictsFilter = new CheckedListBox();
            clbPositionsFilter = new CheckedListBox();
            clbEducationFilter = new CheckedListBox();
            clbJobLevelFilter = new CheckedListBox();
            clbContractTypeFilter = new CheckedListBox();
            clbCompanyFilter = new CheckedListBox();
            clbWorkShiftFilter = new CheckedListBox();
            clbGenderFilter = new CheckedListBox();

            btnClearFilters = new Button();
            lblFilterInfo = new Label();

            InitializeComponent();
            BuildUI();
            LoadData();
        }

        private void BuildUI()
        {
            Text = "🎯 تحلیل دادههای پرسنل - سیستم پیشرفته";
            WindowState = FormWindowState.Maximized;
            RightToLeft = RightToLeft.Yes;
            BackColor = Color.FromArgb(240, 248, 255);
            MinimumSize = new Size(1200, 700);
            Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize);

            // ========== پنل فیلتر اسکرول‌پذیر ==========
            Panel panelFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 350,
                BackColor = Color.FromArgb(230, 240, 250),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };
            RegisterThemedControl(panelFilter);

            int xPos = 15;
            int yPos = 15;
            int colWidth = 180;
            int colHeight = 280;

            // استانها
            CreateFilterColumn(panelFilter, "استانها 🗺️", clbProvincesFilter, xPos, yPos, colWidth, colHeight, ClbProvincesFilter_ItemCheck);
            xPos += colWidth + 10;

            // شهرها
            CreateFilterColumn(panelFilter, "شهرها 🏙️", clbCitiesFilter, xPos, yPos, colWidth, colHeight, ClbCitiesFilter_ItemCheck);
            xPos += colWidth + 10;

            // امور
            CreateFilterColumn(panelFilter, "امور 📋", clbAffairsFilter, xPos, yPos, colWidth, colHeight, ClbAffairsFilter_ItemCheck);
            xPos += colWidth + 10;

            // ادارات
            CreateFilterColumn(panelFilter, "ادارات 🏛️", clbDepartmentsFilter, xPos, yPos, colWidth, colHeight, ClbDepartmentsFilter_ItemCheck);
            xPos += colWidth + 10;

            // نواحی
            CreateFilterColumn(panelFilter, "نواحی 🔺", clbDistrictsFilter, xPos, yPos, colWidth, colHeight, ClbDistrictsFilter_ItemCheck);
            xPos += colWidth + 10;

            // پستها
            CreateFilterColumn(panelFilter, "پستها ⚡", clbPositionsFilter, xPos, yPos, colWidth, colHeight, ClbPositionsFilter_ItemCheck);
            xPos += colWidth + 10;

            // جنسیت
            CreateFilterColumn(panelFilter, "جنسیت 👥", clbGenderFilter, xPos, yPos, colWidth, colHeight, ClbGenderFilter_ItemCheck);
            xPos += colWidth + 10;

            // تحصیلات
            CreateFilterColumn(panelFilter, "تحصیلات 📚", clbEducationFilter, xPos, yPos, colWidth, colHeight, ClbEducationFilter_ItemCheck);
            xPos += colWidth + 10;

            // سطح شغلی
            CreateFilterColumn(panelFilter, "سطح شغلی 📊", clbJobLevelFilter, xPos, yPos, colWidth, colHeight, ClbJobLevelFilter_ItemCheck);
            xPos += colWidth + 10;

            // نوع قرارداد
            CreateFilterColumn(panelFilter, "نوع قرارداد 📄", clbContractTypeFilter, xPos, yPos, colWidth, colHeight, ClbContractTypeFilter_ItemCheck);
            xPos += colWidth + 10;

            // شرکت
            CreateFilterColumn(panelFilter, "شرکت 🏢", clbCompanyFilter, xPos, yPos, colWidth, colHeight, ClbCompanyFilter_ItemCheck);
            xPos += colWidth + 10;

            // شیفت کاری
            CreateFilterColumn(panelFilter, "شیفت کاری ⏰", clbWorkShiftFilter, xPos, yPos, colWidth, colHeight, ClbWorkShiftFilter_ItemCheck);
            xPos += colWidth + 10;

            // تاریخ استخدام
            yPos += colHeight + 20;
            xPos = 15;
            Label lblHireDate = new Label
            {
                Text = "📅 تاریخ استخدام",
                Location = new Point(xPos, yPos),
                Size = new Size(colWidth, 25),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204)
            };
            panelFilter.Controls.Add(lblHireDate);
            RegisterThemedControl(lblHireDate);

            chkHireDateFilter = new CheckBox
            {
                Text = "فعال‌سازی فیلتر",
                Location = new Point(xPos, yPos + 30),
                Size = new Size(colWidth, 25),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize)
            };
            chkHireDateFilter.CheckedChanged += ChkHireDateFilter_CheckedChanged;
            panelFilter.Controls.Add(chkHireDateFilter);
            RegisterThemedControl(chkHireDateFilter);

            dtpHireDateFrom = new DateTimePicker
            {
                Location = new Point(xPos, yPos + 60),
                Size = new Size(colWidth, 30),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize),
                Enabled = false,
                Value = DateTime.Now.AddYears(-10)
            };
            panelFilter.Controls.Add(dtpHireDateFrom);
            RegisterThemedControl(dtpHireDateFrom);

            Label lblTo = new Label
            {
                Text = "تا",
                Location = new Point(xPos, yPos + 95),
                Size = new Size(colWidth, 20),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize)
            };
            panelFilter.Controls.Add(lblTo);
            RegisterThemedControl(lblTo);

            dtpHireDateTo = new DateTimePicker
            {
                Location = new Point(xPos, yPos + 115),
                Size = new Size(colWidth, 30),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize),
                Enabled = false,
                Value = DateTime.Now
            };
            panelFilter.Controls.Add(dtpHireDateTo);
            RegisterThemedControl(dtpHireDateTo);

            // دکمه پاک کردن
            btnClearFilters.Text = "🔄 پاک کردن تمام فیلترها";
            btnClearFilters.Location = new Point(xPos, yPos + 155);
            btnClearFilters.Size = new Size(colWidth, 40);
            btnClearFilters.BackColor = Color.FromArgb(220, 53, 69);
            btnClearFilters.ForeColor = Color.White;
            btnClearFilters.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold);
            btnClearFilters.Click += BtnClearFilters_Click;
            btnClearFilters.FlatStyle = FlatStyle.Flat;
            panelFilter.Controls.Add(btnClearFilters);
            ApplyRoundedCorners(btnClearFilters, 10);
            RegisterThemedControl(btnClearFilters);

            // اطلاعات فیلتر
            lblFilterInfo.Text = "✓ فیلتری فعال نیست";
            lblFilterInfo.Location = new Point(15, 305);
            lblFilterInfo.Size = new Size(1000, 30);
            lblFilterInfo.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold);
            lblFilterInfo.ForeColor = Color.FromArgb(0, 102, 204);
            lblFilterInfo.AutoSize = false;
            panelFilter.Controls.Add(lblFilterInfo);
            RegisterThemedControl(lblFilterInfo);

            // ========== Tab Control ==========
            tabControl.Dock = DockStyle.Fill;
            tabControl.RightToLeft = RightToLeft.Yes;
            tabControl.ItemSize = new Size(180, 35);
            RegisterThemedControl(tabControl);

            // Tab خلاصه آماری
            TabPage tabSummary = CreateSummaryTab();
            tabControl.TabPages.Add(tabSummary);

            // تمام نمودارها
            AddChartTab(tabControl, "📊 ادارات", chartDepartmentPie, dgvDepartmentDetails);
            AddChartTab(tabControl, "💼 پستها", chartPositionPie, dgvPositionDetails);
            AddChartTab(tabControl, "👥 جنسیت", chartGenderPie, null);
            AddChartTab(tabControl, "📈 سطح شغلی", chartJobLevelPie, null);
            AddChartTab(tabControl, "📋 قرارداد", chartContractTypePie, null);
            AddChartTab(tabControl, "🗺️ استان", chartProvincePie, null);
            AddChartTab(tabControl, "📚 تحصیلات", chartEducationPie, null);
            AddChartTab(tabControl, "🏢 شرکت", chartCompanyPie, null);
            AddChartTab(tabControl, "⏰ شیفت", chartWorkShiftPie, null);

            // Tab جدول آمار کامل
            TabPage tabStats = new TabPage("📋 جدول کامل آمار");
            dgvPersonnelStats.Dock = DockStyle.Fill;
            dgvPersonnelStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPersonnelStats.ReadOnly = true;
            dgvPersonnelStats.RightToLeft = RightToLeft.Yes;
            dgvPersonnelStats.BackgroundColor = Color.White;
            dgvPersonnelStats.EnableHeadersVisualStyles = false;
            dgvPersonnelStats.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgvPersonnelStats.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPersonnelStats.ColumnHeadersDefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold);
            dgvPersonnelStats.ColumnHeadersHeight = 35;
            dgvPersonnelStats.DefaultCellStyle.BackColor = Color.White;
            dgvPersonnelStats.DefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize);
            dgvPersonnelStats.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
            tabStats.Controls.Add(dgvPersonnelStats);
            tabControl.TabPages.Add(tabStats);
            RegisterThemedControl(dgvPersonnelStats);

            Controls.Add(tabControl);
            Controls.Add(panelFilter);
        }

        private void CreateFilterColumn(Panel parent, string title, CheckedListBox clb, int x, int y, int width, int height, ItemCheckEventHandler eventHandler)
        {
            Label lbl = new Label
            {
                Text = title,
                Location = new Point(x, y),
                Size = new Size(width, 25),
                Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204)
            };
            parent.Controls.Add(lbl);
            RegisterThemedControl(lbl);

            clb.Location = new Point(x, y + 30);
            clb.Size = new Size(width, height - 30);
            clb.RightToLeft = RightToLeft.Yes;
            clb.ItemCheck += eventHandler;
            clb.BackColor = Color.White;
            clb.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize);
            parent.Controls.Add(clb);
            RegisterThemedControl(clb);
        }

        private void AddChartTab(TabControl tabControl, string title, Chart chart, DataGridView detailsGrid)
        {
            TabPage tab = new TabPage(title);

            if (detailsGrid != null)
            {
                SplitContainer split = new SplitContainer
                {
                    Dock = DockStyle.Fill,
                    Orientation = Orientation.Horizontal,
                    SplitterDistance = 400
                };

                chart.Dock = DockStyle.Fill;
                chart.BackColor = Color.White;
                chart.MinimumSize = new Size(100, 100);
                chart.ChartAreas.Add(new ChartArea("ChartArea1")
                {
                    BackColor = Color.White,
                    Area3DStyle = { Enable3D = true, Inclination = 15, Rotation = 45 }
                });
                chart.MouseClick += Chart_MouseClick;
                split.Panel1.Controls.Add(chart);

                detailsGrid.Dock = DockStyle.Fill;
                detailsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                detailsGrid.ReadOnly = true;
                detailsGrid.RightToLeft = RightToLeft.Yes;
                detailsGrid.BackgroundColor = Color.White;
                detailsGrid.EnableHeadersVisualStyles = false;
                detailsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
                detailsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                detailsGrid.ColumnHeadersDefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold);
                split.Panel2.Controls.Add(detailsGrid);
                RegisterThemedControl(detailsGrid);

                tab.Controls.Add(split);
            }
            else
            {
                chart.Dock = DockStyle.Fill;
                chart.BackColor = Color.White;
                chart.MinimumSize = new Size(100, 100);
                chart.ChartAreas.Add(new ChartArea("ChartArea1")
                {
                    BackColor = Color.White,
                    Area3DStyle = { Enable3D = true, Inclination = 15, Rotation = 45 }
                });
                chart.MouseClick += Chart_MouseClick;
                tab.Controls.Add(chart);
            }

            tabControl.TabPages.Add(tab);
        }

        private TabPage CreateSummaryTab()
        {
            TabPage tab = new TabPage("📊 خلاصه آماری");
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                ReadOnly = true,
                RightToLeft = RightToLeft.Yes,
                BackgroundColor = Color.White,
                EnableHeadersVisualStyles = false
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;
            dgv.DefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize);
            dgv.Columns.Add("Metric", "معیار");
            dgv.Columns.Add("Value", "مقدار");
            tab.Controls.Add(dgv);
            tab.Tag = dgv;
            RegisterThemedControl(dgv);
            return tab;
        }

        private void LoadData()
        {
            try
            {
                if (!dbHelper.TestConnection())
                {
                    MessageBox.Show("❌ اتصال به دیتابیس ناموفق بود.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!analyticsModel.LoadData(dbHelper))
                {
                    MessageBox.Show("❌ خطا در بارگذاری دادهها.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LoadFilterOptions();
                RefreshAllCharts();
                MessageBox.Show($"✅ دادهها با موفقیت بارگذاری شدند.\n👥 تعداد پرسنل: {analyticsModel.TotalPersonnel}", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFilterOptions()
        {
            clbProvincesFilter.Items.Clear();
            foreach (var p in analyticsModel.GetAllProvinces())
                clbProvincesFilter.Items.Add(p, false);

            clbGenderFilter.Items.Clear();
            foreach (var g in analyticsModel.GetAllGenders())
                clbGenderFilter.Items.Add(g, false);

            clbEducationFilter.Items.Clear();
            foreach (var e in analyticsModel.GetAllEducations())
                clbEducationFilter.Items.Add(e, false);

            clbJobLevelFilter.Items.Clear();
            foreach (var j in analyticsModel.GetAllJobLevels())
                clbJobLevelFilter.Items.Add(j, false);

            clbContractTypeFilter.Items.Clear();
            foreach (var c in analyticsModel.GetAllContractTypes())
                clbContractTypeFilter.Items.Add(c, false);

            clbCompanyFilter.Items.Clear();
            foreach (var co in analyticsModel.GetAllCompanies())
                clbCompanyFilter.Items.Add(co, false);

            clbWorkShiftFilter.Items.Clear();
            foreach (var ws in analyticsModel.GetAllWorkShifts())
                clbWorkShiftFilter.Items.Add(ws, false);
        }

        private void ClbProvincesFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                UpdateCitiesAndAffairs();
                RefreshAllCharts();
            });
        }

        private void ClbCitiesFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                UpdateDepartmentsAndDistricts();
                RefreshAllCharts();
            });
        }

        private void ClbAffairsFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                UpdateDepartmentsAndDistricts();
                RefreshAllCharts();
            });
        }

        private void ClbDepartmentsFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                UpdateDistrictsAndPositions();
                RefreshAllCharts();
            });
        }

        private void ClbDistrictsFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                UpdatePositions();
                RefreshAllCharts();
            });
        }

        private void ClbPositionsFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                RefreshAllCharts();
            });
        }

        private void ClbGenderFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                RefreshAllCharts();
            });
        }

        private void ClbEducationFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                RefreshAllCharts();
            });
        }

        private void ClbJobLevelFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                RefreshAllCharts();
            });
        }

        private void ClbContractTypeFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                RefreshAllCharts();
            });
        }

        private void ClbCompanyFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                RefreshAllCharts();
            });
        }

        private void ClbWorkShiftFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                RefreshAllCharts();
            });
        }

        private void ChkHireDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            dtpHireDateFrom.Enabled = chkHireDateFilter.Checked;
            dtpHireDateTo.Enabled = chkHireDateFilter.Checked;
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateFilters();
                RefreshAllCharts();
            });
        }

        private void UpdateFilters()
        {
            List<string> selectedProvinces = clbProvincesFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedCities = clbCitiesFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedAffairs = clbAffairsFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedDepts = clbDepartmentsFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedDistricts = clbDistrictsFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedPositions = clbPositionsFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedGenders = clbGenderFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedEducations = clbEducationFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedJobLevels = clbJobLevelFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedContractTypes = clbContractTypeFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedCompanies = clbCompanyFilter.CheckedItems.Cast<string>().ToList();
            List<string> selectedWorkShifts = clbWorkShiftFilter.CheckedItems.Cast<string>().ToList();

            DateTime? hireFromDate = chkHireDateFilter.Checked ? dtpHireDateFrom.Value : (DateTime?)null;
            DateTime? hireToDate = chkHireDateFilter.Checked ? dtpHireDateTo.Value : (DateTime?)null;

            analyticsModel.SetFilters(selectedProvinces, selectedCities, selectedAffairs, selectedDepts,
                selectedDistricts, selectedPositions, selectedGenders, selectedEducations, selectedJobLevels,
                selectedContractTypes, selectedCompanies, selectedWorkShifts, hireFromDate, hireToDate);

            int filterCount = selectedProvinces.Count + selectedCities.Count + selectedAffairs.Count +
                selectedDepts.Count + selectedDistricts.Count + selectedPositions.Count +
                selectedGenders.Count + selectedEducations.Count + selectedJobLevels.Count +
                selectedContractTypes.Count + selectedCompanies.Count + selectedWorkShifts.Count +
                (chkHireDateFilter.Checked ? 1 : 0);

            lblFilterInfo.Text = filterCount > 0 ? $"🔴 {filterCount} فیلتر فعال" : "✓ فیلتری فعال نیست";
        }

        private void UpdateCitiesAndAffairs()
        {
            clbCitiesFilter.Items.Clear();
            clbAffairsFilter.Items.Clear();
            var selectedProvinces = clbProvincesFilter.CheckedItems.Cast<string>().ToList();

            if (selectedProvinces.Count > 0)
            {
                foreach (var city in analyticsModel.GetCitiesByProvinces(selectedProvinces).Distinct().OrderBy(x => x))
                    clbCitiesFilter.Items.Add(city, false);

                foreach (var affair in analyticsModel.GetAffairsByProvinces(selectedProvinces).Distinct().OrderBy(x => x))
                    clbAffairsFilter.Items.Add(affair, false);
            }
        }

        private void UpdateDepartmentsAndDistricts()
        {
            clbDepartmentsFilter.Items.Clear();
            clbDistrictsFilter.Items.Clear();
            var selectedProvinces = clbProvincesFilter.CheckedItems.Cast<string>().ToList();
            var selectedCities = clbCitiesFilter.CheckedItems.Cast<string>().ToList();
            var selectedAffairs = clbAffairsFilter.CheckedItems.Cast<string>().ToList();

            if (selectedProvinces.Count > 0 || selectedCities.Count > 0 || selectedAffairs.Count > 0)
            {
                foreach (var dept in analyticsModel.GetDepartmentsByFilters(selectedProvinces, selectedCities, selectedAffairs).Distinct().OrderBy(x => x))
                    clbDepartmentsFilter.Items.Add(dept, false);
            }
        }

        private void UpdateDistrictsAndPositions()
        {
            clbDistrictsFilter.Items.Clear();
            var selectedDepts = clbDepartmentsFilter.CheckedItems.Cast<string>().ToList();

            if (selectedDepts.Count > 0)
            {
                foreach (var district in analyticsModel.GetDistrictsByDepartments(selectedDepts).Distinct().OrderBy(x => x))
                    clbDistrictsFilter.Items.Add(district, false);
            }
        }

        private void UpdatePositions()
        {
            clbPositionsFilter.Items.Clear();
            var selectedDistricts = clbDistrictsFilter.CheckedItems.Cast<string>().ToList();

            if (selectedDistricts.Count > 0)
            {
                foreach (var pos in analyticsModel.GetPositionsByDistricts(selectedDistricts).Distinct().OrderBy(x => x))
                    clbPositionsFilter.Items.Add(pos, false);
            }
        }

        private void BtnClearFilters_Click(object sender, EventArgs e)
        {
            clbProvincesFilter.ClearSelected();
            clbCitiesFilter.ClearSelected();
            clbAffairsFilter.ClearSelected();
            clbDepartmentsFilter.ClearSelected();
            clbDistrictsFilter.ClearSelected();
            clbPositionsFilter.ClearSelected();
            clbGenderFilter.ClearSelected();
            clbEducationFilter.ClearSelected();
            clbJobLevelFilter.ClearSelected();
            clbContractTypeFilter.ClearSelected();
            clbCompanyFilter.ClearSelected();
            clbWorkShiftFilter.ClearSelected();
            chkHireDateFilter.Checked = false;

            analyticsModel.ClearFilters();
            lblFilterInfo.Text = "✓ فیلتری فعال نیست";
            LoadFilterOptions();
            RefreshAllCharts();
        }

        private void RefreshAllCharts()
        {
            LoadSummaryTab();
            LoadDepartmentPieChart();
            LoadPositionPieChart();
            LoadGenderPieChart();
            LoadJobLevelPieChart();
            LoadContractTypePieChart();
            LoadProvincePieChart();
            LoadEducationPieChart();
            LoadCompanyPieChart();
            LoadWorkShiftPieChart();
            LoadStatisticalTable();
        }

        private void LoadSummaryTab()
        {
            try
            {
                DataGridView dgv = tabControl.TabPages[0].Tag as DataGridView;
                dgv?.Rows.Clear();
                dgv?.Rows.Add("👥 کل پرسنل", analyticsModel.GetFilteredTotal());
                dgv?.Rows.Add("🏛️ تعداد ادارهها", analyticsModel.GetFilteredDepartmentCount());
                dgv?.Rows.Add("💼 تعداد پستهای شغلی", analyticsModel.GetFilteredPositionCount());
                dgv?.Rows.Add("🗺️ تعداد استانها", analyticsModel.ProvinceCount);
                dgv?.Rows.Add("🏢 تعداد شرکتها", analyticsModel.CompanyCount);
                dgv?.Rows.Add("📈 تعداد سطحهای شغلی", analyticsModel.JobLevelCount);
                dgv?.Rows.Add("📋 تعداد انواع قرارداد", analyticsModel.ContractTypeCount);
                dgv?.Rows.Add("📚 تعداد مدارک تحصیلی", analyticsModel.EducationCount);
                dgv?.Rows.Add("⏰ تعداد شیفت‌های کاری", analyticsModel.WorkShiftCount);
                dgv?.Rows.Add("", "");
                dgv?.Rows.Add("👩 افراد خانم", analyticsModel.GetFilteredFemaleCount());
                dgv?.Rows.Add("👨 افراد آقا", analyticsModel.GetFilteredMaleCount());
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadDepartmentPieChart()
        {
            try
            {
                chartDepartmentPie.Series.Clear();
                var stats = analyticsModel.GetFilteredDepartmentStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats.Take(15))
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartDepartmentPie.Series.Add(series);
                chartDepartmentPie.Titles.Clear();
                chartDepartmentPie.Titles.Add(new Title("📊 توزیع پرسنل در ادارهها") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });

                dgvDepartmentDetails.DataSource = null;
                dgvDepartmentDetails.Columns.Clear();
                dgvDepartmentDetails.Columns.Add("Name", "اداره");
                dgvDepartmentDetails.Columns.Add("Count", "تعداد");
                dgvDepartmentDetails.Columns.Add("Percent", "درصد");
                foreach (var item in stats)
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    dgvDepartmentDetails.Rows.Add(item.Name, item.Count, $"{pct:F1}%");
                }
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadPositionPieChart()
        {
            try
            {
                chartPositionPie.Series.Clear();
                var stats = analyticsModel.GetFilteredPositionStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats.Take(15))
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartPositionPie.Series.Add(series);
                chartPositionPie.Titles.Clear();
                chartPositionPie.Titles.Add(new Title("💼 توزیع پستهای شغلی") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });

                dgvPositionDetails.DataSource = null;
                dgvPositionDetails.Columns.Clear();
                dgvPositionDetails.Columns.Add("Name", "پست");
                dgvPositionDetails.Columns.Add("Count", "تعداد");
                dgvPositionDetails.Columns.Add("Percent", "درصد");
                foreach (var item in stats)
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    dgvPositionDetails.Rows.Add(item.Name, item.Count, $"{pct:F1}%");
                }
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadGenderPieChart()
        {
            try
            {
                chartGenderPie.Series.Clear();
                var stats = analyticsModel.GetFilteredGenderStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats)
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartGenderPie.Series.Add(series);
                chartGenderPie.Titles.Clear();
                chartGenderPie.Titles.Add(new Title("👥 توزیع جنسیت") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadJobLevelPieChart()
        {
            try
            {
                chartJobLevelPie.Series.Clear();
                var stats = analyticsModel.GetFilteredJobLevelStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats)
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartJobLevelPie.Series.Add(series);
                chartJobLevelPie.Titles.Clear();
                chartJobLevelPie.Titles.Add(new Title("📈 توزیع سطح شغلی") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadContractTypePieChart()
        {
            try
            {
                chartContractTypePie.Series.Clear();
                var stats = analyticsModel.GetFilteredContractTypeStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats)
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartContractTypePie.Series.Add(series);
                chartContractTypePie.Titles.Clear();
                chartContractTypePie.Titles.Add(new Title("📋 توزیع نوع قرارداد") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadProvincePieChart()
        {
            try
            {
                chartProvincePie.Series.Clear();
                var stats = analyticsModel.GetFilteredProvinceStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats.Take(20))
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartProvincePie.Series.Add(series);
                chartProvincePie.Titles.Clear();
                chartProvincePie.Titles.Add(new Title("🗺️ توزیع بر اساس استان") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadEducationPieChart()
        {
            try
            {
                chartEducationPie.Series.Clear();
                var stats = analyticsModel.GetFilteredEducationStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats)
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartEducationPie.Series.Add(series);
                chartEducationPie.Titles.Clear();
                chartEducationPie.Titles.Add(new Title("📚 توزیع مدارک تحصیلی") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadCompanyPieChart()
        {
            try
            {
                chartCompanyPie.Series.Clear();
                var stats = analyticsModel.GetFilteredCompanyStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats)
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartCompanyPie.Series.Add(series);
                chartCompanyPie.Titles.Clear();
                chartCompanyPie.Titles.Add(new Title("🏢 توزیع شرکتها") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void LoadWorkShiftPieChart()
        {
            try
            {
                chartWorkShiftPie.Series.Clear();
                var stats = analyticsModel.GetFilteredWorkShiftStatistics();
                int total = stats.Sum(x => x.Count);

                Series series = new Series("درصد")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    CustomProperties = "PieLabelStyle=Outside"
                };

                foreach (var item in stats)
                {
                    double pct = total > 0 ? (item.Count * 100.0) / total : 0;
                    int idx = series.Points.AddXY(item.Name, item.Count);
                    series.Points[idx].Label = $"{item.Name}\n{item.Count} نفر ({pct:F1}%)";
                    series.Points[idx].ToolTip = $"{item.Name}: {item.Count} نفر ({pct:F1}%)";
                }

                chartWorkShiftPie.Series.Add(series);
                chartWorkShiftPie.Titles.Clear();
                chartWorkShiftPie.Titles.Add(new Title("⏰ توزیع شیفت‌های کاری") { Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize + 2, FontStyle.Bold) });
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void Chart_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Chart chart = sender as Chart;
                if (chart == null) return;

                HitTestResult result = chart.HitTest(e.X, e.Y);
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    int pointIndex = result.PointIndex;
                    DataPoint point = result.Series.Points[pointIndex];
                    string itemName = point.AxisLabel;

                    var personnel = analyticsModel.GetPersonnelByFilter(itemName, chart);
                    if (personnel.Count > 0)
                        ShowPersonnelDetails(itemName, personnel);
                    else
                        MessageBox.Show("❌ داده‌ای برای نمایش وجود ندارد.", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }

        private void ShowPersonnelDetails(string category, List<PersonnelDetail> personnel)
        {
            Form detailsForm = new Form
            {
                Text = $"👥 جزئیات پرسنل - {category}",
                Size = new Size(1400, 800),
                StartPosition = FormStartPosition.CenterScreen,
                RightToLeft = RightToLeft.Yes,
                BackColor = Color.FromArgb(240, 248, 255),
                WindowState = FormWindowState.Maximized
            };

            // =============== DataGridView ===============
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                ReadOnly = false,
                RightToLeft = RightToLeft.Yes,
                BackgroundColor = Color.White,
                EnableHeadersVisualStyles = false,
                AllowUserToAddRows = false,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            dgv.Columns.Add("PersonnelID", "ID");
            dgv.Columns["PersonnelID"].Visible = false;
            dgv.Columns.Add("FirstName", "نام");
            dgv.Columns.Add("LastName", "نام‌خانوادگی");
            dgv.Columns.Add("PersonnelNumber", "شماره پرسنلی");
            dgv.Columns.Add("NationalID", "شناسه ملی");
            dgv.Columns.Add("PostName", "پست");
            dgv.Columns.Add("DeptName", "اداره");
            dgv.Columns.Add("Province", "استان");
            dgv.Columns.Add("ContractType", "نوع قرارداد");
            dgv.Columns.Add("HireDate", "تاریخ استخدام");
            dgv.Columns.Add("MobileNumber", "تلفن");

            // سیل‌های اکشن
            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "✏️ ویرایش",
                Text = "ویرایش",
                UseColumnTextForButtonValue = true,
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(40, 167, 69),
                    ForeColor = Color.White,
                    Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Padding = new Padding(5)
                }
            };
            dgv.Columns.Add(editColumn);

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "🗑️ حذف",
                Text = "حذف",
                UseColumnTextForButtonValue = true,
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(220, 53, 69),
                    ForeColor = Color.White,
                    Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Padding = new Padding(5)
                }
            };
            dgv.Columns.Add(deleteColumn);

            int rowIndex = 0;
            foreach (var p in personnel)
            {
                dgv.Rows.Add(p.PersonnelID, p.FirstName, p.LastName, p.PersonnelNumber, p.NationalID, p.PostName,
                    p.DeptName, p.Province, p.ContractType, p.HireDate?.ToString("yyyy/MM/dd"), p.MobileNumber, "ویرایش", "حذف");
                rowIndex++;
            }

            // ============ EVENT HANDLER - FIXED ============
            dgv.CellClick += (sender, e) =>
            {
                if (e.ColumnIndex == dgv.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    int personnelID = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["PersonnelID"].Value);
                    string firstName = dgv.Rows[e.RowIndex].Cells["FirstName"].Value?.ToString() ?? "";
                    
                    try
                    {
                        FormPersonnelEdit editForm = new FormPersonnelEdit(personnelID);
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show($"✅ پرسنل {firstName} با موفقیت به‌روز شد.", "موفق");
                            RefreshAllCharts();
                            detailsForm.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"❌ خطا: {ex.Message}", "خطا");
                    }
                }
                else if (e.ColumnIndex == dgv.Columns["Delete"].Index && e.RowIndex >= 0)
                {
                    int personnelID = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["PersonnelID"].Value);
                    string firstName = dgv.Rows[e.RowIndex].Cells["FirstName"].Value?.ToString() ?? "";
                    string lastName = dgv.Rows[e.RowIndex].Cells["LastName"].Value?.ToString() ?? "";
                    
                    DialogResult result = MessageBox.Show(
                        $"آیا مطمئن هستید که می‌خواهید این پرسنل را حذف کنید?\n\nنام: {firstName} {lastName}\nشماره: {personnelID}",
                        "تأیید حذف",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            DbHelper db = new DbHelper();
                            if (db.DeletePersonnel(personnelID))
                            {
                                MessageBox.Show($"✅ پرسنل {firstName} {lastName} با موفقیت حذف شد.", "موفق");
                                dgv.Rows.RemoveAt(e.RowIndex);
                                RefreshAllCharts();
                                
                                if (dgv.Rows.Count == 0)
                                {
                                    MessageBox.Show("تمام رکوردها حذف شدند.", "اطلاع");
                                    detailsForm.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("❌ خطا در حذف پرسنل. لطفاً دوباره تلاش کنید.", "خطا");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"❌ خطا: {ex.Message}", "خطا");
                        }
                    }
                }
            };

            detailsForm.Controls.Add(dgv);
            detailsForm.ShowDialog();
        }

        private void LoadStatisticalTable()
        {
            try
            {
                dgvPersonnelStats.DataSource = null;
                dgvPersonnelStats.Columns.Clear();
                dgvPersonnelStats.Columns.Add("Metric", "معیار");
                dgvPersonnelStats.Columns.Add("Value", "مقدار");
                dgvPersonnelStats.DefaultCellStyle.Font = new Font(SettingsManager.Instance.PrimaryFont, SettingsManager.Instance.PrimaryFontSize);

                // خلاصه کلی
                dgvPersonnelStats.Rows.Add("═══════════════════", "");
                dgvPersonnelStats.Rows.Add("👥 کل پرسنل", analyticsModel.GetFilteredTotal());
                dgvPersonnelStats.Rows.Add("🏛️ تعداد ادارهها", analyticsModel.GetFilteredDepartmentCount());
                dgvPersonnelStats.Rows.Add("💼 تعداد پستهای شغلی", analyticsModel.GetFilteredPositionCount());
                dgvPersonnelStats.Rows.Add("🗺️ تعداد استانها", analyticsModel.ProvinceCount);
                dgvPersonnelStats.Rows.Add("🏢 تعداد شرکتها", analyticsModel.CompanyCount);
                dgvPersonnelStats.Rows.Add("📈 تعداد سطحهای شغلی", analyticsModel.JobLevelCount);
                dgvPersonnelStats.Rows.Add("📋 تعداد انواع قرارداد", analyticsModel.ContractTypeCount);
                dgvPersonnelStats.Rows.Add("📚 تعداد مدارک تحصیلی", analyticsModel.EducationCount);
                dgvPersonnelStats.Rows.Add("⏰ تعداد شیفت‌های کاری", analyticsModel.WorkShiftCount);

                // جنسیت
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("═════ توزیع جنسیت ═════", "");
                foreach (var g in analyticsModel.GetFilteredGenderStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {g.Name}", g.Count);

                // سطح شغلی
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("════ توزیع سطح شغلی ════", "");
                foreach (var j in analyticsModel.GetFilteredJobLevelStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {j.Name}", j.Count);

                // نوع قرارداد
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("════ توزیع نوع قرارداد ════", "");
                foreach (var c in analyticsModel.GetFilteredContractTypeStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {c.Name}", c.Count);

                // ادارات
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("═════════ تمام ادارات ═════════", "");
                foreach (var d in analyticsModel.GetFilteredDepartmentStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {d.Name}", d.Count);

                // پستها
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("════════ تمام پستهای شغلی ════════", "");
                foreach (var p in analyticsModel.GetFilteredPositionStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {p.Name}", p.Count);

                // استان‌ها
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("════════════ تمام استان‌ها ════════════", "");
                foreach (var pr in analyticsModel.GetFilteredProvinceStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {pr.Name}", pr.Count);

                // شرکتها
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("════════════ تمام شرکتها ════════════", "");
                foreach (var co in analyticsModel.GetFilteredCompanyStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {co.Name}", co.Count);

                // تحصیلات
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("═════════ تمام مدارک تحصیلی ═════════", "");
                foreach (var e in analyticsModel.GetFilteredEducationStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {e.Name}", e.Count);

                // شیفت کاری
                dgvPersonnelStats.Rows.Add("", "");
                dgvPersonnelStats.Rows.Add("═════════ تمام شیفت‌های کاری ═════════", "");
                foreach (var ws in analyticsModel.GetFilteredWorkShiftStatistics())
                    dgvPersonnelStats.Rows.Add($"  • {ws.Name}", ws.Count);
            }
            catch (Exception ex) { MessageBox.Show($"❌ خطا: {ex.Message}"); }
        }
    }

    // ========== AnalyticsDataModel ==========
    public class AnalyticsDataModel
    {
        private List<PersonnelRecord> personnelList = new();
        private List<string> filteredProvinces = new();
        private List<string> filteredCities = new();
        private List<string> filteredAffairs = new();
        private List<string> filteredDepartments = new();
        private List<string> filteredDistricts = new();
        private List<string> filteredPositions = new();
        private List<string> filteredGenders = new();
        private List<string> filteredEducations = new();
        private List<string> filteredJobLevels = new();
        private List<string> filteredContractTypes = new();
        private List<string> filteredCompanies = new();
        private List<string> filteredWorkShifts = new();
        private DateTime? hireDateFrom = null;
        private DateTime? hireDateTo = null;

        private readonly Dictionary<int, string> provinceCache = new();
        private readonly Dictionary<int, string> cityCache = new();
        private readonly Dictionary<int, string> affairCache = new();
        private readonly Dictionary<int, string> departmentCache = new();
        private readonly Dictionary<int, string> districtCache = new();
        private readonly Dictionary<int, string> positionCache = new();
        private readonly Dictionary<int, string> genderCache = new();
        private readonly Dictionary<int, string> degreeCache = new();
        private readonly Dictionary<int, string> jobLevelCache = new();
        private readonly Dictionary<int, string> contractTypeCache = new();
        private readonly Dictionary<int, string> companyCache = new();
        private readonly Dictionary<int, string> workShiftCache = new();

        public int TotalPersonnel { get; private set; }
        public int DepartmentCount { get; private set; }
        public int PositionCount { get; private set; }
        public int ProvinceCount { get; private set; }
        public int CompanyCount { get; private set; }
        public int JobLevelCount { get; private set; }
        public int ContractTypeCount { get; private set; }
        public int EducationCount { get; private set; }
        public int WorkShiftCount { get; private set; }
        public int MaleCount { get; private set; }
        public int FemaleCount { get; private set; }

        public bool LoadData(DbHelper dbHelper)
        {
            try
            {
                LoadAllCaches(dbHelper);

                DataTable dt = dbHelper.ExecuteQuery(@"SELECT PersonnelID, ProvinceID, CityID, AffairID, DeptID, DistrictID, PostNameID, 
                    VoltageID, WorkShiftID, GenderID, FirstName, LastName, FatherName, PersonnelNumber, NationalID, MobileNumber, 
                    BirthDate, HireDate, StartDateOperation, ContractTypeID, JobLevelID, CompanyID, DegreeID, DegreeFieldID, 
                    MainJobTitle, CurrentActivity, StatusID FROM Personnel");

                if (dt?.Rows.Count == 0) return false;

                personnelList.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    personnelList.Add(new PersonnelRecord
                    {
                        PersonnelID = Convert.ToInt32(row["PersonnelID"]),
                        ProvinceID = GetIntValue(row["ProvinceID"]),
                        CityID = GetIntValue(row["CityID"]),
                        AffairID = GetIntValue(row["AffairID"]),
                        DeptID = GetIntValue(row["DeptID"]),
                        DistrictID = GetIntValue(row["DistrictID"]),
                        PostNameID = GetIntValue(row["PostNameID"]),
                        VoltageID = GetIntValue(row["VoltageID"]),
                        WorkShiftID = GetIntValue(row["WorkShiftID"]),
                        GenderID = GetIntValue(row["GenderID"]),
                        FirstName = row["FirstName"]?.ToString() ?? "",
                        LastName = row["LastName"]?.ToString() ?? "",
                        FatherName = row["FatherName"]?.ToString() ?? "",
                        PersonnelNumber = row["PersonnelNumber"]?.ToString() ?? "",
                        NationalID = row["NationalID"]?.ToString() ?? "",
                        MobileNumber = row["MobileNumber"]?.ToString() ?? "",
                        BirthDate = GetDateValue(row["BirthDate"]),
                        HireDate = GetDateValue(row["HireDate"]),
                        StartDateOperation = GetDateValue(row["StartDateOperation"]),
                        ContractTypeID = GetIntValue(row["ContractTypeID"]),
                        JobLevelID = GetIntValue(row["JobLevelID"]),
                        CompanyID = GetIntValue(row["CompanyID"]),
                        DegreeID = GetIntValue(row["DegreeID"]),
                        DegreeFieldID = GetIntValue(row["DegreeFieldID"]),
                        MainJobTitle = GetIntValue(row["MainJobTitle"]),
                        CurrentActivity = GetIntValue(row["CurrentActivity"]),
                        StatusID = GetIntValue(row["StatusID"])
                    });
                }

                CalculateStatistics();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطا در بارگذاری دادهها: {ex.Message}");
                return false;
            }
        }

        private int GetIntValue(object value) => value != DBNull.Value && value != null ? Convert.ToInt32(value) : 0;
        private DateTime? GetDateValue(object value) => value != DBNull.Value && value != null ? Convert.ToDateTime(value) : (DateTime?)null;

        private void LoadAllCaches(DbHelper dbHelper)
        {
            LoadCache(dbHelper, "SELECT ProvinceID, ProvinceName FROM Provinces", provinceCache);
            LoadCache(dbHelper, "SELECT CityID, CityName FROM Cities", cityCache);
            LoadCache(dbHelper, "SELECT AffairID, AffairName FROM TransferAffairs", affairCache);
            LoadCache(dbHelper, "SELECT DeptID, DeptName FROM OperationDepartments", departmentCache);
            LoadCache(dbHelper, "SELECT DistrictID, DistrictName FROM Districts", districtCache);
            LoadCache(dbHelper, "SELECT PostNameID, PostName FROM PostsNames", positionCache);
            LoadCache(dbHelper, "SELECT GenderID, GenderName FROM Gender", genderCache);
            LoadCache(dbHelper, "SELECT DegreeID, DegreeName FROM Degree", degreeCache);
            LoadCache(dbHelper, "SELECT JobLevelID, JobLevelName FROM JobLevel", jobLevelCache);
            LoadCache(dbHelper, "SELECT ContractTypeID, ContractTypeName FROM ContractType", contractTypeCache);
            LoadCache(dbHelper, "SELECT CompanyID, CompanyName FROM Company", companyCache);
            LoadCache(dbHelper, "SELECT WorkShiftID, WorkShiftName FROM WorkShift", workShiftCache);
        }

        private void LoadCache(DbHelper dbHelper, string query, Dictionary<int, string> cache)
        {
            try
            {
                DataTable dt = dbHelper.ExecuteQuery(query);
                if (dt == null) return;

                string keyColumn = dt.Columns[0].ColumnName;
                string valueColumn = dt.Columns[1].ColumnName;

                foreach (DataRow row in dt.Rows)
                {
                    int key = Convert.ToInt32(row[keyColumn]);
                    string value = row[valueColumn]?.ToString() ?? "";
                    cache[key] = value;
                }
            }
            catch { }
        }

        private void CalculateStatistics()
        {
            TotalPersonnel = personnelList.Count;
            DepartmentCount = personnelList.Select(p => p.DeptID).Distinct().Count(x => x > 0);
            PositionCount = personnelList.Select(p => p.PostNameID).Distinct().Count(x => x > 0);
            ProvinceCount = personnelList.Select(p => p.ProvinceID).Distinct().Count(x => x > 0);
            CompanyCount = personnelList.Select(p => p.CompanyID).Distinct().Count(x => x > 0);
            JobLevelCount = personnelList.Select(p => p.JobLevelID).Distinct().Count(x => x > 0);
            ContractTypeCount = personnelList.Select(p => p.ContractTypeID).Distinct().Count(x => x > 0);
            EducationCount = personnelList.Select(p => p.DegreeID).Distinct().Count(x => x > 0);
            WorkShiftCount = personnelList.Select(p => p.WorkShiftID).Distinct().Count(x => x > 0);
            MaleCount = personnelList.Count(p => p.GenderID == 1);
            FemaleCount = personnelList.Count(p => p.GenderID == 2);
        }

        public void SetFilters(List<string> provinces, List<string> cities, List<string> affairs, List<string> depts,
            List<string> districts, List<string> positions, List<string> genders, List<string> educations,
            List<string> jobLevels, List<string> contractTypes, List<string> companies, List<string> workShifts,
            DateTime? hireFrom, DateTime? hireTo)
        {
            filteredProvinces = provinces ?? new();
            filteredCities = cities ?? new();
            filteredAffairs = affairs ?? new();
            filteredDepartments = depts ?? new();
            filteredDistricts = districts ?? new();
            filteredPositions = positions ?? new();
            filteredGenders = genders ?? new();
            filteredEducations = educations ?? new();
            filteredJobLevels = jobLevels ?? new();
            filteredContractTypes = contractTypes ?? new();
            filteredCompanies = companies ?? new();
            filteredWorkShifts = workShifts ?? new();
            hireDateFrom = hireFrom;
            hireDateTo = hireTo;
        }

        public void ClearFilters()
        {
            filteredProvinces.Clear();
            filteredCities.Clear();
            filteredAffairs.Clear();
            filteredDepartments.Clear();
            filteredDistricts.Clear();
            filteredPositions.Clear();
            filteredGenders.Clear();
            filteredEducations.Clear();
            filteredJobLevels.Clear();
            filteredContractTypes.Clear();
            filteredCompanies.Clear();
            filteredWorkShifts.Clear();
            hireDateFrom = null;
            hireDateTo = null;
        }

        private List<PersonnelRecord> GetFiltered()
        {
            var result = personnelList.AsEnumerable();

            if (filteredProvinces.Count > 0)
                result = result.Where(p => filteredProvinces.Contains(provinceCache.ContainsKey(p.ProvinceID) ? provinceCache[p.ProvinceID] : ""));

            if (filteredCities.Count > 0)
                result = result.Where(p => filteredCities.Contains(cityCache.ContainsKey(p.CityID) ? cityCache[p.CityID] : ""));

            if (filteredAffairs.Count > 0)
                result = result.Where(p => filteredAffairs.Contains(affairCache.ContainsKey(p.AffairID) ? affairCache[p.AffairID] : ""));

            if (filteredDepartments.Count > 0)
                result = result.Where(p => filteredDepartments.Contains(departmentCache.ContainsKey(p.DeptID) ? departmentCache[p.DeptID] : ""));

            if (filteredDistricts.Count > 0)
                result = result.Where(p => filteredDistricts.Contains(districtCache.ContainsKey(p.DistrictID) ? districtCache[p.DistrictID] : ""));

            if (filteredPositions.Count > 0)
                result = result.Where(p => filteredPositions.Contains(positionCache.ContainsKey(p.PostNameID) ? positionCache[p.PostNameID] : ""));

            if (filteredGenders.Count > 0)
                result = result.Where(p => filteredGenders.Contains(genderCache.ContainsKey(p.GenderID) ? genderCache[p.GenderID] : ""));

            if (filteredEducations.Count > 0)
                result = result.Where(p => filteredEducations.Contains(degreeCache.ContainsKey(p.DegreeID) ? degreeCache[p.DegreeID] : ""));

            if (filteredJobLevels.Count > 0)
                result = result.Where(p => filteredJobLevels.Contains(jobLevelCache.ContainsKey(p.JobLevelID) ? jobLevelCache[p.JobLevelID] : ""));

            if (filteredContractTypes.Count > 0)
                result = result.Where(p => filteredContractTypes.Contains(contractTypeCache.ContainsKey(p.ContractTypeID) ? contractTypeCache[p.ContractTypeID] : ""));

            if (filteredCompanies.Count > 0)
                result = result.Where(p => filteredCompanies.Contains(companyCache.ContainsKey(p.CompanyID) ? companyCache[p.CompanyID] : ""));

            if (filteredWorkShifts.Count > 0)
                result = result.Where(p => filteredWorkShifts.Contains(workShiftCache.ContainsKey(p.WorkShiftID) ? workShiftCache[p.WorkShiftID] : ""));

            if (hireDateFrom.HasValue && hireDateTo.HasValue)
                result = result.Where(p => p.HireDate.HasValue && p.HireDate >= hireDateFrom && p.HireDate <= hireDateTo);

            return result.ToList();
        }

        public int GetFilteredTotal() => GetFiltered().Count;
        public int GetFilteredDepartmentCount() => GetFiltered().Select(p => p.DeptID).Distinct().Count(x => x > 0);
        public int GetFilteredPositionCount() => GetFiltered().Select(p => p.PostNameID).Distinct().Count(x => x > 0);
        public int GetFilteredMaleCount() => GetFiltered().Count(p => p.GenderID == 1);
        public int GetFilteredFemaleCount() => GetFiltered().Count(p => p.GenderID == 2);

        public List<string> GetAllProvinces() => provinceCache.Values.Distinct().OrderBy(x => x).ToList();
        public List<string> GetAllGenders() => genderCache.Values.Distinct().OrderBy(x => x).ToList();
        public List<string> GetAllEducations() => degreeCache.Values.Distinct().OrderBy(x => x).ToList();
        public List<string> GetAllJobLevels() => jobLevelCache.Values.Distinct().OrderBy(x => x).ToList();
        public List<string> GetAllContractTypes() => contractTypeCache.Values.Distinct().OrderBy(x => x).ToList();
        public List<string> GetAllCompanies() => companyCache.Values.Distinct().OrderBy(x => x).ToList();
        public List<string> GetAllWorkShifts() => workShiftCache.Values.Distinct().OrderBy(x => x).ToList();

        public List<string> GetCitiesByProvinces(List<string> provinces)
        {
            var provinceIds = provinceCache.Where(p => provinces.Contains(p.Value)).Select(p => p.Key).ToList();
            return personnelList.Where(p => provinceIds.Contains(p.ProvinceID) && p.CityID > 0)
                .Select(p => cityCache.ContainsKey(p.CityID) ? cityCache[p.CityID] : "")
                .Where(x => !string.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToList();
        }

        public List<string> GetAffairsByProvinces(List<string> provinces)
        {
            var provinceIds = provinceCache.Where(p => provinces.Contains(p.Value)).Select(p => p.Key).ToList();
            return personnelList.Where(p => provinceIds.Contains(p.ProvinceID) && p.AffairID > 0)
                .Select(p => affairCache.ContainsKey(p.AffairID) ? affairCache[p.AffairID] : "")
                .Where(x => !string.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToList();
        }

        public List<string> GetDepartmentsByFilters(List<string> provinces, List<string> cities, List<string> affairs)
        {
            var provinceIds = provinceCache.Where(p => provinces.Contains(p.Value)).Select(p => p.Key).ToList();
            var cityIds = cityCache.Where(p => cities.Contains(p.Value)).Select(p => p.Key).ToList();
            var affairIds = affairCache.Where(p => affairs.Contains(p.Value)).Select(p => p.Key).ToList();

            var filtered = personnelList.AsEnumerable();
            if (provinceIds.Count > 0) filtered = filtered.Where(p => provinceIds.Contains(p.ProvinceID));
            if (cityIds.Count > 0) filtered = filtered.Where(p => cityIds.Contains(p.CityID));
            if (affairIds.Count > 0) filtered = filtered.Where(p => affairIds.Contains(p.AffairID));

            return filtered.Where(p => p.DeptID > 0)
                .Select(p => departmentCache.ContainsKey(p.DeptID) ? departmentCache[p.DeptID] : "")
                .Where(x => !string.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToList();
        }

        public List<string> GetDistrictsByDepartments(List<string> departments)
        {
            var deptIds = departmentCache.Where(p => departments.Contains(p.Value)).Select(p => p.Key).ToList();
            return personnelList.Where(p => deptIds.Contains(p.DeptID) && p.DistrictID > 0)
                .Select(p => districtCache.ContainsKey(p.DistrictID) ? districtCache[p.DistrictID] : "")
                .Where(x => !string.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToList();
        }

        public List<string> GetPositionsByDistricts(List<string> districts)
        {
            var districtIds = districtCache.Where(p => districts.Contains(p.Value)).Select(p => p.Key).ToList();
            return personnelList.Where(p => districtIds.Contains(p.DistrictID) && p.PostNameID > 0)
                .Select(p => positionCache.ContainsKey(p.PostNameID) ? positionCache[p.PostNameID] : "")
                .Where(x => !string.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToList();
        }

        public List<StatisticItem> GetFilteredDepartmentStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.DeptID > 0).GroupBy(p => p.DeptID)
                .Select(g => new StatisticItem
                {
                    Name = departmentCache.ContainsKey(g.Key) ? departmentCache[g.Key] : $"اداره {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<StatisticItem> GetFilteredPositionStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.PostNameID > 0).GroupBy(p => p.PostNameID)
                .Select(g => new StatisticItem
                {
                    Name = positionCache.ContainsKey(g.Key) ? positionCache[g.Key] : $"پست {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<StatisticItem> GetFilteredGenderStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.GenderID > 0).GroupBy(p => p.GenderID)
                .Select(g => new StatisticItem
                {
                    Name = genderCache.ContainsKey(g.Key) ? genderCache[g.Key] : $"جنسیت {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<StatisticItem> GetFilteredJobLevelStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.JobLevelID > 0).GroupBy(p => p.JobLevelID)
                .Select(g => new StatisticItem
                {
                    Name = jobLevelCache.ContainsKey(g.Key) ? jobLevelCache[g.Key] : $"سطح {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<StatisticItem> GetFilteredContractTypeStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.ContractTypeID > 0).GroupBy(p => p.ContractTypeID)
                .Select(g => new StatisticItem
                {
                    Name = contractTypeCache.ContainsKey(g.Key) ? contractTypeCache[g.Key] : $"قرارداد {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<StatisticItem> GetFilteredProvinceStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.ProvinceID > 0).GroupBy(p => p.ProvinceID)
                .Select(g => new StatisticItem
                {
                    Name = provinceCache.ContainsKey(g.Key) ? provinceCache[g.Key] : $"استان {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<StatisticItem> GetFilteredEducationStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.DegreeID > 0).GroupBy(p => p.DegreeID)
                .Select(g => new StatisticItem
                {
                    Name = degreeCache.ContainsKey(g.Key) ? degreeCache[g.Key] : $"مدرک {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<StatisticItem> GetFilteredCompanyStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.CompanyID > 0).GroupBy(p => p.CompanyID)
                .Select(g => new StatisticItem
                {
                    Name = companyCache.ContainsKey(g.Key) ? companyCache[g.Key] : $"شرکت {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<StatisticItem> GetFilteredWorkShiftStatistics()
        {
            var filtered = GetFiltered();
            return filtered.Where(p => p.WorkShiftID > 0).GroupBy(p => p.WorkShiftID)
                .Select(g => new StatisticItem
                {
                    Name = workShiftCache.ContainsKey(g.Key) ? workShiftCache[g.Key] : $"شیفت {g.Key}",
                    Count = g.Count()
                }).OrderByDescending(x => x.Count).ToList();
        }

        public List<PersonnelDetail> GetPersonnelByFilter(string filterValue, Chart chart)
        {
            var filtered = GetFiltered();

            string title = chart.Titles.Count > 0 ? chart.Titles[0].Text : "";

            if (title.Contains("اداره"))
                return filtered.Where(p => p.DeptID > 0 && departmentCache.ContainsKey(p.DeptID) && departmentCache[p.DeptID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("پست"))
                return filtered.Where(p => p.PostNameID > 0 && positionCache.ContainsKey(p.PostNameID) && positionCache[p.PostNameID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("جنسیت"))
                return filtered.Where(p => p.GenderID > 0 && genderCache.ContainsKey(p.GenderID) && genderCache[p.GenderID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("سطح"))
                return filtered.Where(p => p.JobLevelID > 0 && jobLevelCache.ContainsKey(p.JobLevelID) && jobLevelCache[p.JobLevelID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("قرارداد"))
                return filtered.Where(p => p.ContractTypeID > 0 && contractTypeCache.ContainsKey(p.ContractTypeID) && contractTypeCache[p.ContractTypeID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("استان"))
                return filtered.Where(p => p.ProvinceID > 0 && provinceCache.ContainsKey(p.ProvinceID) && provinceCache[p.ProvinceID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("مدارک") || title.Contains("تحصیلات"))
                return filtered.Where(p => p.DegreeID > 0 && degreeCache.ContainsKey(p.DegreeID) && degreeCache[p.DegreeID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("شرکت"))
                return filtered.Where(p => p.CompanyID > 0 && companyCache.ContainsKey(p.CompanyID) && companyCache[p.CompanyID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("شیفت"))
                return filtered.Where(p => p.WorkShiftID > 0 && workShiftCache.ContainsKey(p.WorkShiftID) && workShiftCache[p.WorkShiftID] == filterValue)
                    .Select(ToDetail).ToList();

            return new List<PersonnelDetail>();
        }

        private PersonnelDetail ToDetail(PersonnelRecord p) => new PersonnelDetail
        {
            PersonnelID = p.PersonnelID,
            FirstName = p.FirstName,
            LastName = p.LastName,
            PersonnelNumber = p.PersonnelNumber,
            NationalID = p.NationalID,
            PostName = positionCache.ContainsKey(p.PostNameID) ? positionCache[p.PostNameID] : "",
            DeptName = departmentCache.ContainsKey(p.DeptID) ? departmentCache[p.DeptID] : "",
            Province = provinceCache.ContainsKey(p.ProvinceID) ? provinceCache[p.ProvinceID] : "",
            ContractType = contractTypeCache.ContainsKey(p.ContractTypeID) ? contractTypeCache[p.ContractTypeID] : "",
            HireDate = p.HireDate,
            MobileNumber = p.MobileNumber
        };
    }

    public class PersonnelRecord
    {
        public int PersonnelID { get; set; }
        public int ProvinceID { get; set; }
        public int CityID { get; set; }
        public int AffairID { get; set; }
        public int DeptID { get; set; }
        public int DistrictID { get; set; }
        public int PostNameID { get; set; }
        public int VoltageID { get; set; }
        public int WorkShiftID { get; set; }
        public int GenderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string PersonnelNumber { get; set; }
        public string NationalID { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? StartDateOperation { get; set; }
        public int ContractTypeID { get; set; }
        public int JobLevelID { get; set; }
        public int CompanyID { get; set; }
        public int DegreeID { get; set; }
        public int DegreeFieldID { get; set; }
        public int MainJobTitle { get; set; }
        public int CurrentActivity { get; set; }
        public int StatusID { get; set; }
    }

    public class StatisticItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class PersonnelDetail
    {
        public int PersonnelID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonnelNumber { get; set; }
        public string NationalID { get; set; }
        public string PostName { get; set; }
        public string DeptName { get; set; }
        public string Province { get; set; }
        public string ContractType { get; set; }
        public DateTime? HireDate { get; set; }
        public string MobileNumber { get; set; }
    }
}