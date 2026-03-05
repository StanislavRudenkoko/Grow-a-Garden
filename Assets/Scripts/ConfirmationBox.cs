using UnityEngine;

public class ConfirmationBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Buy()
    {
        Debug.Log("Player buys Item if coins are sufficient.");
        // logic here
        Destroy(gameObject);
    }
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
