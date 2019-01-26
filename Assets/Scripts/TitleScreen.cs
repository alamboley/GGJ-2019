using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Image title;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        title.color = Color.clear;
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 8; i++)
        {
            title.color = Color.white;
            yield return null;
            title.color = Color.clear;
            yield return null;
        }

        title.color = Color.white;
        yield return new WaitForSeconds(1f);

        for (float f = 0f; f < 1f; f += Time.deltaTime)
        {
            title.color = Color.Lerp(Color.white, Color.clear, f / 1f);
            yield return null;
        }

        title.color = Color.clear;
    }
}
