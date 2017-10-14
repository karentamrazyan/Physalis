using System;
using Physalis.Specs.Framework;

namespace Physalis.Specs.Service.NamespacesAdmin
{
	/// <summary>
	/// Summary description for IExportedNamespace.
	/// </summary>
	public interface IExportedNamespace
	{
        string Name
        {
            get;
        }

        IBundle Exporting
        {
            get;
        }

        IBundle[] Importing
        {
            get;
        }
        string SpecificationVersion
        {
            get;
        }
        bool IsRemovalPending
        {
            get;
        }
    }
}
