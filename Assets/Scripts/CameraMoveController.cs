using UnityEngine;
using System.Collections;

public class CameraMoveController : MonoBehaviour {

	Vector3 initPosition;
    public Vector3 randMinValue, randMaxValue;

	// Use this for initialization
	void Start () {
		initPosition = transform.position;
        Random.seed = 1;
    }
	
	// Update is called once per frame
	void Update () {
		float randX = Random.Range(randMinValue.x, randMaxValue.x);
		float randY = Random.Range(randMinValue.y, randMaxValue.y);
		float randZ = Random.Range(randMinValue.z, randMaxValue.z);
        transform.position = new Vector3(initPosition.x + randX, 
		                                 initPosition.y + randY, 
		                                 initPosition.z + randZ);

	}
}
