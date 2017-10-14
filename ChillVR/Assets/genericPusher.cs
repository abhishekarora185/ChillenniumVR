using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is used for proof of concept only

public class genericPusher : MonoBehaviour {

    public Vector2 direction;
    public float force;

	// Use this for initialization
	void Start () {

        GetComponent<Rigidbody>().AddForce(direction * force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
