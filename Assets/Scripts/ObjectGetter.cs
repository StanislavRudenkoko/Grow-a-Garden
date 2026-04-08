using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class to hold objects that needs to persist between scenes. Singleton
/// Author: Tin Trinh
/// Date: Mar. 25, 2026
/// Source: None
/// </summary>
public class ObjectGetter : MonoBehaviour
{
    private static ObjectGetter objectGetter;
    public Player player;
    public StoreInventory store;
	public PlantData plantData;
	public List<PlantPotController> activePots;
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

	/// <summary>
	/// Gets the player.
	/// </summary>
	/// <returns></returns>
    public static Player GetPlayer()
    {
        return GetInstance.player;
    }
	/// <summary>
	/// Gets the store inventory.
	/// </summary>
	/// <returns></returns>
	public static StoreInventory GetStoreInventory()
    {
        return GetInstance.store;
    }

	/// <summary>
	/// Gets the plants
	/// </summary>
	/// <returns></returns>
	public static PlantData GetPlantData()
	{
		return GetInstance.plantData;
	}

	/// <summary>
	/// Gets the pots
	/// </summary>
	/// <returns></returns>
	public static List<PlantPotController> getPots()
	{
		return GetInstance.activePots;
	}

	public static void setPots(List<PlantPotController> activePots)
	{
		GetInstance.activePots = activePots;
		foreach (PlantPotController pot in activePots)
        {
			DontDestroyOnLoad(pot);
		}
	}
	/// <summary>
	/// Sets the player.
	/// </summary>
	/// <param name="player"></param>
	public static void SetPlayer(Player player)
	{
		GetInstance.player = player;
		DontDestroyOnLoad(player);
	}

	/// <summary>
	/// Sets the store inventory
	/// </summary>
	/// <param name="store"></param>
	public static void SetStoreInventory(StoreInventory store)
	{
		GetInstance.store = store;
		DontDestroyOnLoad(store);
	}
}
