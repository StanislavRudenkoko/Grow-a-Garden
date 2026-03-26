using UnityEngine;
/// <summary>
/// Confirmation box
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public class ConfirmationBox : MonoBehaviour
{
    public Item Item { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Buys the item when clicked. *The logic is currently not implemented
    /// </summary>
    public void Buy()
    {
        Debug.Log("Player buys Item if coins are sufficient.");

        // logic here
        Destroy(gameObject);
    }

    /// <summary>
    /// Closes the confirmation object when clicked.
    /// </summary>
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
