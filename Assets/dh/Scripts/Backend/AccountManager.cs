using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class req_withdrawal : packet
{

}
public class res_withdrawal : packet
{
    public int errorno;
}

public class AccountManager : MonoBehaviour
{

    public void Logout()
    {
        //소리 데이터는 남긴다
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

        //자동로그인을 위해 저장했던 PlayerPrefs 데이터 삭제
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        Debug.Log("소리 제외 playerprefs 키 삭제");

        string staticFolderPath = Path.Combine(Application.persistentDataPath, "static");
        // foreach (string file in Directory.GetFiles(staticFolderPath)) //파일 탐색
        // {
        //     File.Delete(file); //파일 삭제
        // }
        Directory.Delete(staticFolderPath, true);
        Debug.Log("로컬 내의 정적 파일을 모두 삭제하였습니다.");



        //첫 화면으로 이동
        SceneChange.Instance.ChangeToThisScene("Splash");
    }

    public void DeleteAccount()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Data");
        string staticFolderPath = Path.Combine(Application.persistentDataPath, "static");
        Withdrawal((success) =>
        {
            if (success)
            {
                PlayerPrefs.DeleteAll();
                // foreach (string file in Directory.GetFiles(folderPath)) //파일 탐색
                // {
                //     File.Delete(file); //파일 삭제
                // }
                Directory.Delete(folderPath, true);
                Debug.Log("로컬 내의 플레이 데이터를 모두 삭제하였습니다.");


                // foreach (string file in Directory.GetFiles(staticFolderPath)) //파일 탐색
                // {
                //     File.Delete(file); //파일 삭제
                // }
                Directory.Delete(staticFolderPath, true);
                Debug.Log("로컬 내의 정적 파일을 모두 삭제하였습니다.");


                SceneChange.Instance.ChangeToThisScene("Splash");
            }
            else
            {
                Debug.Log("오류가 발생했습니다. 계정을 삭제할 수 없습니다.");
            }
        });

    }


    private void Withdrawal(System.Action<bool> onComplete)
    {
        req_withdrawal reqWithdrawal = new req_withdrawal();
        reqWithdrawal.cmd = 5000;

        var json = JsonConvert.SerializeObject(reqWithdrawal);

        StartCoroutine(NetworkManager.Get($"user/withdrawal?userId={PlayerPrefs.GetString("userId")}", json, (result) =>
        {
            if (result == "server error")
            {
                Debug.LogError("서버 응답 중 오류 발생");
                onComplete(false);
                return;
            }
            res_withdrawal responseResult = JsonConvert.DeserializeObject<res_withdrawal>(result);
            Debug.LogFormat("<color=yellow>{0}</color>", responseResult.cmd);

            if (responseResult.errorno == 0)
            {
                onComplete(true);
            }
            else
            {
                onComplete(false);
            }

        }));
    }

}
