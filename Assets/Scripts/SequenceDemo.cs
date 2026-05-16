using DG.Tweening;
using UnityEngine;

public class SequenceDemo : MonoBehaviour
{
    void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveX(3, 1));
        seq.Append(transform.DOMoveY(3, 1));
        seq.Append(transform.DORotate(new Vector3(0, 0, 180), 1));
        seq.Append(transform.DOScale(2, 1));
    }
}
