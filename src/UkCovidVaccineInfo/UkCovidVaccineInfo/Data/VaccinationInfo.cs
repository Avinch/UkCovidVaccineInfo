using System;

namespace UkCovidVaccineInfo.Data
{
    public class VaccinationInfo
    {
        public DateTime Date { get; set; }
        public int? NewFirstDose { get; set; }
        public int? CumFirstDose { get; set; }
        public int? NewSecondDose { get; set; }
        public int? CumSecondDose { get; set; }

        public int? TotalNew => NewFirstDose + NewSecondDose;
        public int? Total => CumFirstDose + CumSecondDose;
    }
}
