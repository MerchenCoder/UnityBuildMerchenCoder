using System;
using System.Linq;

[Serializable]
public class GameData
{
    public bool[] chapterIsUnlock = new bool[] { true, false };

    //한 번에 초기화 IEnumberable<dataType>(반복할 값, 반복할 횟수).ToArray();
    public bool[] ch1MissionClear = Enumerable.Repeat(false, 15).ToArray();
}