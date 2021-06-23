using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAndPickup : MonoBehaviour
{
    [SerializeField] private Inventory inv;
    [SerializeField] private Transform dropPos;
    [SerializeField] private Camera camPov;

    public void DropItem()
    {
        if (inv.selectedItem == null)
        {
            return;
        }
        GameObject mesh = inv.selectedItem.Mesh;
        if (mesh != null)
        {
            GameObject spawnMesh = Instantiate(mesh, null);
            spawnMesh.transform.position = dropPos.position;
            DropItem dropItem = mesh.GetComponent<DropItem>();
            dropItem.item = new Items(inv.selectedItem, 1);
        }

        inv.selectedItem.Amount--;
        if (inv.selectedItem.Amount <= 0)
        {
            inv.RemoveItemFromInventory(inv.selectedItem);
            inv.selectedItem = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = camPov.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hitinfo;
            if (Physics.Raycast(ray,out hitinfo,25f))
            {
                DropItem dropitem = hitinfo.collider.GetComponent<DropItem>();
                if (dropitem != null)
                {
                    inv.AddItemToInventory(dropitem.item);
                    Destroy(hitinfo.collider.gameObject);
                }
            }
        }
    }
}
