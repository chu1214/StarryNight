// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class StarInfo : MonoBehaviour
{
    private UIManager uiManager;
    
    public int hip { get; set; }
    public string starName { get; set; }
    public string constellationInfo { get; set; }
    public float magnitude { get; set; }
    public float ra { get; set; }
    public float dec { get; set; }

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            uiManager.UpdateStarInfo(hip, starName, constellationInfo, magnitude, ra, dec);
            GameObject.Find("HUD Canvas").transform.Find("StarInfo").gameObject.SetActive(true);
            GameObject marker = GameObject.FindWithTag("Target");
            marker.transform.SetParent(transform, false);
        }
    }
}
