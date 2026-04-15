using System.Security.Cryptography;
using UnityEngine;


///<summary>
/// Hold data between scenes to show real time sprites in the garden scene
/// Authors: Joshua Trepanier
/// Date/revisions: March 4th 2026
///</summary>
public class PlantData : MonoBehaviour
{
    public static PlantData Instance;

    public int currentStage;
    public bool hasSoil;
    public bool hasSeed;
    public int currentPot;

    // new
    public PlantInstance[] plantInstance;

    ///<summary>
    /// Singleton disign pattern to make sure only one PlantData object is made
    /// as these will keep be duplicated as you save plant data
    ///</summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}