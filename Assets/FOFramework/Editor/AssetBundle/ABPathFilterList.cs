using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Assets.FOFramework.Editor.AssetBundle {
    public class ABPathFilterList : ScriptableObject {
        private List<ABPathFilter> filters;
        public List<ABPathFilter> Filters {
            private set { filters = value; }
            get {
                if (filters == null) {
                    filters = new List<ABPathFilter>();
                    AppendEmpty();
                }
                return filters;
            }
        }

        public void AppendEmpty() {
            ABPathFilter filter = new ABPathFilter();
            Filters.Add(filter);
        }
    }
}
