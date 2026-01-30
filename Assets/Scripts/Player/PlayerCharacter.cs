using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private int _maxStamina;
    public int currentStamina;
    public TargetType type;
    [SerializeField] private CurrentPlayerHandler  _currentPlayerHandler;

    [Header("Events")]
    public OnTakeDamageEventSO onTakeDamageEvent;
    public OnTakingHealEventSO onTakingHealEvent;

    private void Start()
    {
        healtPoints = maxHealtPoint;
        currentStamina = _maxStamina;

        HUDManager.instance.CommitPlayerStamina(currentStamina);
        HUDManager.instance.UpdatePlayerHealth(healtPoints, maxHealtPoint);

        SetCurrentPlayerHandler();
    }

    private void Update()
    {
        HUDManager.instance.UpdatePlayerHealth(healtPoints, maxHealtPoint);
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
        public void ResetStamina()
    {
        currentStamina = _maxStamina;
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
