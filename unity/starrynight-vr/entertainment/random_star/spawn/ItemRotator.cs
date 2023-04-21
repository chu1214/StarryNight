// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public float rotationSpeed = 60f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);        
    }
}
