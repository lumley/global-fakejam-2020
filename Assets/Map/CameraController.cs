using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 1;
    public float boundaryX = 10;
    public float boundaryY = 13;
    private Vector3 dragOrigin;
 
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(0)) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        var posX = pos.x * dragSpeed;
        var posY = pos.y * dragSpeed;
        Debug.Log(posX + "-" + posY);
        Vector3 move = new Vector3(posX, posY, 0);
        
        transform.Translate(move, Space.World);

        var transformPosition = transform.position;
        
        transform.position= new Vector3(
            Clamp(transformPosition.x, boundaryX),
            Clamp(transformPosition.y, boundaryY),
            transformPosition.z
        );
    }

    private float Clamp(float value, float bound)
    {
        if (value > bound)
        {
            return bound;
        }

        if (value < -bound)
        {
            return -bound;
        }

        return value;
    }
}
