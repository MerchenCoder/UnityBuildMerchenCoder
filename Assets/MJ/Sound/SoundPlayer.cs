using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Sound", menuName = "Sound/Create New Sound")]
public class SoundPlayer : ScriptableObject
{
    public bool isPlay;
    public string Chap02_BGM;
    [Header("Village, Temple, SecretRoom")]
    public AudioClip[] musicClips = new AudioClip[3];
}
