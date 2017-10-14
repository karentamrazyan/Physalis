using System;
using Physalis.Specs.Framework;
using Physalis.Specs.Service.NamespacesAdmin;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	internal class ExportedNamespace : IExportedNamespace
	{
        #region --- Fields ---
        private readonly NamespaceEntry entry;
        #endregion

        #region --- Properties ---
        public string Name
        {
            get
            {
                return entry.Name;
            }
        }

        public IBundle Exporting
        {
            get
            {
                return Framework.Instance.Namespaces.IsProvider(entry) ? 
                    entry.Bundle : null;
            }
        }

        public Physalis.Specs.Framework.IBundle[] Importing
        {
            get
            {
                // TODO:  Add ExportedNamespace.Importing getter implementation
                return null;
            }
        }

        public string SpecificationVersion
        {
            get
            {
                // TODO:  Add ExportedNamespace.SpecificationVersion getter implementation
                return null;
            }
        }

        public bool IsRemovalPending
        {
            get
            {
                // TODO:  Add ExportedNamespace.IsRemovalPending getter implementation
                return false;
            }
        }
        #endregion
        
        internal ExportedNamespace(NamespaceEntry entry)
		{
            this.entry = entry;
        }
    }
}
