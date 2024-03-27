using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Speaker", menuName = "Speaker/Create New Speaker")]
public class Speaker : ScriptableObject
{
    public bool isPlayer;
    public string speaker_name;
    [Header("Normal, Smile, Sad, Cry, Angry, Surprise ")]
    public Sprite[] standing_sprites = new Sprite[6];

}
