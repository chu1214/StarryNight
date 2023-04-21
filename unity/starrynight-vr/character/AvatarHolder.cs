// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarHolder : MonoBehaviour
{

    public Transform MainAvatarTransform;
    public Transform HeadTransform;
    public Transform HandLeftTransform;
    public Transform HandRightTransform;
    void Start()
    {
        SetLayerRecursively(HeadTransform.gameObject,11);
        SetLayerRecursively(HeadTransform.gameObject,12);
        
    }


    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if(go ==null) return;
        foreach (Transform trans  in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
