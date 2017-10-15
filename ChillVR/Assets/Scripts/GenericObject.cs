using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GenericObjectWeightClass
{
    WEIGHT_LIGHT = 1,
    WEIGHT_MEDIUM = 5,
    WEIGHT_HEAVY = 10
}

[RequireComponent(typeof(AudioSource))]
public class GenericObject : MonoBehaviour {

    public int baseScore = 100;
    public GameObject PopScorePrefab;

    private Vector3 originalPosition;
    private Vector3 originalOrientation;
    public bool misplaced = false;

    public float minimumBangVelocity = 0.8f;

    public AudioClip bangSound;
    private GameManager gameManager;
    
    private Material cleaningPhaseInPostionMaterial;
    private Material cleaningPhaseOutOfPositionMaterial;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            SetMaterial();
            SetMaterialsForCleaningPhase();
        }
        originalPosition = transform.position;
        originalOrientation = transform.rotation.eulerAngles;
	}

    private void Update()
    {
        if ((!gameManager || !gameManager.isCleaningPhase) && !misplaced & IsOutOfPosition() & PopScorePrefab != null)
        {
            InstantiateScore(baseScore);

            misplaced = true;
        }

        if (gameManager && gameManager.isCleaningPhase)
        {
            ChangeMaterialToConveyDistanceFromOriginInCleaningMode();
        }

        if (gameManager && gameManager.isGameOver)
        {
            RemoveFixedJointIfPresent();
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

    public bool IsDisoriented()
    {
        Vector3 currentOrientation = transform.rotation.eulerAngles;

        float xOrientationDifference = Mathf.Abs(currentOrientation.x - originalOrientation.x);
        float zOrientationDifference = Mathf.Abs(currentOrientation.z - originalOrientation.z);
        float totalDifference = xOrientationDifference + zOrientationDifference;

        return (totalDifference > gameManager.AllowedDifferenceInOrientationDegrees);
    }

    public bool IsOutOfPosition()
    {
        float currentDistanceFromOrigin = GetDistanceFromOriginalPosition();

        if (!gameManager)
            return false;

        if ( currentDistanceFromOrigin > gameManager.AllowedDistanceFromOrigin)
        {
            return true;
        }

        else
            return false;

    }

    private void RemoveFixedJointIfPresent()
    {
        FixedJoint attachedJoint = this.gameObject.GetComponent<FixedJoint>();

        if (attachedJoint != null)
        {
            DestroyImmediate(attachedJoint);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((!gameManager || !gameManager.isCleaningPhase) && GetComponent<Rigidbody>().velocity.magnitude > minimumBangVelocity & PopScorePrefab != null)
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
        Debug.Log(gameObject.name);
        newPopScore.GetComponent<PopUpScore>().SetValue((int)value);
        newPopScore.transform.position = transform.position;
    }

    private void SetMaterial()
    {
        int weight = (int)this.gameObject.GetComponent<Rigidbody>().mass;

        if (weight <= (int)GenericObjectWeightClass.WEIGHT_LIGHT)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = gameManager.LightObjectMaterial;
        }
        else if (weight <= (int)GenericObjectWeightClass.WEIGHT_MEDIUM)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = gameManager.MediumObjectMaterial;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = gameManager.HeavyObjectMaterial;
        }
    }

    private void SetMaterialsForCleaningPhase()
    {
        int weight = (int)this.gameObject.GetComponent<Rigidbody>().mass;

        if (weight <= (int)GenericObjectWeightClass.WEIGHT_LIGHT)
        {
            cleaningPhaseInPostionMaterial = gameManager.InPositionLightMaterialCleaningPhase;
            cleaningPhaseOutOfPositionMaterial = gameManager.OutOfPositionLightMaterialCleaningPhase;
        }
        else if (weight <= (int)GenericObjectWeightClass.WEIGHT_MEDIUM)
        {
            cleaningPhaseInPostionMaterial = gameManager.InPositionMediumMaterialCleaningPhase;
            cleaningPhaseOutOfPositionMaterial = gameManager.OutOfPositionMediumMaterialCleaningPhase;
        }
        else
        {
            cleaningPhaseInPostionMaterial = gameManager.InPositionHeavyMaterialCleaningPhase;
            cleaningPhaseOutOfPositionMaterial = gameManager.OutOfPositionHeavyMaterialCleaningPhase;
        }
    }

    private void ChangeMaterialToConveyDistanceFromOriginInCleaningMode()
    {
        if (gameManager.isCleaningPhase)
        {
            if (this.IsOutOfPosition() || this.IsDisoriented())
            {
                this.GetComponent<MeshRenderer>().material = cleaningPhaseOutOfPositionMaterial;
            }
            else
            {
                this.GetComponent<MeshRenderer>().material = cleaningPhaseInPostionMaterial;
            }
        }
    }
    
}
