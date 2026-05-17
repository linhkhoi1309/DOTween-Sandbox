using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    [Header("Fade In Settings")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeInDuration = 1f;

    [Header("Slide In Settings")]
    [SerializeField] private float slideInDuration = 1f;
    [SerializeField] private float delayBetweenButtons = 0.1f;

    [SerializeField] private float slideHorizontalOffset = -Screen.width;

    private List<RectTransform> buttonRectTransforms = new List<RectTransform>();
    private Sequence fadeAndSlideSequence;
    void Start()
    {
        Initialize();
        PlayAnimation();
    }

    private void Initialize()
    {
        Button[] buttons = canvasGroup.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            buttonRectTransforms.Add(button.GetComponent<RectTransform>());
        }
    }

    public void PlayAnimation()
    {
        fadeAndSlideSequence?.Kill();
        fadeAndSlideSequence = DOTween.Sequence();
        canvasGroup.alpha = 0;
        for (int i = 0; i < buttonRectTransforms.Count; i++)
        {
            RectTransform rectTransform = buttonRectTransforms[i];
            Vector2 targetPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(slideHorizontalOffset, targetPosition.y);
            fadeAndSlideSequence.Join(rectTransform.DOAnchorPos(targetPosition, slideInDuration).SetEase(Ease.OutBack).SetDelay(i * delayBetweenButtons));
        }
        fadeAndSlideSequence.Join(canvasGroup.DOFade(1, fadeInDuration).SetEase(Ease.OutQuart));
        fadeAndSlideSequence.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation();
        }
    }
    
    void OnDestroy()
    {
        fadeAndSlideSequence?.Kill();
    } 
}
