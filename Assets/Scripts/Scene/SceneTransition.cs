using System.Collections;
using UnityEngine;

public abstract class SceneTransition : MonoBehaviour
{
    public abstract IEnumerator AnimationTransitionIn();
    public abstract IEnumerator AnimationTransitionOut();
}
