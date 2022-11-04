using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerReader : MonoBehaviour
{
    public float lastValue = 0f;
    
    public void trigger (InputAction.CallbackContext context)
    {
        lastValue = context.ReadValue<float>();
    }
}
