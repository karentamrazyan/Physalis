using System;
using System.Collections;

namespace Physalis.Framework
{
	/// <summary>
	/// All namespaces import or exported by a bundle.
	/// </summary>
	public class BundleNamespaces
	{
        #region --- Fields ---
        private readonly Bundle bundle;
        private ArrayList exports = new ArrayList(1);
        private ArrayList imports = new ArrayList(1);
        private ArrayList dImportPatterns = new ArrayList(1);
        private Hashtable okImports;
        private string failReason;
        #endregion

        public BundleNamespaces()
		{
			// 
			// TODO: Add constructor logic here
			//
		}
    }
}
