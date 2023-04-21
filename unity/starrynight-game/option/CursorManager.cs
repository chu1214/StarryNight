// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] GameObject Option;
    [SerializeField] GameObject Helper;
    [SerializeField] GameObject SpeedUp;
    [SerializeField] GameObject SpeedDown;
    [SerializeField] GameObject Bar;
    [SerializeField] GameObject GameEnd;
    [SerializeField] GameObject EndBtn;  // 게임 종료 버튼
    [SerializeField] GameObject CancelBtn;  // 게임 종료 버튼
    DeadStateController deadState;
    public StarCoinManager starCoinManager;
    public static float speed = 0.03f;
    public bool cursorState;
    public bool optionState;
    bool speedState;

    void Start()
    {
        cursorState = false;
        Cursor.visible = cursorState; // 커서 숨김
    }
    void Update()
    {
        deadState = FindObjectOfType<DeadStateController>();

        if (Input.GetKeyDown(KeyCode.Escape) && !GameEnd.activeSelf)// && !cursorState
        {
            optionState = BoolChange(optionState);
            if (!optionState)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            Option.SetActive(optionState);
            Helper.SetActive(false);
            cursorState = optionState;

        }

        if (deadState.m_isDead || GameEnd.activeSelf)
        {
            cursorState = true;
        }

        if (!cursorState) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;

        Cursor.visible = cursorState;

    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && cursorState)
        {
            cursorState = false;
        }
    }

    bool BoolChange(bool flag)
    {
        return !flag;
    }

    public void ContinueClick() //이어하기 버튼 클릭
    {
        optionState = BoolChange(optionState);
        if (!optionState)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        Option.SetActive(optionState);
        cursorState = optionState;
    }

    public void ExitClick() //종료 버튼 클릭
    {
        starCoinManager.EndGameMethod();
        Application.Quit();
        Debug.Log("게임을 종료합니다.");
    }

    public void HelperClick() // 게임 가이드 클릭
    {
        Helper.SetActive(true);
    }
    public void HelperCloseClick() // 게임 가이드 닫기 클릭
    {
        Helper.SetActive(false);
    }
    public void SpeedUpClick()
    {
        SpeedUp.SetActive(true);
        SpeedDown.SetActive(false);
        speed = 0f;

    }
    public void SpeedDownClick()
    {
        SpeedDown.SetActive(true);
        SpeedUp.SetActive(false);
        speed = 0.03f;
    }
}
