using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public  Slider slider;

    public YellowBar yellowBar;

    public float timeToHidden;

    private float _time;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        if (yellowBar != null)
        {
            yellowBar.SetMaxHealth(maxHealth);
        }
    }

    public void SetCurrentHealth(int currentHealth)
    {

        if (yellowBar != null)
        {
            yellowBar.gameObject.SetActive(true);

            if (currentHealth > slider.value)
            {
                yellowBar.slider.value = currentHealth;
            }
        }

        slider.value = currentHealth;
        _time = timeToHidden;
    }

    private void Update()
    {
        _time = _time - Time.deltaTime;
        if (_time <= 0)
        {
            _time = 0;
            slider.gameObject.SetActive(false);
        }
    }
}
