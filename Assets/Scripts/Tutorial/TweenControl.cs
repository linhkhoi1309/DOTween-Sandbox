using UnityEngine;
using DG.Tweening;

public class TweenControl : MonoBehaviour
{
    Tween tween;
    void Start()
    {
        tween = transform.DOMoveX(5, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tween.Pause();
            Debug.Log("Tween paused");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            tween.Play();
            Debug.Log("Tween played");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            tween.Kill();
            Debug.Log("Tween killed");
        }
    }
}
