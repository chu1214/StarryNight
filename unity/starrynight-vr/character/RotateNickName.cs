// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RotateNickName : MonoBehaviour
{
    private CinemachineVirtualCamera followCam;

    void Start()
    {
        followCam = FindObjectOfType<CinemachineVirtualCamera>();
    }
    void Update()
    {
        transform.rotation = followCam.transform.rotation;
    }
}
