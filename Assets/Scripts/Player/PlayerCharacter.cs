using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private int _maxStamina;
    public int currentStamina;
    public TargetType type;
    [SerializeField] private CurrentPlayerHandler  _currentPlayerHandler;

    [Header("Events")]
    public OnTakeDamageEventSO onTakeDamageEvent;

    private void Start()
    {
        healtPoints = maxHealtPoint;
        currentStamina = _maxStamina;

        HUDManager.instance.CommitPlayerStamina(currentStamina);
        HUDManager.instance.UpdatePlayerHealth(healtPoints, maxHealtPoint);

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

    public void OnTakeDamage(TargetType targetType, int damageValue)
    {
        if (targetType == type)
        {
            //Debug.Log($"{this.name} with the target type of {targetType} is take damage {damageValue}");
            TakeDamage(damageValue);
        }
    }

    public void ResetStamina()
    {
        currentStamina = _maxStamina;
    }

    private void OnEnable()
    {
        onTakeDamageEvent.Register(OnTakeDamage);
    }

    private void OnDisable()
    {
        onTakeDamageEvent.Unregister(OnTakeDamage);
    }
}
