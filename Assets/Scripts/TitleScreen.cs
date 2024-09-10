using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public TMP_Text titleText;
    public GameObject startButton;
    public Text startText;

    public int amount;


    private void Start()
    {
        StartCoroutine(TextAnimation(amount));
    }

    IEnumerator TextAnimation(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // Fade in the text
            titleText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOffset, -1.0f);
            titleText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowInner, 0.5f);

            // Fade out the text
            while (titleText.fontMaterial.GetFloat(ShaderUtilities.ID_GlowOffset) < 0.9f)
            {
                titleText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOffset, titleText.fontMaterial.GetFloat(ShaderUtilities.ID_GlowOffset) + 0.05f);
                yield return new WaitForSeconds(0.01f);
            }
            titleText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOffset, 1.0f);
            while (titleText.fontMaterial.GetFloat(ShaderUtilities.ID_GlowInner) > 0.0f)
            {
                titleText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowInner, titleText.fontMaterial.GetFloat(ShaderUtilities.ID_GlowInner) - 0.01f);
                yield return new WaitForSeconds(0.01f);
            }
            titleText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowInner, 0.0f);
            yield return new WaitForSeconds(0.5f);
        }

        // Make the title back to the correct color
        titleText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowInner, 0.0f);
        // fade in the text

        float t = 0;

        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime / 5f;
            titleText.fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, Color.Lerp(Color.black, Color.white, t));
        }


        // Fade in Button
        startButton.SetActive(true);
        startButton.GetComponent<Button>().interactable = false;
        startText.CrossFadeAlpha(0, 0, false);
        startText.CrossFadeAlpha(1, 2f, false);
        yield return new WaitForSeconds(2f);
        startButton.GetComponent<Button>().interactable = true;
        

    }

    public void StartGame()
    {
        GameManager.Instance.LoadLevel("Tutorial");
    }
}
