using System;

namespace PersonnelManagementApp
{
    /// <summary>
    /// مدل داده‌های جزئیات پرسنل برای نمایش در تحلیلات
    /// </summary>
    public class PersonnelDetail
    {
        public int PersonnelID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string PersonnelNumber { get; set; }
        public string NationalID { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? StartDateOperation { get; set; }
        
        // فیلدهای جغرافیایی
        public string Province { get; set; }
        public string City { get; set; }
        public string Affair { get; set; }
        public string DeptName { get; set; }
        public string District { get; set; }
        
        // فیلدهای شغلی
        public string PostName { get; set; }
        public string VoltageName { get; set; }
        public string WorkShiftName { get; set; }
        public string JobLevelName { get; set; }
        
        // فیلدهای شخصی
        public string GenderName { get; set; }
        public string DegreeName { get; set; }
        public string DegreeFieldName { get; set; }
        
        // فیلدهای سازمانی
        public string CompanyName { get; set; }
        public string ContractType { get; set; }
        public string MainJobTitle { get; set; }
        public string CurrentActivity { get; set; }
        public bool Inconsistency { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
    }
}
