using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPauser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        InventoryController.PauseInventory();
    }
    public void Unpause()
    {
        InventoryController.UnpauseInventory();
    }

    public void InvSummon()
    {
        InventoryController.InventorySummon();
    }


}
