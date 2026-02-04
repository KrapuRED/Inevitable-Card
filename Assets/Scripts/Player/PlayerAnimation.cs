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

    private void Start()
    {
        ResetAnimation();
    }

    public void PlayAnimation(AnimationEffectType effectType)
    {
        Debug.Log($"Player try show VFX of {effectType}");
        _animator.SetTrigger(effectType.ToString());
    }

    private void ResetAnimation()
    {
        _spriteRenderer.sprite = null;
    }
}
