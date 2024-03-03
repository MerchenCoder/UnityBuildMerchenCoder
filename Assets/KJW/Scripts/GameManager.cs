using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int Gem_Num;

    // 젬 상태 변화를 알리기 위한 이벤트
    public event UnityAction<int> OnGemChanged;


    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    /// <summary>
    /// 젬 사용하기
    /// </summary>
    /// <param name="num"></param>
    public bool UseGem(int num)
    {
        if (Gem_Num - num >= 0) 
        {
            Gem_Num -= num;
            OnGemChanged?.Invoke(Gem_Num); // 젬 상태가 변경되었음을 알림
            return true; 
        }
        else return false;
        
    }

    /// <summary>
    /// 현재 젬의 개수 가져오기
    /// </summary>
    /// <returns></returns>
    public int GetNowGem()
    {
        return Gem_Num;
    }

    /// <summary>
    /// 젬 획득하기
    /// </summary>
    /// <param name="num"></param>
    public void GetSomeGem(int num)
    {
        Gem_Num += num;
        OnGemChanged?.Invoke(Gem_Num); // 젬 상태가 변경되었음을 알림
    }
}
