// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

public class StarcoinRes{
    public bool isTaken;
    public int starcoinNum;
}
public class StarcoinReq{
    public int starcoinNum;
}
public class StoryReq{
    public int storyNum;
}

public class StarCoinManager : MonoBehaviour
{
    string originalUrl = "{api url}";
    int TotalStarcoinCount;
    int CurrentStarcoinCount;
    public Text starCoinCountText;
    public Text playerCountText;
    int memberId;
    int storyId = {스토리 번호에 따라 변경};
    DontDestroyObject dontDestroyObject;

    [DllImport("__Internal")]
    private static extern void GameOver(int userId);
    public void EndGameMethod () {
    #if UNITY_WEBGL == true && UNITY_EDITOR == false
        GameOver(memberId);
    #endif
    }
    [DllImport("__Internal")]
    private static extern void GameStart();
    public void StartGameMethod () {
    #if UNITY_WEBGL == true && UNITY_EDITOR == false
        GameStart();
    #endif
    }

    public void requestGameExit(){
        Application.Quit();
    }

    public void getPlayerId(int id){
        memberId = id;
        dontDestroyObject.memberId = memberId;
        StartCoroutine(ReadStarcoin());
    }
    // 1. memberId 를 들고 옴
    // 2. memberId를 dontDestroyObject.memberId에 저장
    // 3. 새로운 Scene에서 dontDestroyObject의 memberId를 memberId로 사용
    

    void Start(){
        dontDestroyObject = FindObjectOfType<DontDestroyObject>();
        if (dontDestroyObject.memberId > 0){
            memberId = dontDestroyObject.memberId;
        }
        else {
            dontDestroyObject.memberId = memberId;
        }

        //DB에 접근하여 별 띄우기
        StartCoroutine(ReadStarcoin());
    }

    public void UpdateStarcoin(int starcoinNum){
        //필요한 데이터 정보 : 회원번호, 스토리번호, 스타코인번호
        StarcoinReq star = new StarcoinReq{
            starcoinNum = starcoinNum
        };

        string json = JsonUtility.ToJson(star);
        
        string url = originalUrl + "/game/starcoin/get/id/"+memberId+"/story/{story}?story="+storyId;
        
        //request Post
        StartCoroutine(requestStarcoin(url, json));
    }

    public void UpdateStoryClear(){
        StoryReq story = new StoryReq{
            storyNum = storyId
        };
        
        string json = JsonUtility.ToJson(story);
        string url = originalUrl + "/game/story/clear/id/"+memberId;
        //request Post
        StartCoroutine(requestStory(url, json));
    }

    IEnumerator ReadStarcoin(){
        //필요한 데이터 정보 : 회원번호, 스토리번호

        string url = originalUrl + "/game/starcoin/list/id/"+memberId+"/story/{story}?story="+storyId;

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();
        //Response Success
        if(www.error == null){
            Debug.Log("[SUCCESS] Read Starcoin");
            //Response 데이터 정리하기
            JObject obj = JObject.Parse(www.downloadHandler.text);
            TotalStarcoinCount = (int) obj["count"];    //별의 총 개수 찾기
            Debug.Log("WWW : " + TotalStarcoinCount);
            for(int i =0; i<TotalStarcoinCount; i++){   //정리된 별 정보 하나씩 꺼내보기
                JObject temp = (JObject) obj["starcoins"][i];
                bool isTaken = (bool) temp["isTaken"];
                int starcoinNum = (int) temp["starcoinNum"];
                string starcoinNumStr;
                if(starcoinNum < 10){
                    starcoinNumStr = "0" + starcoinNum;
                }
                else{
                    starcoinNumStr = "" + starcoinNum;
                }
                if(isTaken == true){ //별의 번호(id) 값이 true라면 이미 획득한 별이다.
                    GameObject star = GameObject.Find("StarCoin (" + starcoinNumStr + ")"); //해당 별을 찾기
                    star.SetActive(false);  // inactive 처리하기
                    CurrentStarcoinCount++;
                }
            }
            //UI에 코인 개수 체크하기
            starCoinCountText.text = "/ " + TotalStarcoinCount; //전체 코인 개수 체크
            playerCountText.text = CurrentStarcoinCount.ToString();
        }
        //Response fail
        else{
            Debug.Log("[ERROR] Read Starcoin");
        }
    }

    IEnumerator requestStarcoin(string URL, string json){
        using (UnityWebRequest www = UnityWebRequest.Put(URL, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler.Dispose();
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            //Response Success
            if(www.error == null){
                Debug.Log("[SUCCESS] Update Starcoin"); 
                CurrentStarcoinCount++;
                playerCountText.text = CurrentStarcoinCount.ToString();
            }
            //Response Fail
            else{
                Debug.Log("[ERROR] Update Starcoin");
            }
            

            www.Dispose();
        }
    }

    IEnumerator requestStory(string URL, string json){
        using (UnityWebRequest www = UnityWebRequest.Put(URL, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler.Dispose();
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            //Response Success
            if(www.error == null){
                Debug.Log("[SUCCESS] Story Clear!"); 
            }
            //Response Fail
            else{
                Debug.Log("[ERROR] Story not Clear");
            }
            

            www.Dispose();
        }
    }
}