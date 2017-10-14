using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScore : MonoBehaviour {

    //needs to face player all the time

    private Text text;
    private Color startColor;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        startColor = text.color;
    }

    // Update is called once per frame
    void Update () {

        text.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0.0f), Time.time);
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        FacePlayer();
	}

    public void SetValue(int value)
    {
        text.text = value.ToString();
    }

    private void FacePlayer()
    {

    }
}
