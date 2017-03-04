using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Assets.FOFramework.Editor.AssetBundle;

public class AssetBundlePackagePanel : EditorWindow {

    private ReorderableList reorderList;

    [MenuItem("Tools/AB Build Panel")]
    static void OpenPanel() {
        GetWindow<AssetBundlePackagePanel>("AB Build", true);
    }

    private void OnEnable() {
        if (reorderList == null)
            InitReorderableList();
    }

    private void InitReorderableList() {
        reorderList = new ReorderableList(ABPathFilterList.Instance.Filters, typeof(ABPathFilter));

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
        GUI.TextField(rect, filter.SimplyPath);
        GUI.enabled = true;

        rect.xMin = rect.xMax + GAP;
        rect.width = 50;
        if (GUI.Button(rect, "Select")) {
            filter.FullPath = SelectFolder();
        }

        rect.xMin = rect.xMax + GAP;
        rect.xMax = RIGHT_BORDER;
        filter.pattern = GUI.TextField(rect, filter.pattern);
    }
    private void DrawHeaderCallback(Rect rect) {
        GUI.Label(rect, "Asset Folder Config");
    }

    private string SelectFolder() {
        string path = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, string.Empty);
        path = path.Replace(Application.dataPath, "Assets");
        return path;
    }

    private void OnGUI() {
        reorderList.DoLayoutList();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Build", GUILayout.Width(60), GUILayout.Height(30))) {
            BuildAssetBundle();
        }
        GUILayout.EndHorizontal();
    }

    [MenuItem("Tools/AB Quick Build")]
    static void BuildAssetBundle() {
        List<ABPathFilter> filters = ABPathFilterList.Instance.Filters;
        foreach (ABPathFilter filter in filters) {

            if (!string.IsNullOrEmpty(filter.SimplyPath) && filter.enable) {
                Debug.Log(filter.FullPath);
                string[] files = Directory.GetFiles(filter.FullPath, filter.pattern);
                foreach (var file in files) {
                    Object obj = AssetDatabase.LoadMainAssetAtPath(file.Replace(Application.dataPath, "Assets"));
                    Object[] objlst = EditorUtility.CollectDependencies(new Object[] { obj });
                    foreach (var obj1 in objlst)
                        Debug.Log(obj1);
                }
            }

        }
    }
}
