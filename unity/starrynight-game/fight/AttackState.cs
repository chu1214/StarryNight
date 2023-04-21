// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > 2f)
        {
            animator.SetBool("isJumpAttack", false);
            animator.SetBool("isBite", false);
        }
    }
}
