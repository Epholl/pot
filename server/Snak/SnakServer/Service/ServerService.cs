using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SnakServer.Service
{
    class ServerService : IServerService
    {
        private ServiceHost serviceHost;

        public ServerService()
        {

        }

        public void StartService()
        {
            // Step 1 Create a URI to serve as the base address.
            Uri baseAddress = new Uri("http://localhost:8000/Snak/");

            // Step 2 Create a ServiceHost instance
            serviceHost = new ServiceHost(typeof(ServerService), baseAddress);

            try
            {
                // Step 3 Add a service endpoint.
                serviceHost.AddServiceEndpoint(typeof(IServerService), new WSHttpBinding(), "ServerService");

                // Step 4 Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                serviceHost.Description.Behaviors.Add(smb);

                // Step 5 Start the service.
                serviceHost.Open();
                Console.WriteLine("The service is ready.");
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                serviceHost.Abort();
            }
        }

        public void StopService()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }
        }

        public int GetSampleData(int input)
        {
            return input +1;
        }
    }
}
