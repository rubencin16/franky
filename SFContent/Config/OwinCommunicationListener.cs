using Microsoft.Owin.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Owin;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFContent
{
    internal class OwinCommunicationListener : ICommunicationListener
    {
        private readonly ServiceEventSource eventSource;
        private readonly Action<IAppBuilder> startup;
        private readonly ServiceContext serviceContext;
        private readonly string endpointName;
        private readonly string appRoot;

        private IDisposable webApp;
        private string publishAddress;
        private string listeningAddress;
        public OwinCommunicationListener(Action<IAppBuilder> startup, ServiceContext serviceContext, ServiceEventSource eventSource, string endpointName) : this(startup, serviceContext, eventSource, endpointName, null) {}
        public OwinCommunicationListener(Action<IAppBuilder> startup, ServiceContext serviceContext, ServiceEventSource eventSource, string endpointName, string appRoot)
        {
            if (startup == null)
            {
                throw new ArgumentNullException(nameof(startup));
            }

            if (serviceContext == null)
            {
                throw new ArgumentNullException(nameof(serviceContext));
            }

            if (endpointName == null)
            {
                throw new ArgumentNullException(nameof(endpointName));
            }

            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            this.startup = startup;
            this.serviceContext = serviceContext;
            this.endpointName = endpointName;
            this.eventSource = eventSource;
            this.appRoot = appRoot;
        }
        public void Abort()
        {
            eventSource.Message("Aborting web server on endpoint {0}", this.endpointName);
            StopWebServer();
        }

        public Task CloseAsync(CancellationToken cancellationtoken)
        {
            eventSource.Message("Closing web server on endpoint {0}", this.endpointName);
            StopWebServer();
            return Task.FromResult(true);
        }

        public Task<string> OpenAsync(CancellationToken cancellationtoken)
        {
            var serviceEndpoint = this.serviceContext.CodePackageActivationContext.GetEndpoint(this.endpointName);
            var protocol = serviceEndpoint.Protocol;
            int port = serviceEndpoint.Port;

            if (serviceContext is StatefulServiceContext)
            {
                StatefulServiceContext statefulServiceContext = serviceContext as StatefulServiceContext;

                listeningAddress = string.Format(CultureInfo.InvariantCulture,"{0}://+:{1}/{2}{3}/{4}/{5}", protocol, port,string.IsNullOrWhiteSpace(appRoot) ? string.Empty : appRoot.TrimEnd('/') + '/', statefulServiceContext.PartitionId, statefulServiceContext.ReplicaId,Guid.NewGuid());
            }
            else if (serviceContext is StatelessServiceContext)
            {
                string root = string.IsNullOrWhiteSpace(appRoot) ? string.Empty : appRoot.TrimEnd('/') + '/';
                listeningAddress = string.Format(CultureInfo.InvariantCulture,$"{protocol}://+:{port}/{root}");
                //listeningAddress = $"{CultureInfo.InstalledUICulture}{protocol}://{port}/{root}" + '/';
            }
            else
            {
                throw new InvalidOperationException();
            }

            publishAddress = listeningAddress.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN).ToLower();

            try
            {
                eventSource.Message("Starting web server on " + listeningAddress);
                webApp = WebApp.Start(listeningAddress, appBuilder => startup.Invoke(appBuilder));
                eventSource.Message("Listening on " + this.publishAddress);

                return Task.FromResult(publishAddress);
            }
            catch (Exception ex)
            {
                eventSource.Message($"Web server failed to open endpoint {endpointName}. {ex.ToString()}");

                StopWebServer();
                throw;
            }
        }

        private void StopWebServer()
        {
            if (webApp != null)
            {
                try
                {
                    webApp.Dispose();
                }
                catch (ObjectDisposedException)
                {
                    throw;
                }
            }
        }

    }
}
