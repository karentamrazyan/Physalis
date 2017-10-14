using System;
using System.Collections;

using Physalis.Specs.Service.NamespacesAdmin;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class NamespacesAdmin : INamespacesAdmin
	{
		public NamespacesAdmin()
		{
        }

        public IExportedNamespace[] GetExportedNamespaces(Physalis.Specs.Framework.IBundle bundle)
        {
            Namespaces namespaces = Framework.Instance.Namespaces;
            ICollection list = namespaces.GetNamespacesProvidedBy(bundle);
            int size = list.Count;
            if (size > 0) 
            {
                IExportedNamespace[] res = new IExportedNamespace[size];
                IEnumerator i = list.GetEnumerator();
                for (int pos = 0; pos < size;) 
                {
                    res[pos++] = new ExportedNamespace((NamespaceEntry) i.Current);
                }
                return res;
            } 
            else 
            {
                return null;
            }
        }

        public IExportedNamespace GetExportedNamespace(string name)
        {
            // TODO:  Add NamespaceAdmin.GetExportedNamespace implementation
            return null;
        }

        public void RefreshNamespaces(Physalis.Specs.Framework.IBundle[] bundles)
        {
            // TODO:  Add NamespaceAdmin.RefreshNamespaces implementation
        }
    }
}
