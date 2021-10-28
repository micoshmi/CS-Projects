using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    private Transform player;
    private Vector3 tempPos;
    [SerializeField]
    private float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //repeating the function if player == null
        if(player == null)
        {
            return;
        }

        //following the player with the game cam on axis x
        tempPos = transform.position;
        tempPos.x = player.position.x;
        if (tempPos.x < minX)
        {
            tempPos.x = minX;
        }
        if (tempPos.x > maxX)  
        {
            tempPos.x = maxX;
        }
        transform.position = tempPos;
    }
}
