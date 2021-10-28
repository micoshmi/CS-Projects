using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    [HideInInspector]
    public float monsterSpeed;

    private Rigidbody2D myBody;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //enemy movement speed
        myBody.velocity = new Vector2(monsterSpeed, myBody.velocity.y);
    }
}
