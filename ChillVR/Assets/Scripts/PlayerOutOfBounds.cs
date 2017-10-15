using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutOfBounds : MonoBehaviour {

    public string playerBoundingVolumeName;
    public Vector3 resetSpawnLocation;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerBoundingVolumeName)
        {
            other.transform.position = resetSpawnLocation;
        }
    }


}
