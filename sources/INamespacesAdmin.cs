using System;
using Physalis.Specs.Framework;

namespace Physalis.Specs.Service.NamespacesAdmin
{
	/// <summary>
	/// Summary description for NamespaceAdmin.
	/// </summary>
	public interface INamespacesAdmin
	{
        IExportedNamespace[] GetExportedNamespaces(IBundle bundle);        
        IExportedNamespace GetExportedNamespace(string name);        
        void RefreshNamespaces(IBundle[] bundles);
    }
}
