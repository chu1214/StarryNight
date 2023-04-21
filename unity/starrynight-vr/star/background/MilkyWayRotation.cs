// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkyWayRotation : MonoBehaviour
{
    void Awake()
    {
       Quaternion x = Quaternion.Euler(-61.2f, 0, 0);
       Quaternion y = Quaternion.Euler(0, 95.0f, 0);

       transform.rotation = x * y;
    }
}
