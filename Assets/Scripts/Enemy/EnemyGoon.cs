using UnityEngine;

public class EnemyGoon : Enemy
{
    [SerializeField] private CurrentEnemyHandler currentEnemyHandler;
    [SerializeField] private TargetType type;

    [Header("Events")]
    public OnTakeDamageEventSO onTakeDamageEvent;
    public OnTakingHealEventSO onTakingHealEvent;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Start()
    {
        if (enemyData != null)
        {
            
            nameCharacter = enemyData.enemyName;
            maxHealtPoint = enemyData.maxHealth;
            healtPoints = enemyData.maxHealth;
            baseDamage = enemyData.baseDamage;
        }


        HUDManager.instance.UpdateEnemyHealth(healtPoints, maxHealtPoint);
        HUDManager.instance.UpdateEnemyBaseDamage(baseDamage);
        SetCurrentEnemy();
    }

    protected override void OnHealthChanged()
    {
        HUDManager.instance.UpdateEnemyHealth(healtPoints, maxHealtPoint);
    }

    public void SetCurrentEnemy()
    {
        if (currentEnemyHandler == null)
        {
            currentEnemyHandler = GetComponentInChildren<CurrentEnemyHandler>();

        }

        currentEnemyHandler.SetCurrentEnemy(this);
    }

    public void OnTakeDamage(TargetType targetType, int damageValue)
    {
        if (targetType == type)
        {
            //Debug.Log($"{this.name} with the target type of {targetType} is take damage {damageValue}");
            TakeDamage(damageValue);
            SoundEffectManager.instance.PlaySoundEffectOneClip("EnemyHurt");
        }
    }

    private void OnTakingHealing(TargetType targetType, int damageValue)
    {
        if (targetType == type)
        {
            //Debug.Log($"{this.name} with the target type of {targetType} is take damage {damageValue}");
            TakeHealing(damageValue);
        }
    }

    public override void OnDeath()
    {
        //tell manager player is win
        BattleManager.instance.SelectWinner(type);
        enemyData.isElimination = true;
        HUDManager.instance.UpdateEnemyHealth(0, maxHealtPoint);
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        onTakeDamageEvent.Register(OnTakeDamage);
        onTakingHealEvent.Register(OnTakingHealing);
    }

    private void OnDisable()
    {
        onTakeDamageEvent.Unregister(OnTakeDamage);
        onTakingHealEvent.Unregister(OnTakingHealing);
    }
}
