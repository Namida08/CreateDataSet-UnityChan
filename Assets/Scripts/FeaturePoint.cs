using UnityEngine;
using System.Collections;

public class FeaturePoint : MonoBehaviour {

    public int number;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().enabled = false;
		foreach (Transform child in transform) 
		{ 
			child.gameObject.GetComponent<Renderer>().enabled = false; 
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int GetNumber() {
        return number;
    }

    public void SetNumber(int n)
    {
        number = n;
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<TextMesh>().text = number.ToString();
        }

    }

}
