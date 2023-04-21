// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class VRCharacterAnimationController : MonoBehaviourPun
{
    
    [SerializeField] public InputActionReference input_Jump;
    [SerializeField] public InputActionReference input_InteractObject;
   
    [SerializeField] private InputActionProperty MoveValue;
    [SerializeField] private InputActionProperty TurnValue;
   
    [SerializeField] private Animator m_animator;

    [SerializeField] private Rigidbody m_rigidBody;
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    public float originalSpeed;
    public Vector3 originalSize;
    public float originalJump;

    [SerializeField] private GameObject XROrigin;
    [SerializeField] private  ActionBasedContinuousMoveProvider moveProvider;
    
    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    public bool m_wasGrounded;
    public Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;
    public bool usingItem = false;
    
    private List<Collider> m_collisions = new List<Collider>();
    // Start is called before the first frame update
    public GameObject effect;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            this.enabled = false;
        }
        moveProvider   = gameObject.GetComponent<ActionBasedContinuousMoveProvider>();
    }

    private void Start()
    {  originalSpeed =    moveProvider.moveSpeed;
       originalJump =  m_jumpForce ;
       originalSize =  gameObject.transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
   
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        

        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }
    
        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {

        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (collider.CompareTag("Item") && !usingItem)
        {
            IItem item = collider.GetComponent<IItem>();
            
            item.Use(gameObject);
            usingItem = true;
        }
    }
    
    private void OnTriggerStay(Collider collider)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        if (collider.CompareTag("Firework"))
        {
            if (input_InteractObject.action.triggered)
            {
                Firework firework = collider.gameObject.GetComponent<Firework>();
                firework.Fireworking();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return; 
        }

        if (!m_jumpInput && input_Jump.action.triggered)
        {
 
            m_jumpInput = true;
        }
       

    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return; 
        m_animator.SetBool("Grounded",m_isGrounded);
        m_animator.SetBool("Grounded",m_isGrounded);

        moveUpdate();
        jumpingAndLanding();
        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    
    }

    private void moveUpdate()
    {

        Vector2 move = MoveValue.action?.ReadValue<Vector2>() ?? Vector2.zero;
        Vector2 turn = TurnValue.action?.ReadValue<Vector2>() ?? Vector2.zero;
        if (move != Vector2.zero || turn !=Vector2.zero)
        {
            float val = move != null ? move.magnitude : turn.magnitude;
            m_animator.SetFloat("MoveSpeed", 1f);
        }
        else
        {
            m_animator.SetFloat("MoveSpeed", 0);
        }
        
     
    }

    private void jumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;
        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
    
    // 아이템 관련 코드 


    [PunRPC]
    public void MultiplySpeed(float multiplySpeed, float multiplyTime)
    {

 
        moveProvider.moveSpeed *= multiplySpeed;
        StartCoroutine(backToNormal(multiplyTime));
    }

    [PunRPC]
    public void MultiplySize(float multiplySize, float multiplyTime)
    {
 
        gameObject.transform.localScale *= multiplySize;
        StartCoroutine(backToNormal(multiplyTime));
    }

    [PunRPC]
    public void MultiplyJump(float multiplyJump, float multiplyTime)
    {
        m_jumpForce *= multiplyJump;
        StartCoroutine(backToNormal(multiplyTime));
    }

    [PunRPC]
    public void TurnOnEffect(float multiplyTime)
    {
        effect.SetActive(true);
        StartCoroutine(backToNormal(multiplyTime));
    }

    IEnumerator backToNormal(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("원래값으로 돌아가기"+originalJump+","+originalSize+","+originalSpeed);
        moveProvider.moveSpeed = 2;
        m_jumpForce = 4;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        transform.position += Vector3.up * 2;
        effect.SetActive(false);
        
        photonView.RPC("ChangeUsingItem", RpcTarget.All, false);
    }
    
    [PunRPC]
    public void ChangeUsingItem(bool state)
    {
        usingItem = state;
    }
    
}
