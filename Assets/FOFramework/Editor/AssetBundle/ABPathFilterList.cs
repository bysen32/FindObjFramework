﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Assets.FOFramework.Editor.AssetBundle {
    public class ABPathFilterList : ScriptableObject {
        [SerializeField]
        private List<ABPathFilter> filters;
        public List<ABPathFilter> Filters {
            get {
                if (filters == null) {
                    filters = new List<ABPathFilter>();
                    AppendEmpty();
                }
                return filters;
            }
        }

        private static ABPathFilterList instance;
        public static ABPathFilterList Instance {
            get {
                if (instance == null) {
                    instance = AssetDatabase.LoadAssetAtPath<ABPathFilterList>(ConfigFilePath);
                    if (instance == null) {
                        instance = CreateInstance<ABPathFilterList>();
                    }
                }
                return instance;
            }
        }

        private const string configFileName = "pathfilter.asset";
        public static string ConfigFilePath {
            get {
                return System.IO.Path.Combine("Assets/FOFramework", configFileName);
            }
        }

        public static void SavePathFilter() {
            if (AssetDatabase.LoadAssetAtPath<ABPathFilterList>(ConfigFilePath)) {
                EditorUtility.SetDirty(Instance);
            } else {
                AssetDatabase.CreateAsset(Instance, ConfigFilePath);
            }
        }

        public void AppendEmpty() {
            ABPathFilter filter = new ABPathFilter();
            Filters.Add(filter);
        }

    }
}
