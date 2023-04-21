// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Transform target;
    public StarCoinManager starCoinManager;
    public AudioSource audio;
    
    public Material mat;

    void OnTriggerEnter(Collider other){
        //플레이어가 별과 닿으면
        if(other.tag == "StarCoin"){
            getStarCoin(other);
            //changeWinterToSpring();
        }
        //저승으로 향하는 포탈과 닿으면
        else if (other.name == "PortalToHell"){
            goToHell();
        }
        //이승으로 향하는 포탈과 닿으면
        else if (other.name == "PortalToVillage"){
            goToVillage();
        }
    }

    void getStarCoin(Collider other){
            //DB에 접근 -> 해당 스타코인 먹었다는 표시
            audio.Play();
            other.gameObject.SetActive(false);  //해당 스타코인 비활성화처리
            string str = other.gameObject.name; //스타코인의 이름
            int starcoinNum = Int32.Parse(str.Substring(str.Length-3, 2));  //해당 스타코인 번호 찾기
            starCoinManager.UpdateStarcoin(starcoinNum);
    }

    static public void changeWinterToSpring(){
        Color newColor = new Color(15f/255f, 174f/255f, 0);
            //나무 흰색에서 초록색으로 변경
            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            foreach (GameObject tree in trees){
                Material[] material = tree.GetComponent<MeshRenderer>().materials;
                try{
                    //material이 두 개인 나무면 첫 번째(몸통)가 아닌 두 번째(나뭇잎)의 색을 초록색으로 설정
                    material[1].color = newColor;
                }
                catch{
                    //material이 한 개뿐인 나무면 해당 material(나뭇잎)의 색을 초록색으로 설정
                    material[0].color = newColor;
                }
            }
            GameObject plane = GameObject.Find("VillagePlane");
            plane.GetComponent<MeshRenderer>().material.color = newColor;
    }

    void goToHell(){
        //페이드아웃 추가
            transform.position = new Vector3(-15, 1, -65); //저승 맵의 특정 위치로 이동
    }

    void goToVillage(){
            transform.position = new Vector3(-5, 1, 70);   //이승 맵의 특정 위치로 이동

    }

    public void SetSoundVolume(float volume)
    {
        audio.volume = volume; //소리 크기 조절
    }
}
