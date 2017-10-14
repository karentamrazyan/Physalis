using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class FrameworkEventArgs : EventArgs
	{
        private FrameworkEvent evt;
        
        private IBundle bundle;

        private Exception exception;
        
        public FrameworkEvent Event
        {
            get
            {
                return evt;
            }
        }

        public IBundle Bundle
        {
            get
            {
                return bundle;
            }
        }

        public Exception Exception
        {
            get
            {
                return exception;
            }
        }

        public FrameworkEventArgs(FrameworkEvent evt, IBundle bundle, Exception exception) : base()
		{
            this.evt = evt;
            this.bundle = bundle;
            this.exception = exception;
        }
	}
}
