using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    #region Structs
    [Serializable]
    public struct Attributes
    {
        public string name;
        public float curValue;
        public float maxValue;
        public float regenValue;
        public Image disImage;
    }
    #endregion

    #region Variables
    public Attributes[] attributes = new Attributes[3];
    #endregion
}
