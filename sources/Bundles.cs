using System;
using System.Collections;
using System.IO;
using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	internal class Bundles
	{
        #region --- Fields ---
        private Hashtable bundles;
        #endregion
        
        #region --- Properties ---
        IBundle this[string location]
        {
            get
            {
                return (IBundle) bundles[location];
            }
        }
        IBundle this[Int32 id]
        {
            get
            {
                lock(bundles)
                {
                    IBundle bundle = null;
                    IEnumerator enumerator = bundles.GetEnumerator();
                    while(enumerator.MoveNext())
                    {
                        if(((IBundle) enumerator.Current).Id == id)
                        {
                            bundle = (IBundle) enumerator.Current;
                            break;
                        }
                    }

                    return bundle;
                }
            }
        }
        IBundle[] All
        {
            get
            {
                lock(this.bundles)
                {
                    IBundle[] all = new IBundle[bundles.Count];
                    Int32 i = 0;
                    IEnumerator enumerator = bundles.GetEnumerator();
                    while(enumerator.MoveNext())
                    {
                        all[i++] = (IBundle) enumerator.Current;
                    }
                    return all;
                }
            }
        }

        #endregion

        internal Bundles()
		{
            bundles = new Hashtable();
        }

        internal void Install(IBundle bundle)
        {
            bundles[bundle.Location] = bundle;
        }
        
        internal IBundle Install(string location, Stream stream)
        {
            lock(this)
            {
                IBundle bundle = (IBundle) bundles[location];
                if(bundle  == null)
                {
                    //TODO: To be completed ASAP
                }

                return bundle;
            }
        }

        void Remove(string location)
        {
            bundles.Remove(location);
        }
	}
}
