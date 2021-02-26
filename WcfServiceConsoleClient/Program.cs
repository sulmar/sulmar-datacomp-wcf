using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WcfServiceApp;
using WcfServiceApp.Models;

namespace WcfServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // ProxyTest();

            Uri uri = new Uri("http://localhost:51075/Service1.svc");
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            ChannelFactory<IService1> proxy = new ChannelFactory<IService1>(binding, endpoint);

            IService1 service = proxy.CreateChannel();

            try
            {
                decimal tax = service.TaxCalculate(100, 0);
            }

            /// dodaj do kontraktu [FaultContract(typeof(DivideByZeroFault))]
            /// 
            catch (FaultException<DivideByZeroFault> e)
            {
                Console.WriteLine(e.Detail.Error);
            }
            catch(FaultException e)
            {
                
            }

            catch(Exception e)
            {

            }

            

            //Customer customer = service.Get(Guid.NewGuid());

            //string response = service.Ping("Hello");

            //string result = service.GetData(100);

            

            //Console.WriteLine(result);

        }

        private static void ProxyTest()
        {
            Uri uri = new Uri("http://localhost:51075/Service1.svc");
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            HelloServiceProxy helloServiceProxy = new HelloServiceProxy(binding, endpoint);

            string result = helloServiceProxy.GetData(100);
        }
    }

    public class HelloServiceProxy : ClientBase<IService1>, IService1
    {
        public HelloServiceProxy(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        public Customer Get(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            return this.Channel.GetData(value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            return this.Channel.GetDataUsingDataContract(composite);
        }

        public string Ping(string message)
        {
            throw new NotImplementedException();
        }

        public decimal TaxCalculate(decimal amount)
        {
            throw new NotImplementedException();
        }

        public decimal TaxCalculate(decimal amount, decimal tax)
        {
            throw new NotImplementedException();
        }
    }
}
