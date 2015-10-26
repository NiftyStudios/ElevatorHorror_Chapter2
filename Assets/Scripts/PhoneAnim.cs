using UnityEngine;
using System.Collections;

public class PhoneAnim : MonoBehaviour {

    private Animator _phoneAnim;
    
	// Use this for initialization
	void Start () {
        _phoneAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
   
        if (Input.GetButtonDown("Fire1"))
        {
            _phoneAnim.SetBool("PhoneUse", true);
       
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            _phoneAnim.SetBool("PhoneUse", false);


        }

    }

}
