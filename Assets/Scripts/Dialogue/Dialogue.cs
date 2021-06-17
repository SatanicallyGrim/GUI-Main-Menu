using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    #region Variables
    public string alliance;
    public string smallTalk;
    public bool firstDialogue;

    public LineOfDialogue farewell;
    public LineOfDialogue[] speachPaths;
    #endregion
    

    // Update is called once per frame
    void Update()
    {
        if (!firstDialogue)
            return;
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    DialogueHandler.diaInstance.LoadDialogue(this);
        //}
    }
}
