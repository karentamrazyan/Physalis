using System;

namespace Physalis.Framework
{
    /// <summary>
    /// Use this attribute to specify all namespaces imported by the bundle.
    /// Physalis equivalent to the <seealso cref="Import-Package"/> OSGi manifest key.
    /// 
    /// Imported namespaces have to be specified as a single string where namespaces are separated by the ';' character,
    /// or directly with a string array.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class ImportNamespaceAttribute : NamespaceAttribute
	{
        public string[] Imports
        {
            get
            {
                return this.Namespaces;
            }
        }
        
        public ImportNamespaceAttribute(string imports) : base(imports)
        {
        }

        public ImportNamespaceAttribute(string[] imports) : base(imports)
		{
        }
	}
}
