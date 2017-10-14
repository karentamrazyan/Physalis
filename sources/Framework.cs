using System;
using System.IO;
using Physalis.Framework.Storage;
using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// The Physalis framwork. Designed as a singleton,
	/// there is only one instance of this class per application.
	/// </summary>
    public class Framework
    {
        #region --- Constants ---
        private const string DATA_STORAGE = "data";
        #endregion

        #region --- Fields ---
        private IBundleStorage storage;
        private DirectoryInfo data;
        private Namespaces namespaces;
        private IBundle system;
        private Bundles bundles;
        private Services services;
        #endregion
        
        #region --- Singleton ---
        public static readonly Framework Instance = new Framework();
        #endregion

        #region --- Events & Delegates ---
        public delegate void ServiceEventHandler(object sender, ServiceEventArgs args);
        public event ServiceEventHandler ServiceEvent;
        #endregion
        
        #region --- Properties ---
        public object this[string index]
        {
            get
            {
                return null;
            }
        }
        IBundle System
        {
            get
            {
                return system;
            }
        }
        internal Namespaces Namespaces
        {
            get
            {
                return namespaces;
            }
        }
        #endregion
        
        private Framework()
        {
        }
        
        public void Intialize()
		{
            services = new Services();
            storage  = new BundleStorage();
            data = new DirectoryInfo(DATA_STORAGE);
            namespaces = new Namespaces();
            system = new SystemBundle();
            bundles = new Bundles();
            bundles.Install(system);

            services.Register(  system,
		                        new string [] { typeof(NamespacesAdmin).FullName },
		                        new NamespacesAdmin(),
		                        null);

//            startLevelService = new StartLevelImpl();
//            services.Register(  system,
//                                new String [] { typeof(StartLevel).FullName },
//                                startLevelService,
//                                null);
//            startLevelService.open();
        }

        internal void FireServiceEvent(ServiceEventArgs args)
        {
            if(ServiceEvent != null)
            {
                ServiceEvent(this, args);
            }
        }
	}
}
