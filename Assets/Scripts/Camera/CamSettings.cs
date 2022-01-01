using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script using for Camera Settings
/// 1-Calculating Ortographic camera size
/// </summary>
///
[RequireComponent(typeof(Camera))]
public class CamSettings : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = this.GetComponent<Camera>();
        InitializeSettings();
    }


    /// <summary>
    /// Initialize functions called
    /// </summary>
    void InitializeSettings()
    {
        CalculateOSize();
    }
    /// <summary>
    /// Calculate Ortographic Camera size and set that
    /// </summary>
    void CalculateOSize()
    {
        float camOSize = 0;
        camOSize = ((720.0f / 1280.0f) * 5) / ((float)Screen.width / (float)Screen.height);
        mainCamera.orthographicSize = camOSize;

    }

}
