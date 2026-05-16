# DOTween Sandbox notes

## Move

Based on `Assets/Scripts/Move.cs`.

To move an object with DOTween, import DOTween and call a movement tween on the object's transform.

```csharp
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveX(5, 2);
    }
}
```

`DOMoveX(5, 2)` moves the GameObject to world position `x = 5` over `2` seconds. The `y` and `z` positions stay unchanged.

You can also move to a full position:

```csharp
transform.DOMove(new Vector3(5, 0, 0), 2);
```

This moves the object to `(5, 0, 0)` over `2` seconds.

Use `.From()` when you want the object to start from a value and animate back to its current position:

```csharp
transform.DOMove(new Vector3(5, 0, 0), 2).From();
```

This places the object at `(5, 0, 0)` first, then moves it back to wherever it was before the tween started.

To run another tween after the first one finishes, use `OnComplete`:

```csharp
transform.DOMoveX(5, 2).OnComplete(() =>
{
    transform.DOMoveY(3, 1);
});
```

This moves to `x = 5` over `2` seconds, then moves to `y = 3` over `1` second.

Tweens can be customized with easing:

```csharp
transform.DOMoveX(5, 2).SetEase(Ease.InOutQuad);
transform.DOMoveX(5, 2).SetEase(Ease.Linear);
transform.DOMoveX(5, 2).SetEase(Ease.InSine);
transform.DOMoveX(5, 2).SetEase(Ease.OutBounce);
```

- `Ease.InOutQuad` starts slow, speeds up in the middle, and slows down at the end.
- `Ease.Linear` moves at a constant speed.
- `Ease.InSine` starts slow and accelerates.
- `Ease.OutBounce` starts fast and bounces at the end.

Use `SetDelay` to wait before the tween starts:

```csharp
transform.DOMoveX(5, 2).SetDelay(1).SetEase(Ease.InOutQuad);
```

This waits `1` second, then moves to `x = 5` over `2` seconds with `Ease.InOutQuad`.

## Rotate

Based on `Assets/Scripts/Rotate.cs`.

Use `DORotate` to rotate a GameObject's transform.

```csharp
using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 180), 1)
            .SetDelay(1)
            .SetEase(Ease.OutBounce);
    }
}
```

`DORotate(new Vector3(0, 0, 180), 1)` rotates the object to `(0, 0, 180)` degrees over `1` second.

For a 2D sprite, the `z` value is usually the most important rotation axis, because it spins the sprite on the screen. In this example, the object rotates halfway around on the `z` axis.

The chained methods customize when and how the rotation happens:

- `SetDelay(1)` waits `1` second before starting.
- `SetEase(Ease.OutBounce)` makes the rotation overshoot and bounce into the final value.

## Scale

Based on `Assets/Scripts/Scale.cs`.

Use `DOScale` to change an object's scale over time.

```csharp
using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(new Vector3(2, 2, 2), 1)
            .SetDelay(1)
            .SetEase(Ease.OutBounce);
    }
}
```

`DOScale(new Vector3(2, 2, 2), 1)` scales the object to twice its normal size on the `x`, `y`, and `z` axes over `1` second.

For uniform scaling, you can also pass a single number:

```csharp
transform.DOScale(2, 1);
```

This is a shorter way to scale all axes to `2` over `1` second.

Like the move and rotate examples, scale tweens can use `SetDelay` and `SetEase`:

- `SetDelay(1)` waits before scaling.
- `Ease.OutBounce` makes the object bounce into its final size.

## Sequence

Based on `Assets/Scripts/SequenceDemo1.cs` and `Assets/Scripts/SequenceDemo2.cs`.

A `Sequence` lets you combine multiple tweens into one timeline. This is useful when you want tweens to happen in a specific order or at the same time.

First, create a sequence:

```csharp
Sequence seq = DOTween.Sequence();
```

### Running tweens one after another

`Append` adds a tween to the end of the sequence.

```csharp
using DG.Tweening;
using UnityEngine;

public class SequenceDemo1 : MonoBehaviour
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
```

This sequence does four actions in order:

1. Move to `x = 3` over `1` second.
2. Move to `y = 3` over `1` second.
3. Rotate to `(0, 0, 180)` over `1` second.
4. Scale to `2` over `1` second.

Because each tween is added with `Append`, the next tween waits for the previous tween to finish.

### Running tweens at the same time

`Join` adds a tween that plays at the same time as the previous tween in the sequence.

```csharp
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
    }
}
```

This sequence starts by moving to `x = 3` over `1` second. During that same `1` second, the object also scales to `(2, 2, 2)` and changes color to red.

Use this pattern when several animation changes should feel like one combined action.

## Loop

## Miscellaneous
### Difference between SpriteRenderer.material.DOColor and SpriteRenderer.DOColor

- SpriteRenderer.material.DOColor tweens the material's _Color property. Accessing .material creates a unique material instance for this renderer, which may increase memory usage and reduce batching efficiency if used often.
        
- SpriteRenderer.DOColor tweens SpriteRenderer.color directly without creating a new material instance, making it more lightweight and usually preferable for standard sprite color/fade effects.

## References

1. [Easing Function Cheat Sheet](https://easings.net/)
