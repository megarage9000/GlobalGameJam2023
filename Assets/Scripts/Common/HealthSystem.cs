using System.Collections;
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
    public float invincivilityWindow = 1f;
    private SliderBar HealthBar;
    private bool canSetHealth = true;

    void Start()
    {
        Debug.Log("Start");
        HealthBar = GetComponentInChildren<SliderBar>();
        currHealth = maxHealth;
        initHealthBar();
    }

    public virtual void TakeDamage(int damage)
    {
        if(canSetHealth) {
            currHealth -= damage;
            
            setHealthBar(currHealth);

            Debug.Log($"current health is {currHealth}");
            if (currHealth <= 0) {
                triggerDeathEvent();
            }
        }
    }

    IEnumerator SetHealthTimer() {
        canSetHealth = false;
        yield return new WaitForSeconds(invincivilityWindow);
        canSetHealth = true;
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

    private void setHealthBar(int hp)
    {
        HealthBar.SetValue((int)hp);
        StartCoroutine(SetHealthTimer());
    }
}
