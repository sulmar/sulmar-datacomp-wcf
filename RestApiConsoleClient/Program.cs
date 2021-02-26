using RestServices;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestApiConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            string uzytkownik = "Datacomp";
            string password = "data_comp";

            string loginpassword = $"{uzytkownik}:{password}";

            string parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes(loginpassword));

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://firma.data-comp.eu:4820/PortalPracownika20/");
            // client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", parameter);

            IUzytkownikService uzytkownikService = new RestApiUzytkownikService(client);

          
            var zalogujZadanie = new ZalogujZadanie { pUzytkownik = "pracownik0", pHaslo = "1235554" };

            try
            {
                var response = await uzytkownikService.Zaloguj(zalogujZadanie);


                Console.WriteLine($"GidSesji: {response.ZalogujResult.GidSesji}");
            }

            catch(MyFaultException e)
            {

                Console.WriteLine(e.Fault.Detail.SzczegolyBleduUslugi);
            }

        }
    }
}
