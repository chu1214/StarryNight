// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{

    public Vector2 turn;
    public float sensivity = .5f;
    public float speed = 1;
    public GameObject mover;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensivity;
        turn.y += Input.GetAxis("Mouse Y") * sensivity;

        mover.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        transform.localRotation = Quaternion.Euler(-turn.y,0,0);
    }
}
