using System;
using System.Collections.Generic;

namespace UkCovidVaccineInfo.Data
{
    public class VaccinationResponse
    {
        public List<VaccinationInfo> Data { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
