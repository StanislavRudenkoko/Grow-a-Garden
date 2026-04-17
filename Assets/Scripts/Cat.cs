using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Cat class to interact with the cat in the store.
/// Author: Tin Trinh
/// Date: Apr. 15, 2026
/// Source: None
/// </summary>
public class Cat : MonoBehaviour, IPointerClickHandler
{
    public GameObject speechBubble;
    public Player player;
    private string[] catDialogue = new string[]{
        "Meow~",
        "Find what you're lookin fur?",
        "Meo meo meo",
        "The store restocks everrryday.",
        "Hiya!",
        "Mrrr",
        "Furrrtilizer helps plants grow!",
        "Make sure to water your plants meow~",
        "Meowww",
        "Purrr",
        "...",
        "Meeeeowwwww",
        "Murrp",
        "Meoo",
        "Miaou",
        "Sitting here is hard work!",
        "Don't you have a garrrden to tend to?",
        "Mew mew",
        "prrrrrrrrrrrrr",
        "Hey, watch the tail!",
        };

    void Start()
    {
        player = ObjectGetter.GetPlayer();
        speechBubble.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Mrrrow.\nWelcome, {player.PlayerName}-mew.";
    }

    /// <summary>
    /// Replaces the dialogue in the speech bubble to a cat dialogue phrase.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        speechBubble.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        speechBubble.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = catDialogue[UnityEngine.Random.Range(0, catDialogue.Length)];
    }

}
