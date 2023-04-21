// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem 
{
    // 입력으로 받는 target은 아이템 효과가 적용될 대상
    void Use(GameObject target);
}
