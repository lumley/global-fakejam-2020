using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CombatClickable : MonoBehaviour, IPointerClickHandler
{
    // Class that will Read input. It will know when it has been clicked.

    public UnityEvent OnClick;


    public void OnPointerClick(PointerEventData eventData) => OnClick?.Invoke();
}
