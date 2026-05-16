using UnityEngine;
using DG.Tweening;

public class SequenceDemo2 : MonoBehaviour
{
    void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveX(3, 1))
            .Join(transform.DOScale(new Vector3(2, 2, 2), 1))
            .Join(GetComponent<SpriteRenderer>().material.DOColor(Color.red, 1));
        //.Join(GetComponent<SpriteRenderer>().DOColor(Color.red, 1));
    }
}
