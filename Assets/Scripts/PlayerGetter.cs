using System.Dynamic;
using UnityEngine;

public class PlayerGetter : MonoBehaviour
{
    private static PlayerGetter playerGetter;
    public Player player;
    public static PlayerGetter GetInstance
	{
		get
		{
			if (!playerGetter)
			{
				playerGetter = new GameObject().AddComponent<PlayerGetter>();
				// name it for easy recognition
				playerGetter.name = playerGetter.GetType().ToString();
				// mark root as DontDestroyOnLoad();
				DontDestroyOnLoad(playerGetter.gameObject);
			}
			return playerGetter;
		}
	}
    public static Player GetPlayer()
    {
        return GetInstance.player;
    }
	public static void SetPlayer(Player setPlayer)
	{
		GetInstance.player = setPlayer;
	}
    public override string ToString()
    {
        return base.ToString();
    }
}
