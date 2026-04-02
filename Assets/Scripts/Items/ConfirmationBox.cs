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
    public Store Store {get; set;}
    public GameObject content;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Action()
    {
        
    }
    /// <summary>
    /// Closes the confirmation object when clicked.
    /// </summary>
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
