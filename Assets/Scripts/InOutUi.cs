using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class InOutUi : MonoBehaviour
{
    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private GameObject popupGroup;
    [SerializeField] private GameObject popup;

    private List<GameObject> popups = new();

    public TMP_InputField TextInput => textInput;


    public void Out(string str)
    {
        var obj = Instantiate(popup, popupGroup.transform);
        obj.GetComponentInChildren<TMP_Text>().text = str;
        popups.Add(obj);

        if (popups.Count > 4)
        {
            Destroy(popups[0]);
            popups.RemoveAt(0);
        }

        for (var i = 0; i < popups.Count;)
            if (popups[i] == null) popups.RemoveAt(i);
            else i++;

        Destroy(obj, 7);
    }


    public void Clear()
    {
        foreach (var p in popups)
            Destroy(p);

        popups = new List<GameObject>();
    }
}