using System;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;

    public UiManager UiManager => uiManager;

    public InputManager InputManager { get; private set; }
    public AssemblerInterpreter AssemblerInterpreter { get; private set; }
    public Action OnAppExit;

    private void Start()
    {
        InputManager = gameObject.AddComponent<InputManager>();

        foreach (var item in FindObjectsOfType<CodeInputScroll>()) 
            item.Init(this);

        AssemblerInterpreter = new AssemblerInterpreter();
        AssemblerInterpreter.Init(this);
    }

    private void OnApplicationQuit()
    {
        OnAppExit?.Invoke();
    }
}