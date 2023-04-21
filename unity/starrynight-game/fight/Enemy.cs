// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int HP = 100;
    public Animator animator;
    public Slider healthBar;
    Material[] mats;
    ParticleSystem particleObject;

    // AudioSource & AudioClips
    AudioSource audioSource;
    public AudioClip LionDamage;
    public AudioClip EnemyDamage;
    public AudioClip LionDeath;

    //
    private bool enemyAttacked;
    private bool characterIsAttacking;

    CharacterItemAnimator character;
    Weapon weapon;
    GameManger gamemanager;
    
    void Awake()
    {
        mats = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials;
        particleObject = gameObject.GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        character = FindObjectOfType<CharacterItemAnimator>();
        weapon = FindObjectOfType<Weapon>();
        gamemanager = FindObjectOfType<GameManger>();
    }

    void Update() { healthBar.value = HP; }

    public void TakeDamage(int damageAmount)
    {   
        particleObject.Play();
        HP -= damageAmount;
        StartCoroutine(ChangeEnemyColor());

        // Damage Sound
        audioSource.PlayOneShot(EnemyDamage);

        StartCoroutine(ABC());
        
        // Damage Sound 2
        audioSource.PlayOneShot(LionDamage);

        if (HP <= 0)
        {
            // Death Sound
            audioSource.clip = LionDeath;
            audioSource.Play();
            gamemanager.deadLionCounts += 1;
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 5f);
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }

    private IEnumerator ChangeEnemyColor()
    {
        foreach(Material mat in mats){ mat.color = Color.red; }
        yield return new WaitForSeconds(0.1f);
        foreach(Material mat in mats){ mat.color = Color.white; }
    }

    private IEnumerator ABC()
    {
        yield return new WaitForSeconds(0.1f);
    }
}