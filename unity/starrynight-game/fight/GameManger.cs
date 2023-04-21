// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public GameObject imageRestart;
    [SerializeField] GameObject Lion1;
    [SerializeField] GameObject Lion2; 
    [SerializeField] GameObject Lion3; 

    ThirdPersonCamera thirdPersonCamera;
    Strafer strafer;
    public int deadLionCounts = 0;
    public int currentLionCounts = 0; 
    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();
        imageRestart.SetActive(false);
        thirdPersonCamera.m_lookDistance = 10f;
        thirdPersonCamera.m_depthOffset = -2.52f;
        thirdPersonCamera.m_verticalOffset = 0.84f;
        thirdPersonCamera.m_horizontalOffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLionCounts < deadLionCounts)
        {
            if (deadLionCounts == 1)
                Lion1.SetActive(true);
            else if (deadLionCounts == 2)
                Lion2.SetActive(true);
            else if (deadLionCounts >= 3)
                Lion3.SetActive(true);
            currentLionCounts = deadLionCounts;
        }
    }

    public void OnClickRestart()
    {
        imageRestart.SetActive(false);
        strafer = FindObjectOfType<Strafer>();
        strafer.Restart();
        Time.timeScale = 1f;
    }
}
