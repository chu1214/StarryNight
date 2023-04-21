// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;

public class HighlitVocie : MonoBehaviour
{
    [SerializeField]
    private Image micImage; 

    [SerializeField]
    private Image speakerImage;

    [SerializeField]
    private PhotonVoiceView photonVoiceView;

    private void Awake()
    {
        
        //micImage = GetComponentInParent<VoiceDebugUI>().transform.Find("Image_Mic").GetComponent<Image>();
        this.photonVoiceView = this.GetComponentInParent<PhotonVoiceView>();
        this.micImage.enabled = false;
        this.speakerImage.enabled = false;

    }
    void Start()
    {
        
    }
    void Update()
    {
        this.micImage.enabled = this.photonVoiceView.IsRecording;
        this.speakerImage.enabled = this.photonVoiceView.IsSpeaking;
    }
}
