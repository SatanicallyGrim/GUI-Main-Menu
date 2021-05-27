using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    #region Variables
    private Dialogue loadedDialogue;
    public static DialogueHandler diaInstance;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform dialogueButtonPanel;
    [SerializeField] private Text npcResponseText;
    #endregion

    private void Awake()
    {
        if (diaInstance == null)
        {
            diaInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void LoadDialogue(Dialogue _dialogue)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        loadedDialogue = _dialogue;
        int i = 0;
        Button spawnButton;
        ClearButtons();
        foreach (LineOfDialogue item in _dialogue.speachPaths)
        {
            float? currentApproval = FactionsHandler.allianceInstance.AllianceApproval(loadedDialogue.alliance);
            if (currentApproval != null && currentApproval > item.minApproval)
            {
                spawnButton = Instantiate(buttonPrefab, dialogueButtonPanel).GetComponent<Button>();
                spawnButton.GetComponentInChildren<Text>().text = item.npcQuestion;
                int i2 = i;
                spawnButton.onClick.AddListener(delegate { ButtonPressed(i2); });
                i++;
            }
        }
        spawnButton = Instantiate(buttonPrefab, dialogueButtonPanel).GetComponent<Button>();
        spawnButton.GetComponentInChildren<Text>().text = _dialogue.farewell.npcQuestion;
        spawnButton.onClick.AddListener(EndConvo);
        print(_dialogue.smallTalk);
        DisplayResponse(loadedDialogue.smallTalk);
    }
    void EndConvo()
    {
        ClearButtons();
        print(loadedDialogue.farewell.npcResponse);
        DisplayResponse(loadedDialogue.farewell.npcResponse);
        if (loadedDialogue.farewell.nextSpeachPath != null)
        {
            LoadDialogue(loadedDialogue.farewell.nextSpeachPath);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void DisplayResponse(string _response)
    {
        npcResponseText.text = _response;
    }
    void ButtonPressed(int _index)
    {
        if (loadedDialogue.speachPaths[_index].nextSpeachPath != null)
        {
            LoadDialogue(loadedDialogue.speachPaths[_index].nextSpeachPath);
        }
        else
        {
            DisplayResponse(loadedDialogue.speachPaths[_index].npcResponse);
        }
    }
    public void ClearButtons()
    {
        foreach (Transform child in dialogueButtonPanel)
        {
            Destroy(child.gameObject);
        }
    }
}
