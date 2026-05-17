using UnityEngine;
using DG.Tweening;
public class UIController2 : MonoBehaviour
{
    public RectTransform [] buttons;
    public float slideDuration = 0.6f;
    public float delayBetweenButtons = 0.1f;

    private float slideHorizontalOffset = -500f;
    void Start()
    {
        PlaySlideInAnimation();
    }

    void PlaySlideInAnimation()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            RectTransform button = buttons[i];
            Vector2 targetPos = button.anchoredPosition;
            button.anchoredPosition = new Vector2(slideHorizontalOffset, targetPos.y);
            button.DOAnchorPos(targetPos, slideDuration).SetDelay(i * delayBetweenButtons).SetEase(Ease.OutBack);
        }
    }
}
