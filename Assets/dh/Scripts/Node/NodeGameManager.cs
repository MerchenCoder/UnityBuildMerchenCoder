using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System;

public class NodeGameManager : MonoBehaviour
{
    public static NodeGameManager Instance { get; private set; }
    public class MissionData
    {
        public string missionCode;
        public string missionTitle;
        public string[] actionNodes;
        public string missionInfo;
        public string[] nodeLabels;
        public int reward;
        public bool hasNodeLimit;
        public int nodeOpenIndex;
    }

    public string dataFileName;

    public MissionData missionData = new MissionData();
    // Start is called before the first frame update
    private void Awake()
    {
        //싱글톤//
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        LoadData();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void LoadData()
    {
        string filePath = Application.dataPath + "/Data/" + dataFileName;
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            missionData = JsonUtility.FromJson<MissionData>(jsonString);

            Debug.Log("미션 데이터 불러오기 완료");
        }
    }
}
