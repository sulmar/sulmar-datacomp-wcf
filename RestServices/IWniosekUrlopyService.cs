using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestServices
{
    public interface IWniosekUrlopyService
    {

    }

    public interface IUzytkownikService
    {
        Task<Rootobject> Zaloguj(ZalogujZadanie zadanie);        
    }

    public class MyApiUzytkownikService : IUzytkownikService
    {
        public Task<Rootobject> Zaloguj(ZalogujZadanie zadanie)
        {
            throw new NotImplementedException();
        }
    }

    public class RestApiUzytkownikService : IUzytkownikService
    {
        private readonly HttpClient client;

        public RestApiUzytkownikService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Rootobject> Zaloguj(ZalogujZadanie zadanie)
        {
            string json = zadanie.Serialize();

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Zaloguj", content).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                string xml = await response.Content.ReadAsStringAsync();

                Fault fault = xml.DeserializeTo<Fault>();

                throw new MyFaultException(fault);
            }

            response.EnsureSuccessStatusCode();

            //if (response.IsSuccessStatusCode)
            //{

            //}
            //else
            //{
            //    throw new ApplicationException();
            //}

            string responseString = await response.Content.ReadAsStringAsync();

            Rootobject rootobject = responseString.Deserialize<Rootobject>();


            return rootobject;

        }
    }


    public class MyFaultException : Exception
    {
        public MyFaultException(Fault fault)
        {
            Fault = fault;
        }

        public Fault Fault { get; private set; }


    }

 

    public class ZalogujZadanie
    {
        public string pUzytkownik  { get; set; }
        public string pHaslo { get; set; }
    }


    #region Zalogujresult Model

    public class Rootobject
    {
        public Zalogujresult ZalogujResult { get; set; }
    }

    public class Zalogujresult
    {
      //  public Komunikaty[] Komunikaty { get; set; }
        public bool CzyUdostepnicPrzegladaniePlacPodleglych { get; set; }
        public bool CzyUproszczonySystemOdbic { get; set; }
        public bool CzyWylaczycAkceptacjeWnioskowNaStronieGlownej { get; set; }
        public bool CzyWylaczycAkceptacjeZastepstwaNaStronieGlownej { get; set; }
        public bool CzyWylaczycWiadomosciNaStronieGlownej { get; set; }
        public bool CzyZadanieZgodyNaPrzetwarzanieDanych { get; set; }
        public DateTime DataICzasWygasnieciaSesji { get; set; }
        public string GidSesji { get; set; }
        public string GidUzytkownika { get; set; }
        public Kontekstpracownika KontekstPracownika { get; set; }
        public string OpisUzytkownika { get; set; }
        public Pracownicytozsamizuzytkownikiem[] PracownicyTozsamiZUzytkownikiem { get; set; }
        public string TrescZgodyNaPrzetwarzanieDanych { get; set; }
       // public Uprawnienia[] Uprawnienia { get; set; }
    }

    public class Kontekstpracownika
    {
        public object AngazDo { get; set; }
        public DateTime AngazOd { get; set; }
        public string CharakterPracy { get; set; }
        public string CharakterPracyOpis { get; set; }
        public bool CzyZaznaczony { get; set; }
        public DateTime DataPrzyjecia { get; set; }
        public object DataZwolnienia { get; set; }
        public string DodatkowyOpis { get; set; }
        public string DodatkowyOpisOpis { get; set; }
        public string FormaUmowy { get; set; }
        public string FormaUmowyOpis { get; set; }
        public string Gid { get; set; }
        public string Imie { get; set; }
        public string Komorka { get; set; }
        public string KomorkaOpis { get; set; }
        public string MiejsceWykonywaniaPracy { get; set; }
        public string MiejsceWykonywaniaPracyOpis { get; set; }
        public string Nazwisko { get; set; }
        public string NazwiskoImie { get; set; }
        public string NazwiskoPanienskie { get; set; }
        public int NumerEwidencyjny { get; set; }
        public string Pesel { get; set; }
        public string Pion { get; set; }
        public string PionOpis { get; set; }
        public string Platnik { get; set; }
        public string PlatnikOpis { get; set; }
        public string Region { get; set; }
        public string RegionOpis { get; set; }
        public object RzeczywistaDataZwolnienia { get; set; }
        public object RzeczywistaDataZwolnieniaNaDzis { get; set; }
        public string SposobRozwiazaniaUmowy { get; set; }
        public string SposobRozwiazaniaUmowyOpis { get; set; }
        public string Stanowisko { get; set; }
        public string StanowiskoOpis { get; set; }
        public string TypUmowy { get; set; }
        public string TypUmowyOpis { get; set; }
        public object UmowaDo { get; set; }
        public object UmowaOd { get; set; }
        public object[] Uprawnienia { get; set; }
    }

    //public class Komunikaty
    //{
    //    public int CzyOpisWidoczny { get; set; }
    //    public DateTime DataICzas { get; set; }
    //    public string Gid { get; set; }
    //    public string KolorWypelnieniaObrazka { get; set; }
    //    public string KolorWypelnieniaObrazkaHTML { get; set; }
    //    public string NazwaUzytkownika { get; set; }
    //    public string ObrazekSVG { get; set; }
    //    public string Opis { get; set; }
    //    public int Rodzaj { get; set; }
    //    public int Status { get; set; }
    //    public string Tytul { get; set; }
    //}

    public class Pracownicytozsamizuzytkownikiem
    {
        public object AngazDo { get; set; }
        public DateTime AngazOd { get; set; }
        public string CharakterPracy { get; set; }
        public string CharakterPracyOpis { get; set; }
        public bool CzyZaznaczony { get; set; }
        public DateTime DataPrzyjecia { get; set; }
        public object DataZwolnienia { get; set; }
        public string DodatkowyOpis { get; set; }
        public string DodatkowyOpisOpis { get; set; }
        public string FormaUmowy { get; set; }
        public string FormaUmowyOpis { get; set; }
        public string Gid { get; set; }
        public string Imie { get; set; }
        public string Komorka { get; set; }
        public string KomorkaOpis { get; set; }
        public string MiejsceWykonywaniaPracy { get; set; }
        public string MiejsceWykonywaniaPracyOpis { get; set; }
        public string Nazwisko { get; set; }
        public string NazwiskoImie { get; set; }
        public string NazwiskoPanienskie { get; set; }
        public int NumerEwidencyjny { get; set; }
        public string Pesel { get; set; }
        public string Pion { get; set; }
        public string PionOpis { get; set; }
        public string Platnik { get; set; }
        public string PlatnikOpis { get; set; }
        public string Region { get; set; }
        public string RegionOpis { get; set; }
        public object RzeczywistaDataZwolnienia { get; set; }
        public object RzeczywistaDataZwolnieniaNaDzis { get; set; }
        public string SposobRozwiazaniaUmowy { get; set; }
        public string SposobRozwiazaniaUmowyOpis { get; set; }
        public string Stanowisko { get; set; }
        public string StanowiskoOpis { get; set; }
        public string TypUmowy { get; set; }
        public string TypUmowyOpis { get; set; }
        public object UmowaDo { get; set; }
        public object UmowaOd { get; set; }
        public object[] Uprawnienia { get; set; }
    }

    public class Uprawnienia
    {
        // public string __type { get; set; }
        public bool CzyDodawanie { get; set; }
        public bool CzyDodawanieDotyczy { get; set; }
        public bool CzyDodawanieGrupa { get; set; }
        public bool CzyDodawanieGrupaDotyczy { get; set; }
        public bool CzyDodawanieZastepstwo { get; set; }
        public bool CzyEdycja { get; set; }
        public bool CzyEdycjaDotyczy { get; set; }
        public bool CzyEdycjaGrupa { get; set; }
        public bool CzyEdycjaGrupaDotyczy { get; set; }
        public bool CzyEdycjaZastepstwo { get; set; }
        public bool CzyModulAdministracyjny { get; set; }
        public bool CzyUruchamianie { get; set; }
        public bool CzyUruchamianieDotyczy { get; set; }
        public bool CzyUruchamianieGrupa { get; set; }
        public bool CzyUruchamianieGrupaDotyczy { get; set; }
        public bool CzyUruchamianieZastepstwo { get; set; }
        public bool CzyUsuwanie { get; set; }
        public bool CzyUsuwanieDotyczy { get; set; }
        public bool CzyUsuwanieGrupa { get; set; }
        public bool CzyUsuwanieGrupaDotyczy { get; set; }
        public bool CzyUsuwanieZastepstwo { get; set; }
        public string Gid { get; set; }
        public int MozliweUprawnienia { get; set; }
        public string Nazwa { get; set; }
        public string NazwaTypu { get; set; }
        public string Opis { get; set; }
        public string SciezkaGid { get; set; }
        public string SciezkaNazw { get; set; }
        public int Wciecie { get; set; }
    }

    #endregion

    #region Fault Model

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ws/2005/05/envelope/none")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ws/2005/05/envelope/none", IsNullable = false)]
    public partial class Fault
    {

        private FaultCode codeField;

        private FaultReason reasonField;

        private FaultDetail detailField;

        /// <remarks/>
        public FaultCode Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public FaultReason Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }

        /// <remarks/>
        public FaultDetail Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ws/2005/05/envelope/none")]
    public partial class FaultCode
    {

        private string valueField;

        /// <remarks/>
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ws/2005/05/envelope/none")]
    public partial class FaultReason
    {

        private FaultReasonText textField;

        /// <remarks/>
        public FaultReasonText Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ws/2005/05/envelope/none")]
    public partial class FaultReasonText
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ws/2005/05/envelope/none")]
    public partial class FaultDetail
    {

        private SzczegolyBleduUslugi szczegolyBleduUslugiField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Datacomp.Fundament")]
        public SzczegolyBleduUslugi SzczegolyBleduUslugi
        {
            get
            {
                return this.szczegolyBleduUslugiField;
            }
            set
            {
                this.szczegolyBleduUslugiField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Datacomp.Fundament")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Datacomp.Fundament", IsNullable = false)]
    public partial class SzczegolyBleduUslugi
    {

        private byte kodField;

        private string opisField;

        private string tytulField;

        private object uzytkownikField;

        /// <remarks/>
        public byte Kod
        {
            get
            {
                return this.kodField;
            }
            set
            {
                this.kodField = value;
            }
        }

        /// <remarks/>
        public string Opis
        {
            get
            {
                return this.opisField;
            }
            set
            {
                this.opisField = value;
            }
        }

        /// <remarks/>
        public string Tytul
        {
            get
            {
                return this.tytulField;
            }
            set
            {
                this.tytulField = value;
            }
        }

        /// <remarks/>
        public object Uzytkownik
        {
            get
            {
                return this.uzytkownikField;
            }
            set
            {
                this.uzytkownikField = value;
            }
        }
    }

    #endregion


    public static class XmlExtensions
    {
        public static T DeserializeTo<T>(this string xml)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Fault));
                T deserialized = (T)serializer.Deserialize(ms);

                return deserialized;
            }
        }
    }

    #region DataContractJsonSerializer Extension Methods

    public static class Extensions
    {
        public static string Serialize<T>(this T p)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(stream, p);
                stream.Position = 0;
                var streamReader = new StreamReader(stream);
                return streamReader.ReadToEnd();
            }
        }

        public static T Deserialize<T>(this string json)
            where T : new()
        {
            T deserialized = new T();
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(deserialized.GetType());
                deserialized = (T)serializer.ReadObject(ms);
            }
            return deserialized;
        }

    }

    #endregion

}

