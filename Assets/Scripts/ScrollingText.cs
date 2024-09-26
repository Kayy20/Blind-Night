using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    public float startingY, targetY, speed;
    public bool fadeOutWhenDone = false;

    public GameObject thankYouText;

    public TMP_Text buttonText;
    public Button button;

    private void Start()
    {
        // call a coroutine to scroll the
        transform.position = new Vector3(transform.position.x, startingY, transform.position.z);
        if (targetY == 0)
        {
            targetY = (Screen.height / 2) + (Screen.height / 4);
        }
        StartCoroutine(ScrollText());
    }

    private IEnumerator ScrollText()
    {
        // lerp text up with the speed in mind
        while (transform.position.y < targetY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            yield return null;
        }
        if (fadeOutWhenDone)
        {
            GetComponentInChildren<TMP_Text>().CrossFadeAlpha(0, 4f, false);
            thankYouText.SetActive(true);
            thankYouText.GetComponent<TMP_Text>().CrossFadeAlpha(0, 0, false);
            yield return new WaitForSeconds(4f);
            thankYouText.GetComponent<TMP_Text>().CrossFadeAlpha(1, 3f, false);
            yield return new WaitForSeconds(3f);
            button.gameObject.SetActive(true);
            buttonText.CrossFadeAlpha(0, 0, false);
            button.interactable = false;
            buttonText.CrossFadeAlpha(1, 3f, false);
            yield return new WaitForSeconds(3f);
            button.interactable = true;
        }
    }
}
