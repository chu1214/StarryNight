// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Outline text = gameObject.GetComponentInChildren<Outline>();

        text.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Outline text = gameObject.GetComponentInChildren<Outline>();

        text.enabled = false;
    }
}
