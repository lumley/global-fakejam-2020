using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MapClick : MonoBehaviour, IPointerClickHandler
{

    public UnityEvent<Vector3> mapClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        mapClicked.Invoke(eventData.pointerCurrentRaycast.worldPosition);
    }
}
