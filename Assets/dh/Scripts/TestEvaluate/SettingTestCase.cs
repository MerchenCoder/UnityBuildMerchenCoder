using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SettingTestCase : MonoBehaviour
{
    [Serializable]
    public struct TestCaseData
    {
        public bool hasTestCase;
        public int inputNum;
        public List<InputInfo> inputInfo;

        public struct InputInfo
        {
            public string name;
            public string type;
        }
        public int testCaseLength;
        public Dictionary<string, TestCase> testCaseInput;

        //일단 input output 모두 문자열로 받아오기
        public struct TestCase
        {
            public List<string> input;
            public List<string> output;
        }

    }

    //문제별 테스트 케이스 파일 이름을 어떻게 할지 정해야함
    public string TestCaseDataFileName;



    //테스트 케이스 데이터를 가져온다.
    //테스트 케이스가 존재하면 리스트에 차례로 넣어준다.
    //run을 위해 -> 첫 번째 테스트 케이스를 변수에 할당한다.
    private void Awake()
    {

        //Json 파일 경로
        string jsonFilePath = Application.dataPath + "/Data/" + TestCaseDataFileName;

        if (File.Exists(jsonFilePath))
        {
            //Json 파일을 문자열로 읽어오기
            string jsonString = File.ReadAllText(jsonFilePath);
            // JSON 문자열을 UnityTestCase 구조체로 역직렬화
            TestCaseData testCaseData = JsonUtility.FromJson<TestCaseData>(jsonString);
            Debug.Log("테스트 케이스 데이터 불러오기 완료");

        }
        else
        {
            Debug.Log("Can't Find Test Case Data");
        }
    }


    private void Start()
    {

    }

    //제출시 채점 실행
    public void Test()
    {

    }


    //정답 채크
    public bool CheckAnswer()
    {
        return true;
    }



    //맞았으면 json에 clear여부를 저장하든지.. 추후에 로직 추가 

}
