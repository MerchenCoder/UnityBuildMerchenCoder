using UnityEngine;

public class ParticleSystemControl : MonoBehaviour
{

    private ParticleSystem particle;
    public bool isPlay = false;
    private void Awake()
    {

        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {

        if (isPlay && !particle.isPlaying)
        {
            isPlay = false;
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
}