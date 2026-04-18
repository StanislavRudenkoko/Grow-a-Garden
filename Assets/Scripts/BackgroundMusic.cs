/// <summary>
/// BackgroundMusic
/// Author: Stanislav Rudenko
/// Date: Apr. 17, 2026
/// Source: with help of Claude AI
/// </summary>

using UnityEngine;

/// <summary>
/// Ensures a single background music object survives scene loads via <c>DontDestroyOnLoad</c>.
/// </summary>
public class BackgroundMusic : MonoBehaviour
{
    /// <summary>Singleton; the first instance in the session is kept, duplicates are destroyed.</summary>
    public static BackgroundMusic instance;

    /// <summary>Registers this object as the persistent music holder or destroys it if one already exists.</summary>
    private void Awake()
    {
        if (instance == null) 
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
