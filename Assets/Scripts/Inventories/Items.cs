using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    public enum ItemType
    {
        Weapons,
        Armour,
        Food,
        Potions,
        Scrolls,
        Ingredients, 
        Crafting,
        Quests, 
        Fortune
    }

    #region Item Variables
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private int value;
    [SerializeField] private int amount;
    [SerializeField] private Texture2D icon;
    [SerializeField] private GameObject mesh;
    [SerializeField] private ItemType type;
    [SerializeField] private int damage;
    [SerializeField] private int armour;
    [SerializeField] private int heal;
    #endregion

    #region Public Properties
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }
    public Texture2D Icon
    {
        get { return icon; }
        set { icon = value; }
    }
    public GameObject Mesh
    {
        get { return mesh; }
        set { mesh = value; }
    }
    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int Armour
    {
        get { return armour; }
        set { armour = value; }
    }
    public int Heal
    {
        get { return heal; }
        set { heal = value; }
    }
    #endregion

    public Items(Items _copyItem, int _copyAmount)
    {
        Name = _copyItem.Name;
        Description = _copyItem.Description;
        Value = _copyItem.Value;
        Amount = _copyAmount;
        Icon = _copyItem.Icon;
        Mesh = _copyItem.Mesh;
        Type = _copyItem.Type;
        Damage = _copyItem.Damage;
        Armour = _copyItem.Armour;
        Heal = _copyItem.Heal;
    }
}
