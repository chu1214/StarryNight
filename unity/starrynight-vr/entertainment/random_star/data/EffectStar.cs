// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EffectStar : MonoBehaviourPun, IItem
{
    public float effect_time = 60.0f;

    public void Use(GameObject target)
    {
        SimpleCharacterControl character = target.GetComponent<SimpleCharacterControl>();

        if (character != null)
        {
            character.photonView.RPC("TurnOnEffect", RpcTarget.All, effect_time);
        }
        else
        {
            // vr 캐릭터
            VRCharacterAnimationController _controller  = target.GetComponent<VRCharacterAnimationController>();
            _controller.photonView.RPC("TurnOnEffect",RpcTarget.All, effect_time);
            
        }
        
        PhotonNetwork.Destroy(gameObject);
    }
}
