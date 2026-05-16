using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 180), 1).SetDelay(1).SetEase(Ease.OutBounce);
    }
}
