using UnityEngine;
[System.Serializable]
public class LineOfDialogue
{
    [TextArea(4,6)]
    public string npcQuestion, npcResponse;
    public float minApproval = -1f;
    public Dialogue nextSpeachPath;

    [System.NonSerialized]
    public int buttonID;
}
