using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factions
{
    
    public string allianceName;
    [SerializeField, Range(-1, 1)]
    public float _approval;
    public float approval
    {
        get
        {
            return _approval;
        }
        set
        {
            _approval = Mathf.Clamp(value, -1, 1);
        }
    }
    public Factions (float initalApproval)
    {
        _approval = initalApproval;
    }
    
}
public class FactionsHandler : MonoBehaviour
{
    Dictionary<string, Factions> alliances;
    [SerializeField]
    List<Factions> GenerateAlliances;
    public static FactionsHandler allianceInstance;

    public void Awake()
    {
        if (allianceInstance == null)
        {
            allianceInstance = this;
        }
        else
        {
            Destroy(this);
        }
        alliances = new Dictionary<string, Factions>();
        foreach (Factions alliance in GenerateAlliances)
        {
            alliances.Add(alliance.allianceName, new Factions(alliance.approval));
        }
    }

    public float? AllianceApproval(string _allianceName,float _value)
    {
        if (alliances.ContainsKey(_allianceName))
        {
            alliances[_allianceName]._approval += _value;
            return alliances[_allianceName]._approval;
        }
        return null;
    }
    public float? AllianceApproval(string _allianceName)
    {
        if (alliances.ContainsKey(_allianceName))
        {
            return alliances[_allianceName]._approval;
        }
        return null;
    }
    
}
