using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FOFramework.Editor.AssetBundle {

    [Serializable]
    public class ABPathFilter {
        public bool     enable  = true;
        public string   path    = string.Empty;
        public string   filter  = "*.prefab";
    }

}
