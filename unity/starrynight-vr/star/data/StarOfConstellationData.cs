// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
public class StarOfConstellationData : CsvData
{
    public int Hip { get; set; }    // Hip 번호
    public string StarNum { get; set; }   // 별자리 내부 번호
    public string ConstellationShort { get; set; }     // 별자리 약자

    public override void SetData(string[] data)
    {
        Hip = int.Parse(data[0]);
        StarNum = data[1];
        ConstellationShort = data[2];
    }
}
