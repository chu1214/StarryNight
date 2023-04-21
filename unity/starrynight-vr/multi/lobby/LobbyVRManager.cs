// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyVRManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    public Text connectionInfoText;
    public Button joinButton;
    public InputField nicknameInput;

    public GameObject selectedMark;
    public string selectedName;
    public string characterName;

    void Start()
    {
        // 접속에 필요한 게임 버전 설정
        PhotonNetwork.GameVersion = gameVersion;
        // 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();
        
        // 접속 버튼 비활성화
        joinButton.interactable = false;
        // 접속 시도 중 텍스트 표시
        connectionInfoText.text = "마스터 서버에 접속 중...";
        
        // 닉네임 입력창 비활성화 
        nicknameInput.interactable = false;
        
        // 기본 캐릭터 반영
        selectedName = "Male1";
        characterName = "Male1";

    }

    public override void OnConnectedToMaster()
    {
        // 접속 버튼 활성화
        joinButton.interactable = true;
        //닉네임 입력창 비활성화 
        nicknameInput.interactable = true;
        // 접속 정보 표시
        connectionInfoText.text = "마스터 서버와 연결됨";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // 접속 버튼 비활성화
        joinButton.interactable = false;
        // 접속 정보 표시
        connectionInfoText.text = "마스터 서버와 연결되지 않음\n재접속 시도중...";
        
        // 마스터 서버 재접속
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {

        // 중복 접속 시도를 막기 위해 접속 튼 비활성화
        joinButton.interactable = false;
        //닉네임 입력창 비활성화 
        // nicknameInput.interactable = false;
        //
        // //닉네임을 입력하지 않았다면 연결하지 않음.
        // if (nicknameInput.text == "")
        // {
        //     connectionInfoText.text = "닉네임을 입력해주세요.";
        //     nicknameInput.interactable = true;
        //     joinButton.interactable = true;
        //     return;
        // }
        Debug.Log("접속 버튼 클릭 ");
        PhotonNetwork.LocalPlayer.NickName = "VRUser1";
        // PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable{ { "CharacterName", characterName } });


    // 마스터 서버에 접속 중이라면
    if (PhotonNetwork.IsConnected)
        {
            // 방 접속 실행
            connectionInfoText.text = "방에 접속 중...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // 마스터 서버에 접속 중이 아니라면 마스터 서버에 접속 시도
            connectionInfoText.text = "마스터 서버와 연결되지 않음\n재접속 시도중...";
            // 마스터 서버 재접속
            PhotonNetwork.ConnectUsingSettings();
            
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 접속 상태 표시
        connectionInfoText.text = "새로운 방 생성 중...";
        // 최대 20명 수용이 가능한 빈 방 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 20 });
    }

    public override void OnJoinedRoom()
    {
        // 접속 상태 표시
        connectionInfoText.text = "방 참가 성공";
        // 모든 참가자가 Constellation 씬을 로드
        PhotonNetwork.LoadLevel("Constellation5");
    }

    public void ChoiceCharacter(string selectedName)
    {
        this.selectedName = selectedName;

        if (selectedName.Equals("Male1"))
        {
            selectedMark.transform.localPosition = new Vector3(-216, 124, 0);
        } else if (selectedName.Equals("Male2"))
        {
            selectedMark.transform.localPosition = new Vector3(-70, 124, 0);
        }else if (selectedName.Equals("Male3"))
        {
            selectedMark.transform.localPosition = new Vector3(80, 124, 0);
        }else if (selectedName.Equals("Male4"))
        {
            selectedMark.transform.localPosition = new Vector3(232, 124, 0);
        }else if (selectedName.Equals("Female1"))
        {
            selectedMark.transform.localPosition = new Vector3(-211, -82, 0);
        }else if (selectedName.Equals("Female2"))
        {
            selectedMark.transform.localPosition = new Vector3(-69, -82, 0);
        }else if (selectedName.Equals("Female3"))
        {
            selectedMark.transform.localPosition = new Vector3(80, -82, 0);
        }else if (selectedName.Equals("Female4"))
        {
            selectedMark.transform.localPosition = new Vector3(223, -82, 0);
        }
    }

    public void Choice()
    {
        this.characterName = this.selectedName;
    }
    
}
