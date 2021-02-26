using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using WcfServiceApp.Models;

namespace WcfServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [GlobalErrorHandlerBehavior(typeof(GlobalErrorHandler))]
    public class Service1 : IService1
    {
        public Customer Get(Guid customerId)
        {

            return new Customer { Id = customerId, FirstName = "John", LastName = "Smith" };
        }

        public Customer GetCustomerById(Guid customerId)
        {
            var headers = WebOperationContext.Current.IncomingRequest.Headers["fsd"];

            return new Customer { Id = customerId, FirstName = "John", LastName = "Smith" };
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string Ping(string message)
        {
            return "Pong";
        }

        public decimal TaxCalculate(decimal amount, decimal tax)
        {
            //if (tax == 0)
            //{
            //    // throw new FaultException("amount is zero", new FaultCode("DivideByZero"));

            //    throw new NotImplementedException();

            //    DivideByZeroFault divideByZeroFault = new DivideByZeroFault { Error = "2000", Details = "amount is zero" };

            //    throw new FaultException<DivideByZeroFault>(divideByZeroFault, "DivideByZero");

            //}

            return amount / tax;
        }
    }

    [DataContract]
    public class DivideByZeroFault
    {
        [DataMember]
        public string Error { get; set; }
        [DataMember]
        public string Details { get; set; }
    }

    public class GlobalErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
                return;

            FaultException faultException = new FaultException("General service error");
            MessageFault messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, null);
        }
    }

    public class GlobalErrorHandlerBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly Type errorHandlerType;

        public GlobalErrorHandlerBehaviorAttribute(Type errorHandlerType)
        {
            this.errorHandlerType = errorHandlerType;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler = (IErrorHandler)Activator.CreateInstance(errorHandlerType);

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;

                if (channelDispatcher != null)
                    channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }


    }

}
