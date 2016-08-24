using UnityEngine;
using UnityEditor; // 追加
using System.Collections;
using System.Linq;

public class FeaturePointEditor{

    [MenuItem("FeaturePointEditor/UpdateShowNumber")]
    static void ShowNumberUpdate()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("FeaturePoint")
            .OrderBy(t => t.transform.GetSiblingIndex())
            .ToArray();
        int count = 0;
        foreach (var point in points){
            point.name = string.Format("FeaturePoint ({0})", count);
            point.GetComponent<FeaturePoint>().SetNumber(count);
            count++;
        }
    }


    [MenuItem("FeaturePointEditor/add")]
    static void Add()
    {
        Debug.Log("mi zissou");
    }


}
