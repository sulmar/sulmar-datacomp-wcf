using RestApiUWPClient.Models;
using RestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiUWPClient.ViewModels
{
    public class LoginViewModel
    {
        public Pracownik Pracownik { get; set; }

        private readonly IUzytkownikService uzytkownikService;

        public bool JestZalogowany { get; set; }


        public LoginViewModel()
             : this(new RestApiUzytkownikService(new System.Net.Http.HttpClient()))
        {
            Pracownik = new Pracownik { Login = "john", Password = "125" };
        }

        public LoginViewModel(IUzytkownikService uzytkownikService)
        {
            this.uzytkownikService = uzytkownikService;
        }

        public async Task  Zaloguj()
        {
            ZalogujZadanie zalogujZadanie = new ZalogujZadanie { pUzytkownik = Pracownik.Login, pHaslo = Pracownik.Password };

            await uzytkownikService.Zaloguj(zalogujZadanie);

            JestZalogowany = true;

        }
    }
}
