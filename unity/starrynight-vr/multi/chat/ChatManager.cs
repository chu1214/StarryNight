// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPun
{
    public string username;
    public int maxMessages = 25;
    public Font chatFont;
    public GameObject chatPanel, chatEntry;
    public InputField chatBox;
    public ScrollRect scrollRect;

    public Color mine;
    public Color others;

    [SerializeField] private List<Message> messageList = new List<Message>();
    void Start()
    {
        username = PhotonNetwork.NickName;
        photonView.RPC("joinMessageToChat",RpcTarget.All,PhotonNetwork.NickName);
    }

   
    void Update()
    {
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                photonView.RPC("sendMessageToChat",RpcTarget.All,PhotonNetwork.NickName,chatBox.text);
                chatBox.text = "";
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }
    }
    
    [PunRPC]
    public void joinMessageToChat(string sender)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
        Message newMessage = new Message(); 
        newMessage.text = "[시스템] " +sender+"님이 입장하셨습니다.";

        GameObject newText = Instantiate(chatEntry, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;

        newMessage.textObject.color = Color.red;
        newMessage.textObject.font = chatFont;
        newMessage.textObject.fontSize = 20;
        messageList.Add(newMessage);
        
        Invoke("MoveScrollToBottom", 0.1f);
    }

    [PunRPC]
    public void sendMessageToChat(string sender,string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
        Message newMessage = new Message(); 
        newMessage.text = sender+": "+text;

        GameObject newText = Instantiate(chatEntry, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;

        newMessage.textObject.color = MessageTypeColor(sender);
        newMessage.textObject.font = chatFont;
        newMessage.textObject.fontSize = 20;
        messageList.Add(newMessage);
        
        Invoke("MoveScrollToBottom", 0.1f);
    }

    Color MessageTypeColor(string sender)
    {
        Color color = sender == username ? mine : others;
        return color;
    }

    void MoveScrollToBottom()
    {
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
}

[System.Serializable]
public class Message 
{
    public string text;
    public Text textObject;

}
