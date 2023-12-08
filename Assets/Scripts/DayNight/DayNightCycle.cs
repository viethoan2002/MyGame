using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle: MonoBehaviour
{
    public DisplayDayNight displayDayNight;

    [SerializeField]
    private int timeDay;

    public float timeSmooth;
    public int currentTime;
    public bool isDay;
    public bool isRun = true;

    public static event Action OnDay;
    public static event Action OnNight;


    void Start()
    {
        displayDayNight.SetDayNight(isDay);
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (isRun)
        {
            currentTime++;
            if (isDay)
                displayDayNight.Filler((float)currentTime / timeDay);
            else
                displayDayNight.Filler(1 - (float)currentTime / timeDay);

            if (currentTime > timeDay)
            {
                OnChanged();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnChanged()
    {
        if (isDay)
        {
            isDay = false;
            OnNight?.Invoke();
            StartCoroutine(RotateLight(60, -10));
        }
        else
        {
            isDay = true;
            OnDay?.Invoke();
            StartCoroutine(RotateLight(180, 60));
        }

        currentTime = 0;
        displayDayNight.SetDayNight(isDay);
    }

    IEnumerator RotateLight(float startAngle,float endAngle)
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeSmooth)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, elapsedTime / timeSmooth);
            transform.rotation = Quaternion.Euler(currentAngle, 30, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(endAngle, 30, 0);
    }
}
