// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UseUIOnOff : MonoBehaviourPun
{
    public GameObject ui;

    private void OnEnable()
    {
        if (!photonView.IsMine)
        {
            ui.SetActive(false);
        }
    }
}
