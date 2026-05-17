using DG.Tweening;
using UnityEngine;

public class UIController3 : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    
    void Start()
    {
        canvasGroup.DOFade(1f, fadeDuration);
    }
}
