using UnityEngine;
using TMPro;

public class PlantDefinitionTooltipUI : TooltipBase
{
    public TMP_Text displayNameText;
    public TMP_Text scientificNameText;
    public TMP_Text familyText;
    public TMP_Text descriptionText;

    public void SetData(PlantDefinition def)
    {
        displayNameText.text = def.displayName;
        scientificNameText.text = def.scientificName;
        familyText.text = def.family;
        descriptionText.text = def.descriptionShort;
    }

    public void ShowBeside(Vector2 anchorPosition)
    {
        rectTransform.position = anchorPosition + new Vector2(350, 0);
        ClampToScreen();
        FadeIn();
    }
}