using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Contatos.Controllers
{
    public class ApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public ApiController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<Estado>> GetEstadosAsync()
        {
            string apiUrl = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    List<Estado> estados = JsonConvert.DeserializeObject<List<Estado>>(responseData);
                    return estados;
                }
                else
                {
                    return new List<Estado>();
                }
            }
            catch (Exception ex)
            {
                return new List<Estado>();
            }
        }

        public async Task<List<Municipio>> GetMunicipiosPorEstadoAsync(string estado)
        {
            string apiUrl = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{estado}/municipios";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    List<Municipio> municipios = JsonConvert.DeserializeObject<List<Municipio>>(responseData);
                    Console.Write(municipios);
                    return municipios;
                }
                else
                {
                    return new List<Municipio>();
                }
            }
            catch (Exception ex)
            {
                return new List<Municipio>();
            }
        }
    }

    public class Estado
    {
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }

    public class Municipio
    {
        public string Nome { get; set; }
    }
}