using System;
using System.Collections;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class Namespace
	{
        #region --- Fields ---
        private readonly string name;
        private bool zombie = false;
        private NamespaceEntry provider;
        private ArrayList exporters = new ArrayList(1);
        private ArrayList importers = new ArrayList();
        #endregion

        #region --- Properties ---
        internal bool IsEmpty
        {
            get
            {
                lock(this)
                {
                    return exporters.Count == 0 && importers.Count == 0;
                }
            }
        }
        //TODO: Cyclic dependencie, to be re-design
        internal NamespaceEntry Provider
        {
            get
            {
                return provider;
            }
            set
            {
                provider = value;
            }
        }
        internal bool Zombie
        {
            set
            {
                zombie = value;
            }
        }
        #endregion

        internal Namespace(String name) 
        {
            this.name = name;
        }

        internal void AddExporter(NamespaceEntry entry) 
        {
            lock(this.exporters)
            {
                int i = Math.Abs(BinarySearch(exporters, entry) + 1);
                exporters[i] = entry;
                entry.Namespace = this;
            }
        }

        internal bool RemoveExporter(NamespaceEntry entry) 
        {
            if (entry == provider) 
            {
                return false;
            }

            lock(this.exporters)
            {
                for (int i = exporters.Count - 1; i >= 0; i--) 
                {
                    if (entry == exporters[i]) 
                    {
                        exporters.RemoveAt(i);
                        entry.Namespace = null;
                        break;
                    }
                }
            }

            return true;
        }

        internal void AddImporter(NamespaceEntry entry) 
        {
            lock(this.importers)
            {
                int i = Math.Abs(BinarySearch(importers, entry) + 1);
                importers[i] = entry;
                entry.Namespace = this;
            }
        }

        internal void RemoveImporter(NamespaceEntry entry) 
        {
            lock(this.importers)
            {
                for (int i = importers.Count - 1; i >= 0; i--) 
                {
                    if (entry == importers[i]) 
                    {
                        importers.RemoveAt(i);
                        entry.Namespace = null;
                        break;
                    }
                }
            }
        }

        /**
         * Do binary search for a package entry in the list with the same
         * version number add the specifies package entry.
         *
         * @param list Sorted list of package entries to search.
         * @param entry Package entry to search for.
         * @return index of the found entry. If no entry is found, return
         *         <tt>(-(<i>insertion point</i>) - 1)</tt>.  The insertion
         *         point</i> is defined as the point at which the
         *         key would be inserted into the list.
         */

        /// <summary>
        /// Search the given <see cref="NamespaceEntry"/> in the given <see cref="IList"/>
        /// with the same version.
        /// 
        /// </summary>
        /// <param name="list">The <see cref="IList"/> where the entry is goind to be searched.</param>
        /// <param name="entry">The namespace to be searched.</param>
        /// <returns>The index of the found entry if any</returns>
        private Int32 BinarySearch(IList list, NamespaceEntry entry) 
        {
            int l = 0;
            int u = list.Count - 1;

            while (l <= u) 
            {
                int m = (l + u) / 2;
                int v = ((NamespaceEntry)list[m]).CompareVersion(entry);
                if (v > 0) 
                {
                    l = m + 1;
                } 
                else if (v < 0) 
                {
                    u = m - 1;
                } 
                else 
                {
                    return m;
                }
            }

            return -(l + 1);  // key not found.
        }
    }
}
