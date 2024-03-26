using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerData
{
    public int playerGem = 0;
    public Dictionary<string, playerPosition> playLog;

    public struct playerPosition
    {
        public float x;
        public float y;
        public float z;

    }


}
