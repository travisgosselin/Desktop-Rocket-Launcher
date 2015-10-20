using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace RocketLauncher.Host.Logic.TeamFoundation
{
    public class SoapServiceHost : ServiceHost
    {
        private readonly Type _associatedContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoLimitServiceHost"/> class.
        /// </summary>
        /// <param name="associatedContract">The associated contract.</param>
        public SoapServiceHost(Type associatedContract)
        {
            this._associatedContract = associatedContract;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoLimitServiceHost"/> class.
        /// </summary>
        /// <param name="associatedContract">The associated contract.</param>
        /// <param name="singletonInstance">The singleton instance.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        public SoapServiceHost(Type associatedContract, object singletonInstance, params Uri[] baseAddresses)
          : base(singletonInstance, baseAddresses)
        {
            this._associatedContract = associatedContract;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoLimitServiceHost"/> class.
        /// </summary>
        /// <param name="associatedContract">The associated contract.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        public SoapServiceHost(Type associatedContract, Type serviceType, params Uri[] baseAddresses)
          : base(serviceType, baseAddresses)
        {
            this._associatedContract = associatedContract;
        }

        /// <summary>
        /// Invoked during the transition of a communication object into the opening state.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when contract cannot be found to add no limit binding to service</exception>
        protected override void OnOpening()
        {
            base.OnOpening();

            // Check to see if the service host already has a ServiceMetadataBehavior
            var smb = Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (smb == null)
            {
                smb = new ServiceMetadataBehavior();
            }

            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Default;
            Description.Behaviors.Add(smb);

            foreach (var baseAddress in BaseAddresses) 
            {
                var found = false;
                foreach (var se in Description.Endpoints)
                {
                    if (se.Address.Uri == baseAddress)
                    {
                        found = true;
                        se.Binding = new WSHttpBinding(SecurityMode.None);
                    }
                }

                if (!found)
                {
                    var foundContract = false;
                    var enumerator = ImplementedContracts.Values.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        var contractType = enumerator.Current.ContractType;
                        if (contractType.FullName.Equals(this._associatedContract.FullName))
                        {
                            AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
                            AddServiceEndpoint(contractType, new WSHttpBinding(SecurityMode.None), baseAddress);

                            foundContract = true;
                            break;
                        }
                    }

                    if (!foundContract)
                    {
                        throw new InvalidOperationException(string.Format("Cannot add binding to service because its specified contract is not found: {0}", this._associatedContract.FullName));
                    }
                }
            }
        }
    }
}