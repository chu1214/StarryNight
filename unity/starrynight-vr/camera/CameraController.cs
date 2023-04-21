// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


    public class CameraController : MonoBehaviour
    {



        [SerializeField] float cameraRotationSpeed = 2f;

        private Vector3 oldMousePosition;

        void Start()
        {
            
            Debug.Log("시작 위치 : "+ transform);
        }


        void Update()
        {
            if( Input.GetMouseButtonDown(1))
            {
                oldMousePosition = Input.mousePosition;
                Debug.Log("마우스 클릭 : 위치"+oldMousePosition);
                return;
            }


            if (Input.GetMouseButton(1))
            {

                Vector3 currentMousePosition = Input.mousePosition;                
                               
                if ( currentMousePosition.x < oldMousePosition.x)
                {
                    float x = transform.eulerAngles.x;
                    float y = transform.eulerAngles.y;
                    transform.eulerAngles = new Vector3(x, y + cameraRotationSpeed);
                }

                if (currentMousePosition.x > oldMousePosition.x)
                {
                    float x = transform.eulerAngles.x;
                    float y = transform.eulerAngles.y;
                    transform.eulerAngles = new Vector3(x, y - cameraRotationSpeed);
                }
                 Debug.Log("마우스 클릭 해제 : 위치"+currentMousePosition);
      
            }

        }

    }

