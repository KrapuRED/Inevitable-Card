using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CrossFade : SceneTransition
{
    public CanvasGroup crossFade;
    public float duration = 1f;

    public override IEnumerator AnimationTransitionIn()
    {
        // Implement cross-fade in animation logic here
        yield return crossFade.DOFade(1f, duration);
    }

    public override IEnumerator AnimationTransitionOut()
    {
        // Implement cross-fade out animation logic here
        yield return crossFade.DOFade(0f, duration);
    }
}
