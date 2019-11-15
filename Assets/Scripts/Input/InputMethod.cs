﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines if button for specific method was pressed or not
/// </summary>
public abstract class InputMethod : MonoBehaviour
{
    public abstract float GetHorizontalDirection();
    public abstract bool PressedJump();
}
