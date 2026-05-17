using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnExitButtonClicked()
    {
        PlayExitButtonAnimation();
    }

    private void OnSettingsButtonClicked()
    {
        PlaySettingsButtonAnimation();
    }

    private void OnPlayButtonClicked()
    {
        PlayStartButtonAnimation();
    }

    void PlayStartButtonAnimation()
    {
        playButton.transform.DOScale(1.2f, 0.2f)
        .SetEase(Ease.OutBounce).OnComplete(() =>
        {
            playButton.transform.DOScale(1f, 0.2f);
        });
    }

    void PlaySettingsButtonAnimation()
    {
        settingsButton.transform.DORotate(new Vector3(0,0, 30), 0.2f, RotateMode.Fast)
        .SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    void PlayExitButtonAnimation()
    {
        exitButton.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 10, 1);
    }

    void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }
}
