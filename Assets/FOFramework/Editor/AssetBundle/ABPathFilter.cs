using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.FOFramework.Editor.AssetBundle {

    [Serializable]
    public class ABPathFilter {
        public bool     enable      = true;
        public string   pattern      = "*.prefab";
        private string  simplypath  = string.Empty;
        private string  fullpath    = string.Empty;

        public string FullPath {
            set {
                fullpath = value;
                SimplyPath = fullpath.Replace(Application.dataPath, "Assets");
            }
            get { return fullpath; }
        }
        public string SimplyPath {
            private set { simplypath = value; }
            get { return simplypath; }
        }
    }
}
