using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindingManager : MonoBehaviour
{
    private static BindingManager instance = null;
    //used to actually access the bindings by thier names when handeling input
    private Dictionary<string, Binding> bindingsMap = new Dictionary<string, Binding>();
    //contains all bindings for easy iterations over all over them
    private List<Binding> bindingsList = new List<Binding>();

    [SerializeField] private List<Binding> defaultBindings = new List<Binding>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
        }
        PopulateBindingsDictionaries();
        LoadBindings();
    }
    
    private void PopulateBindingsDictionaries()
    {
        foreach (Binding binding in defaultBindings)
        {
            if (bindingsMap.ContainsKey(binding.Name))
            {
                continue;
            }
            bindingsMap.Add(binding.Name, binding);
            bindingsList.Add(binding);

        }
    }
    private void LoadBindings()
    {
        foreach (Binding binding in bindingsList)
        {
            binding.Load();
        }
    }
    public static bool BindingPressed(string _key)
    {
        // Attempt to retrieve the binding
        Binding binding = GetBinding(_key);

        if (binding != null)
        {
            // We got the binding so get its pressed state
            return binding.Pressed();
        }

        // No binding matches the passed key so log a message and return false
        Debug.LogWarning("No binding matches the passed key: " + _key);
        return false;
    }
    public static bool BindingHeld(string _key)
    {
        //get the binding
        Binding binding = GetBinding(_key);
        if (binding != null)
        {
            //we have a binding is it being hel down
            return binding.Held();
        }
        //we don't have a binding
        Debug.LogWarning("No binding matches the passed key: " + _key);
        return false;
    }
    public static void Rebind(string _name, KeyCode _value)
    {
        Binding binding = GetBinding(_name);
        if (binding != null)
        {
            binding.Rebind(_value);
        }
    }
    public static Binding GetBinding(string _key)
    {
        if (instance.bindingsMap.ContainsKey(_key))
        {
            return instance.bindingsMap[_key];
        }
        return null;
    }
    
}
