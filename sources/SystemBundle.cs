using System;
using System.Collections;
using System.IO;
using System.Text;
using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	internal class SystemBundle : Bundle
	{
        #region --- Constants ---
        private const string SYS_NAMESPACE = "Physalis.Framework.System.Namespaces";
        private const string SYS_NAMESPACE_FILE = "Physalis.Framework.System.Namespaces.File";
        #endregion

        #region --- Fields ---
        private Hashtable headers;
        #endregion

        #region --- Properties ---
        IBundleContext Context
        {
            set
            {
                context = value;
            }
        }
        #endregion

        internal SystemBundle() : base(0, Constants.SYSTEM_BUNDLE_LOCATION)
        {
            this.theState = BundleState.Starting;
            this.context = new BundleContext(this);

            //TODO: Add properties
            StringBuilder sp = new StringBuilder(/*System.getProperty(SYS_NAMESPACE, "")*/);
            if (sp.Length > 0) 
            {
                sp.Append(",");
            }

//            addSysPackagesFromFile(sp, System.getProperty(SYS_NAMESPACE_FILE, null));
//    
//            addSystemPackages(sp);
//    
//            bpkgs = new BundlePackages(this, sp.toString(), null, null);
//            bpkgs.registerPackages();
//            bpkgs.resolvePackages();
        }

        void AddSystemPackages(StringBuilder sp) 
        {
//            String name = Type.GetType(IBundle)
//                .class.getName();
//            name = name.substring(0, name.lastIndexOf('.'));
//            sp.append(name + ";" + Constants.PACKAGE_SPECIFICATION_VERSION +
//            "=" + Framework.SPEC_VERSION);
//    
//            // Set up packageadmin package
//            name = PackageAdmin.class.getName();
//                                    name = name.substring(0, name.lastIndexOf('.'));
//                                    sp.append("," + name + ";" + Constants.PACKAGE_SPECIFICATION_VERSION +
//                                    "=" +  PackageAdminImpl.SPEC_VERSION);
//    
//            // Set up permissionadmin package
//            name = PermissionAdmin.class.getName();
//                                    name = name.substring(0, name.lastIndexOf('.'));
//                                    sp.append("," + name + ";" + Constants.PACKAGE_SPECIFICATION_VERSION +
//                                    "=" +  PermissionAdminImpl.SPEC_VERSION);
//
//            // Set up startlevel package
//            name = StartLevel.class.getName();
//                                name = name.substring(0, name.lastIndexOf('.'));
//                                sp.append("," + name + ";" + Constants.PACKAGE_SPECIFICATION_VERSION +
//                                "=" +  StartLevelImpl.SPEC_VERSION);
//    
//            // Set up tracker package
//            name = ServiceTracker.class.getName();
//                                    name = name.substring(0, name.lastIndexOf('.'));
//                                    sp.append("," + name + ";" + Constants.PACKAGE_SPECIFICATION_VERSION +
//                                    "=" +  "1.2");
//
//        // Set up URL package
//        name = org.osgi.service.url.URLStreamHandlerService.class.getName();
//                            name = name.substring(0, name.lastIndexOf('.'));
//                            sp.append("," + name + ";" + Constants.PACKAGE_SPECIFICATION_VERSION +
//                            "=" +  "1.0");
    }

/**
    * Read a file with package names and add them to a stringbuffer.
    */
//    void addSysPackagesFromFile(StringBuffer sp, String sysPkgFile) 
//    {
//
//        if(sysPkgFile != null) 
//        {
//            File f = new File(sysPkgFile);
//            if(!f.exists() || !f.isFile()) 
//            {
//                throw new RuntimeException("System package file '" + sysPkgFile + 
//                    "' does not exists");
//            } 
//            else 
//            {
//                if(Debug.packages) 
//                {
//                    Debug.println("adding system packages from file " + sysPkgFile);
//                }
//                BufferedReader in = null;
//                try 
//                {
//                    in = new BufferedReader(new FileReader(sysPkgFile));
//                    String line;
//                    for(line = in.readLine(); line != null; 
//                        line = in.readLine()) 
//                    {
//                        line = line.trim();
//                        if(line.length() > 0 && !line.startsWith("#")) 
//                        {
//                            sp.append(line);
//                            sp.append(",");
//                        }
//                    } 
//                } 
//                catch (IOException e) 
//                {
//                    throw new IllegalArgumentException("Failed to read " + sysPkgFile + ": " + e);
//                } 
//                finally 
//                {
//                    try {   in.close();  } 
//                    catch (Exception ignored) { }
//                }
//            }
//        }
//    }

    public override bool HasPermission(object permission) 
    {
        return true;
    }

    public override void Start()
    {
//        framework.checkAdminPermission();
    }


    public override void Stop()
    {
//        framework.checkAdminPermission();
        Starter.Shutdown(0);
    }


    public override void Update(Stream input)
    {
//        framework.checkAdminPermission();
        if((bool) Starter.Instance.Properties["Physalis.Framework.Restart.Allow"])
        {
            Starter.Restart();
        } 
        else 
        {
            Starter.Shutdown(2);
        }
    }

    public override void Uninstall()
    {
//        framework.checkAdminPermission();
        throw new BundleException("uninstall of System bundle is not allowed");
    }
    
    /**
     * Get header data. Simulate EXPORT-PACKAGE.
     *
     * @see org.osgi.framework.Bundle#getHeaders
     */
//    public Dictionary getHeaders() 
//{
//    framework.checkAdminPermission();
//    if (headers == null) 
//{
//    headers = new HeaderDictionary();
//    headers.put(Constants.BUNDLE_NAME, Constants.SYSTEM_BUNDLE_LOCATION);
//    headers.put(Constants.EXPORT_PACKAGE, framework.packages.systemPackages());
//}
//    return new HeaderDictionary(headers);
//}

//    /**
//     * Get class loader for this bundle.
//     */
//    ClassLoader getClassLoader() 
//    {
//        return getClass().getClassLoader();
//    }

    void SystemActive() 
    {
        this.theState = BundleState.Active;
    }

    void SystemShuttingdown() 
    {
        this.theState = BundleState.Stopping;
    }

}
}
