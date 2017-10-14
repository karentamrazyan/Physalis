using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class ServiceEventArgs : EventArgs
	{
        private IServiceReference reference;

        private ServiceTransition transition;
        
        public IServiceReference Reference
        {
            get
            {
                return reference;
            }
        }

        public ServiceTransition Transition
        {
            get
            {
                return transition;
            }
        }
        
        public ServiceEventArgs(ServiceTransition transition, IServiceReference reference) : base()
		{
		    this.transition = transition;
            this.reference = reference;
        }
	}
}
