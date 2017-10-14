using System;
using Physalis.Specs.Framework;

namespace Physalis.Specs.Service.StartLevel
{
	/// <summary>
	/// The StartLevel service allows management agents to manage a start level
	/// assigned to each bundle and the active start level of the Framework.
	/// There is at most one StartLevel service present in the OSGi environment.
	/// </summary>
	public interface IStartLevel
	{
        Int32 StartLevel
        {
            get;
            set;
        }

        Int32 InitialStartLevel
        {
            get;
            set;
        }

        Int32 GetBundleStartLevel(IBundle bundle);
        void SetBundleStartLevel(IBundle bundle, Int32 startlevel);

        bool IsBundlePersistentlyStarted(IBundle bundle);
    }
}
