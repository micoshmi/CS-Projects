using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldAmount = 5;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.gold += goldAmount;
            // +gold
            GameManager.instance.ShowText("+" + goldAmount + " Gold!", 25, Color.yellow, transform.position, Vector3.up * 25, 2.0f);
        }
    }
}

