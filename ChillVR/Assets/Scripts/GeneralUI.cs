using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GeneralUI : MonoBehaviour {

    public GameObject introText1;
    public GameObject introText2;
    public GameObject incomingCall;

    public GameObject callMessage;

    public GameObject scoreText;
    public AudioClip ringSFX;
    public AudioClip tickSFX;
    public float messingTime = 120;
    public float cleaningTime = 120;

    public GameObject timer1;
    public GameObject timer2;

    private float timeLeftSeconds;
    private int lastTimeSeconds;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartingPrompt());
        timeLeftSeconds = 0.0f;
        timer1.GetComponent<Text>().enabled = false;
        timer2.GetComponent<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateAndDisplayTimeLeft();
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
        StartCoroutine(CleaningTime());
        GameObject.Find("GameManager").GetComponent<GameManager>().StartCleaningPhase();
    }

    private IEnumerator CleaningTime()
    {
        timeLeftSeconds = cleaningTime;
        timer1.GetComponent<Text>().enabled = true;
        timer2.GetComponent<Text>().enabled = true;

        yield return new WaitForSeconds(cleaningTime);

        GameObject.Find("GameManager").GetComponent<GameManager>().ComputeFinalScore();
        GameObject.Find("Controller (left)").GetComponent<ControllerPickUp>().enabled = false;
        GameObject.Find("Controller (right)").GetComponent<ControllerPickUp>().enabled = false;
    }

    private void UpdateAndDisplayTimeLeft()
    {
        int timeMinutes = (int)(timeLeftSeconds / 60.0f);
        string minutesString = timeMinutes + "";

        int timeSeconds = ((int)timeLeftSeconds) % 60;
        string secondsString = timeSeconds + "";
        if (timeSeconds >= 0 && timeSeconds <= 9)
        {
            secondsString = "0" + secondsString;
        }
        if (timeSeconds < 6 && timeSeconds < lastTimeSeconds)
        {
            GetComponent<AudioSource>().PlayOneShot(tickSFX);
        }

        timer1.GetComponent<Text>().text = minutesString + ":" + secondsString;
        timer2.GetComponent<Text>().text = minutesString + ":" + secondsString;

        if (timeLeftSeconds > 0.0f)
        {
            timeLeftSeconds -= Time.deltaTime;
            lastTimeSeconds = timeSeconds;
        }
    }
}
