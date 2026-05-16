using UnityEngine;
using DG.Tweening;

public class Loop : MonoBehaviour
{
    void Start()
    {
        // Move the object to x=3 over 1 second, and then loop this movement back and forth 5 times
        // transform.DOMoveX(3, 1).SetLoops(5, LoopType.Yoyo);

        // Scale the object to twice its size over 1 second, and then loop this scaling back and forth indefinitely
        // transform.DOScale(new Vector3(2, 2, 2), 1).SetLoops(-1, LoopType.Yoyo);

        // Scale the object to twice its size over 1 second, and then loop this scaling back from the original size to the new size indefinitely
        // transform.DOScale(new Vector3(2, 2, 2), 1).SetLoops(-1, LoopType.Restart);

        // Scale the object to twice its size over 1 second, and then loop this scaling by incrementing the scale each time indefinitely
        transform.DOScale(new Vector3(2, 2, 2), 1).SetLoops(-1, LoopType.Incremental);
    }
}
