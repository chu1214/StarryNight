// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
public class StarNameData : CsvData
{
    public int Hip { get; set; }             // 별자리 ID
    public string Name { get; set; }        // 영어명
    public string KoreanName { get; set; }  // 한글명

    public override void SetData(string[] data)
    {
        Hip = int.Parse(data[0]);
        Name = data[1];
        KoreanName = data[2];
    }
}
