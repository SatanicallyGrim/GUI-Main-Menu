using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BindingButton : MonoBehaviour
{
    [SerializeField] private string bindingToMap;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI bindingName;
    private bool isRebinding = false;

    private void Setup(string _toMap)
    {
        bindingToMap = _toMap;

        //Attomatically set the onclick function and change the nam text to the binding
        button.onClick.AddListener(OnClick);
        bindingName.text = _toMap;
        //Update the button text with the binding's value and make the GO Active
        BindingUtilities.UpdateTextWithBinding(bindingToMap, buttonText);
        gameObject.SetActive(true);
    }
    private void Start()
    {
        if (string.IsNullOrEmpty(bindingToMap))
        {
            gameObject.SetActive(false);
            return;
        }
        Setup(bindingToMap);
    }
    private void Update()
    {
        if (isRebinding)
        {
            KeyCode pressed = BindingUtilities.GetAnyPressedKey();
            if (pressed != KeyCode.None)
            {
                BindingManager.Rebind(bindingToMap, pressed);
                BindingUtilities.UpdateTextWithBinding(bindingToMap, buttonText);

                isRebinding = false;
            }
        }
    }
    private void OnClick()
    {
        isRebinding = true;
    }
}
