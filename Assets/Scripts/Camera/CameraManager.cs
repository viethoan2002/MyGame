using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager: MonoBehaviour
{
    public static CameraManager Instance;
    public Transform player;
    public Vector3 distance;

    [Range(5, 15)]
    public float kk = 5;

    private void Awake()
    {
        if (CameraManager.Instance == null)
        {
            CameraManager.Instance = this;
        }
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + distance;
    }
}
