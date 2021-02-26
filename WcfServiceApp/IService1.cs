using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using WcfServiceApp.Models;

namespace WcfServiceApp
{
    [MessageContract]
    public class CustomerRequest
    {
        [MessageHeader(Namespace = "http://domain.com")]
        public string SecretKey { get; set; }

        [MessageBodyMember(Namespace = "http://domain.com")]
        public int CustomerId { get; set; }
    }

    [MessageContract]
    public class CustomerReponse
    {
        [MessageBodyMember]
        public Customer Customer { get; set; }

        [MessageHeader]
        public string Note { get; set; }
    }


    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Customer Get(Guid customerId);

        // GET api/customers/{customerId}
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "api/customers/{idsession}")]
        Customer GetCustomerById(Guid customerId);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        string Ping(string message);


        [OperationContract]
        // [FaultContract(typeof(DivideByZeroFault))]
        decimal TaxCalculate(decimal amount, decimal tax);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
