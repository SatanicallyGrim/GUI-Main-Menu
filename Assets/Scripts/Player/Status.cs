using System;
using UnityEngine;

public class Status : PlayerStats
{
    #region Structs
    [Serializable]
    public struct StatBlock
    {
        public string characterName;
        public int value;
        public int tempValue;
    }
    #endregion

    #region Variables
    public StatBlock[] charStats = new StatBlock[6];
    #endregion
}
