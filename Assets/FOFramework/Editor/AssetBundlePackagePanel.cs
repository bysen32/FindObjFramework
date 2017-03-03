using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Assets.FOFramework.Editor.AssetBundle;

public class AssetBundlePackagePanel : EditorWindow {

    private ReorderableList reorderList;

    [MenuItem("Tools/AB Package Setting")]
    static void OpenPanel() {
        GetWindow<AssetBundlePackagePanel>("AB Package", true);
    }

    private void OnEnable() {
        if (reorderList == null)
            InitReorderableList();
    }

    private void InitReorderableList() {
        reorderList = new ReorderableList(ABPathFilterList.Instance.Filters, typeof(ABPathFilter));

        reorderList.onAddCallback = (ReorderableList list) => {
            ABPathFilterList.Instance.AppendEmpty();
            Repaint();
        };
        reorderList.drawElementCallback = DrawElementCallback;
        reorderList.drawHeaderCallback = DrawHeaderCallback;
    }
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused) {
        const int GAP = 5;
        float RIGHT_BORDER = rect.xMax;
        ABPathFilter filter = ABPathFilterList.Instance.Filters[index];

        rect.y += 1;
        rect.width = 16;
        rect.height = 18;

        filter.enable = GUI.Toggle(rect, filter.enable, GUIContent.none);

        rect.xMin = rect.xMax + GAP;
        rect.xMax = RIGHT_BORDER - 300;
        GUI.enabled = false;
        GUI.TextField(rect, filter.path);
        GUI.enabled = true;

        rect.xMin = rect.xMax + GAP;
        rect.width = 50;
        if (GUI.Button(rect, "Select")) {
            filter.path = SelectFolder();
        }

        rect.xMin = rect.xMax + GAP;
        rect.xMax = RIGHT_BORDER;
        filter.filter = GUI.TextField(rect, filter.filter);
    }
    private void DrawHeaderCallback(Rect rect) {
        GUI.Label(rect, "Asset Folder Config");
    }

    private string SelectFolder() {
        string path = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, string.Empty);
        path = path.Replace(Application.dataPath, "Assets");
        return path;
    }

    static void BuildAssetBundle() {

    }
    static void SavePathFilters() {
        ABPathFilterList.SavePathFilter();
    }

    private void OnGUI() {
        reorderList.DoLayoutList();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save", GUILayout.Width(50))) {
            SavePathFilters();
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Build", GUILayout.Width(50))) {
            BuildAssetBundle();
        }
        GUILayout.EndHorizontal();

    }
}
