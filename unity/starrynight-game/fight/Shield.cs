// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    CharacterItemAnimator character;
    public bool isBlocked;
    Croucher croucher;

    void Start()
    {
        isBlocked = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        croucher = FindObjectOfType<Croucher>();
    }

    public void ShieldUp()
    {
        if (croucher.m_moveState.ToString() != "Crouching")
        {    
            gameObject.GetComponent<BoxCollider>().enabled = true;
            StopCoroutine("Hold");
            StartCoroutine("Hold");
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator Hold()
    {
        isBlocked = true;
        yield return new WaitForSeconds(0.5f);
        isBlocked = false;
    }
}
