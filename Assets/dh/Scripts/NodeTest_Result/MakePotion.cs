using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePotion : MonoBehaviour
{
    [Header("Effect")]
    [SerializeField]
    private GameObject makeEffect;
    [SerializeField]
    private GameObject powderEffect;

    [Header("Sprite")]
    public Sprite potionImg;
    public Sprite originImg;

    public void MakePotion_Success()
    {
        Debug.Log("Success 호출");
        makeEffect.GetComponent<ParticleSystem>().Play();
        makeEffect.GetComponent<ParticleSystemControl>().isPlay = true;
        GetComponent<SpriteRenderer>().sprite = potionImg;
    }
    public void MakePotion_Fail()
    {
        makeEffect.GetComponent<ParticleSystem>().Play();
        makeEffect.GetComponent<ParticleSystemControl>().isPlay = true;
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void PutPowder()
    {
        Debug.Log("putPowder실행");
        powderEffect.GetComponent<ParticleSystem>().Play();
        powderEffect.GetComponent<ParticleSystemControl>().isPlay = true;
    }

    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().sprite = originImg;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
