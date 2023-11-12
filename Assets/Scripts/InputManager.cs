using System;
using JetBrains.Annotations;
using UnityEngine;

public sealed class InputManager : MonoBehaviour
{
    [CanBeNull] public Action LoadButtonPressed = null;
    [CanBeNull] public Action SaveButtonPressed = null;
    [CanBeNull] public Action StartButtonPressed = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            StartButtonPressed?.Invoke();

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
            SaveButtonPressed?.Invoke();

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            LoadButtonPressed?.Invoke();
    }
}