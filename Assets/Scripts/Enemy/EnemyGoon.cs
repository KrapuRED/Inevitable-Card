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
            _spriteRenderer.color = Color.red;
            nameCharacter = enemyData.enemyName;
            maxHealtPoint = enemyData.maxHealth;
            healtPoints = enemyData.maxHealth;
            baseDamage = enemyData.baseDamage;
        }

        SetCurrentEnemy();
    }

    private void Update()
    {
        HUDManager.instance.UpdateEnemyHealth(healtPoints, maxHealtPoint);
    }

    public void SetCurrentEnemy()
    {
        if (currentEnemyHandler == null)
        {
            currentEnemyHandler = GameObject.FindGameObjectWithTag("EventEnemyHandler")
                                            .GetComponent<CurrentEnemyHandler>();
        }

        currentEnemyHandler.SetCurrentEnemy(this);
    }

    public void OnTakeDamage(TargetType targetType, int damageValue)
    {
        if (targetType == type)
        {
            //Debug.Log($"{this.name} with the target type of {targetType} is take damage {damageValue}");
            TakeDamage(damageValue);
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
        BattleManager.instance.SelectWinner("enemy");
        //Desycing with battle manager
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
