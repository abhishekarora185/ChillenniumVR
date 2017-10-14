using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampShadeBehavior : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().useGravity = true;
        transform.parent = null;
    }
}
