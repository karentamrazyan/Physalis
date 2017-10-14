using System;

namespace Physalis.Framework
{
	/// <summary>
	/// Use this attribute to specify all namespaces exported by the bundle.
	/// Physalis equivalent to the <seealso cref="Export-Package"/> OSGi manifest key.
	/// 
	/// Exported namespaces have to be specified as a single string where namespaces are separated by the ';' character,
	/// or directly with a string array.
	/// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class ExportNamespaceAttribute : NamespaceAttribute
	{
        public string[] Exports
        {
            get
            {
                return this.Namespaces;
            }
        }
        
        public ExportNamespaceAttribute(string exports) : base(exports)
        {
        }

        public ExportNamespaceAttribute(string[] exports) : base(exports)
		{
        }
	}
}
