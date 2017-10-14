using System;
using System.Collections;
using System.IO;

namespace Physalis.Framework.Storage
{
	/// <summary>
	/// Summary description for IBundleStorage.
	/// </summary>
	public interface IBundleStorage
	{
        IBundleArchive InsertBundle(string location, FileStream stream);

        IBundleArchive ReplaceBundle(IBundleArchive old, FileStream stream);

        IBundleArchive[] All
        {
            get;
        }

        Queue LaunchedAtStartup
        {
            get;
        }
    }
}
