using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// PoseDataを管理するクラス
/// </summary>
public class PoseData
{
    public Transform rootTransform = null;

    /// <summary>
    /// PoseDataのコンストラクタ
    /// </summary>
    /// <param name="rootTransform">PoseDataを取得する親Transformデータ</param>
    public PoseData(Transform rootTransform)
    {
        this.rootTransform = rootTransform;
    }

    /// <summary>
    /// PoseDataのコンストラクタ
    /// </summary>
    /// <param name="dict">PoseDataとして読み込むデータ</param>
    public PoseData(Dictionary<string, object> dict)
    {
        fromDict(dict);
    }

    /// <summary>
    /// rootTransformを起点としたTransformデータを辞書データにして返す．
    /// </summary>
    /// <returns>PoseDataを記録した辞書データ</returns>
    public Dictionary<string, object> toDict()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        transformToDict(rootTransform, dict);
        return dict;
    }

    /// <summary>
    /// dictデータを読み込み，シーン中のTransformにセットする．
    /// </summary>
    /// <param name="dict">PoseDataとして読み込むデータ</param>
    public void fromDict(Dictionary<string, object> dict)
    {
        foreach (var transformData in dict)
        {
            GameObject transformObject = GameObject.Find(transformData.Key);

            var dataDict = transformData.Value as Dictionary<string, object>;

            Vector3 eulerAngles = ilistToVector3((IList)dataDict["eulerAngles"]);
            Vector3 position = ilistToVector3((IList)dataDict["position"]);
            Vector3 scale = ilistToVector3((IList)dataDict["scale"]);

            transformObject.transform.localEulerAngles = eulerAngles;
            transformObject.transform.localPosition = position;
            transformObject.transform.localScale = scale;
        }
    }

    private void transformToDict(Transform curentTransform, Dictionary<string, object> dict)
    {
        Dictionary<string, object> dataDict = new Dictionary<string, object>();
        Vector3 eulerAngles = curentTransform.localEulerAngles;

        vector3ToDict("eulerAngles", eulerAngles, dataDict);
        vector3ToDict("position", curentTransform.localPosition, dataDict);
        vector3ToDict("scale", curentTransform.localScale, dataDict);

        dict.Add(curentTransform.name, dataDict);

        foreach (Transform childTransform in curentTransform)
        {
            transformToDict(childTransform, dict);
        }
    }

    private void vector3ToDict(string name, Vector3 vec, Dictionary<string, object> dict)
    {
        List<float> vecData = new List<float>();
        vecData.Add(vec.x);
        vecData.Add(vec.y);
        vecData.Add(vec.z);
        dict.Add(name, vecData);
    }

    private Vector3 ilistToVector3(IList listValues)
    {
        var vecData = (IList)listValues;

        Vector3 vec = new Vector3();
        vec.x = System.Convert.ToSingle(vecData[0]);
        vec.y = System.Convert.ToSingle(vecData[1]);
        vec.z = System.Convert.ToSingle(vecData[2]);

        return vec;
    }
}
