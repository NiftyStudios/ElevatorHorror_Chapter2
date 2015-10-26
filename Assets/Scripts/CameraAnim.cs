using UnityEngine;
using System.Collections;

public class CameraAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("e")){
			GetComponent<Animation>().Play("Phone");
		}
	}
}
