using System;
using System.Collections;
using Physalis.Utils;
using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// Handle all namespaces exported and imported in the framework.
	/// </summary>
	public class Namespaces
	{
        #region --- Fields ---
        private Hashtable namespaces = new Hashtable();
        private ArrayList tempResolved = null;
        private Hashtable tempProvider = null;
        #endregion

        internal Namespaces()
		{
        }

        internal void RegisterPackages(IEnumerator exports, IEnumerator imports) 
        {
            lock(namespaces)
            {
                while(exports.MoveNext()) 
                {
                    NamespaceEntry pe = (NamespaceEntry) exports.Current;
                    Namespace p = (Namespace) namespaces[pe.Name];
                    if (p == null) 
                    {
                        p = new Namespace(pe.Name);
                        namespaces[pe.Name] = p;
                    }
                    p.AddExporter(pe);
#if DEBUG
                    TracesOutputProvider.TracesOutput.OutputTrace("registerPackages: export, " + pe);
#endif
                }

                while (imports.MoveNext()) 
                {
                    NamespaceEntry pe = (NamespaceEntry) imports.Current;
                    Namespace p = (Namespace) namespaces[pe.Name];
                    if (p == null) 
                    {
                        p = new Namespace(pe.Name);
                        namespaces[pe.Name] = p;
                    }
                    p.AddImporter(pe);
#if DEBUG
                    TracesOutputProvider.TracesOutput.OutputTrace("registerPackages: import, " + pe);
#endif
                }
            }
        }

        internal NamespaceEntry RegisterDynamicImport(NamespaceEntry pe) 
        {
            lock(namespaces)
            {
#if DEBUG
                TracesOutputProvider.TracesOutput.OutputTrace("dynamicImportPackage: try " + pe);
#endif

                Namespace p = (Namespace) namespaces[pe.Name];
                if (p != null && p.Provider != null) 
                {
                    p.AddImporter(pe);
#if DEBUG
                    TracesOutputProvider.TracesOutput.OutputTrace("dynamicImportPackage: added " + pe);
#endif
                    return p.Provider;
                }
                return null;
            }
        }

        internal bool UnregisterPackages(IEnumerator exports, IEnumerator imports, bool force) 
        {
            lock(namespaces)
            {
                bool allRemoved = true;
                while (exports.MoveNext()) 
                {
                    NamespaceEntry pe = (NamespaceEntry)exports.Current;
                    Namespace p = pe.Namespace;
                    if(p != null) 
                    {
#if DEBUG
                        TracesOutputProvider.TracesOutput.OutputTrace("unregisterPackages: unregister export - " + pe);
#endif
                        if(!p.RemoveExporter(pe)) 
                        {
                            if(force) 
                            {
                                p.Provider = null;
                                p.RemoveExporter(pe);
#if DEBUG
                                TracesOutputProvider.TracesOutput.OutputTrace("unregisterPackages: forced unregister - " + pe);
#endif
                            } 
                            else 
                            {
                                allRemoved = false;
                                p.Zombie = true;
#if DEBUG
                                TracesOutputProvider.TracesOutput.OutputTrace("unregisterPackages: failed to unregister - " + pe);
#endif
                                continue;
                            }
                        }

                        if (p.IsEmpty) 
                        {
                            namespaces.Remove(pe.Name);
                        }
                    }
                }

                if(allRemoved) 
                {
                    while(imports.MoveNext()) 
                    {
                        NamespaceEntry pe = (NamespaceEntry)imports.Current;
                        Namespace p = pe.Namespace;
                        if (p != null) 
                        {
#if DEBUG
                            TracesOutputProvider.TracesOutput.OutputTrace("unregisterPackages: unregister import - " + pe.ToString());
#endif
                            p.RemoveImporter(pe);
                            if (p.IsEmpty) 
                            {
                                namespaces.Remove(pe.Name);
                            }
                        }
                    }
                }
                return allRemoved;
            }
        }

        internal ICollection GetNamespacesProvidedBy(IBundle b) 
        {
            ArrayList res = new ArrayList();
            for (IEnumerator i = namespaces.Values.GetEnumerator(); i.MoveNext();) 
            {
                Namespace p = (Namespace) i.Current;
                if (p.Provider != null && (b == null || b == p.Provider.Bundle)) 
                {
                    res.Add(p.Provider);
                }
            }

            return res;
        }

        internal bool IsProvider(NamespaceEntry pe) 
        {
            return pe.Namespace != null && pe.Namespace.Provider == pe;
        }
        
        //        synchronized List checkResolve(Bundle bundle, IEnumerator pkgs) 
//                          {
//                              if (Debug.namespaces) 
//                              {
//                                  Debug.println("checkResolve: " + bundle);
//                              }
//                              if (tempResolved != null) 
//                              {
//                                  // If we entry with tempResolved set, it means that we already have
//                                  // resolved bundles. Check that it is true!
//                                  if (!tempResolved.contains(bundle)) 
//                                  {
//                                      framework.listeners.frameworkError(bundle,
//                                          new Exception("checkResolve: InternalError1!"));
//                                  }
//                                  return null;
//                              }
//                              tempProvider = new HashMap();
//                              tempResolved = new ArrayList();
//                              tempResolved.add(bundle);
//                              List res = resolvePackages(pkgs);
//                              if (res.size() == 0) 
//                              {
//                                  for (IEnumerator i = tempProvider.values().iterator(); i.MoveNext();) 
//                                  {
//                                      NamespaceEntry pe = (NamespaceEntry)i.Current;
//                                      pe.Namespace.provider = pe;
//                                  }
//                                  tempResolved.remove(0);
//                                  for (IEnumerator i = tempResolved.iterator(); i.MoveNext();) 
//                                  {
//                                      BundleImpl bs = (BundleImpl)i.Current;
//                                      if (bs.getUpdatedState() == Bundle.INSTALLED) 
//                                      {
//                                          framework.listeners.frameworkError(bs,
//                                              new Exception("checkResolve: InternalError2!"));
//                                      }
//                                  }
//                                  res = null;
//                              }
//                              tempProvider = null;
//                              tempResolved = null;
//                              return res;
//                          }


        /**
         * Get selected provider of a package.
         *
         * @param pkg Exported package.
         * @return NamespaceEntry that exports the package, null if no provider.
         */
//        synchronized NamespaceEntry getProvider(String pkg) 
//                              {
//                                  Namespace p = (Namespace)namespaces.get(pkg);
//                                  if (p != null) 
//                                  {
//                                      return p.provider;
//                                  } 
//                                  else 
//                                  {
//                                      return null;
//                                  }
//                              }
    
    
        /**
         * Check if a package is in zombie state.
         *
         * @param pe Package to check.
         * @return True if pkg is zombie exported.
         */
//        synchronized bool isZombiePackage(NamespaceEntry pe) 
//                             {
//                                 return pe.Namespace != null && pe.Namespace.zombie;
//                             }
    
    
        /**
         * Get all namespaces exported by the system.
         *
         * @return Export-package string for system bundle.
         */
//        synchronized String systemPackages() 
//                            {
//                                StringBuffer res = new StringBuffer();
//                                for (IEnumerator i = namespaces.values().iterator(); i.MoveNext();) 
//                                {
//                                    Namespace p = (Namespace)i.Current;
//                                    NamespaceEntry pe = p.provider;
//                                    if (pe != null && framework.systemBundle == pe.bundle) 
//                                    {
//                                        if (res.length() > 0) 
//                                        {
//                                            res.append(", ");
//                                        }
//                                        res.append(pe.NamespaceString());
//                                    }
//                                }
//                                return res.toString();
//                            }


        /**
         * Get specification version of an exported package.
         *
         * @param pkg Exported package.
         * @param bundle Exporting bundle.
         * @return Version of package or null if unspecified.
         */
//        synchronized String getPackageVersion(String pkg) 
//                            {
//                                Namespace p = (Namespace)namespaces.get(pkg);
//                                NamespaceEntry pe = p.provider;
//                                return pe != null && pe.version.isSpecified() ? pe.version.toString() : null;
//                            }



        /**
         * Get active importers of a package.
         *
         * @param pkg Package.
         * @return List of bundles importering.
         */
//        synchronized Collection getPackageImporters(String pkg) 
//                                {
//                                    Namespace p = (Namespace)namespaces.get(pkg);
//                                    Set res = new HashSet();
//                                    if (p != null && p.provider != null) 
//                                    {
//                                        List i = p.importers;
//                                        for (int x =  0; x < i.size(); x++ ) 
//                                        {
//                                            NamespaceEntry pe = (NamespaceEntry)i.get(x);
//                                            if (pe.bundle.state != Bundle.INSTALLED) 
//                                            {
//                                                res.add(pe.bundle);
//                                            }
//                                        }
//                                    }
//                                    return res;
//                                }


        /**
         * Get bundles affected by zombie namespaces.
         * Compute a graph of bundles starting with the specified bundles.
         * If no bundles are specified, compute a graph of bundles starting
         * with all exporting a zombie package.
         * Any bundle that imports a package that is currently exported
         * by a bundle in the graph is added to the graph. The graph is fully
         * constructed when there is no bundle outside the graph that imports a
         * package from a bundle in the graph. The graph may contain
         * <tt>UNINSTALLED</tt> bundles that are currently still
         * exporting namespaces.
         *
         * @param bundles Initial bundle set.
         * @return List of bundles affected.
         */
//        synchronized Collection getZombieAffected(Bundle [] bundles) 
//                                {
//                                    HashSet affected = new HashSet();
//                                    if (bundles == null) 
//                                    {
//                                        if (Debug.namespaces) 
//                                        {
//                                            Debug.println("getZombieAffected: check - null");
//                                        }
//                                        for (IEnumerator i = namespaces.values().iterator(); i.MoveNext();) 
//                                        {
//                                            Namespace p = (Namespace)i.Current;
//                                            NamespaceEntry pe = p.provider;
//                                            if (pe != null && p.zombie) 
//                                            {
//                                                if (Debug.namespaces) 
//                                                {
//                                                    Debug.println("getZombieAffected: found zombie - " + pe.bundle);
//                                                }
//                                                affected.add(pe.bundle);
//                                            }
//                                        }
//                                    } 
//                                    else 
//                                    {
//                                        for (int i = 0; i < bundles.length; i++) 
//                                        {
//                                            if (bundles[i] != null) 
//                                            {
//                                                if (Debug.namespaces) 
//                                                {
//                                                    Debug.println("getZombieAffected: check - " + bundles[i]);
//                                                }
//                                                affected.add(bundles[i]);
//                                            }
//                                        }
//                                    }
//                                    ArrayList moreBundles = new ArrayList(affected);
//                                    for (int i = 0; i < moreBundles.size(); i++) 
//                                    {
//                                        BundleImpl b = (BundleImpl)moreBundles.get(i);
//                                        for (IEnumerator j = b.getExports(); j.MoveNext(); ) 
//                                        {
//                                            NamespaceEntry pe = (NamespaceEntry)j.Current;
//                                            if (pe.Namespace != null && pe.Namespace.provider == pe) 
//                                            {
//                                                for (IEnumerator k = getPackageImporters(pe.Name).iterator(); k.MoveNext(); ) 
//                                                {
//                                                    Bundle ib = (Bundle)k.Current;
//                                                    if (!affected.contains(ib)) 
//                                                    {
//                                                        moreBundles.add(ib);
//                                                        if (Debug.namespaces) 
//                                                        {
//                                                            Debug.println("getZombieAffected: added - " + ib);
//                                                        }
//                                                        affected.add(ib);
//                                                    }
//                                                }
//                                            }
//                                        }
//                                    }
//                                    return affected;
//                                }

        //
        // Private methods.
        //

        /**
         * Check if a bundle has all its package dependencies resolved.
         *
         * @param pkgs List of namespaces to be resolved.
         * @return List of namespaces not resolvable.
         */
//        private List resolvePackages(IEnumerator pkgs) 
//        {
//            ArrayList res = new ArrayList();
//            while (pkgs.MoveNext()) 
//            {
//                NamespaceEntry pe = (NamespaceEntry)pkgs.Current;
//                if (Debug.namespaces) 
//                {
//                    Debug.println("resolvePackages: check - " + pe.NamespaceString());
//                }
//                NamespaceEntry provider = pe.Namespace.provider;
//                if (provider == null) 
//                {
//                    provider = (NamespaceEntry)tempProvider.get(pe.Name);
//                    if (provider == null) 
//                    {
//                        provider = pickProvider(pe.Namespace);
//                    } 
//                    else if (Debug.namespaces) 
//                    {
//                        Debug.println("resolvePackages: " + pe.Name + " - has temporay provider - "
//                            + provider);
//                    }
//                } 
//                else if (Debug.namespaces) 
//                {
//                    Debug.println("resolvePackages: " + pe.Name + " - has provider - " + provider);
//                }
//                if (provider == null) 
//                {
//                    if (Debug.namespaces) 
//                    {
//                        Debug.println("resolvePackages: " + pe.Name + " - has no provider");
//                    }
//                    res.add(pe);
//                } 
//                else if (provider.compareVersion(pe) < 0) 
//                {
//                    if (Debug.namespaces) 
//                    {
//                        Debug.println("resolvePackages: " + pe.Name + " - provider has wrong version - " + provider + ", need " + pe.version + ", has " + provider.version);
//                    }
//                    res.add(pe);
//                }
//            }
//            return res;
//        }


        /**
         * Find a provider for specified package.
         *
         * @param pkg Package to find provider for.
         * @return Package entry that can provide.
         */
//        private NamespaceEntry pickProvider(Namespace p) 
//        {
//            if (Debug.namespaces) 
//            {
//                Debug.println("pickProvider: for - " + p.pkg);
//            }
//            NamespaceEntry provider = null;
//            for (IEnumerator i = p.exporters.iterator(); i.MoveNext(); ) 
//            {
//                NamespaceEntry pe = (NamespaceEntry)i.Current;
//                if ((pe.bundle.state & (Bundle.RESOLVED|Bundle.STARTING|Bundle.ACTIVE|Bundle.STOPPING)) != 0) 
//                {
//                    provider = pe;
//                    break;
//                }
//                if (pe.bundle.state == Bundle.INSTALLED) 
//                {
//                    if (tempResolved.contains(pe.bundle)) 
//                    {
//                        provider = pe;
//                        break;
//                    }
//                    int oldTempStartSize = tempResolved.size();
//                    HashMap oldTempProvider = (HashMap)tempProvider.clone();
//                    tempResolved.add(pe.bundle);
//                    List r = resolvePackages(pe.bundle.getImports());
//                    if (r.size() == 0) 
//                    {
//                        provider = pe;
//                        break;
//                    } 
//                    else 
//                    {
//                        tempProvider = oldTempProvider;
//                        for (int x = tempResolved.size() - 1; x >= oldTempStartSize; x--) 
//                        {
//                            tempResolved.remove(x);
//                        }
//                    }
//                }
//            }
//            if (provider != null) 
//            {
//                if (Debug.namespaces) 
//                {
//                    Debug.println("pickProvider: " + p.pkg + " - got provider - " + provider);
//                }
//                tempProvider.put(p.pkg, provider);
//            }
//            return provider;
//        }

    }
}
