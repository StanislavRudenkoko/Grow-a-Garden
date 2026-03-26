using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ObjectGetter : MonoBehaviour
{
    private static ObjectGetter objectGetter;
    public Player player;
    public StoreInventory store;
    public static ObjectGetter GetInstance
	{
		get
		{
			if (!objectGetter)
			{
				objectGetter = new GameObject().AddComponent<ObjectGetter>();
				// name it for easy recognition
				objectGetter.name = objectGetter.GetType().ToString();
				// mark root as DontDestroyOnLoad();
				DontDestroyOnLoad(objectGetter.gameObject);
			}
			return objectGetter;
		}
	}
    public static Player GetPlayer()
    {
        return GetInstance.player;
    }
	public static StoreInventory GetStoreInventory()
    {
        return GetInstance.store;
    }
	public static void SetPlayer(Player player)
	{
		GetInstance.player = player;
		DontDestroyOnLoad(player);
	}
	public static void SetStoreInventory(StoreInventory store)
	{
		GetInstance.store = store;
		DontDestroyOnLoad(store);
	}
    public override string ToString()
    {
        return base.ToString();
    }
}
