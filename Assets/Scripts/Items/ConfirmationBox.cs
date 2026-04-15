using UnityEngine;
/// <summary>
/// Confirmation box
/// Author: Tin Trinh
/// Date: Mar. 4, 2026
/// Source: None
/// </summary>
public abstract class ConfirmationBox : MonoBehaviour
{
    public Item Item { get; set; }
    public Store Store { get; set; }
    public GameObject content;
    public Slot Slot { get; set; }


    public abstract void Action();

    /// <summary>
    /// Closes the confirmation object when clicked.
    /// </summary>
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
