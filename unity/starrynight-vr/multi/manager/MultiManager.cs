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

public class MultiManager : MonoBehaviourPunCallbacks,IPunObservable
{
    public static MultiManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<MultiManager>();
            }

            return m_instance;
        }
    }

    private static MultiManager m_instance;

    public GameObject playerPrefab;
    public GameObject playerNickname;
    public string characterName;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        // 로컬 오브젝트라면 쓰기 부분이 실행됨
    
    }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Vector3 randomSpawnPos = new Vector3(84.7f, 30, 56f);

        Hashtable table = PhotonNetwork.LocalPlayer.CustomProperties;

        characterName = (string)table["CharacterName"];

        playerPrefab = Resources.Load<GameObject>(characterName);

        Debug.Log(characterName);
        Debug.Log(playerPrefab);
        Debug.Log(playerPrefab.name);
        
        GameObject player= PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Good");
            PhotonNetwork.LeaveRoom();
        }
    }
    
    public override void OnLeftRoom()
    {
        Debug.Log("Bye");
        SceneManager.LoadScene("Lobby");
    }
}
