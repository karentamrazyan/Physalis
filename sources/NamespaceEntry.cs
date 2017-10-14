using System;
using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// Class used to handle a namespace all related info like bundles dependant on this namespace...
	/// </summary>
    public class NamespaceEntry
    {
        #region --- Fields ---
        readonly private string name;
        readonly private Bundle bundle;
        readonly private Version version;
        private Namespace space;
        #endregion

        #region --- Properties ---
        //TODO: Cyclic dependencie, to be re-design
        internal Namespace Namespace
        {
            get
            {
                return space;
            }
            set
            {
                space = value;
            }
        }
        internal string Name
        {
            get
            {
                return name;
            }
        }
        internal IBundle Bundle
        {
            get
            {
                return bundle;
            }
        }
        #endregion
        
        #region --- Constructors ---
        internal NamespaceEntry(string p, Version v, Bundle b) 
        {
            this.name = p;
            this.version = v;
            this.bundle = b;
        }

        internal NamespaceEntry(string p, string v, Bundle b) 
        {
            this.name = p;
            this.version = new Version(v);
            this.bundle = b;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="pe">The entry to be copied.</param>
        internal NamespaceEntry(NamespaceEntry pe) 
        {
            this.name = pe.name;
            this.version = pe.version;
            this.bundle = pe.bundle;
        }
        #endregion

        #region --- System.Object overloading ---
        public override bool Equals(Object obj)
        {
            NamespaceEntry o = (NamespaceEntry)obj;
            return name.Equals(o.name) && bundle == o.bundle && version.Equals(o.version);
        }
        public override int GetHashCode() 
        {
            return name.GetHashCode() + bundle.GetHashCode() + version.GetHashCode();
        }
        public override string ToString() 
        {
            return name + "; " + Constants.PACKAGE_SPECIFICATION_VERSION + "=" + version;
        }
        public string ToString(NamespaceEntryEnumeration level) 
        {
            switch(level)
            {
                case NamespaceEntryEnumeration.Simple :     return name;
                case NamespaceEntryEnumeration.Standard :   return this.ToString();
                case NamespaceEntryEnumeration.Complete :   return this.ToString() + "(" + bundle + ")";
                default : return name;
            }
        }
        #endregion

        internal bool NameEqual(NamespaceEntry other) 
        {
            return name.Equals(other.name);
        }

        public Int32 CompareVersion(NamespaceEntry entry)
        {
            return version.CompareTo(entry.version);
        }

    }
}
