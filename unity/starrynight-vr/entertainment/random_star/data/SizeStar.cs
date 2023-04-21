// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SizeStar : MonoBehaviourPun, IItem
{
    public float multiply_size = 5;
    public float multiply_time = 60.0f;

    public void Use(GameObject target)
    {
        SimpleCharacterControl character = target.GetComponent<SimpleCharacterControl>();

        if (character != null)
        {
            character.photonView.RPC("MultiplySize", RpcTarget.All, multiply_size, multiply_time);
        }
        else
        {
            // vr 캐릭터
            VRCharacterAnimationController _controller  = target.GetComponent<VRCharacterAnimationController>();
            _controller.photonView.RPC("MultiplySize",RpcTarget.All, multiply_size,multiply_time);
            
        }
        
        PhotonNetwork.Destroy(gameObject);
    }
}
