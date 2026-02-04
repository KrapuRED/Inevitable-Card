using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationEffectType effectType)
    {
        Debug.Log($"Player try show VFX of {effectType}");
        _animator.ResetTrigger(effectType.ToString());
        _animator.SetTrigger(effectType.ToString());
    }

    private void ResetAnimation()
    {
        _spriteRenderer.sprite = null;
    }
}
