using System;

namespace Physalis.Framework.Storage
{
	/// <summary>
	/// 
	/// </summary>
	public class BundleStorage : IBundleStorage
	{
		public BundleStorage()
		{
        }
        #region --- IBundleStorage Members ---

        public IBundleArchive InsertBundle(string location, System.IO.FileStream stream)
        {
            // TODO:  Add BundleStorage.InsertBundle implementation
            return null;
        }

        public IBundleArchive ReplaceBundle(IBundleArchive old, System.IO.FileStream stream)
        {
            // TODO:  Add BundleStorage.ReplaceBundle implementation
            return null;
        }

        public IBundleArchive[] All
        {
            get
            {
                // TODO:  Add BundleStorage.All getter implementation
                return null;
            }
        }

        public System.Collections.Queue LaunchedAtStartup
        {
            get
            {
                // TODO:  Add BundleStorage.LaunchedAtStartup getter implementation
                return null;
            }
        }

        #endregion
    }
}
