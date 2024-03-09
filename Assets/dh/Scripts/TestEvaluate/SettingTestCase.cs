using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using Newtonsoft.Json;
public class SettingTestCase : MonoBehaviour
{
    [Serializable]
    public struct TestCaseData
    {
        public bool hasTestCase;
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

    //입력 노드 프리팹
    public GameObject inputNodeBtn_int;
    public GameObject inputNodeBtn_bool;
    public GameObject inputNodeBtn_string;


    private FunctionManager functionManager;
    private Canvas mainCanvas;
    private Transform nodeMenu;
    private Transform funcNodeMenu;
    Transform nodeMenuSpawnPoint;
    Transform funcMenuSpawnPoint;


    [NonSerialized]
    public TestCaseData testCaseData;

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
            // testCaseData = JsonUtility.FromJson<TestCaseData>(jsonString);


            //Unity의 JsonUtility 클래스는 배열과 객체(즉, 리스트와 딕셔너리)를 직접적으로 역직렬화할 수 없습니다
            // JSON 파서 라이브러리를 사용하여 직접 파싱하는 것이 필요. JSON 파서 라이브러리 중 하나인 Newtonsoft.Json을 사용하면 간단하게 이 문제를 해결할 수 있습니다. 
            testCaseData = JsonConvert.DeserializeObject<TestCaseData>(jsonString);

            Debug.Log("테스트 케이스 데이터 불러오기 완료");
        }
        else
        {
            Debug.Log("Can't Find Test Case Data");
        }


        functionManager = FindObjectOfType<FunctionManager>();

        mainCanvas = FindFirstObjectByType<Canvas>();
        nodeMenu = mainCanvas.transform.GetChild(1);
        nodeMenuSpawnPoint = nodeMenu.GetChild(1).GetChild(0);


    }


    private void Start()
    {
        funcNodeMenu = functionManager.canvasFuncMakeInstance.transform.GetChild(2);
        funcMenuSpawnPoint = funcNodeMenu.GetChild(1).GetChild(0);

        Debug.Log(funcMenuSpawnPoint);
        Debug.Log(nodeMenuSpawnPoint);
        //1. 테스트 케이스에 입력 변수가 있다면 입력 변수 노드를 노드 메뉴에 삽입해 준다.
        if (testCaseData.hasTestCase)
        {
            Debug.Log(testCaseData.inputInfo[0]);
            for (int i = 0; i < testCaseData.inputInfo.Count; i++)
            {
                Debug.Log(i);
                string name = testCaseData.inputInfo[i].name;
                string type = testCaseData.inputInfo[i].type;
                GameObject inputNodeBtn1 = SetInputNodeBtn(type, name);
                GameObject inputNodeBtn2 = SetInputNodeBtn(type, name);
                //노드 메뉴에 넣어주기
                inputNodeBtn1.transform.SetParent(nodeMenuSpawnPoint, false);
                //함수 노드 메뉴에도 넣어주기
                inputNodeBtn2.transform.SetParent(funcMenuSpawnPoint, false);

            }



        }

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




    //입력 노드 버튼, 버튼에 연결된 프리팹 내부 값 조정
    GameObject SetInputNodeBtn(string type, string name)
    {
        GameObject nodeBtn = null;
        if (type == "int")
        {
            //노드 버튼 인스턴스 생성
            nodeBtn = Instantiate(inputNodeBtn_int);
        }
        else if (type == "bool")
        {
            nodeBtn = Instantiate(inputNodeBtn_bool);

        }
        else
        {
            nodeBtn = Instantiate(inputNodeBtn_string);
        }
        //노드 버튼 이름 수정
        nodeBtn.GetComponentInChildren<TextMeshProUGUI>().text = name;
        nodeBtn.GetComponent<InputNodeBtn>().inputNodeName = name;

        return nodeBtn;

    }




    //맞았으면 json에 clear여부를 저장하든지.. 추후에 로직 추가 

}
