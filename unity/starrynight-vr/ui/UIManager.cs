// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    private static UIManager m_instance;

    public Text cityText;
    public Text dateText;
    public Text timeText;

    public Text starNameText;
    public Text constellationInfoText;
    public Text magnitudeText;
    public Text raText;
    public Text decText;

    public void UpdateCityText(string cityName)
    {
    
        cityText.text = cityName;
   
    }

    public void UpdateDateTimeText(DateTime dateTime)
    {
        dateText.text = dateTime.Year + "년 " + dateTime.Month + "월 " + dateTime.Day + "일";
        timeText.text = dateTime.Hour + "시 " + dateTime.Minute + "분 " + dateTime.Second + "초";
    }

    public void UpdateStarInfo(int hip, string starName, string constellationInfo, float magnitude, float ra, float dec)
    {
        if (starName == null)
        {
            starName = "HIP " + hip;
        }
        starNameText.text = starName;
        constellationInfoText.text = constellationInfo;
        magnitudeText.text = "겉보기 등급 : " + magnitude;
        raText.text = "적경 : " + ra;
        decText.text = "적위 : " + dec;
    }
}
