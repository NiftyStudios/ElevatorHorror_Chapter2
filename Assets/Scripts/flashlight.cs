using UnityEngine;
using System.Collections;

public class flashlight : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (GetComponent<Light>().enabled == true)
			{
				GetComponent<Light>().enabled = false;
				Debug.Log("light is now false");
			}
			else if(GetComponent<Light>().enabled == false)
			{
				GetComponent<Light>().enabled = true;
				Debug.Log("light is now true");
			}
		}
		
	}
	
}