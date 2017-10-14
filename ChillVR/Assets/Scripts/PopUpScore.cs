using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScore : MonoBehaviour {

    //needs to face player all the time

    public Text text;

    private Color startColor;

    private float lerpValue = 0.0f;

    private void Start()
    {
        startColor = text.color;
        StartCoroutine(SelfDestructor());
    }

    // Update is called once per frame
    void Update () {

        text.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0.0f), lerpValue);
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        FacePlayer();
        lerpValue += Time.deltaTime;
    }

    public void SetValue(int value)
    {
        text.text = value.ToString();

        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(value);
    }

    private void FacePlayer()
    {

    }

    private IEnumerator SelfDestructor()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
