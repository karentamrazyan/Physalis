using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public interface IServiceReference
	{
        string[] Keys
        {
            get;
        }

        IBundle Bundle
        {
            get;
        }

        object this[string index]
        {
            get;
        }

        IBundle[] GetUsingBundles();
    }
}
