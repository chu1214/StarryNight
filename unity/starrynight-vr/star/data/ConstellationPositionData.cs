// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
public class ConstellationPositionData : CsvData
{
    public int Id { get; set; }                 // 별자리 ID
    public float RightAscension { get; set; }   // 적경
    public float Declination { get; set; }      // 적위

    public override void SetData(string[] data)
    {
        Id = int.Parse(data[0]);
        RightAscension = RightAscensionToDegree(int.Parse(data[1]), int.Parse(data[2]));
        Declination = DeclinationToDegree(int.Parse(data[3]));
    }
}