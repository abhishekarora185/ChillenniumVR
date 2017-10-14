using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GenericObject : MonoBehaviour {

    public int baseScore = 100;
    public GameObject PopScorePrefab;

    private Vector3 originalPosition;
    public bool misplaced = false;

    public float minimumBangVelocity = 0.8f;

    public AudioClip bangSound;


	// Use this for initialization
	void Start () {

        originalPosition = transform.position;
	}

    private void Update()
    {
        if (!misplaced & IsOutOfPosition() & PopScorePrefab != null)
        {
            InstantiateScore(baseScore);

            misplaced = true;
        }
    }

    public float GetDistanceFromOriginalPosition()
    {
        float xDist = Mathf.Abs(transform.position.x - originalPosition.x);
        float yDist = Mathf.Abs(transform.position.y - originalPosition.y);
        float zDist = Mathf.Abs(transform.position.z - originalPosition.z);

        float returnValue = Mathf.Sqrt(Mathf.Pow(xDist, 2) + Mathf.Pow(yDist, 2) + Mathf.Pow(zDist, 2));

        return returnValue;
    }

    private bool IsOutOfPosition()
    {
        float currentDistanceFromOrigin = GetDistanceFromOriginalPosition();

        if (currentDistanceFromOrigin > 0.5)
        {
            return true;
        }

        else
            return false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > minimumBangVelocity & PopScorePrefab != null)
        {
            InstantiateScore(GetComponent<Rigidbody>().velocity.magnitude * 500);

            if (bangSound)
            {
                GetComponent<AudioSource>().PlayOneShot(bangSound);
            }
               

        }
    }


    private void InstantiateScore(float value)
    {
        GameObject newPopScore = Instantiate(PopScorePrefab);
        newPopScore.GetComponent<PopUpScore>().SetValue((int)value);
        newPopScore.transform.position = transform.position;
    }
    

}
