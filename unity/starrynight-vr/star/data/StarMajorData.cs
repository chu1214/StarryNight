// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
public class StarMajorData : StarData
{
    public override void SetData(string[] data)
    {
        Hip = int.Parse(data[0]);
        RightAscension = RightAscensionToDegree(int.Parse(data[1]), int.Parse(data[2]), float.Parse(data[3]));
        var plusMinus = -1.0f;
        if (data[4] == "1")
        {
            plusMinus = 1.0f;
        }
        Declination = DeclinationToDegree(plusMinus, int.Parse(data[5]), int.Parse(data[6]), float.Parse(data[7]));
        ApparentMagnitude = float.Parse(data[8]);
        ColorType = "A";    // 데이터가 없기 때문에 흰색으로 고정
    }
}