// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MultiManagerVR : MonoBehaviourPunCallbacks,IPunObservable
{
    public static MultiManagerVR instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<MultiManagerVR>();
            }

            return m_instance;
        }
    }

    private static MultiManagerVR m_instance;

    public GameObject playerPrefab;
    public GameObject playerNickname;
    public string characterName;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        // 로컬 오브젝트라면 쓰기 부분이 실행됨
    
    }

    private void Awake()
    {
        Debug.Log("실행 ");
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Vector3 randomSpawnPos = new Vector3(85.7f, 30, 51.9f);

        Hashtable table = PhotonNetwork.LocalPlayer.CustomProperties;
        var selectedIndex = (int)table["selectedAvatarIndex"];
        String name = "VRPlayer" + (selectedIndex + 1);
      
        Debug.Log("멀티 매니저 시작 ");
        GameObject player= PhotonNetwork.Instantiate(name, randomSpawnPos, Quaternion.identity);
        Debug.Log("캐릭터 생성 ",player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
