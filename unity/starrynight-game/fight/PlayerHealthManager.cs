// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public Text CurrentHealthText;
    Strafer strafer;
    GameObject UI_Health;
    
    void Awake()
    {
        strafer = GameObject.Find("Knight").GetComponent<Strafer>();
        UI_Health = GameObject.Find("UI_Health");
    }
    void Update()
    {
        CurrentHealthText.text = strafer.curHealth.ToString();
    }
}
