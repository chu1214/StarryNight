// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damageAmount = 10;
    
    AudioSource audioData;
    CharacterItemAnimator character;
    Enemy enemy;
    private bool enemyAttacked;
    private float attackChanceCount = 1.0f;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        character = FindObjectOfType<CharacterItemAnimator>();
        enemy = FindObjectOfType<Enemy>();
    }

    public void Use()
    {
        audioData.Play(0);
        attackChanceCount = 1.0f;
        StartCoroutine("Swing");
    }

    IEnumerator Swing()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !character.playerAttackState && attackChanceCount > 0)
        {
            other.GetComponent<Rigidbody>().AddForce(-transform.up * 100f);
            // other.transform.position += transform.forward * Time.deltaTime * 250f;
            attackChanceCount = attackChanceCount - 1.0f;
            other.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }
}
