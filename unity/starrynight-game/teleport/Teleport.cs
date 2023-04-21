// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public float animTime = 1.5f;
    private Image fadeImage;

    private float start = 1f;
    private float end = 0f;
    public float time = 0f;

    public bool stopIn = false;
    public bool stopOut = true;
    public bool isTeleporting = false;

    DeadStateController deadState;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
        deadState = FindObjectOfType<DeadStateController>();
    }

    void Update()
    {
        if (stopOut == false && time <= 1.5f){ PlayFadeOut(); }
        if (stopIn == false && time <= 1.5f){ PlayFadeIn(); }
        if (time >= 1.5f && stopIn == false){
            stopIn = true;
            time = 0;
            isTeleporting = false;
        }

        if (time >= 1.5f && stopOut == false){
            stopIn = false;
            stopOut = true;
            time = 0;
            isTeleporting = false;
        }
    }

    public void PlayFadeIn()
    {
        time += Time.deltaTime / animTime;
        Color color = fadeImage.color;
        color.a = Mathf.Lerp(start, end, time);
        fadeImage.color = color;
        isTeleporting = true;
    }

    public void PlayFadeOut()
    {
        time += Time.deltaTime / animTime;
        if (deadState.IsDead) fadeImage.color = Color.black;
        Color color = fadeImage.color;
        color.a = Mathf.Lerp(end, start, time);
        fadeImage.color = color;
        isTeleporting = true;
    }

}
