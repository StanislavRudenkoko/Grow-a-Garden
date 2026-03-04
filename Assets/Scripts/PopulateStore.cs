using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PopulateStore : MonoBehaviour
{
    public GameObject item;
    public int numberToCreate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Populate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Populate()
    {
        GameObject newObject;
        for (int i = 0; i < numberToCreate; i++)
        {
            newObject = (GameObject)Instantiate(item, transform);
            Debug.Log(newObject.GetType());
            
        }
    }
}
