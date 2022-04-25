using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go; // go stands for game object
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    // Showing the text
    public void Show()
    {
        active = true;
        //right now
        lastShown = Time.time;
        go.SetActive(active);
    }
    // Hiding the text
    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }

        // when the object(text) gettin hidden
        if (Time.time - lastShown > duration)
        {
            Hide();
        }
        go.transform.position += motion * Time.deltaTime;
    }
}
