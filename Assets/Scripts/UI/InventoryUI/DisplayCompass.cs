using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCompass : MonoBehaviour
{
    public Transform playerCompass;
    public Transform home;
    public Image directionUI;
    public TMP_Text distanceHome;

    private void Update()
    {
        playerCompass.LookAt(home);
        distanceHome.text = $"{Vector3.Distance(playerCompass.position, home.position):F2} m";
        if (playerCompass.transform.rotation.eulerAngles.y < 0)
        {
            UpdateDirection(Mathf.Abs(playerCompass.transform.rotation.eulerAngles.y) + 135);
        }
        else
        {
            UpdateDirection(135 - playerCompass.transform.rotation.eulerAngles.y);
        }
    }

    void UpdateDirection(float angle)
    {
        directionUI.rectTransform.rotation = Quaternion.Euler(60, 0, angle);
    }
}
