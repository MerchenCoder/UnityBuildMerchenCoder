using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
using Newtonsoft.Json;


public class NodeLabelControl : MonoBehaviour
{
    public List<string> nodeArray;
    public GameObject nodeLabelPrefab;
    private string colorFileName = "NodeColor.json";

    [Serializable]
    public struct ColorData
    {
        public Dictionary<string, string> colorDic;
    }
    ColorData colorData;


    private void Awake()
    {
        //Json 파일 가져와서 구조체로 역직렬화 (라이브러리 사용)
        string jsonFilePath = Application.dataPath + "/Data/" + colorFileName;
        if (File.Exists(jsonFilePath))
        {
            string jsonString = File.ReadAllText(jsonFilePath); //json파일 문자열로 읽어오기
            colorData = JsonConvert.DeserializeObject<ColorData>(jsonString);
        }
        else
        {
            Debug.Log("파일을 찾을 수 없습니다.");
        }
    }

    // Start is called before the first frame update

    //GameManager는 npc에 연결된 데이터를 통해 문제 데이터를 가져온다.
    //mission panel은 GameManager에 설정된 문제 데이터를 읽어와 info를 setting 해야 한다.
    void Start()
    {
        //GameManager에서 문제에서 사용하는 노드 정보를 가져오는 로직으로 수정해야 함.
        //현재는 임시로 nodeArray에 데이터 삽입.
        foreach (string node in nodeArray)
        {
            GameObject nodeLabel = Instantiate(nodeLabelPrefab);
            nodeLabel.GetComponentInChildren<TextMeshProUGUI>().text = node;
            string hexaCode = colorData.colorDic[node];
            nodeLabel.GetComponent<Image>().color = HexaToColor(hexaCode);
            nodeLabel.transform.SetParent(this.transform, false);

        }

        transform.parent.GetComponent<VerticalLayoutGroup>().enabled = false;
        transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Color HexaToColor(string hexaCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexaCode, out color))
        {
            return color;
        }
        else
        {
            Debug.Log("Hexa code에서 color 변환 실패. 화이트를 임시로 반환합니다.");
            return Color.white;
        }




    }
}
