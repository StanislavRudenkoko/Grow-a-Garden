using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// A side panel that lists all soil types.
/// Appears when the player picks "Add Soil" from the dropdown.
///
/// HOW TO SET UP IN UNITY:
///   Same as PotDropdownUI — a panel with VerticalLayoutGroup,
///   start inactive, assign buttonPrefab.
/// </summary>
public class SoilPickerUI : MonoBehaviour
{
    [Header("References")]
    public GameObject buttonPrefab;

    private PlantPotController targetPot;
    private PotDropdownUI ownerDropdown;

    public static SoilPickerUI Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    public void OpenFor(PlantPotController pot, PotDropdownUI dropdown)
    {
        targetPot = pot;
        ownerDropdown = dropdown;

        // Clear old buttons
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        // Build one button per soil type
        foreach (string soil in PlantManager.Instance.soilTypes)
        {
            string captured = soil; // capture for lambda
            GameObject go = Instantiate(buttonPrefab, transform);
            go.GetComponentInChildren<TMP_Text>().text = soil;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Soil button clicked: " + captured);
                OnSoilSelected(captured);
            });
            go.GetComponent<Button>().onClick.AddListener(() => OnSoilSelected(captured));
        }

        // Position beside the dropdown
        transform.position = ownerDropdown.transform.position + new Vector3(200, 0, 0);
        gameObject.SetActive(true);
    }

    private void OnSoilSelected(string soilType)
    {
        //targetPot.potData.soilType = soilType; use dirt for now
        targetPot.potData.soilType = "Dirt";
        targetPot.RefreshVisuals();

        gameObject.SetActive(false);
        ownerDropdown.Close();
    }
}
