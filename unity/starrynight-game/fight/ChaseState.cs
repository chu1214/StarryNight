// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    private List<string> attackList = new List<string>() {"isJumpAttack", "isBite"}; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();  
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 3.5f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > 15f)
        {
            animator.SetBool("isChasing", false);
        }

        if (distance < 2f)
        {
            animator.SetBool(attackList[Random.Range(0, 2)], true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position); // Stop enemy
    }

}
