using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cat : MonoBehaviour, IPointerClickHandler
{
    public GameObject speechBubble;
    private string[] catDialogue = new string[]{
        "Meow~",
        "Find what your lookin fur?",
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

    public void OnPointerClick(PointerEventData eventData)
    {
        speechBubble.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        speechBubble.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = catDialogue[UnityEngine.Random.Range(0, catDialogue.Length)];
    }

}
