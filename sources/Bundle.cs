using System;
using Physalis.Specs.Framework;
using Physalis.Framework.Storage;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class Bundle : IBundle
	{
        #region --- Fields ---
        protected BundleState theState;
        private Int32 id;
        private string location;
        private IBundleArchive archive;
        protected IBundleContext context;
        #endregion

        #region --- Properties ---
        /// <summary>
        /// The bundle ID
        /// </summary>
        public Int32 Id
        {
            get
            {
                return id;
            }
        }
        /// <summary>
        /// The bundle theState
        /// </summary>
        public BundleState State
        {
            get
            {
                return theState;
            }
        }
        /// <summary>
        /// The bundle location
        /// </summary>
        public string Location
        {
            get
            {
                return location;
            }
        }
        public System.Collections.IDictionary Headers
        {
            get
            {
                // TODO:  Add Bundle.Headers getter implementation
                return null;
            }
        }

        public IServiceReference[] RegisteredServices
        {
            get
            {
                // TODO:  Add Bundle.RegisteredServices getter implementation
                return null;
            }
        }

        public IServiceReference[] ServicesInUse
        {
            get
            {
                // TODO:  Add Bundle.ServicesInUse getter implementation
                return null;
            }
        }
        #endregion
        
        public Bundle(Int32 id, string location)
		{
            this.id = id;
            this.location = location;
        }

        public Bundle(IBundleArchive ba)
        {
            this.id = ba.Id;
            this.location = ba.Location;
            this.archive = ba;
            this.theState = BundleState.Installed;

//            doExportImport();
//            FileTree dataRoot = fw.getDataStorage();
//            if (dataRoot != null) 
//            {
//                bundleDir = new FileTree(dataRoot, Long.toString(id));
//            }
//            ProtectionDomain pd = null;
//            if (fw.bPermissions) 
//            {
//                try 
//                {
//                    URL bundleUrl = new URL(BundleURLStreamHandler.PROTOCOL, Long.toString(id), "");
//                    PermissionCollection pc = fw.permissions.getPermissionCollection(this);
//                    pd = new ProtectionDomain(new CodeSource(bundleUrl, 
//                        (java.security.cert.Certificate[])null), 
//                        pc);
//                } 
//                catch (MalformedURLException never) { }
//            }
//            protectionDomain = pd;
//
//            int oldStartLevel = archive.getStartLevel();
//
//            try 
//            {
//                if(framework.startLevelService == null) 
//                {
//                    archive.setStartLevel(0);
//                } 
//                else 
//                {
//                    if(oldStartLevel == -1) 
//                    {
//                        archive.setStartLevel(framework.startLevelService.getInitialBundleStartLevel());
//                    } 
//                    else 
//                    {
//                    } 
//                }
//            } 
//            catch (Exception e) 
//            {
//                Debug.println("Failed to set start level on #" + getBundleId() + ": " + e);
//            }
        }

        public virtual void Start()
        {
            // TODO:  Add Bundle.Start implementation
        }

        public virtual void Stop()
        {
            // TODO:  Add Bundle.Stop implementation
        }

        public virtual void Update()
        {
            // TODO:  Add Bundle.Update implementation
        }

        public virtual void Update(System.IO.Stream input)
        {
            // TODO:  Add Bundle.Physalis.Specs.Framework.IBundle.Update implementation
        }

        public virtual void Uninstall()
        {
            // TODO:  Add Bundle.Uninstall implementation
        }

        public string GetResource(string name)
        {
            // TODO:  Add Bundle.GetResource implementation
            return null;
        }

        public virtual bool HasPermission(object permission)
        {
            // TODO:  Add Bundle.hasPermission implementation
            return false;
        }

        #region --- Private methods ---
        private void ProcessExportImport() 
        {
//            bpkgs = new Namespaces(this,
//                archive.getAttribute(Constants.EXPORT_PACKAGE),
//                archive.getAttribute(Constants.IMPORT_PACKAGE),
//                archive.getAttribute(Constants.DYNAMICIMPORT_PACKAGE));
//            bpkgs.registerPackages();
        }
        #endregion
    }
}
