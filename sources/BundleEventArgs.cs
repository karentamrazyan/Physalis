using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class BundleEventArgs : EventArgs
	{
		private IBundle bundle;

        private BundleTransition transition;
        
        public IBundle Bundle
        {
            get
            {
                return bundle;
            }
        }

        public BundleTransition Transition
        {
            get
            {
                return transition;
            }
        }

        public BundleEventArgs(IBundle bundle, BundleTransition transition) : base()
        {
            this.bundle = bundle;
            this.transition = transition;
        }
	}
}
