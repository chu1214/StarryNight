// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class TPScharacterController : MonoBehaviourPun
{
    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform cameraArm;
    
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody m_rigidBody;
    
    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    [SerializeField] private bool m_jumpInput = false;

    [SerializeField] private bool m_isGrounded;

    private Camera mainCamera;
    private List<Collider> m_collisions = new List<Collider>();
    
    void Start()
    {
        //animator = characterBody.GetComponent<Animator>();
        //m_rigidBody = characterBody.GetComponent<Rigidbody>();
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        animator.SetBool("Grounded", m_isGrounded);
        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
        Move();
        LookAround();
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

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        //animator.SetBool("isMove",isMove);
    
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            //transform.position += moveDir * Time.deltaTime * 5f;

            m_currentV = Mathf.Lerp(m_currentV, moveInput.y, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, moveInput.x, Time.deltaTime * m_interpolation);

            transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);
           // cameraArm.transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);
       
         
            animator.SetFloat("MoveSpeed", m_currentV);
        JumpingAndLanding();
    }

    private void LookAround()
    {
       
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

     //   Vector3 dir = mainCamera.transform.localRotation * Vector3.forward;
       // transform.localRotation = mainCamera.transform.localRotation;
    
      //cameraArm.rotation=Quaternion.Euler(camAngle.x,mainCamera.transform.localRotation.y,camAngle.z);
  
    // mainCamera.transform.Rotate(transform.rotation);
     
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }
}
