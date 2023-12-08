using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private Light lightR;
    public float lightHeight;

    private void Start()
    {
        lightR = GetComponent<Light>();
    }

    private void OnEnable()
    {
        DayNightCycle.OnDay += TurnOff;
        DayNightCycle.OnNight += TurnOn;
    }

    private void OnDisable()
    {
        DayNightCycle.OnDay -= TurnOff;
        DayNightCycle.OnNight -= TurnOn;
    }

    public void TurnOff()
    {
        lightR.intensity = 0;
    }

    public void TurnOn()
    {
        lightR.intensity = lightHeight;
    }
}
