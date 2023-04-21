// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCoordinateTimezoneData : CsvData
{
    public string name { get; set; } // 도시 이름
    public string countryName { get; set; } // 국가 이름
    public float lat { get; set; } // 도시 위도
    public float lng { get; set;  } // 도시 경도
    public float timezone { get; set; } // 타임존 (기준: 대한민국)
    
    public override void SetData(string[] data)
    {
        name = data[1];
        countryName = data[4];
        lat = float.Parse(data[2]);
        lng = float.Parse(data[3]);
        timezone = float.Parse(data[5]);
    }
}
