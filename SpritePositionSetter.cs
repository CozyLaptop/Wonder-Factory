using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSetter : MonoBehaviour
{

    void Awake()
    {
        SetPosition();
    }

    void Update()
    {
        SetPosition();
    }

    void SetPosition()
    {
        // If you want to change the transform, use this
        Vector3 newPosition = transform.position;
        //newPosition.z = transform.position.y / 10;
        newPosition.z = 0;
        transform.position = newPosition;

        // Or if you want to change the SpriteRenderer's sorting order, use this
        // GetComponent<SpriteRenderer>().sortingOrder = (int)transform.position.y;
    }

}
