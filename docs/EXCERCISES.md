# DOTween Exercises

These exercises are designed to feel closer to real production game tasks than isolated tween demos. Do not look for one perfect solution. Focus on clean structure, reusable animation code, cancellation, and how the animation should feel in-game.

## Exercise 1: Main Menu Entrance Transition

Difficulty: Easy

### Objective

Build a reusable main menu entrance animation where several UI buttons slide in with staggered timing and the whole panel fades from invisible to visible.

### Requirements

- Create a menu panel that contains at least three buttons.
- Use a `CanvasGroup` to fade the panel in.
- Use `RectTransform.DOAnchorPos` to slide each button from off-screen into its original position.
- Add a small delay between each button.
- Use an ease that feels polished, such as `Ease.OutBack` or `Ease.OutCubic`.
- The animation should play automatically when the menu opens.

### Constraints

- Do not hard-code every button animation one by one.
- The system should work if the number of buttons changes.
- The original button positions should come from the scene layout, not magic final positions in code.
- If the menu is opened twice, the animation should not stack multiple active tweens on the same UI elements.

### Suggested architecture

- Create a `MenuEntranceAnimator` component.
- Expose a `CanvasGroup`, an array or list of `RectTransform` menu items, a slide offset, duration, delay, and easing settings.
- Store any active `Sequence` or tweens so they can be killed before replaying.
- Have one public method such as `Play()` that resets the UI state and starts the entrance animation.

### Common mistakes

- Forgetting to reset the panel alpha before replaying the fade.
- Moving UI with `DOMove` instead of `DOAnchorPos`.
- Reading the target position after already moving the button off-screen.
- Creating a new sequence every time without killing the old one.
- Making the animation depend on a fixed screen resolution.

### Optional advanced extension

Add an exit animation that reverses the flow: buttons slide out, the panel fades out, and a callback is invoked when the animation finishes.

### Interview-style review questions

- Why is `RectTransform.anchoredPosition` usually better than world position for UI animation?
- How would you prevent a player from pressing buttons while the menu is still animating?
- Where should the menu animation logic live: inside the button scripts, the screen controller, or a reusable animator component?
- What should happen if `Play()` is called while the previous animation is still running?

### Edge cases

- The button list is empty.
- A button reference is missing.
- The panel starts inactive.
- The menu is opened, closed, and opened again quickly.
- The game is paused and `Time.timeScale` is set to `0`.

### Production considerations

- Consider whether this animation should use unscaled time for pause menus.
- Avoid per-frame allocations when replaying the animation many times.
- Keep timing values configurable for designers.
- Make sure the system supports different screen sizes and anchors.

## Exercise 2: Reward Popup With Count-Up Feedback

Difficulty: Easy to Medium

### Objective

Create a reward popup that appears after a level ends, reveals several rewards one by one, and gives each reward a satisfying scale or punch animation.

### Requirements

- Show a popup panel using fade and scale animation.
- Display at least three reward rows, such as coins, gems, and experience.
- Reveal reward rows one after another using a `Sequence`.
- Animate each reward icon with `DOPunchScale` or a short `DOScale` bounce.
- Animate the reward amount from `0` to its final value.
- Provide a way to skip or instantly complete the popup animation.

### Constraints

- Do not put all animation logic directly in one giant method.
- The reward popup should accept reward data from outside instead of using only hard-coded values.
- The popup should handle being closed before the full animation finishes.
- Reopening the popup should reset all visual state.

### Suggested architecture

- Create a `RewardPopupView` that owns the UI references.
- Create a small data type such as `RewardItemData` with an icon, label, and amount.
- Create a reusable `RewardRowView` for one row.
- Let the popup build one main `Sequence` that controls panel fade, panel scale, row reveals, and count-up tweens.
- Store the sequence so `Skip()`, `Close()`, or `OnDestroy()` can complete or kill it cleanly.

### Common mistakes

- Forgetting to kill the count-up tween when the popup closes.
- Animating text with repeated string allocations every frame without thinking about frequency.
- Leaving reward rows visible from a previous popup.
- Making the skip button start a second animation instead of completing the current one.
- Using the same duration for every reward amount, even when one reward is much larger than another.

### Optional advanced extension

Add a "claim" button that becomes interactable only after the reveal sequence finishes or after the player skips the animation.

### Interview-style review questions

- How would you design the popup so a different game mode can reuse it with different reward types?
- What should `Skip()` do: finish all tweens instantly, kill them, or jump to a final state manually?
- How would you avoid bugs if the popup is destroyed while a tween callback is waiting to run?
- Which parts of the animation should be data-driven?

### Edge cases

- There are no rewards.
- The reward amount is `0`.
- The player presses skip multiple times.
- The popup is closed mid-animation.
- The same reward row prefab is reused through pooling.

### Production considerations

- Reward popups often appear frequently, so pooling row views may matter.
- Count-up text can allocate strings; keep an eye on profiler data.
- Audio, haptics, and particles may need to sync with sequence moments.
- Designers will likely tune durations, delays, and easing many times.

## Exercise 3: Damage Feedback And Floating Combat Text

Difficulty: Medium

### Objective

Build a combat feedback system that plays hit reactions on an enemy and spawns floating damage text at the hit position.

### Requirements

- When damage is received, the enemy should briefly flash, punch scale, or shake.
- Spawn floating combat text showing the damage number.
- The text should rise upward, fade out, and optionally scale or punch at spawn.
- Multiple hits in quick succession should be supported.
- Critical hits should use a stronger animation than normal hits.
- The system should clean up text objects after their animation completes.

### Constraints

- Do not instantiate and destroy text objects for every hit if you can avoid it.
- Do not let old tweens keep running on reused pooled text objects.
- The enemy hit animation should not permanently change the enemy scale or color.
- The combat system should not need to know DOTween details.

### Suggested architecture

- Create an `IDamageFeedback` or `DamageFeedbackView` component on enemies.
- Create a `FloatingTextPool` that reuses text objects.
- Create a `FloatingCombatTextView` with a `Play(amount, worldPosition, isCritical)` method.
- Use a sequence for each text object: reset state, scale in, move upward, fade out, then return to pool.
- Store active tweens on both enemy feedback and floating text views so they can be killed before reuse.

### Common mistakes

- Reusing pooled objects without resetting alpha, scale, position, or text.
- Forgetting that `SpriteRenderer.material` can create material instances.
- Starting a new hit flash while the previous color tween is still active.
- Using world-space movement for UI text without converting world position to screen or canvas position.
- Letting critical hit feedback become visually noisy when many hits happen at once.

### Optional advanced extension

Add combo support: if several hits land within a short window, show a combo counter that scales up and refreshes its fade-out timer.

### Interview-style review questions

- How would you separate combat logic from visual feedback?
- What should happen if the enemy dies while its hit animation is still playing?
- How would you pool floating text safely with DOTween?
- How would you make critical hits feel stronger without making the screen unreadable?

### Edge cases

- Ten hits happen in the same frame.
- A pooled text object is reused before its previous tween is killed.
- The enemy is disabled during a tween.
- Damage is blocked or equals `0`.
- The canvas camera changes or the UI uses a different render mode.

### Production considerations

- Pooling is important because combat text can be spawned very often.
- Avoid excessive shake or punch animations that harm readability.
- Make animation values configurable per enemy type or hit type.
- Profile GC allocations during heavy combat scenes.

## Exercise 4: Screen Transition And Async Scene Flow

Difficulty: Medium to Advanced

### Objective

Create a screen transition controller that fades out, waits for a simulated async load, then fades back in.

### Requirements

- Use a full-screen UI overlay with a `CanvasGroup`.
- Fade the overlay in before loading starts.
- Show a loading animation while loading is in progress.
- Fade the overlay out after loading completes.
- Expose one public method that starts the transition flow.
- Prevent multiple transitions from running at the same time.

### Constraints

- Do not allow user input during the transition.
- The loading animation should be cancellable or stoppable.
- The flow should be readable and maintainable even if more steps are added later.
- Avoid leaving the overlay visible if the flow fails or is cancelled.

### Suggested architecture

- Create a `ScreenTransitionController`.
- Store the active transition `Sequence`.
- Use separate methods for `FadeIn`, `PlayLoadingLoop`, `StopLoadingLoop`, and `FadeOut`.
- Use a boolean or state enum to block duplicate transition requests.
- If you use async code, keep DOTween callback ownership clear and handle cancellation.

### Common mistakes

- Starting a second scene transition while the first one is still running.
- Forgetting to kill an infinite loading loop.
- Leaving the loading spinner scaled, rotated, or faded after the transition.
- Mixing scene-loading logic and animation details in a way that is hard to test.
- Assuming callbacks will always run even if the object is destroyed.

### Optional advanced extension

Add progress display support: the loading text or progress bar should animate smoothly toward the real loading progress instead of snapping.

### Interview-style review questions

- How would you design the transition API so gameplay code does not touch DOTween directly?
- What happens if scene loading fails?
- How do you cancel a transition safely?
- Should the transition use scaled or unscaled time?

### Edge cases

- The player triggers two scene changes quickly.
- Loading finishes almost instantly.
- Loading takes a long time.
- The transition object is destroyed during the flow.
- The game is paused while loading begins.

### Production considerations

- Scene transitions are shared infrastructure, so keep the API small and predictable.
- Infinite tweens need explicit cleanup.
- Use profiler and memory tools if the transition runs often.
- Think about accessibility: very flashy transitions may need reduced-motion options.

## Exercise 5: Gacha Reveal Or Card Pack Opening Sequence

Difficulty: Advanced

### Objective

Build a polished reveal sequence for opening a card pack or gacha reward, where items appear one by one with rarity-based animation intensity.

### Requirements

- Display a pack, chest, or reveal panel.
- Animate an opening sequence before showing rewards.
- Reveal at least five reward cards or item slots.
- Use different animation profiles for common, rare, and legendary items.
- Use sequences to coordinate fade, scale, rotation, punch, and delay.
- Allow the player to tap to speed up or skip the reveal.
- After the reveal finishes, show a final summary state.

### Constraints

- Do not hard-code each reward slot as a separate animation method.
- The reveal should be driven by a list of reward data.
- The system should support future changes such as ten-card reveals or different rarity tiers.
- Skip logic must leave the UI in a correct final state.
- Avoid creating long chains of anonymous callbacks that are hard to debug.

### Suggested architecture

- Create `RevealSequenceController` to own the flow.
- Create `RewardCardView` to own one card's visuals.
- Create `RarityAnimationProfile` data for timing, ease, scale, punch strength, color, and effects.
- Build the reveal sequence from reward data and card views.
- Store active tweens or sequences per card, plus one master sequence for the full reveal.
- Make `CompleteInstantly()` set the final visual state without relying on every callback firing.

### Common mistakes

- Encoding rarity behavior with many `if` statements scattered across UI code.
- Letting skipped animations leave cards half-transparent or half-scaled.
- Creating one huge sequence that is impossible to inspect or cancel safely.
- Forgetting to reset card state when the pack is opened again.
- Overusing rotation, bounce, and punch until the reveal feels noisy.

### Optional advanced extension

Add a "hold to reveal all" interaction where holding the pointer accelerates the active sequence and releasing returns to normal speed.

### Interview-style review questions

- How would you make rarity animation data editable by designers?
- What is the difference between skipping to the end and killing the sequence?
- How would you test that every reward card reaches a valid final state?
- How would you prevent performance spikes during a ten-card reveal?

### Edge cases

- The reward list has fewer cards than available slots.
- The reward list has more cards than visible slots.
- The player skips during the pack opening, before any card appears.
- The player skips while a legendary reveal effect is playing.
- A reward image fails to load or is missing.

### Production considerations

- Gacha and pack reveals often need strong polish, but they must still be responsive.
- Use pooling for card views and visual effects.
- Keep animation profiles data-driven so designers can tune them without code changes.
- Make sure the final summary state is deterministic, even when the reveal is interrupted.
