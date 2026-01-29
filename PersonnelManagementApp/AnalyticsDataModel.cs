using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace PersonnelManagementApp
{
    /// <summary>
    /// مدل داده‌ها برای تحلیل آماری پرسنل
    /// </summary>
    public class AnalyticsDataModel
    {
        private DataTable personnelTable;
        private List<PersonnelDetail> filteredPersonnel;
        private List<PersonnelDetail> allPersonnel;

        // فیلترهای فعلی
        private List<string> selectedProvinces = new List<string>();
        private List<string> selectedCities = new List<string>();
        private List<string> selectedAffairs = new List<string>();
        private List<string> selectedDepartments = new List<string>();
        private List<string> selectedDistricts = new List<string>();
        private List<string> selectedPositions = new List<string>();
        private List<string> selectedGenders = new List<string>();
        private List<string> selectedEducations = new List<string>();
        private List<string> selectedJobLevels = new List<string>();
        private List<string> selectedContractTypes = new List<string>();
        private List<string> selectedCompanies = new List<string>();
        private List<string> selectedWorkShifts = new List<string>();
        private DateTime? hireDateFrom;
        private DateTime? hireDateTo;

        // شمارندگان
        public int TotalPersonnel { get; private set; }
        public int ProvinceCount { get; private set; }
        public int CityCount { get; private set; }
        public int CompanyCount { get; private set; }
        public int JobLevelCount { get; private set; }
        public int ContractTypeCount { get; private set; }
        public int EducationCount { get; private set; }
        public int WorkShiftCount { get; private set; }

        // کش‌های لیست‌ها
        private List<string> allProvinces = new List<string>();
        private List<string> allCities = new List<string>();
        private List<string> allGenders = new List<string>();
        private List<string> allEducations = new List<string>();
        private List<string> allJobLevels = new List<string>();
        private List<string> allContractTypes = new List<string>();
        private List<string> allCompanies = new List<string>();
        private List<string> allWorkShifts = new List<string>();

        public AnalyticsDataModel()
        {
            filteredPersonnel = new List<PersonnelDetail>();
            allPersonnel = new List<PersonnelDetail>();
        }

        public bool LoadData(DbHelper dbHelper)
        {
            try
            {
                // بارگذاری داده‌های پرسنل
                personnelTable = dbHelper.ExecuteQuery(
                    @"SELECT p.PersonnelID, p.FirstName, p.LastName, p.FatherName, p.PersonnelNumber, p.NationalID,
                      p.MobileNumber, p.BirthDate, p.HireDate, p.StartDateOperation,
                      pr.ProvinceName as Province, c.CityName as City, ta.AffairName as Affair, d.DeptName,
                      dist.DistrictName as District, pn.PostName, v.VoltageName, ws.WorkShiftName,
                      g.GenderName, jl.JobLevelName, ct.ContractTypeName, co.CompanyName,
                      deg.DegreeName, degf.DegreeFieldName, p.MainJobTitle, p.CurrentActivity,
                      p.Inconsistency, st.StatusName, p.Description
                    FROM Personnel p
                    LEFT JOIN Province pr ON p.ProvinceID = pr.ProvinceID
                    LEFT JOIN City c ON p.CityID = c.CityID
                    LEFT JOIN TransferAffairs ta ON p.AffairID = ta.AffairID
                    LEFT JOIN OperationDepartments d ON p.DeptID = d.DeptID
                    LEFT JOIN Districts dist ON p.DistrictID = dist.DistrictID
                    LEFT JOIN PostNames pn ON p.PostNameID = pn.PostNameID
                    LEFT JOIN VoltageLevels v ON p.VoltageID = v.VoltageID
                    LEFT JOIN WorkShifts ws ON p.WorkShiftID = ws.WorkShiftID
                    LEFT JOIN Gender g ON p.GenderID = g.GenderID
                    LEFT JOIN JobLevel jl ON p.JobLevelID = jl.JobLevelID
                    LEFT JOIN ContractType ct ON p.ContractTypeID = ct.ContractTypeID
                    LEFT JOIN Company co ON p.CompanyID = co.CompanyID
                    LEFT JOIN Degree deg ON p.DegreeID = deg.DegreeID
                    LEFT JOIN DegreeField degf ON p.DegreeFieldID = degf.DegreeFieldID
                    LEFT JOIN StatusPresence st ON p.StatusID = st.StatusID
                    ORDER BY p.FirstName"
                );

                // تبدیل به PersonnelDetail
                ConvertToPersonnelDetails();

                // بارگذاری لیست‌های اختیاری
                LoadLookupData(dbHelper);

                TotalPersonnel = allPersonnel.Count;
                ProvinceCount = allProvinces.Count;
                CityCount = allCities.Count;
                CompanyCount = allCompanies.Count;
                JobLevelCount = allJobLevels.Count;
                ContractTypeCount = allContractTypes.Count;
                EducationCount = allEducations.Count;
                WorkShiftCount = allWorkShifts.Count;

                filteredPersonnel = new List<PersonnelDetail>(allPersonnel);
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
                return false;
            }
        }

        private void ConvertToPersonnelDetails()
        {
            allPersonnel.Clear();
            foreach (DataRow row in personnelTable.Rows)
            {
                var detail = new PersonnelDetail
                {
                    PersonnelID = (int)row["PersonnelID"],
                    FirstName = row["FirstName"]?.ToString() ?? "",
                    LastName = row["LastName"]?.ToString() ?? "",
                    FatherName = row["FatherName"]?.ToString() ?? "",
                    PersonnelNumber = row["PersonnelNumber"]?.ToString() ?? "",
                    NationalID = row["NationalID"]?.ToString() ?? "",
                    MobileNumber = row["MobileNumber"]?.ToString() ?? "",
                    BirthDate = row["BirthDate"] is DBNull ? null : (DateTime?)row["BirthDate"],
                    HireDate = row["HireDate"] is DBNull ? null : (DateTime?)row["HireDate"],
                    StartDateOperation = row["StartDateOperation"] is DBNull ? null : (DateTime?)row["StartDateOperation"],
                    Province = row["Province"]?.ToString() ?? "نامشخص",
                    City = row["City"]?.ToString() ?? "نامشخص",
                    Affair = row["Affair"]?.ToString() ?? "نامشخص",
                    DeptName = row["DeptName"]?.ToString() ?? "نامشخص",
                    District = row["District"]?.ToString() ?? "نامشخص",
                    PostName = row["PostName"]?.ToString() ?? "نامشخص",
                    VoltageName = row["VoltageName"]?.ToString() ?? "نامشخص",
                    WorkShiftName = row["WorkShiftName"]?.ToString() ?? "نامشخص",
                    GenderName = row["GenderName"]?.ToString() ?? "نامشخص",
                    JobLevelName = row["JobLevelName"]?.ToString() ?? "نامشخص",
                    ContractType = row["ContractTypeName"]?.ToString() ?? "نامشخص",
                    CompanyName = row["CompanyName"]?.ToString() ?? "نامشخص",
                    DegreeName = row["DegreeName"]?.ToString() ?? "نامشخص",
                    DegreeFieldName = row["DegreeFieldName"]?.ToString() ?? "نامشخص",
                    MainJobTitle = row["MainJobTitle"]?.ToString() ?? "نامشخص",
                    CurrentActivity = row["CurrentActivity"]?.ToString() ?? "نامشخص",
                    Inconsistency = row["Inconsistency"] is DBNull ? false : (bool)row["Inconsistency"],
                    StatusName = row["StatusName"]?.ToString() ?? "نامشخص",
                    Description = row["Description"]?.ToString() ?? ""
                };
                allPersonnel.Add(detail);
            }
        }

        private void LoadLookupData(DbHelper dbHelper)
        {
            try
            {
                allProvinces = GetUniqueValues(allPersonnel.Select(p => p.Province).ToList());
                allCities = GetUniqueValues(allPersonnel.Select(p => p.City).ToList());
                allGenders = GetUniqueValues(allPersonnel.Select(p => p.GenderName).ToList());
                allEducations = GetUniqueValues(allPersonnel.Select(p => p.DegreeName).ToList());
                allJobLevels = GetUniqueValues(allPersonnel.Select(p => p.JobLevelName).ToList());
                allContractTypes = GetUniqueValues(allPersonnel.Select(p => p.ContractType).ToList());
                allCompanies = GetUniqueValues(allPersonnel.Select(p => p.CompanyName).ToList());
                allWorkShifts = GetUniqueValues(allPersonnel.Select(p => p.WorkShiftName).ToList());
            }
            catch { }
        }

        private List<string> GetUniqueValues(List<string> values)
        {
            return values.Where(v => v != "نامشخص" && !string.IsNullOrEmpty(v))
                        .Distinct()
                        .OrderBy(v => v)
                        .ToList();
        }

        public void SetFilters(List<string> provinces, List<string> cities, List<string> affairs, List<string> depts,
            List<string> districts, List<string> positions, List<string> genders, List<string> educations,
            List<string> jobLevels, List<string> contractTypes, List<string> companies, List<string> workShifts,
            DateTime? fromDate, DateTime? toDate)
        {
            selectedProvinces = provinces ?? new List<string>();
            selectedCities = cities ?? new List<string>();
            selectedAffairs = affairs ?? new List<string>();
            selectedDepartments = depts ?? new List<string>();
            selectedDistricts = districts ?? new List<string>();
            selectedPositions = positions ?? new List<string>();
            selectedGenders = genders ?? new List<string>();
            selectedEducations = educations ?? new List<string>();
            selectedJobLevels = jobLevels ?? new List<string>();
            selectedContractTypes = contractTypes ?? new List<string>();
            selectedCompanies = companies ?? new List<string>();
            selectedWorkShifts = workShifts ?? new List<string>();
            hireDateFrom = fromDate;
            hireDateTo = toDate;

            ApplyFilters();
        }

        public void ClearFilters()
        {
            selectedProvinces.Clear();
            selectedCities.Clear();
            selectedAffairs.Clear();
            selectedDepartments.Clear();
            selectedDistricts.Clear();
            selectedPositions.Clear();
            selectedGenders.Clear();
            selectedEducations.Clear();
            selectedJobLevels.Clear();
            selectedContractTypes.Clear();
            selectedCompanies.Clear();
            selectedWorkShifts.Clear();
            hireDateFrom = null;
            hireDateTo = null;
            filteredPersonnel = new List<PersonnelDetail>(allPersonnel);
        }

        private void ApplyFilters()
        {
            filteredPersonnel = allPersonnel.Where(p =>
                (selectedProvinces.Count == 0 || selectedProvinces.Contains(p.Province)) &&
                (selectedCities.Count == 0 || selectedCities.Contains(p.City)) &&
                (selectedAffairs.Count == 0 || selectedAffairs.Contains(p.Affair)) &&
                (selectedDepartments.Count == 0 || selectedDepartments.Contains(p.DeptName)) &&
                (selectedDistricts.Count == 0 || selectedDistricts.Contains(p.District)) &&
                (selectedPositions.Count == 0 || selectedPositions.Contains(p.PostName)) &&
                (selectedGenders.Count == 0 || selectedGenders.Contains(p.GenderName)) &&
                (selectedEducations.Count == 0 || selectedEducations.Contains(p.DegreeName)) &&
                (selectedJobLevels.Count == 0 || selectedJobLevels.Contains(p.JobLevelName)) &&
                (selectedContractTypes.Count == 0 || selectedContractTypes.Contains(p.ContractType)) &&
                (selectedCompanies.Count == 0 || selectedCompanies.Contains(p.CompanyName)) &&
                (selectedWorkShifts.Count == 0 || selectedWorkShifts.Contains(p.WorkShiftName)) &&
                (!hireDateFrom.HasValue || p.HireDate >= hireDateFrom) &&
                (!hireDateTo.HasValue || p.HireDate <= hireDateTo)
            ).ToList();
        }

        // آمار کلی
        public int GetFilteredTotal() => filteredPersonnel.Count;
        public int GetFilteredDepartmentCount() => GetStatistics(p => p.DeptName).Count;
        public int GetFilteredPositionCount() => GetStatistics(p => p.PostName).Count;
        public int GetFilteredFemaleCount() => filteredPersonnel.Count(p => p.GenderName.Contains("خانم") || p.GenderName.Contains("زن"));
        public int GetFilteredMaleCount() => filteredPersonnel.Count(p => p.GenderName.Contains("آقا") || p.GenderName.Contains("مرد"));

        // توزیع‌های آماری
        public List<(string Name, int Count)> GetFilteredDepartmentStatistics() =>
            GetStatistics(p => p.DeptName).OrderByDescending(x => x.Count).ToList();

        public List<(string Name, int Count)> GetFilteredPositionStatistics() =>
            GetStatistics(p => p.PostName).OrderByDescending(x => x.Count).ToList();

        public List<(string Name, int Count)> GetFilteredGenderStatistics() =>
            GetStatistics(p => p.GenderName).OrderByDescending(x => x.Count).ToList();

        public List<(string Name, int Count)> GetFilteredJobLevelStatistics() =>
            GetStatistics(p => p.JobLevelName).OrderByDescending(x => x.Count).ToList();

        public List<(string Name, int Count)> GetFilteredContractTypeStatistics() =>
            GetStatistics(p => p.ContractType).OrderByDescending(x => x.Count).ToList();

        public List<(string Name, int Count)> GetFilteredProvinceStatistics() =>
            GetStatistics(p => p.Province).OrderByDescending(x => x.Count).ToList();

        public List<(string Name, int Count)> GetFilteredEducationStatistics() =>
            GetStatistics(p => p.DegreeName).OrderByDescending(x => x.Count).ToList();

        public List<(string Name, int Count)> GetFilteredCompanyStatistics() =>
            GetStatistics(p => p.CompanyName).OrderByDescending(x => x.Count).ToList();

        public List<(string Name, int Count)> GetFilteredWorkShiftStatistics() =>
            GetStatistics(p => p.WorkShiftName).OrderByDescending(x => x.Count).ToList();

        private List<(string Name, int Count)> GetStatistics(Func<PersonnelDetail, string> selector)
        {
            return filteredPersonnel
                .GroupBy(selector)
                .Where(g => g.Key != "نامشخص" && !string.IsNullOrEmpty(g.Key))
                .Select(g => (g.Key, g.Count()))
                .ToList();
        }

        // لیست‌های اختیاری
        public List<string> GetAllProvinces() => new List<string>(allProvinces);
        public List<string> GetAllCities() => new List<string>(allCities);
        public List<string> GetAllGenders() => new List<string>(allGenders);
        public List<string> GetAllEducations() => new List<string>(allEducations);
        public List<string> GetAllJobLevels() => new List<string>(allJobLevels);
        public List<string> GetAllContractTypes() => new List<string>(allContractTypes);
        public List<string> GetAllCompanies() => new List<string>(allCompanies);
        public List<string> GetAllWorkShifts() => new List<string>(allWorkShifts);

        public List<string> GetCitiesByProvinces(List<string> provinces) =>
            filteredPersonnel.Where(p => provinces.Contains(p.Province))
                           .Select(p => p.City)
                           .Distinct()
                           .OrderBy(c => c)
                           .ToList();

        public List<string> GetAffairsByProvinces(List<string> provinces) =>
            filteredPersonnel.Where(p => provinces.Contains(p.Province))
                           .Select(p => p.Affair)
                           .Distinct()
                           .OrderBy(a => a)
                           .ToList();

        public List<string> GetDepartmentsByFilters(List<string> provinces, List<string> cities, List<string> affairs) =>
            filteredPersonnel.Where(p =>
                (provinces.Count == 0 || provinces.Contains(p.Province)) &&
                (cities.Count == 0 || cities.Contains(p.City)) &&
                (affairs.Count == 0 || affairs.Contains(p.Affair)))
                .Select(p => p.DeptName)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

        public List<string> GetDistrictsByDepartments(List<string> depts) =>
            filteredPersonnel.Where(p => depts.Contains(p.DeptName))
                           .Select(p => p.District)
                           .Distinct()
                           .OrderBy(d => d)
                           .ToList();

        public List<string> GetPositionsByDistricts(List<string> districts) =>
            filteredPersonnel.Where(p => districts.Contains(p.District))
                           .Select(p => p.PostName)
                           .Distinct()
                           .OrderBy(pos => pos)
                           .ToList();

        public List<PersonnelDetail> GetPersonnelByFilter(string filterValue, Chart chart)
        {
            return filteredPersonnel.ToList();
        }
    }
}
