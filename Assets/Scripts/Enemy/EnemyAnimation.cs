using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayAnimation(AnimationEffectType effectType)
    {
        Debug.Log($"Player try show VFX of {effectType}");
        _animator.ResetTrigger(effectType.ToString());
        _animator.SetTrigger(effectType.ToString());
    }
}
