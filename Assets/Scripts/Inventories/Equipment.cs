using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EquipmentSlot
{
    [SerializeField] private Items item;
    public Items EquipedItem
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            itemEquiped.Invoke(this);
        }
    }
    public Transform visualLocation;
    public Vector3 offset;

    public delegate void ItemEquiped(EquipmentSlot _item);
    public event ItemEquiped itemEquiped;
}
public class Equipment : MonoBehaviour
{
    #region Equipment Slots
    public EquipmentSlot primarySlot;
    public EquipmentSlot secondarySlot;
    public EquipmentSlot defensiveSlot;

    #endregion

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        primarySlot.itemEquiped += EquipItem;
        secondarySlot.itemEquiped += EquipItem;
        defensiveSlot.itemEquiped += EquipItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        EquipItem(primarySlot);
        EquipItem(secondarySlot);
        EquipItem(defensiveSlot);
    }

    public void EquipItem(EquipmentSlot _item)
    {
        if (_item.EquipedItem.Mesh == null)
            return;

        foreach (Transform child in _item.visualLocation)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject meshInstance = Instantiate(_item.EquipedItem.Mesh, _item.visualLocation);
        meshInstance.transform.localPosition = primarySlot.offset;
        OffsetLocal offset = meshInstance.GetComponent<OffsetLocal>();

        meshInstance.transform.position += offset.positionOffset;
        meshInstance.transform.localRotation = Quaternion.Euler(offset.rotationOffset);
        meshInstance.transform.localScale = offset.scaleOffset;
    }
}
