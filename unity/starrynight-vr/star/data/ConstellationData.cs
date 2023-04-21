// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using System.Collections.Generic;

public class ConstellationData
{
    public ConstellationNameData Name;          // 별자리 이름의 데이터
    public ConstellationPositionData Position;  // 별자리 위치의 데이터
    public List<StarData> Stars;                // 별자리 데이터
    public List<ConstellationLineData> Lines;   // 별자리 선의 데이터
}