using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using Physalis.Utils;

namespace Physalis.Framework
{
	/// <summary>
	/// Physalis entry point. Is a singleton.
	/// </summary>
	public class Starter
	{
        #region --- Constants ---
        public const string PHYSALIS_DIR_PROP = "Physalis.dir";
        public const string PHYSALIS_DIR_DEF = "fwdir";
        #endregion

        #region --- Fields ---
        private IDictionary properties;
        #endregion

        #region --- Properties ---
        /// <summary>
        /// Physalis framework properties. The values are stored in a XML file.
        /// </summary>
        public IDictionary Properties
        {
            get
            {
                return properties;
            }
        }
        /// <summary>
        /// Return the Physalis assembly version.
        /// </summary>
        /// <returns>The Physalis version as string</returns>
        private string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        #endregion

        #region --- Singleton ---
        public static readonly Starter Instance = new Starter();
        #endregion
        
        public Starter()
		{
		}

        /// <summary>
        /// Start Physalis here.
        /// </summary>
        public void Start()
        {
            TracesOutputProvider.TracesOutput.OutputTrace("Physalis is starting...\n");
            TracesOutputProvider.TracesOutput.OutputTrace(String.Format("Physalis framework, version {0}\nCopyright 2004 Physalis. All Rights Reserved.\nSee http://physalis.berlios.de for more information.\n", Version));

            TracesOutputProvider.TracesOutput.OutputTrace("Process configuration file...\n");
            
            InitializeProperties();

            try
            {
                InitCache((string) this.Properties[PHYSALIS_DIR_PROP]);
                Framework.Instance.Intialize();
            }
            catch(Exception e)
            {
                TracesOutputProvider.TracesOutput.OutputTrace("Error while initializing the Physalis framework.");
                TracesOutputProvider.TracesOutput.OutputTrace(e.Message);
            }
        }

        /// <summary>
        /// Initialize the cache folder, if this folder already exists,
        /// it is deleted and re-created.
        /// </summary>
        /// <param name="cache">The cache folder name.</param>
        private void InitCache(string cache)
        {
            // Get the application path
            string path = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetCallingAssembly().GetName().CodeBase )
                + Path.DirectorySeparatorChar + cache;

#if DEBUG
            TracesOutputProvider.TracesOutput.OutputTrace("Cache initialization: " + path);
#endif 
            
            DirectoryInfo folder = new DirectoryInfo(path);
            if(folder.Exists)
            {
                folder.Delete(true);
            }

            folder.Create();
        }

        /// <summary>
        /// Initialize the Physalis properties.
        /// </summary>
        private void InitializeProperties()
        {
            properties = new ListDictionary();
            properties.Add(PHYSALIS_DIR_PROP, PHYSALIS_DIR_DEF);
        }

        static public void Shutdown(int exitcode)
        {
//            framework.checkAdminPermission();
//            AccessController.doPrivileged(new PrivilegedAction() {
//	        public Object run() {
//	        Thread t = new Thread() {
//	            public void run() {
//		        if (bootMgr != 0) {
//		        try {
//		            framework.stopBundle(bootMgr);
//		        } catch (BundleException e) {
//		            System.err.println("Stop of BootManager failed, " +
//				            e.getNestedException());
//		        }
//		        }
//		        framework.shutdown();
//		        System.exit(exitcode);
//	            }
//	            };
//	        t.setDaemon(false);
//	        t.start();
//	        return null;
//	        }
//            });
        }

        static public void Restart()
        {
//            framework.checkAdminPermission();

//            AccessController.doPrivileged(new PrivilegedAction() {
//	        public Object run() {
//	        Thread t = new Thread() {
//	            public void run() {
//		        if (bootMgr != 0) {
//		        try {
//		            framework.stopBundle(bootMgr);
//		        } catch (BundleException e) {
//		            System.err.println("Stop of BootManager failed, " +
//				            e.getNestedException());
//		        }
//		        }
//		        framework.shutdown();
//
//		        try {
//		        if (bootMgr != 0) {
//		            framework.launch(bootMgr);
//		        } else {
//		            framework.launch(0);
//		        }
//		        } catch (Exception e) {
//		        println("Failed to restart framework", 0);
//		        }
//	            }
//	            };
//	        t.setDaemon(false);
//	        t.start();
//	        return null;
//	        }
//            });
        }
    }
}
