using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
    void Start()
    {
        // Move the GameObject to x=5 over 2 seconds
        // transform.DOMoveX(5, 2);

        // Move the GameObject to (5, 0, 0) over 2 seconds (same as above but explicitly setting y and z)
        // transform.DOMove(new Vector3(5, 0, 0), 2);

        // Move the GameObject to its current position from (5, 0, 0) over 2 seconds
        // transform.DOMove(new Vector3(5, 0, 0), 2).From();

        // Move the GameObject to x=5 over 2 seconds, then move it to y=3 over 1 second
        // transform.DOMoveX(5, 2).OnComplete(() =>
        // {
        //     transform.DOMoveY(3, 1);
        // });

        // Move the GameObject to x=5 over 2 seconds with an ease-in-out quad easing
        // Remarks: Ease.InOutQuad starts slow, speeds up in the middle, and slows down again at the end.
        // transform.DOMoveX(5, 2).SetEase(Ease.InOutQuad);

        // Move the GameObject to x=5 over 2 seconds with a linear easing
        // Remarks: Ease.Linear moves at a constant speed from start to finish.
        // transform.DOMoveX(5, 2).SetEase(Ease.Linear);

        // Move the GameObject to x=5 over 2 seconds with an ease-in sine easing
        // Remarks: Ease.InSine starts slow and accelerates.
        // transform.DOMoveX(5, 2).SetEase(Ease.InSine);
        // transform.DOMoveX(5, 2).SetEase(Ease.InSine);

        // Move the GameObject to x=5 over 2 seconds with an ease-out bounce easing
        // Remarks: Ease.OutBounce starts fast and bounces at the end.
        // transform.DOMoveX(5, 2).SetEase(Ease.OutBounce);

        // Move the GameObject to x=5 over 2 seconds with an ease-in-out quad easing, and delay the start by 1 second
        // Remarks: SetDelay() delays the start of the tween by the specified time.
        transform.DOMoveX(5, 2).SetDelay(1).SetEase(Ease.InOutQuad);
    }
}
