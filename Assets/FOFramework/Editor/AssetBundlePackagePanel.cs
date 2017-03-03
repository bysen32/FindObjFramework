using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Assets.FOFramework.Editor.AssetBundle;

public class AssetBundlePackagePanel : EditorWindow {

    private ABPathFilterList pathFilters;
    private ReorderableList reorderList;

    [MenuItem("Tools/AB Package Setting")]
    static void OpenPanel() {
        GetWindow<AssetBundlePackagePanel>("AB Package", true);
    }

    private void OnEnable() {
        pathFilters = new ABPathFilterList();
        reorderList = new ReorderableList(pathFilters.Filters, typeof(List<string>));

        reorderList.draggable = true;

        reorderList.onAddCallback = (ReorderableList list) => {
            pathFilters.AppendEmpty();
            Repaint();
        };

        reorderList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            const int GAP = 5;
            float RIGHT_BORDER = rect.xMax;
            ABPathFilter filter = pathFilters.Filters[index];

            rect.y += 1;
            rect.width = 16;
            rect.height = 18;

            filter.enable = GUI.Toggle(rect, filter.enable, GUIContent.none);

            rect.xMin = rect.xMax + GAP;
            rect.xMax = RIGHT_BORDER - 300;
            GUI.enabled = false;
            filter.path = GUI.TextField(rect, filter.path);
            GUI.enabled = true;

            rect.xMin = rect.xMax + GAP;
            rect.width = 50;
            if (GUI.Button(rect, "Select")) {
                Debug.Log("On Select Button");
            }

            rect.xMin = rect.xMax + GAP;
            rect.xMax = RIGHT_BORDER;
            filter.filter = GUI.TextField(rect, filter.filter);
        };

        reorderList.drawHeaderCallback = (Rect rect) => {
            GUI.Label(rect, "Path Config");
        };
    }

    static void BuildAssetBundle() {

    }

    private void OnGUI() {
        reorderList.DoLayoutList();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("build", GUILayout.Width(50))) {
            BuildAssetBundle();
        }
        GUILayout.EndHorizontal();

    }
}
