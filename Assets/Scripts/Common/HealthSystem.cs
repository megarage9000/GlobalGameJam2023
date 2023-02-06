using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 30;
    [SerializeField]
    private int minHealth = 0;
    [SerializeField]
    private int currHealth;

    public UnityEvent OnDeath;
    private SliderBar HealthBar;

    void Start()
    {
        Debug.Log("Start");
        HealthBar = GetComponentInChildren<SliderBar>();
        currHealth = maxHealth;
        initHealthBar();
    }

    public virtual void TakeDamage(int damage)
    {
        currHealth -= damage;
        setHealthBar(currHealth);
        Debug.Log($"current health is {currHealth}");
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
        Debug.Log("Initialize health bar");
        HealthBar.SetMaxValue((int)maxHealth);
        HealthBar.SetMinValue((int)minHealth);
        HealthBar.SetValue((int)maxHealth);
    }

    private void setHealthBar(int hp)
    {
        HealthBar.SetValue((int)hp);
    }
}
