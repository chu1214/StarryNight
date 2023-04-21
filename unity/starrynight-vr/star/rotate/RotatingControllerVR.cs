// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Networking;

public class RotatingControllerVR : MonoBehaviour
{
    [SerializeField] TextAsset cityCoordinateTimezoneCSV;
    List<CityCoordinateTimezoneData> cityCoordinateTimezoneData;
    public string url = "www.naver.com";
    public string date;
    
    public DateTime dateTime;
    public DateTime originTime;
    public float lat;
    public float lng;
    public Vector3 RotatingAngleVector3;

    private Coroutine runningCoroutine = null;
    public UIManager uiManager;

    void LoadCSV()
    {
        cityCoordinateTimezoneData = CsvLoader<CityCoordinateTimezoneData>.LoadData(cityCoordinateTimezoneCSV);
    }
    
    
    void Awake()
    {
        LoadCSV();
      
    }

    private void Start()
    {
        InvokeRepeating("addSecond", 0, 1f);
        uiManager = UIManager.instance;
    }

    public IEnumerator WebChk(float timezone, string cityName) {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else {
                // 시간 변환
                date = request.GetResponseHeader("date"); //이곳에서 반송된 데이터에 시간 데이터가 존재
                dateTime = DateTime.Parse(date).ToLocalTime(); // ToLocalTime() 메소드로 한국시간으로 변환시켜 준다.
                originTime = dateTime.AddHours(-9);
                dateTime = dateTime.AddHours((double)timezone);

                // 회전 일치
                RotatingAngleVector3 = new Vector3(90.0f - lat, 180.0f + (float)HA(), 0);
                Quaternion x = Quaternion.Euler(RotatingAngleVector3.x, 0, 0);
                Quaternion y = Quaternion.Euler(0, RotatingAngleVector3.y, 0);
                ConstellationViewer constellationViewer = FindObjectOfType<ConstellationViewer>();
                constellationViewer.transform.rotation = x * y;
                
                // UIManager에 시간 전달
                uiManager.UpdateDateTimeText(dateTime);
            }
        }
    }

    void addSecond()
    {
        dateTime = dateTime.AddSeconds(1);
        // UIManager에 시간 전달
        uiManager.UpdateDateTimeText(dateTime);
    }
    
    public static bool isJulianDate(int year, int month, int day)
    {
        // 1582년 이전의 모든 날짜 : 율리우스력
        if (year < 1582)
            return true;
        // 1582년 이후의 모든 날짜 : 그레고리력
        else if (year > 1582)
            return false;
        else
        {
            // 1582년인 경우 10월 4일 이전은 율리우스력 15일 이후는 그레고리력
            if (month < 10)
                return true;
            else if (month > 10)
                return false;
            else
            {
                if (day < 5)
                    return true;
                else if (day > 14)
                    return false;
                else
                    // 1582년 10월 5일에서 1582년 10월 14일 사이의 모든 날짜는 유효하지 않음
                    throw new ArgumentOutOfRangeException(
                        "유효하지 않은 날짜입니다.");
            }
        }
    }

    private double DateToJD(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
        // 날짜를 기준으로 달력 결정(율리우스력 or 그레고리력)
        bool JulianCalendar = isJulianDate(year, month, day);

        int M = month > 2 ? month : month + 12;
        int Y = month > 2 ? year : year - 1;
        double D = day + hour / 24.0 + minute / 1440.0 + (second + millisecond / 1000.0) / 86400.0;
        int B = JulianCalendar ? 0 : 2 - Y / 100 + Y / 100 / 4;

        return (int)(365.25 * (Y + 4716)) + (int)(30.6001 * (M + 1)) + D + B - 1524.5;
    }

    public double JD()
    {
        DateTime date = originTime;
        return DateToJD(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
    }
    
    public void ChangeCity(string name) 
    {
        Debug.Log("지역 변경"+name);
        // 시간 및 회전 조절
        foreach (var data in cityCoordinateTimezoneData)
        {
            if (data.name.Equals(name))
            {
                lat = data.lat;
                lng = data.lng;
                
                if(runningCoroutine != null)
                {
                    StopCoroutine(runningCoroutine);
                }
                runningCoroutine = StartCoroutine(WebChk(data.timezone, data.name));
                
                // uiManager에 시간 전달
                Debug.Log(data.name);
                uiManager.UpdateCityText(data.name);
            }
        }
    }
    
    double HA()
    {
        double latToRad = (Math.PI / 180.0) * lat;
        double lngToRad = (Math.PI / 180.0) * lng;

        double gmst = greenwichMeanSiderealTime(JD());
        double localSiderealTime=(gmst+lngToRad)%(2*Math.PI);

        double H=(localSiderealTime - 0);
        if(H<0){H+=2*Math.PI;}
        if(H>Math.PI){H=H-2*Math.PI;}

        double az = (Math.Atan2(Math.Sin(H), Math.Cos(H)*Math.Sin(latToRad) - Math.Tan((Math.PI / 180.0) * 90)*Math.Cos(latToRad)));
        az-=Math.PI;

        if(az<0){az+=2*Math.PI;}
        
        return H * 180.0f / Math.PI;
    }
    
    double greenwichMeanSiderealTime(double jd){
        // IAU 2000 세차 수량에 대한 식
        double t = ((jd - 2451545.0)) / 36525.0;

        double gmst=this.earthRotationAngle(jd)+(0.014506 + 4612.156534*t + 1.3915817*t*t - 0.00000044 *t*t*t - 0.000029956*t*t*t*t - 0.0000000368*t*t*t*t*t)/60.0/60.0*Math.PI/180.0;  //eq 42
        gmst%=2*Math.PI;
        if(gmst<0) gmst+=2*Math.PI;

        return gmst;
    }

    double earthRotationAngle(double jd){

        double t = jd- 2451545.0;
        double f = jd%1.0;

        double theta = 2 * Math.PI * (f + 0.7790572732640 + 0.00273781191135448 * t); 
        theta%=2*Math.PI;
        if(theta<0)theta+=2*Math.PI;
        
        return theta;
    }
}
