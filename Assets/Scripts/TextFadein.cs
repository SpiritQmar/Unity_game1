using System.Collections;
using UnityEngine;
using TMPro;

public class TextFadein : MonoBehaviour
{
    public TextMeshProUGUI loreText;
    public float lineDelay = 1f;
    public float wordDelay = 0.2f;
    public float fadeDuration = 0.5f;
    [TextArea] public string fullText = "Это первая строка.\nЭто вторая строка.\nЭто третья строка.";

    private void Start()
    {
        loreText.text = "";
        StartCoroutine(ShowTextLineByLine());
    }

    IEnumerator ShowTextLineByLine()
    {
        string[] lines = fullText.Split('\n');

        foreach (string line in lines)
        {
            yield return StartCoroutine(ShowWordsWithFade(line));
            yield return new WaitForSeconds(lineDelay);
        }
    }

    IEnumerator ShowWordsWithFade(string line)
    {
        string[] words = line.Split(' ')

        foreach (string word in words)
        {
            loreText.text += word + " ";
            yield return StartCoroutine(FadeInLastWord());
            yield return new WaitForSeconds(wordDelay);
        }

        loreText.text += "\n";
    }

    IEnumerator FadeInLastWord()
    {
        TMP_TextInfo textInfo = loreText.textInfo;
        loreText.ForceMeshUpdate();

        int lastWordStartIndex = loreText.text.LastIndexOf(" ") + 1;
        int wordLength = loreText.text.Length - lastWordStartIndex;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            for (int i = lastWordStartIndex; i < lastWordStartIndex + wordLength; i++)
            {
                if (i < textInfo.characterCount)
                {
                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                    Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

                    byte newAlpha = (byte)Mathf.Lerp(0, 255, alpha);
                    vertexColors[vertexIndex + 0].a = newAlpha;
                    vertexColors[vertexIndex + 1].a = newAlpha;
                    vertexColors[vertexIndex + 2].a = newAlpha;
                    vertexColors[vertexIndex + 3].a = newAlpha;
                }
            }

            loreText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            yield return null;
        }
    }
}
