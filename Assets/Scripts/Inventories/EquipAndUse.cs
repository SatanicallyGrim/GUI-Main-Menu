using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAndUse : MonoBehaviour
{
    #region Variables
    public Inventory inv;
    #endregion
    
    #region Spawn Locations 
    public Transform headPos;
    public Transform weaponPos;
    public Transform apparelPos;
    #endregion
    private enum EquipPoints
    {
        head,
        apparel,
        weapon
    }
    private void Start()
    {
        
    }

    public void EquipItemOnPlayer()
    {
        if (inv.selectedItem == null)
        {
            return;
        }
        GameObject mesh = inv.selectedItem.Mesh;
        if (mesh != null)
        {
            GameObject spawnedMesh = Instantiate(mesh, headPos);
            
        }
        
    }
    
}
