using System.Collections;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    public CanvasGroup group;
    
    public void Show()
    {
        gameObject.SetActive(true);
        group.alpha = 1f;
    }

    public void Hide() => StartCoroutine(Fade());

    private IEnumerator Fade()
    {
        while (group.alpha>0f)
        {
            group.alpha -= 0.03f;
            yield return new WaitForSeconds(0.03f);
        }
        gameObject.SetActive(false);
    }
}
