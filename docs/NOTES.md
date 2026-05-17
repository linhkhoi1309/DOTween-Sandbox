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

Based on `Assets/Scripts/Loop.cs`.

Use `SetLoops` when you want a tween to repeat.

```csharp
transform.DOMoveX(3, 1).SetLoops(5, LoopType.Yoyo);
```

`SetLoops(5, LoopType.Yoyo)` repeats the tween `5` times. The `Yoyo` loop type makes the tween go forward, then backward, then forward again.

In this example, the object moves to `x = 3` over `1` second, then moves back toward its starting position, repeating that back-and-forth motion until the loop count is finished.

Use `-1` for infinite loops:

```csharp
transform.DOScale(new Vector3(2, 2, 2), 1)
    .SetLoops(-1, LoopType.Yoyo);
```

This scales the object to `(2, 2, 2)` over `1` second, then scales it back down, repeating forever.

### Loop types

`LoopType.Yoyo` plays forward, then backward:

```csharp
transform.DOScale(new Vector3(2, 2, 2), 1)
    .SetLoops(-1, LoopType.Yoyo);
```

This is useful for pulsing animations, such as an object growing and shrinking.

`LoopType.Restart` plays forward, jumps back to the start value, then plays forward again:

```csharp
transform.DOScale(new Vector3(2, 2, 2), 1)
    .SetLoops(-1, LoopType.Restart);
```

This repeatedly scales from the original size to `(2, 2, 2)`. Each loop restarts from the original scale instead of reversing smoothly.

`LoopType.Incremental` adds the tween's change each time it loops:

```csharp
transform.DOScale(new Vector3(2, 2, 2), 1)
    .SetLoops(-1, LoopType.Incremental);
```

This scales the object larger each loop. If the object starts at scale `(1, 1, 1)`, the first loop scales toward `(2, 2, 2)`, then the next loop continues increasing from there.

Be careful with infinite incremental loops, because the value keeps growing until the tween is stopped.

## Miscellaneous
### Difference between SpriteRenderer.material.DOColor and SpriteRenderer.DOColor

- SpriteRenderer.material.DOColor tweens the material's _Color property. Accessing .material creates a unique material instance for this renderer, which may increase memory usage and reduce batching efficiency if used often.
        
- SpriteRenderer.DOColor tweens SpriteRenderer.color directly without creating a new material instance, making it more lightweight and usually preferable for standard sprite color/fade effects.

## Tween Controls

Based on `Assets/Scripts/TweenControl.cs`.

Store a tween in a `Tween` variable when you want to control it after it has been created.

```csharp
using UnityEngine;
using DG.Tweening;

public class TweenControl : MonoBehaviour
{
    Tween tween;

    void Start()
    {
        tween = transform.DOMoveX(5, 2)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.OutSine);
    }
}
```

`transform.DOMoveX(5, 2)` creates the tween. Assigning it to `tween` lets other methods pause, resume, or stop that same tween later.

In this example, the tween moves the object to `x = 5` over `2` seconds, loops forever with `LoopType.Yoyo`, and uses `Ease.OutSine`.

### Pause and play

Use `Pause` to stop a tween without destroying it:

```csharp
if (Input.GetKeyDown(KeyCode.Space))
{
    tween.Pause();
    Debug.Log("Tween paused");
}
```

Pressing `Space` pauses the tween at its current position.

Use `Play` to resume a paused tween:

```csharp
if (Input.GetKeyDown(KeyCode.Q))
{
    tween.Play();
    Debug.Log("Tween played");
}
```

Pressing `Q` continues the tween from where it was paused.

### Kill

Use `Kill` to stop and destroy a tween:

```csharp
if (Input.GetKeyDown(KeyCode.E))
{
    tween.Kill();
    Debug.Log("Tween killed");
}
```

Pressing `E` kills the tween. After a tween is killed, it cannot be resumed with `Play`; you need to create a new tween if you want the animation to run again.

### Full example

```csharp
using UnityEngine;
using DG.Tweening;

public class TweenControl : MonoBehaviour
{
    Tween tween;

    void Start()
    {
        tween = transform.DOMoveX(5, 2)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.OutSine);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tween.Pause();
            Debug.Log("Tween paused");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            tween.Play();
            Debug.Log("Tween played");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            tween.Kill();
            Debug.Log("Tween killed");
        }
    }
}
```

## UI Animation Examples

Based on `Assets/Scripts/UIController1.cs`, `Assets/Scripts/UIController2.cs`, and `Assets/Scripts/UIController3.cs`.

### UIController1: button click animations

`UIController1` connects Unity UI `Button` clicks to DOTween animations.

```csharp
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIController1 : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button exitButton;
}
```

The three button fields are marked with `[SerializeField]`, so they can be assigned from the Unity Inspector while still staying `private` in code.

### Adding button listeners

Use `onClick.AddListener` to connect each button to a method.

```csharp
void Start()
{
    playButton.onClick.AddListener(OnPlayButtonClicked);
    settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    exitButton.onClick.AddListener(OnExitButtonClicked);
}
```

When a button is clicked, Unity calls the matching method:

```csharp
private void OnPlayButtonClicked()
{
    PlayStartButtonAnimation();
}

private void OnSettingsButtonClicked()
{
    PlaySettingsButtonAnimation();
}

private void OnExitButtonClicked()
{
    PlayExitButtonAnimation();
}
```

This keeps the click-handling code separate from the animation code, which makes the script easier to read and expand.

### Play button animation

The play button grows slightly, then returns to its original size.

```csharp
void PlayStartButtonAnimation()
{
    playButton.transform.DOScale(1.2f, 0.2f)
        .SetEase(Ease.OutBounce)
        .OnComplete(() =>
        {
            playButton.transform.DOScale(1f, 0.2f);
        });
}
```

`DOScale(1.2f, 0.2f)` scales the button up to `1.2` over `0.2` seconds. `OnComplete` starts a second tween after the first one finishes, scaling the button back to `1`.

### Settings button animation

The settings button rotates back and forth.

```csharp
void PlaySettingsButtonAnimation()
{
    settingsButton.transform.DORotate(new Vector3(0, 0, 30), 0.2f, RotateMode.Fast)
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
}
```

`DORotate(new Vector3(0, 0, 30), 0.2f, RotateMode.Fast)` rotates the button to `30` degrees on the `z` axis over `0.2` seconds.

`SetLoops(2, LoopType.Yoyo)` makes the button rotate forward and back, creating a quick wiggle effect.

### Exit button animation

The exit button uses a punch scale animation.

```csharp
void PlayExitButtonAnimation()
{
    exitButton.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 10, 1);
}
```

`DOPunchScale` briefly pushes the button away from its current scale, then returns it. It is useful for quick click feedback because it feels like a pop instead of a smooth resize.

The parameters are:

1. `Vector3.one * 0.3f`: how much scale is added during the punch.
2. `0.3f`: how long the punch lasts.
3. `10`: how much vibration the punch has.
4. `1`: how elastic the punch feels.

### Removing button listeners

Remove listeners in `OnDestroy` when the object is destroyed.

```csharp
void OnDestroy()
{
    playButton.onClick.RemoveListener(OnPlayButtonClicked);
    settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
    exitButton.onClick.RemoveListener(OnExitButtonClicked);
}
```

This cleans up the button events and avoids leaving references behind after the UI controller is gone.

### UIController2: slide-in buttons

`UIController2` animates a list of UI buttons into place when the scene starts.

```csharp
using UnityEngine;
using DG.Tweening;

public class UIController2 : MonoBehaviour
{
    public RectTransform[] buttons;
    public float slideDuration = 0.6f;
    public float delayBetweenButtons = 0.1f;

    private float slideHorizontalOffset = -500f;
}
```

The `buttons` array stores the UI elements that should slide in. Each element is a `RectTransform`, which is the transform type used by Unity UI objects.

`slideDuration` controls how long each button takes to move into place. `delayBetweenButtons` controls the stagger between buttons, so they do not all move at the exact same time.

The animation starts from `Start`:

```csharp
void Start()
{
    PlaySlideInAnimation();
}
```

Inside `PlaySlideInAnimation`, each button's original anchored position is saved as the target position:

```csharp
Vector2 targetPos = button.anchoredPosition;
```

Then the button is moved off to the left before the tween starts:

```csharp
button.anchoredPosition = new Vector2(slideHorizontalOffset, targetPos.y);
```

Finally, `DOAnchorPos` moves it back to the saved target position:

```csharp
button.DOAnchorPos(targetPos, slideDuration)
    .SetDelay(i * delayBetweenButtons)
    .SetEase(Ease.OutBack);
```

`DOAnchorPos` is the UI version of a move tween for `RectTransform` objects. It changes the button's `anchoredPosition` instead of its world position.

`SetDelay(i * delayBetweenButtons)` creates the staggered effect. The first button starts immediately, the second waits `0.1` seconds, the third waits `0.2` seconds, and so on.

`Ease.OutBack` makes each button slightly overshoot its final position before settling back, which gives the slide-in a snappy UI feel.

Full example:

```csharp
void PlaySlideInAnimation()
{
    for (int i = 0; i < buttons.Length; i++)
    {
        RectTransform button = buttons[i];
        Vector2 targetPos = button.anchoredPosition;

        button.anchoredPosition = new Vector2(slideHorizontalOffset, targetPos.y);

        button.DOAnchorPos(targetPos, slideDuration)
            .SetDelay(i * delayBetweenButtons)
            .SetEase(Ease.OutBack);
    }
}
```

### UIController3: fade in a CanvasGroup

`UIController3` fades in a `CanvasGroup` when the scene starts.

```csharp
using DG.Tweening;
using UnityEngine;

public class UIController3 : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        canvasGroup.DOFade(1f, fadeDuration);
    }
}
```

A `CanvasGroup` controls the opacity of a UI object and its children. This is useful when you want to fade a whole panel, menu, or group of buttons at once.

`DOFade(1f, fadeDuration)` animates the CanvasGroup's alpha to `1`, making it fully visible over `fadeDuration` seconds.

For the fade-in to be visible, the CanvasGroup should usually start with alpha set to `0` in the Inspector or before the tween starts:

```csharp
canvasGroup.alpha = 0f;
canvasGroup.DOFade(1f, fadeDuration);
```

## References

1. [Easing Function Cheat Sheet](https://easings.net/)
