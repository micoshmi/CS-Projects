using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        // Being able to update every single floating text in the array, every frame
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }    
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); // transfer world space to screen space s we can use it in the UI
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();

    }

    // Pulling mechanic for the floating text
    private FloatingText GetFloatingText()
    {
        // Taking floatingTexts array and lookin for something that isnt active
        FloatingText txt = floatingTexts.Find(t => !t.active);

        // if we dont find an active one, we create it
        if(txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
