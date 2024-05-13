using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanish : MonoBehaviour
{
    [Header("Vanish")]
    [SerializeField]
    private GameObject vanishEffect;

    public void VanishMagic()
    {

        vanishEffect.GetComponent<ParticleSystem>().Play();
        vanishEffect.GetComponent<ParticleSystemControl>().isPlay = true;
    }


    public void VanishMagnic_Fail()
    {
        vanishEffect.GetComponent<ParticleSystem>().Play();

    }


}
