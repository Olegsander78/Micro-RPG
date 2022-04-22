using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName;

    public void PickUpItem()
    {
        FindObjectOfType<Player>().AddItemToInventory(ItemName);
        Destroy(gameObject);
    }
}
