using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetAnimation();
    }

    public void PlayAnimation(AnimationEffectType effectType)
    {
        Debug.Log($"Player try show VFX of {effectType}");
    }

    private void ResetAnimation()
    {
        _spriteRenderer.sprite = null;
    }
}
