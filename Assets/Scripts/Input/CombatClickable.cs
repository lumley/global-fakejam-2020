﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class CombatClickable : MonoBehaviour, IPointerClickHandler
{
    // Reads input. Transmits when it has been clicked.
    public UnityEvent OnClick;

    public void OnPointerClick(PointerEventData eventData) => OnClick?.Invoke();
}
