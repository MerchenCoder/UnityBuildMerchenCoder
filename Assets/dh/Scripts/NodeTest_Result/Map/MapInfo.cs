using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInfo : MonoBehaviour
{
    [System.Serializable]
    public class map2DRowArray
    {
        public int[] row;
    }
    [SerializeField]
    [Tooltip("Block = 0, Road, Start, End")]
    private map2DRowArray[] map2DArray;
    [SerializeField]
    [Tooltip("Block = 0, Road, Start, End")]
    private GameObject[] mapBlockPrefabs = new GameObject[4];
    public int map2DArrayRowCount
    {
        get => map2DArray.Length;
    }
    public int map2DArrayColumnCount
    {
        get => map2DArray[0].row.Length;
    }

    //[Header("RealTime Map Block")]
    //public GameObject[,] mapBlock2DArray = new GameObject[5, 7];


    void Awake()
    {

        for (int i = 0; i < map2DArray.Length; i++)
        {

            for (int j = 0; j < map2DArray[i].row.Length; j++)
            {
                if (map2DArray[i].row[j] > 3 || map2DArray[i].row[j] < 0)
                {
                    Debug.Log($"map2DArray - {i},{j} 인덱스 오류");
                    GameObject errorBlock = Instantiate(mapBlockPrefabs[1], transform, false);
                    errorBlock.name = "ErrorBlock";
                    errorBlock.GetComponent<Image>().color = Color.red;

                    //mapBlock2DArray[i, j] = errorBlock;
                }
                else
                {
                    GameObject block = Instantiate(mapBlockPrefabs[map2DArray[i].row[j]], transform, false);
                    //mapBlock2DArray[i, j] = block;
                }
            }
        }
    }

}
