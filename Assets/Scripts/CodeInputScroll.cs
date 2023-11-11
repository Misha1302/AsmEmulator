using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public sealed class CodeInputScroll : MonoBehaviour, IGameManagerInitializable
{
    private readonly List<int> pointLines = new();

    private TMP_InputField codeField;
    private GameManager gameManager;
    private string oldText;

    private void Start()
    {
        codeField = GetComponent<TMP_InputField>();
        codeField.scrollSensitivity = 0.25f;
    }

    public void Init(GameManager gm)
    {
        gameManager = gm;
    }
}