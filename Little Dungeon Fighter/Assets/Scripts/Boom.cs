using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}