using System;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    public Action OnAppExit;

    public UiManager UiManager => uiManager;

    public InputManager InputManager { get; private set; }
    public AssemblerInterpreter AssemblerInterpreter { get; private set; }

    private void Start()
    {
        InputManager = gameObject.AddComponent<InputManager>();

        AssemblerInterpreter = new AssemblerInterpreter();
        AssemblerInterpreter.Init(this);
    }

    private void OnApplicationQuit()
    {
        OnAppExit?.Invoke();
    }
}