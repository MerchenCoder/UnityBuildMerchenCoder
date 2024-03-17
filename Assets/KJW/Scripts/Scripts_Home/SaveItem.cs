using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveItem : MonoBehaviour
{
    static int TYPE_NUM = 11;
    List<List<GameObject>> gameObjects;
    List<List<ThisItem>> itemList;

    // 저장될 폴더 경로
    private string folderPath;
    private string filePath;

    // JSON 데이터 생성
    [System.Serializable]
    public class SaveItemData
    {
        public int[] myItem;
        public List<string> boughtItems; // 구매한 아이템의 이름을 저장할 리스트

        // 생성자에서 리스트 초기화
        public SaveItemData()
        {
            boughtItems = new List<string>();
        }
    }
    private SaveItemData itemData;


    private void Start()
    {
        // 초기화
        itemData = new SaveItemData();
        itemData.myItem = new int[TYPE_NUM];
        itemList = new List<List<ThisItem>>();
        gameObjects = new List<List<GameObject>>();
        folderPath = Application.persistentDataPath + "/Data";
        filePath = Path.Combine(folderPath, "myItemList.json");

        // 게임오브젝트 할당
        int size = transform.childCount;
        for (int i = 0; i < size; i++)
        {
            int typeSize = transform.GetChild(i).childCount;
            gameObjects.Add(new List<GameObject>());
            itemList.Add(new List<ThisItem>());
            for (int j = 0; j < typeSize; j++)
            {
                gameObjects[i].Add(transform.GetChild(i).GetChild(j).gameObject);
                itemList[i].Add(transform.GetChild(i).GetChild(j).GetComponent<ThisItem>());
                gameObjects[i][j].SetActive(false);
            }
        }

        CreateJsonFile();
    }

    private void CreateJsonFile()
    {
        // 폴더가 없다면 생성
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        // 파일이 없다면 생성
        if (!File.Exists(filePath))
        {
            // 11개 타입이 있으며 책상, 의자, 컴퓨터, 벽지만 설정 (기본 아이템 index 1, 비어있을 경우 0)
            for (int i = 0; i < TYPE_NUM; i++)
            {
                if (i == 0 || i == 1 || i == 4 || i == 10)
                {
                    itemData.myItem[i] = 1;
                    itemList[i][0].isBought = true;
                    itemData.boughtItems.Add(itemList[i][0].item.item_name);
                }
                else
                {
                    itemData.myItem[i] = 0;
                    itemList[i][1].isBought = false;
                }
            }

            // JSON 데이터를 문자열로 직렬화
            string jsonData = JsonUtility.ToJson(itemData);

            // 파일에 JSON 데이터 쓰기
            File.WriteAllText(filePath, jsonData);

            Debug.Log("JSON 파일이 생성되었습니다: " + filePath);
        }
        LoadItemData();
    }

    private void LoadItemData()
    {
        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            // 파일이 존재하면 JSON 데이터 읽기
            string jsonData = File.ReadAllText(filePath);

            // JSON 데이터를 역직렬화
            itemData = JsonUtility.FromJson<SaveItemData>(jsonData);

            // 로드한 데이터 사용
            SettingItem(itemData);
        }
        else
        {
            Debug.LogError("JSON 파일을 찾을 수 없습니다: " + filePath);
        }
    }

    public void SettingItem(SaveItemData loadedData)
    {
        // 구매 아이템 반영
        if (loadedData.boughtItems == null)
        {
            Debug.LogError("불러온 데이터가 올바르지 않습니다.");
            return;
        }

        foreach (var itemName in loadedData.boughtItems)
        {
            foreach (var itemListRow in itemList)
            {
                foreach (var item in itemListRow)
                {
                    if (item.item.item_name == itemName)
                    {
                        item.isBought = true;
                    }
                }
            }
        }


        // 적용한 아이템 활성화
        for (int i = 0; i < loadedData.myItem.Length; i++)
        {
            if (loadedData.myItem[i] == 0) continue;
            else gameObjects[i][loadedData.myItem[i] - 1].SetActive(true);
        }
    }

    /// <summary>
    /// 구매한 아이템 저장
    /// </summary>
    /// <param name="type"></param>
    /// <param name="index"></param>
    public void SaveItemDataBuy(string name)
    {
        itemData.boughtItems.Add(name);
        // JSON 데이터를 문자열로 직렬화
        string jsonData = JsonUtility.ToJson(itemData);
        // 파일에 JSON 데이터 쓰기
        File.WriteAllText(filePath, jsonData);
    }

    /// <summary>
    /// 적용한 아이템 저장
    /// </summary>
    /// <param name="type"></param>
    /// <param name="index"></param>
    public void SaveItemDataSet(int type, int index)
    {
        itemData.myItem[type] = index;
        SettingItem(itemData);

        // JSON 데이터를 문자열로 직렬화
        string jsonData = JsonUtility.ToJson(itemData);
        // 파일에 JSON 데이터 쓰기
        File.WriteAllText(filePath, jsonData);
    }
}
