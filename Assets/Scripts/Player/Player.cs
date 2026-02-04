using UnityEngine;

public class Player : Character
{
    public int maxStamina;
    private  int _currentStamina;
    public int currentStamina => _currentStamina;

    public TargetType type;
    [SerializeField] private CurrentPlayerHandler  _currentPlayerHandler;
    [SerializeField] private PlayerAnimation _playerAnimation;
    public PlayerAnimation playerAnimation => _playerAnimation;

    [Header("Events")]
    public OnTakeDamageEventSO onTakeDamageEvent;
    public OnTakingHealEventSO onTakingHealEvent;

    private void Start()
    {
        healtPoints = maxHealtPoint;
        _currentStamina = maxStamina;

        HUDManager.instance.CommitPlayerStamina(_currentStamina);
        HUDManager.instance.UpdatePlayerHealth(healtPoints, maxHealtPoint);
        HUDManager.instance.UpdatePlayerBaseDamage(baseDamage);

        SetCurrentPlayerHandler();
    }

    private void SetCurrentPlayerHandler()
    {
        if (_currentPlayerHandler == null)
        {
            _currentPlayerHandler = GameObject.FindGameObjectWithTag("EventPlayerHandler")
                                    .GetComponent<CurrentPlayerHandler>();
        }
        _currentPlayerHandler.SetCurrentPlayer(this);
    }

    private void OnTakeDamage(TargetType targetType, int damageValue)
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
            //Debug.Log($"{this.name} with the target type of {targetType} is take heal {damageValue}");
            TakeHealing(damageValue);
        }
    }

    protected override void OnHealthChanged()
    {
        HUDManager.instance.UpdatePlayerHealth(healtPoints, maxHealtPoint);
    }

    public void ResetStamina()
    {
        _currentStamina = maxStamina;
        HUDManager.instance.CommitPlayerStamina(_currentStamina);
    }

    public override void OnDeath()
    {
        BattleManager.instance.SelectWinner(type);
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
