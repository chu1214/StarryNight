// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using Photon.Pun;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.UI;

public class VoiceUse : MonoBehaviourPun
{
    private PunVoiceClient punVoiceClient;
    private PhotonVoiceView recorder;
    
    private float volumeBeforeMute;

    private bool micState = true;
    private bool speakerState = true;

    public RawImage micImage;
    public RawImage speakerImage;

    private void Start()
    {
        this.volumeBeforeMute = AudioListener.volume;
        recorder = GetComponent<PhotonVoiceView>();
    }

    private void OnEnable()
    {
        recorder = GetComponent<PhotonVoiceView>();
    }

    public void MicToggle()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        if (!micState)
        {
            micImage.color = new Color(0, 0, 0, 1);
            this.recorder.RecorderInUse.TransmitEnabled = true;
            micState = true;
        }
        else
        {
            micImage.color = new Color(0, 0, 0, 100/255f);
            this.recorder.RecorderInUse.TransmitEnabled = false;
            micState = false;
        }
    }

    public void SpeakerToggle()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        if (!speakerState)
        {
            speakerImage.color = new Color(0, 0, 0, 1);
            AudioListener.volume = this.volumeBeforeMute;
            this.volumeBeforeMute = 0f;
            speakerState = true;
        }
        else
        {

            speakerImage.color = new Color(0, 0, 0, 100/255f);
            this.volumeBeforeMute = AudioListener.volume;
            AudioListener.volume = 0f;
            speakerState = false;
        }
    }
}
