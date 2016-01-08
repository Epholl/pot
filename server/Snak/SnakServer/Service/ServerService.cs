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
            Uri baseAddress = new Uri("http://localhost:8000/Snak/");
            serviceHost = new ServiceHost(typeof(ServerService), baseAddress);
            SL.Register(this);
        }

        public void StartService()
        {
            try
            {
                serviceHost.AddServiceEndpoint(typeof(IServerService), new WSHttpBinding(), "ServerService");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                serviceHost.Description.Behaviors.Add(smb);
                
                serviceHost.Open();
            }
            catch (CommunicationException ce)
            {
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
            MainWindow mainWindow = (MainWindow)SL.Get(typeof(MainWindow));
            mainWindow.runningInstancesLabel.Content = "WCF called: " + input;
            return input +1;
        }
    }
}
