using UnityEngine;
using System.Collections;
using Common;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Linq;

public class CaptureScreenshot : MonoBehaviour{
	
	public Transform target;
    public Vector3 targetVector;
    public int SUPER_SIZE = 3;
    public int number = 1000;
    private int count;
    private GameObject[] points;

    void Start(){
		count = 0;
        points = GameObject.FindGameObjectsWithTag("FeaturePoint")
            .OrderBy(t => t.GetComponent<FeaturePoint>().GetNumber())
            .ToArray();
    }
	
	void LateUpdate () {
        //NOTE: 0回目はなぜかスクショされない
        if (count > 0 && count <= number) {
            transform.LookAt(targetVector);
            Capture(count - 1);
            SaveFeaturePoint(count - 1);
            Debug.Log("Save: " + (count - 1));
        }
        count++;
    }

    //画面をスクショ
    [UnityEditor.MenuItem("Edit/CaptureScreenshot")]
	private void Capture(int i){
		Application.CaptureScreenshot(Define.SAVE_DIR + GetFileName(i) + ".png", SUPER_SIZE);
	}
	
	//特徴点保存
	private void SaveFeaturePoint (int i){
		string out_file = Define.SAVE_DIR + GetFileName(i);
		using (StreamWriter writer = new StreamWriter(out_file, false, Encoding.GetEncoding("utf-8"))) {
			writer.WriteLine (GetFileName(i) + ".png");
			foreach (var point in points) {
				writer.WriteLine (GetScreenPoint (point.transform.position));
			}
		}
	}
	
	private string GetScreenPoint(Vector3 point){
		Vector3 s = Camera.main.WorldToScreenPoint (point);
		return Math.Round(s.x) * SUPER_SIZE + " " + (Screen.height - Math.Round(s.y)) * SUPER_SIZE;
	}

	private string GetFileName(int i){
		return "image_" + i;
	}

}
