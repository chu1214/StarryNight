// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource background;
    
    public void SetSoundVolume(float volume)
    {
        background.volume = volume; //소리 크기 조절
    }
}
