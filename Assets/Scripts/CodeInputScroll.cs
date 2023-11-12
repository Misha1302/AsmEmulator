using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public sealed class CodeInputScroll : MonoBehaviour
{
    private TMP_InputField codeField;
    private string oldText;

    private void Start()
    {
        codeField = GetComponent<TMP_InputField>();
        codeField.scrollSensitivity = 0.25f;
    }
}