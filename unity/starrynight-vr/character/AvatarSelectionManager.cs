// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
public class AvatarSelectionManager : MonoBehaviour
{
    [SerializeField]
    GameObject AvatarSelectionPlatformGameobject;

    [SerializeField]
    GameObject VRPlayerGameobject;


    public GameObject[] selectableAvatarModels;
    public GameObject[] loadableAvatarModels;

    public int selectedAvatarIndex = 0;

    public AvatarInputConverter avatarInputConverter;


    public static AvatarSelectionManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        //If we do not have any selected Avatar, we simply use the default one which is the first Avatar in the SelectableAvatarModels list.
        selectedAvatarIndex = 0;
        ActivateAvatarModelAt(selectedAvatarIndex);


    }

    public void NextAvatar()
    {
        selectedAvatarIndex += 1;
        if (selectedAvatarIndex >= selectableAvatarModels.Length)
        {
            selectedAvatarIndex = 0;
        }
        ActivateAvatarModelAt(selectedAvatarIndex);

    }

    public void PreviousAvatar()
    {
        selectedAvatarIndex -= 1;

        if (selectedAvatarIndex < 0)
        {
            selectedAvatarIndex = selectableAvatarModels.Length - 1;
        }
        ActivateAvatarModelAt(selectedAvatarIndex);
        
    }

    private void ActivateAvatarModelAt(int avatarIndex)
    {
        foreach (GameObject selectableAvatarModel in selectableAvatarModels)
        {
            selectableAvatarModel.SetActive(false);
        }

        selectableAvatarModels[avatarIndex].SetActive(true);
        Debug.Log(selectedAvatarIndex);
        LoadAvatarModelAt(selectedAvatarIndex);
    }

    private void LoadAvatarModelAt(int avatarIndex)
    {
        
        ExitGames.Client.Photon.Hashtable playerSelectionProp = new ExitGames.Client.Photon.Hashtable() { { "selectedAvatarIndex", selectedAvatarIndex } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerSelectionProp);
    }
}
