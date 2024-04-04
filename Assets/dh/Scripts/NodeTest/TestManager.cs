using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.Threading;
public class TestManager : MonoBehaviour
{
    //---싱글톤 생성---//
    public static TestManager Instance { get; private set; }


    [Serializable]
    public struct TestCaseData
    {
        public bool hasTestCaseInput;
        public List<InputInfo> inputInfo;

        public struct InputInfo
        {
            public string name;
            public string type;
        }
        public int testCaseLength;
        public Dictionary<string, TestCase> testCase;

        //일단 input output 모두 문자열로 받아오기
        public struct TestCase
        {
            public List<string> input;
            public List<string> output;
        }

    }

    //문제별 테스트 케이스 파일 이름을 어떻게 할지 정해야함
    public string TestCaseDataFileName;

    //입력 노드 버튼 프리팹
    public GameObject inputNodeBtn_int;
    public GameObject inputNodeBtn_bool;
    public GameObject inputNodeBtn_string;

    //액션 노드 버튼 프리팹
    public GameObject[] actionNodeBtn;


    //입력 변수 버튼 삽입을 위한 요소
    private Canvas mainCanvas;
    private Transform nodeMenu;
    private Transform funcNodeMenu;
    Transform input_NodeMenuSpawnPoint;
    Transform input_funcMenuSpawnPoint;
    Transform action_NodeMenuSpawnPoint;
    Transform action_funcMenuSpawnPoint;


    [NonSerialized]
    public TestCaseData testCaseData;



    //채점을 위한 테스트 케이스 입출력, 사용자 출력 리스트 변수
    public List<string> currentInput;
    public List<string> currentOutput;
    public List<string> playerOutput; //플레이어가 실행할 때 생성되는 output을 담는 리스트



    //채점 결과 UI
    public Canvas SubmitResultCanvas;
    private GameObject success;
    private Text rewardTxt;
    private GameObject fail;


    //테스트 케이스 데이터를 가져온다.
    //테스트 케이스가 존재하면 리스트에 차례로 넣어준다.
    //run을 위해 -> 첫 번째 테스트 케이스를 변수에 할당한다.

    //-----씬이 언로드될 때 싱글톤 파괴-----//
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }


    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("TestnManager 싱글톤 객체 파괴");
        Instance = null; // Instance 변수를 초기화하여 새로운 싱글톤 객체 생성을 허용
        Destroy(gameObject);

    }

    private void Awake()
    {
        //-----싱글톤-----//
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //테스트 데이터 가져오기
        GetTestData();

        //functionManager = FindObjectOfType<FunctionManager>();

        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        nodeMenu = mainCanvas.transform.GetChild(1);
        input_NodeMenuSpawnPoint = nodeMenu.GetChild(0).GetChild(0);
        action_NodeMenuSpawnPoint = nodeMenu.GetChild(1).GetChild(0);

    }


    private void Start()
    {

        //변수 설정
        SubmitResultCanvas = GameObject.Find("Canvas_Submit_Parent").GetComponentInChildren<Canvas>(true);
        success = SubmitResultCanvas.transform.GetChild(1).gameObject;
        rewardTxt = success.transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
        fail = SubmitResultCanvas.transform.GetChild(2).gameObject;

        funcNodeMenu = FunctionManager.Instance.canvasFuncMakeInstance.transform.GetChild(2);
        input_funcMenuSpawnPoint = funcNodeMenu.GetChild(0).GetChild(0);
        action_funcMenuSpawnPoint = funcNodeMenu.GetChild(1).GetChild(0);
        // Debug.Log(funcMenuSpawnPoint);
        // Debug.Log(nodeMenuSpawnPoint);

        //-----------입력 변수 노드 처리---------//
        PutInputNodeBtn();

        //------------액션 노드 처리 ---------------//
        PutActionNodeBtn();


        //리워드 처리
        rewardTxt.text = GameManager.Instance.missionData.reward.ToString();

    }


    private void GetTestData()
    {
        TestCaseDataFileName = "TestCase" + GameManager.Instance.missionData.missionCode + ".json";

        //Json 파일 경로
        string jsonFilePath = Application.dataPath + "/Data/TestCase/" + TestCaseDataFileName;

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

            SettingCurrentCase(1);
            Debug.Log("현재 케이스 입력/출력 배열 셋팅 완료");
        }
        else
        {
            Debug.Log("Can't Find Test Case Data");
        }

    }

    private void PutInputNodeBtn()
    {
        //노드 메뉴 패널 초기화
        for (int i = 0; i < input_NodeMenuSpawnPoint.childCount; i++)
        {
            if (i <= 1)
            {
                continue;
            }
            Destroy(input_NodeMenuSpawnPoint.GetChild(i).gameObject);
        }
        //함수노드 메뉴 패널 초기화
        for (int i = 0; i < input_funcMenuSpawnPoint.childCount; i++)
        {
            if (i <= 1)
            {
                continue;
            }
            Destroy(input_funcMenuSpawnPoint.GetChild(i).gameObject);
        }
        //테스트 케이스에 입력 변수가 있다면 입력 변수 노드를 노드 메뉴에 삽입해 준다.
        if (testCaseData.hasTestCaseInput)
        {
            for (int i = 0; i < testCaseData.inputInfo.Count; i++)
            {
                string name = testCaseData.inputInfo[i].name;
                string type = testCaseData.inputInfo[i].type;
                GameObject inputNodeBtn1 = SetInputNodeBtn(type, name, i);
                GameObject inputNodeBtn2 = Instantiate(inputNodeBtn1);
                //노드 메뉴에 넣어주기
                inputNodeBtn1.transform.SetParent(input_NodeMenuSpawnPoint, false);
                //함수 노드 메뉴에도 넣어주기
                inputNodeBtn2.transform.SetParent(input_funcMenuSpawnPoint, false);

            }
        }
    }
    private void PutActionNodeBtn()
    {
        //노드 메뉴 패널 초기화
        for (int i = 0; i < action_NodeMenuSpawnPoint.childCount; i++)
        {
            if (i == 0)
            {
                continue;
            }
            Destroy(action_NodeMenuSpawnPoint.GetChild(i).gameObject);
        }
        //함수노드 메뉴 패널 초기화
        for (int i = 0; i < action_funcMenuSpawnPoint.childCount; i++)
        {
            if (i == 0)
            {
                continue;
            }
            Destroy(action_funcMenuSpawnPoint.GetChild(i).gameObject);
        }
        string[] actionNodeArray = GameManager.Instance.missionData.actionNodes;
        //문제에서 사용되는 액션 노드가 있다면 액션 노드 버튼을 생성하고, 액션 노드 프리팹을 찾아서 연결해준다.
        if (actionNodeArray.Length != 0)
        {
            foreach (string actionNode in actionNodeArray)
            {
                //액션노드프리팹이름/액션노드한글이름/액션노드타입번호
                string[] actionNodeInfo = actionNode.Split("/");

                GameObject newActionNodeBtn1 = Instantiate(actionNodeBtn[int.Parse(actionNodeInfo[2]) - 1]);
                newActionNodeBtn1.GetComponentInChildren<TextMeshProUGUI>().text = actionNodeInfo[1];
                newActionNodeBtn1.GetComponent<NodeMenuBtn>().nodePrefab = Resources.Load<GameObject>("Prefabs/Node/Action/" + actionNodeInfo[0]);

                GameObject newActionNodeBtn2 = Instantiate(newActionNodeBtn1);

                newActionNodeBtn1.transform.SetParent(action_NodeMenuSpawnPoint, false);
                newActionNodeBtn2.transform.SetParent(action_funcMenuSpawnPoint, false);

            }
        }
    }

    public void RestTestCase()
    {
        GetTestData();
        PutInputNodeBtn();
        PutActionNodeBtn();
        rewardTxt.text = GameManager.Instance.missionData.reward.ToString();

    }


    //제출시 채점 실행
    public IEnumerator Test()
    {
        Debug.Log("채점중");
        SubmitResultCanvas.gameObject.SetActive(true);
        for (int i = 0; i < testCaseData.testCaseLength; i++)
        {
            //현재 테스트 케이스 설정
            SettingCurrentCase(i + 1);
            Debug.Log("Run 끝나길 기다리는 중");
            yield return StartCoroutine(NodeManager.Instance.RunProgram());
            bool result = CheckAnswer();
            if (NodeManager.Instance.CompileError || !result) //컴파일 오류가 있거나 결과가 output 배열과 다른 경우
            {
                Debug.Log((i + 1).ToString() + " 번째 테스트 케이스 실패. 채점 종료"); //실패 안내 관련 로직으로 변경해야함.
                yield return new WaitForSeconds(1.5f);
                fail.gameObject.SetActive(true);
                StopAllCoroutines();
                yield break;
            }

            Debug.Log((i + 1).ToString() + "번째 테스트 케이스 통과");
        }
        yield return new WaitForSeconds(1.5f);
        Success();
    }

    public void Fail()
    {
        StopAllCoroutines();
        bool result = CheckAnswer();
        if (NodeManager.Instance.CompileError || !result) //컴파일 오류가 있거나 결과가 output 배열과 다른 경우
        {
            Debug.Log("컴파일 오류로 채점 종료"); //실패 안내 관련 로직으로 변경해야함.
            Thread.Sleep(1500);
            fail.gameObject.SetActive(true);
        }

    }
    public void Success()
    {
        MissionClear();
        Debug.Log("모든 테스트 케이스를 통과하였습니다.");
        Debug.Log("채점종료");
    }


    //정답 채크
    public bool CheckAnswer()
    {
        if (currentOutput.Count == playerOutput.Count)
        {
            for (int i = 0; i < currentOutput.Count; i++)
            {
                if (currentOutput[i] != playerOutput[i])
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }

    }


    public void SettingCurrentCase(int currentCount)
    {
        TestCaseData.TestCase testCase = testCaseData.testCase[currentCount.ToString()];
        currentInput = testCase.input;
        currentOutput = testCase.output;

        //리스트 초기화
        playerOutput.Clear();

    }




    //입력 노드 버튼, 버튼에 연결된 프리팹 내부 값 조정
    GameObject SetInputNodeBtn(string type, string name, int index)
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
        nodeBtn.GetComponent<InputNodeBtn>().inputIndex = index;

        return nodeBtn;

    }

    //맞았으면 clear여부 업데이트 & 저장
    public void MissionClear()
    {
        //"-"로 split
        string[] chapter_mission = GameManager.Instance.missionData.missionCode.Split("-");
        //미션 state clear로 변경
        DataManager.Instance.UpdateMissionState(int.Parse(chapter_mission[0]), int.Parse(chapter_mission[1]), true);
        //보상 반영
        GameManager.Instance.GetSomeGem(GameManager.Instance.missionData.reward);

    }





    //실패 -> 다시하기
    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
