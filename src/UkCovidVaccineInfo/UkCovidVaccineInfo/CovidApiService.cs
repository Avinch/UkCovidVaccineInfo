using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using UkCovidVaccineInfo.Data;

namespace UkCovidVaccineInfo
{
    public class CovidApiService
    {
        private readonly HttpClient _http;

        public CovidApiService()
        {
            _http = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

            _http.BaseAddress = new Uri("https://api.coronavirus.data.gov.uk/v1/");
            _http.DefaultRequestHeaders.Add("Accepts",
                "application/json; application/xml; text/csv; application/vnd.PHE-COVID19.v1+json; application/vnd.PHE-COVID19.v1+xml");
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<VaccinationResponse> GetData()
        {
            var endpoint =
                "data?filters=areaType=overview&structure=" +
                "{\"Date\": \"date\"," +
                "\"NewFirstDose\":\"newPeopleVaccinatedFirstDoseByPublishDate\"," +
                "\"CumFirstDose\":\"cumPeopleVaccinatedFirstDoseByPublishDate\"," +
                "\"NewSecondDose\":\"newPeopleVaccinatedSecondDoseByPublishDate\"," +
                "\"CumSecondDose\":\"cumPeopleVaccinatedSecondDoseByPublishDate\"}";

            var response = await _http.GetAsync(endpoint);
            var contentString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = Deserialize<VaccinationResponse>(contentString);

            return data;
        }

        private T Deserialize<T>(string content)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(content, options);
        }
    }
}
