using System;

namespace Physalis.Framework
{
    /// <summary>
    /// This class is an internal base class for <see cref="ExportNamespaceAttribute"/>
    /// and <see cref="ExportNamespaceAttribute"/>.
    /// </summary>
    public class NamespaceAttribute : System.Attribute
    {
        private const Char SEPARATOR = ';';
        /// <summary>
        /// List of namespaces hold by the attribute.
        /// </summary>
        private string[] spaces;

        /// <summary>
        /// Get or set the list of namespaces.
        /// </summary>
        protected string[] Namespaces
        {
            get
            {
                return spaces;
            }
            set
            {
                spaces = value;
            }
        }

        /// <summary>
        /// default constructor.
        /// </summary>
        public NamespaceAttribute()
        {
        }
        
        /// <summary>
        /// Fill the list of namespaces from the given string.
        /// Assume the separator is ';'.
        /// </summary>
        /// <param name="spaces"></param>
        public NamespaceAttribute(string spaces)
        {
            this.spaces = spaces.Split(SEPARATOR);
        }
        
        /// <summary>
        /// Fill the namespaces list from the given one.
        /// </summary>
        /// <param name="spaces"></param>
        public NamespaceAttribute(string[] spaces)
        {
            this.spaces = spaces;
        }
    }
}
