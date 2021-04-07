using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Binding 
{

    public string Name { get { return name; } }
    public KeyCode Value { get { return value; } }
    public string ValueDisplay { get { return BindingUtilities.TranslateKeycode(value); } }

    [SerializeField] private string name;
    [SerializeField] private KeyCode value;

    public Binding(string _name, KeyCode _defaultValue)
    {
        name = _name;
        value = _defaultValue;
    }
    public void Save()
    {
        PlayerPrefs.SetInt(name, (int) value);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        value = (KeyCode)PlayerPrefs.GetInt(name, (int) value);
    }
    public void Rebind(KeyCode _new)
    {
        value = _new;
        Save();
    }
    public bool Pressed()
    {
        return Input.GetKeyDown(value);
    }public bool Held()
    {
        return Input.GetKey(value);
    }
    public bool Release()
    {
        return Input.GetKeyUp(value);
    }
}
