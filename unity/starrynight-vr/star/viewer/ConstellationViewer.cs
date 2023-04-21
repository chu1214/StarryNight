// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using System.Linq;
using UnityEngine;

public class ConstellationViewer : MonoBehaviourPun, IPunObservable
{
    // 별자리 CSV 데이터
    [SerializeField] TextAsset starDataCSV;
    [SerializeField] TextAsset starMajorDataCSV;
    [SerializeField] TextAsset starNameDataCSV;
    [SerializeField] TextAsset constellationNameDataCSV;
    [SerializeField] TextAsset constellationPositionDataCSV;
    [SerializeField] TextAsset constellationLineDataCSV;
    [SerializeField] TextAsset starOfConstellationCSV;

    [SerializeField] GameObject constellationPrefab; // 별자리의 프리팹

    // 별자리 데이터
    List<StarData> starData;
    List<StarMajorData> starMajorData;
    List<StarNameData> starNameData;
    List<ConstellationNameData> constellationNameData;
    List<ConstellationPositionData> constellationPositionData;
    List<ConstellationLineData> constellationLineData;
    List<StarOfConstellationData> starOfConstellationData;

    private RotatingController rotatingController;
    
    // 정리한 별자리의 데이터
    private List<ConstellationData> constellationData;
    
    // 시작 도시
    public string currentCity = "서울";

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(currentCity);
        }
        else
        {
            
            string cityname = (string) stream.ReceiveNext();
            if (cityname != currentCity)
            {
                currentCity = cityname;
                Rotating(currentCity);
            }
        }
    }
    
    void Start()
    {
        // CSV 데이터 읽기
        LoadCSV();

        // 별자리 데이터의 정리
        ArrangementData();

        // 별자리의 작성
        CreateConstellation();

        rotatingController = FindObjectOfType<RotatingController>();
        Rotating(currentCity);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, ((365.0f + (365.0f/360.0f)) / 360.0f) * Time.deltaTime, 0) * Time.deltaTime);
        GameObject[] constellationNames = GameObject.FindGameObjectsWithTag("ConstellationName");
        foreach (var constellationName in constellationNames)
        {
            constellationName.transform.eulerAngles = new Vector3(constellationName.transform.eulerAngles.x,
                constellationName.transform.eulerAngles.y, 0.0f);
        }
    }

    public void MultiRotating(string name)
    {
        photonView.RPC("Rotating", RpcTarget.All, name);
    }
    
    // 위도 경도 기준 시간 조절 및 회전
    [PunRPC]
    public void Rotating(string name) {
        if (PhotonNetwork.IsMasterClient)
        {
            currentCity = name;
        }
        rotatingController.ChangeCity(name);
    }

    // CSV 데이터 읽기
    void LoadCSV()
    {
        starData = CsvLoader<StarData>.LoadData(starDataCSV);
        starMajorData = CsvLoader<StarMajorData>.LoadData(starMajorDataCSV);
        starNameData = CsvLoader<StarNameData>.LoadData(starNameDataCSV);
        constellationNameData = CsvLoader<ConstellationNameData>.LoadData(constellationNameDataCSV);
        constellationPositionData = CsvLoader<ConstellationPositionData>.LoadData(constellationPositionDataCSV);
        constellationLineData = CsvLoader<ConstellationLineData>.LoadData(constellationLineDataCSV);
        starOfConstellationData = CsvLoader<StarOfConstellationData>.LoadData(starOfConstellationCSV);
    }

    // 별자리 데이터의 정리
    void ArrangementData()
    {
        // 별 데이터를 통합
        MergeStarData();

        constellationData = new List<ConstellationData>();

        // 별자리 이름으로부터 별자리에 필요한 데이터를 수집
        foreach (var name in constellationNameData)
        {
            constellationData.Add(CollectConstellationData(name));
        }

        // 별자리에 사용되지 않는 별의 수집
        var data = new ConstellationData();
        data.Stars = starData.Where(s => s.UseConstellation == false).ToList();
        constellationData.Add(data);
    }

    // 별 데이터를 통합
    void MergeStarData()
    {
        // 이번에 사용할 필요한 별을 판별한다
        foreach (var star in starMajorData)
        {
            // 같은 데이터가 있는가?
            var data = starData.FirstOrDefault(s => star.Hip == s.Hip);
            if (data != null)
            {
                // 같은 데이터가 있으면, 위치 데이터를 갱신한다
                data.RightAscension = star.RightAscension;
                data.Declination = star.Declination;
            }
            else
            {
                // 같은 데이터가 없는 경우, 10등성보다 밝으면 리스트 목록에 추가
                if (star.ApparentMagnitude <= 10.0f)
                {
                    starData.Add(star);
                }
            }
        }
        
        // 별의 이름을 붙여준다.
        foreach (var star in starNameData)
        {
            // 이름이 있는 별인지 확인
            var data = starData.FirstOrDefault(s => star.Hip == s.Hip);
            
            // 이름이 있다면
            if (data != null)
            {
                // 한글 이름을 넣어준다.
                data.StarName = star.KoreanName;
            }
        }
        
        // 어떤 별자리의 몇 번째 별인지 넣어준다.
        foreach (var star in starOfConstellationData)
        {
            // 별자리 구성원인 별인지 확인
            var data = starData.FirstOrDefault(s => star.Hip == s.Hip);

            // 구성원인 별이라면
            if (data != null)
            {
                // 별자리 약칭을 가지고 한글 이름을 가져온다.
                var conName = constellationNameData.FirstOrDefault(s => star.ConstellationShort == s.Summary);
                
                // 넣어주기
                data.ConstellationPart = conName.KoreanName + "자리 " + star.StarNum;
            }
        }
    }

    // 별자리 데이터의 수집
    ConstellationData CollectConstellationData(ConstellationNameData name)
    {
        var data = new ConstellationData();

        // 별자리의 이름 등록
        data.Name = name;

        // 별자리 ID가 같은 것을 등록
        data.Position = constellationPositionData.FirstOrDefault(s => name.Id == s.Id);

        // 별자리 약칭이 같은 것을 등록
        data.Lines = constellationLineData.Where(s => name.Summary == s.Name).ToList();

        // 별자리 선이 사용하고 있는 별을 등록
        data.Stars = new List<StarData>();
        foreach (var line in data.Lines)
        {
            var start = starData.FirstOrDefault(s => s.Hip == line.StartHip);
            data.Stars.Add(start);
            var end = starData.FirstOrDefault(s => s.Hip == line.EndHip);
            data.Stars.Add(end);

            // 별자리로 사용되는 별
            start.UseConstellation = end.UseConstellation = true;
        }

        return data;
    }

    // 별자리의 작성
    void CreateConstellation()
    {
        // 각 별자리를 작성
        foreach (var data in constellationData)
        {
            var constellation = Instantiate(constellationPrefab);
            var drawConstellation = constellation.GetComponent<DrawConstellation>();

            drawConstellation.ConstellationData = data;

            // 자신의 자식으로 한다
            constellation.transform.SetParent(transform, false);
        }
    }
}