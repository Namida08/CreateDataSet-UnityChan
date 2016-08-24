using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

using MiniJSON;
using System.IO;

/// <summary>
/// PoseData保存用のGUI
/// </summary>
/// <remarks>
/// PoseDataが保存されるファイルパス: Assets/[dataDirName]/[dataName].json
/// </remarks>
public class PoseEditor : EditorWindow
{
    private string rootTransformName = "Character1_Reference";
    private string dataDirName = "PoseData";
    private string dataName = "pose";

    void OnGUI()
    {
        // rootTransformName
        GUILayout.BeginHorizontal();
        GUILayout.Label("Root Transform: ", GUILayout.Width(110));
        rootTransformName = (string)EditorGUILayout.TextField(rootTransformName);
        if (GUILayout.Button("select"))
        {
            selectTransform();
        }
        GUILayout.EndHorizontal();

        // dataDirName
        GUILayout.BeginHorizontal();
        GUILayout.Label("Data Directory: ", GUILayout.Width(110));
        dataDirName = (string)EditorGUILayout.TextField(dataDirName);
        GUILayout.EndHorizontal();

        // dataName
        GUILayout.BeginHorizontal();
        GUILayout.Label("Data Name: ", GUILayout.Width(110));
        dataName = (string)EditorGUILayout.TextField(dataName);
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("save pose"))
        {
            savePose();
        }

        if (GUILayout.Button("load pose"))
        {
            loadPose();
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// PoseDataが保存されるファイルパス
    /// </summary>
    /// <remarks>
    /// Assets/[dataDirName]/[dataName].json
    /// </remarks>
    private string poseFile()
    {
        string dataDirPath = Application.dataPath + "/" + dataDirName;
        if (!Directory.Exists(dataDirPath))
        {
            Directory.CreateDirectory(dataDirPath);
        }
        string filepath = dataDirPath + "/" + dataName + ".json";
        return filepath;
    }

    /// <summary>
    /// 現在選択されているオブジェクトをrootTransformNameに設定
    /// </summary>
    private void selectTransform()
    {
        if (Selection.activeGameObject != null)
        {
            rootTransformName = Selection.activeGameObject.name;
        }

    }

    /// <summary>
    /// PoseDataの保存
    /// </summary>
    private void savePose()
    {
        GameObject targetTransformObject = GameObject.Find(rootTransformName);

        PoseData poseData = new PoseData(targetTransformObject.transform);
        Dictionary<string, object> dict = poseData.toDict();

        string jsonData = Json.Serialize(dict);

        string filepath = poseFile();
        Debug.Log(filepath);
        Debug.Log(jsonData);

        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filepath))
        {
            file.Write(jsonData);
        }
    }

    /// <summary>
    /// PoseDataの読み込み
    /// </summary>
    private void loadPose()
    {
        string filepath = poseFile();

        string jsonData = "";

        using (System.IO.StreamReader file =
            new System.IO.StreamReader(filepath))
        {
            jsonData = file.ReadToEnd();
        }

        Dictionary<string, object> dict = Json.Deserialize(jsonData) as Dictionary<string, object>;

        PoseData poseData = new PoseData(dict);
    }
}
