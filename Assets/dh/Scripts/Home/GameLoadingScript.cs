using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class GameLoadingScript : MonoBehaviour
{
    public float loadValue;
    public Text loadText;
    public Slider loadingProgress;
    string[] files;


    // Start is called before the first frame update
    void Start()
    {
        loadValue = 0.0f;
    }

    public IEnumerator Loading()
    {
        //파일 로드부터
        // files = Directory.GetFiles(Application.persistentDataPath, "*.json", SearchOption.AllDirectories);
        // if (files.Length == 0)
        // {
        //     Debug.Log("파일없음");
        //     //로컬에 초기화 파일 생성
        //     DataManager.Instance.InitializeGameStatusData();
        //     GameManager.Instance.InitializePlayerData();
        //     //서버로 저장
        //     DataManager.Instance.GetComponent<Save>().SavePlayerData();
        //     DataManager.Instance.GetComponent<Save>().SaveGameStatusData();
        // }
        // else
        // {
        //     Debug.Log("파일 있음");
        //     Debug.Log(files[0]);
        //     //서버로부터 데이터 로드...
        //     DataManager.Instance.LoadGameStatusData();
        //     GameManager.Instance.LoadPlayerData();
        //     // Debug.Log(DataManager.Instance.gameStateData.ch1MissionClear[0]);

        // }
        // loadValue = 50;
        // loadingProgress.value = loadValue;
        // loadText.text = loadValue.ToString();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Home");
        asyncLoad.allowSceneActivation = false;

        //isDone 은 asyncLoad.progress의 연산이 완료되었는지 확인한다.
        //allowSceneActivation을 true로 하면 씬 로드가 완료되면 바로 씬 전환이 되지만,
        //allowSceneActivation을 false로 하면 씬 로드 progress가 0.9에서 중지된다.
        //따라서 allowSceneActivation false를 쓰는 경우 while문 밖을 탈출하지 못한다.
        //이 경우 allowSceneActivation true로 만들 수 있는 방법을 추가해 주어야한다.
        while (!asyncLoad.isDone)
        {
            //Debug.Log(asyncLoad.progress);

            loadValue = Mathf.Round(asyncLoad.progress * 100 * 100) / 100;
            loadingProgress.value = loadValue;
            loadText.text = loadValue.ToString();

            if (asyncLoad.progress >= 0.9f)
            {

                break;
            }
            Debug.Log("씬 준비 완료. 잠시 대기");
            yield return new WaitForSeconds(1.5f);
            asyncLoad.allowSceneActivation = true;

        }
        // while (loadingProgress.value < 100f)
        // {
        //     loadValue += 0.5f;

        //     loadingProgress.value = loadValue;
        //     loadText.text = loadValue.ToString();
        //     if (loadingProgress.value == 100f)
        //     {
        //         StopCoroutine(Loading());
        //     }
        //     yield return null;
        // }
    }
}
