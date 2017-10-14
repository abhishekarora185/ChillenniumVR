using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObject : MonoBehaviour {

    public int baseScore = 100;
    public GameObject PopScorePrefab;

    private Vector3 originalPosition;
    private bool misplaced = false;

	// Use this for initialization
	void Start () {

        
	}

    private void Update()
    {
        //if (!misplaced & IsOutOfPosition() & PopScorePrefab != null)
        //{
        //    GameObject newPopScore = Instantiate(PopScorePrefab);
        //    newPopScore.GetComponent<PopUpScore>().SetValue(baseScore);
        //    newPopScore.transform.position = transform.position;

        //    misplaced = true;
        //}
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
}
