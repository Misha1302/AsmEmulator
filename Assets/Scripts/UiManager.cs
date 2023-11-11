using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RamFiller))]
public sealed class UiManager : MonoBehaviour
{
    private static readonly int _start = Animator.StringToHash("start");

    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private TMP_Text[] registers;
    [SerializeField] private Animator blinkAnimator;
    [SerializeField] private float blinkTime;
    [SerializeField] private GameObject popupGroup;
    [SerializeField] private GameObject popup;


    private List<GameObject> popups = new();

    public RamFiller RamFiller { get; private set; }

    public TMP_InputField TextInput => textInput;

    private void Awake()
    {
        blinkAnimator.gameObject.SetActive(false);
        RamFiller = GetComponent<RamFiller>();
    }

    public void UpdateRegs(int[] regs)
    {
        for (var i = 0; i < regs.Length; i++)
            registers[i].text = regs[i].ToString();
    }


    public void Clear()
    {
        foreach (var t in registers)
            t.text = "0";

        foreach (var p in popups)
            Destroy(p);

        popups = new List<GameObject>();
    }

    public void Blink(Action callback)
    {
        blinkAnimator.gameObject.SetActive(true);
        blinkAnimator.SetTrigger(_start);

        StartCoroutine(ExecuteAfter(callback, blinkTime));
    }

    private IEnumerator ExecuteAfter(Action callback, float f)
    {
        yield return new WaitForSeconds(f);
        callback();
        blinkAnimator.gameObject.SetActive(false);
    }

    public void Popup(string str)
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
}