using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowBar : MonoBehaviour
{
    public Slider slider;

    public EnemyHealthBar parentHealthBar;

    float timer = 0;

    private void Start()
    {
        slider = GetComponent<Slider>();
        parentHealthBar = GetComponentInParent<EnemyHealthBar>();
    }

    private void OnEnable()
    {
        if (timer <= 0)
        {
            timer = 0.5f;
        }
    }

    public void SetMaxHealth(int maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            if (slider.value > parentHealthBar.slider.value)
            {
                slider.value = slider.value - 10;
            }
            else if (slider.value <= parentHealthBar.slider.value)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            timer = timer - Time.deltaTime;
        }
    }
}
