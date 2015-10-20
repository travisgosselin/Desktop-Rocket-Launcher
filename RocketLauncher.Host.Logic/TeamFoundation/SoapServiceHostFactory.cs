using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace RocketLauncher.Host.Logic.TeamFoundation
{
    public class SoapServiceHostFactory : ServiceHostFactory
    {
         private readonly Type _associatedContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoLimitServiceHostFactory"/> class.
        /// </summary>
        /// <param name="associatedContract">The associated contract.</param>
         public SoapServiceHostFactory(Type associatedContract)
        {
            this._associatedContract = associatedContract;
        }

        /// <summary>
        /// Creates a <see cref="T:System.ServiceModel.ServiceHost" /> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host.</param>
        /// <param name="baseAddresses">The <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses for the service hosted.</param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost" /> for the type of service specified with a specific base address.
        /// </returns>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new SoapServiceHost(this._associatedContract, serviceType, baseAddresses);
        }
    }
}