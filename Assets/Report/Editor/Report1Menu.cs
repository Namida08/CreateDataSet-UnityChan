using UnityEngine;
using UnityEditor;

/// <summary>
/// レポート課題用のメニュー
/// </summary>
public class Report1Menu : MonoBehaviour
{
    [MenuItem("Report1/PoseEditor")]
    public static void showEditor()
    {
        EditorWindow.GetWindow(typeof(PoseEditor));
    }
}
