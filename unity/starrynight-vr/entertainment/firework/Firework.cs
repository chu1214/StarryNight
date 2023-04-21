// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Random = System.Random;

public class Firework : MonoBehaviourPun
{

    public String[] Fires;
    private int Fire;
    private GameObject Instance;
    
    // Start is called before the first frame update
    void Start()
    {
        Fires = new String[]
        {
            "Fireworks bees",
            "Fireworks blue pion",
            "Fireworks comet 2",
            "Fireworks comet",
            "Fireworks crossover",
            "Fireworks georgine",
            "Fireworks gold",
            "Fireworks honey",
            "Fireworks lilac",
            "Fireworks mine 2",
            "Fireworks mine",
            "Fireworks minion",
            "Fireworks osier",
            "Fireworks palm",
            "Fireworks pion",
            "Fireworks rainbow",
            "Fireworks red pion",
            "Fireworks scarlet",
            "Fireworks violet",
            "Fireworks with flares whites"
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject canvas = gameObject.transform.Find("Canvas").gameObject;
        canvas.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        GameObject canvas = gameObject.transform.Find("Canvas").gameObject;
        canvas.SetActive(false);
    }

    public void Fireworking()
    {
        Random rand = new Random();
        Fire = rand.Next(0, Fires.Length);

        GameObject obj = Resources.Load<GameObject>(Fires[Fire]);
        
        if(Instance != null) PhotonNetwork.Destroy(Instance);
        {
            Instance = PhotonNetwork.Instantiate(obj.name, gameObject.transform.position,  Quaternion.identity);
        }

    }
}
