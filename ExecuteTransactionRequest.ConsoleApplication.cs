using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ExecuteTransactionRequestConsoleApp
{
    public class Program
    {
        static string username = string.Empty;
        static IOrganizationService _service;
        public static void Main(string[] args)
        {
            try
            {
                ConnectToMSCRM("arunav@rbhyd123.onmicrosoft.com", "Qwerty@123", "https://rbhyd123.crm8.dynamics.com/XRMServices/2011/Organization.svc");
                Guid userid = ((WhoAmIResponse)_service.Execute(new WhoAmIRequest())).UserId;
                if (userid != Guid.Empty)
                {
                    Console.WriteLine("Connection Established Successfully");
                }
                Console.ReadLine();
                Entity order = new Entity("salesorder");
                order["name"] = "Sales Order 2 Created";
                Console.WriteLine("name  is initialised in order");
                Console.ReadLine();
                Entity invoice = new Entity("invoice");
                invoice["name"] = "Invoice 2 Created";
                Console.WriteLine("name is initialised in Invoice");
                Console.ReadLine();
                Entity email = new Entity("email");
                email["subject"] = "Email 2 Created";
                Console.WriteLine("name is initialised in email");
                Console.ReadLine();
                //2. Pass the objects to the Target of each request
                CreateRequest createOrderRequest = new CreateRequest
                {
                    Target = order
                };
                CreateRequest createInvoiceRequest = new CreateRequest
                {
                    Target = invoice
                };
                CreateRequest createEmailRequest = new CreateRequest
                {
                    Target = email
                };
                //3. Instantiate the ExecuteTransactionRequest object and pass each request. Note that the transaction will execute the requests in the order that they are added. Also note that I have created a plugin which will force the Invoice to fail during creation, so we can see what happens with the entire transaction.
                ExecuteTransactionRequest transactionRequest = new ExecuteTransactionRequest
                {
                    // Pass independent operations 
                    Requests = new OrganizationRequestCollection
                    {
                    createOrderRequest,
                    createInvoiceRequest, // we have forced this request to fail 
                    createEmailRequest
                    },
                };
                //4.Pass the request to the Execute method.


                ExecuteTransactionResponse transactResponse =
                (ExecuteTransactionResponse)_service.Execute(transactionRequest);
                Console.WriteLine("Service.Execute is called");
                Console.ReadLine();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                Console.ReadLine();
            }
        }

        private static void ConnectToMSCRM(string UserName, string Password, string SoapOrgServiceUri)
        {
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = UserName;
                credentials.UserName.Password = Password;
                Uri serviceUri = new Uri(SoapOrgServiceUri);
                OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
                proxy.EnableProxyTypes();
                _service = (IOrganizationService)proxy;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}
