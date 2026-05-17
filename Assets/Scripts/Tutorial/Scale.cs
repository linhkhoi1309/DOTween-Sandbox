using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(new Vector3(2, 2, 2), 1).SetDelay(1).SetEase(Ease.OutBounce);
    }
}
