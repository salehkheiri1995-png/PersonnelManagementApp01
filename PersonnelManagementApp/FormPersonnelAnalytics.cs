        public List<PersonnelDetail> GetPersonnelByFilter(string filterValue, Chart chart)
        {
            var filtered = GetFiltered();

            // تعیین اینکه کدام نمودار کلیک شده است
            string title = chart.Titles.Count > 0 ? chart.Titles[0].Text : "";

            // Debug: نام نمودار را نمایش بده
            MessageBox.Show($"نمودار انتخاب شده: {title}\nمقدار انتخاب شده: {filterValue}");

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

            // مهم: تحصیلات رو دقیق‌تر پیدا کن
            if (title.Contains("تحصیلات") || title.Contains("مدارک") || title.Contains("Degree") || title.Contains("Education"))
            {
                var result = filtered.Where(p => p.DegreeID > 0 && degreeCache.ContainsKey(p.DegreeID) && degreeCache[p.DegreeID] == filterValue)
                    .Select(ToDetail).ToList();
                MessageBox.Show($"نتایج یافت شده برای تحصیلات: {result.Count} نفر");
                return result;
            }

            if (title.Contains("شرکت"))
                return filtered.Where(p => p.CompanyID > 0 && companyCache.ContainsKey(p.CompanyID) && companyCache[p.CompanyID] == filterValue)
                    .Select(ToDetail).ToList();

            if (title.Contains("شیفت"))
                return filtered.Where(p => p.WorkShiftID > 0 && workShiftCache.ContainsKey(p.WorkShiftID) && workShiftCache[p.WorkShiftID] == filterValue)
                    .Select(ToDetail).ToList();

            return new List<PersonnelDetail>();
        }