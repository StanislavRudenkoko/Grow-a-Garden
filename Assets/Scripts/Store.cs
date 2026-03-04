using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
    Object[] Inventory { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Inventory = Resources.LoadAll("Assets/Items");
        foreach (Item item in Inventory.Cast<Item>())
        {

            Debug.Log(item.Description);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
