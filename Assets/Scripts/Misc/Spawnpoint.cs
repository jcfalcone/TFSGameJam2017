﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour 
{

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Spawnpoint.png", true);    
    }
}
