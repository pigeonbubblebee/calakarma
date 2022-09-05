using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamClamp : MonoBehaviour
{
    [Header("Cam Clamp Settings")]

    [SerializeField]
    private Transform follow;

    public float minX;
    public float maxX;

    public PostProcessLayer ppL;
    public PostProcessVolume ppV;

    void Awake()
    {
        SettingsSave.Load();

        ppL.enabled = SettingsSave.postProcess;
        ppV.enabled = SettingsSave.postProcess;
    }

    // Clamps Camera Position
    void Update()
    {
        Vector3 pos = new Vector3(Mathf.Clamp(follow.position.x, minX, maxX), follow.position.y, -10);
        transform.position = pos;
    }
}
