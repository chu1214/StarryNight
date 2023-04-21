// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotating : MonoBehaviour
{
    public int rotationSpeed = -60;
    public GameObject button;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    
    public void hideAnotherPlace()
    {
        transform.SetParent(null, false);
    }
}
