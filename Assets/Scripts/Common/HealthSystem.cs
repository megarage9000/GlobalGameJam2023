using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private uint maxHealth = 30;
    [SerializeField]
    private uint minHealth = 0;
    [SerializeField]
    private uint currHealth;

    public UnityEvent OnDeath;
    private SliderBar HealthBar;

    void Start()
    {
        HealthBar = GetComponentInChildren<SliderBar>();
        currHealth = maxHealth;
        initHealthBar();
    }

    public virtual void TakeDamage(uint damage)
    {
        currHealth -= damage;
        setHealthBar(currHealth);

        if (currHealth <= 0)
        {
            triggerDeathEvent();
        }
    }

    private void triggerDeathEvent()
    {
        if (OnDeath != null) { OnDeath.Invoke(); }
    }

    private void initHealthBar()
    {
        HealthBar.SetMaxValue((int)maxHealth);
        HealthBar.SetMinValue((int)minHealth);
        HealthBar.SetValue((int)maxHealth);
    }

    private void setHealthBar(uint hp)
    {
        HealthBar.SetValue((int)hp);
    }
}
