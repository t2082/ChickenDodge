using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }
}
