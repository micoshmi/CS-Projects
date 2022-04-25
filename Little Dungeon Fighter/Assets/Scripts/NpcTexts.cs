using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTexts : Colliderble
{
    public string message;

    private float coolDown = 4.0f;
    private float lastShout = -4.0f;

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastShout > coolDown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, coolDown);
        }
    }
}
