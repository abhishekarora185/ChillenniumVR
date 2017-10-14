using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GeneralUI : MonoBehaviour {

    public GameObject introText1;
    public GameObject introText2;
    public GameObject incomingCall;

    public GameObject callMessage;

    public GameObject scoreText;
    public AudioClip ringSFX;
    public float messingTime = 120;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartingPrompt());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private IEnumerator StartingPrompt()
    {
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < 7; ++i)
        {
            introText2.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            introText2.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        introText1.SetActive(false);
        introText2.SetActive(false);

        scoreText.SetActive(true);


        yield return new WaitForSeconds(messingTime);

        introText1.SetActive(false);
        introText2.SetActive(false);
        scoreText.SetActive(false);
        incomingCall.SetActive(true);

        GetComponent<AudioSource>().PlayOneShot(ringSFX);

        StartCoroutine(RingRoutine());

        for (int i = 0; i < 10; ++i)
        {
            incomingCall.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            incomingCall.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }


    }

    private IEnumerator RingRoutine()
    {
        for (int i = 0; i < 5; ++i)
        {
            GetComponent<AudioSource>().PlayOneShot(ringSFX);
            yield return new WaitForSeconds(3.0f);
        }

        introText1.SetActive(false);
        introText2.SetActive(false);
        scoreText.SetActive(false);
        incomingCall.SetActive(false);
        callMessage.SetActive(true);

        yield return new WaitForSeconds(7.0f);

        callMessage.SetActive(false);

        scoreText.SetActive(true);

        //initiate cleaning phase of the game here-----------------------------
    }
}
