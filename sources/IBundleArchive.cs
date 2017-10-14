using System;
using System.Collections;

namespace Physalis.Framework.Storage
{
	/// <summary>
	/// Summary description for IBundleArchive.
	/// </summary>
	public interface IBundleArchive
	{
        IDictionary Attributes
        {
            get;
        }

        Int32 Id
        {
            get;
        }

        string Location
        {
            get;
        }

        Int32 StartLevel
        {
            get;
            set;
        }

        bool Persistent
        {
            get;
            set;
        }

        bool LaunchedAtStartup
        {
            get;
            set;
        }

        void Purge();

        void Close();

        // TODO: Relevant for Physalis ?
        // byte[] getClassBytes(String component);

        // TODO: Relevant for Physalis ?
        // boolean componentExists(String component);

        // TODO: Relevant for Physalis ?
        // InputStream getInputStream(String component);

        // TODO: Relevant for Physalis ?
        // String getNativeLibrary(String component);
    }
}
