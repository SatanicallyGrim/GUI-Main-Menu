using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keybinds : MonoBehaviour
{
    [SerializeField]
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    [System.Serializable]
    public struct KeyUiSetup
    {
        public string keyName;
        public TextMeshProUGUI keyDisplayText;
        public string defaultKey;
    }
    public KeyUiSetup[] baseSetup;
    public GameObject currentButton;
    public Color32 changedKey = Color.yellow;
    public Color32 selectedKey = Color.blue;

    public void Awake()
    {
        for (int i = 0; i < baseSetup.Length; i++)
        {
            baseSetup[i].keyDisplayText.transform.parent.name = baseSetup[i].keyName;
            keys.Add(baseSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(baseSetup[i].keyName, baseSetup[i].defaultKey)));
            baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();
        }
    }
    public void SaveKeys()
    {
        foreach (var thisKey in keys)
        {
            PlayerPrefs.SetString(thisKey.Key, thisKey.Value.ToString());
        }
        PlayerPrefs.Save();
        WHiteButtons();
    }
    public void WHiteButtons()
    {
        for (int i = 0; i < baseSetup.Length; i++)
        {
            baseSetup[i].keyDisplayText.transform.parent.GetComponent<Image>().color = Color.white;
        }
    }
    public void ChangeKey(GameObject clickedKeys)
    {
        currentButton = clickedKeys;
        if (clickedKeys != null)
        {
            currentButton.GetComponent<Image>().color = selectedKey;
        }
    }
    private void OnGUI()
    {
        string newKey = "";
        Event e = Event.current;

        if (currentButton == null)
            return;
        if (e.isKey)
        {
            newKey = e.keyCode.ToString();
        }

        if (newKey != "")
        {
            //change our key in the dictionary to the key we just pressed
            keys[currentButton.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
            //change the display text
            currentButton.GetComponentInChildren<TextMeshProUGUI>().text = newKey;
            //change the colour to display changed Key
            currentButton.GetComponent<Image>().color = changedKey;
            //reset to avoid errors with future rebinds
            currentButton = null;
        }
    }
}
