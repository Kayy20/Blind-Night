using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    public TMP_Text titleText;

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

    }
}
