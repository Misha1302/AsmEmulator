using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public sealed class RamFiller : MonoBehaviour
{
    [SerializeField] private int cellsCount;
    [SerializeField] private GameObject cell;
    [SerializeField] private RectTransform parent;

    private List<TMP_Text> cells = new();
    [CanBeNull] public Action<RamFiller> OnGenerated = null;

    public IReadOnlyList<TMP_Text> Cells => cells;

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        var rect = parent.rect;
        var w = rect.size.x / cellsCount;
        var h = w;

        var xPos = rect.x;
        var yPos = rect.y + h * (cellsCount - 1);

        var array = new TMP_Text[cellsCount * cellsCount];

        for (var j = 0; j < cellsCount; j++)
        {
            for (var i = 0; i < cellsCount; i++)
            {
                var t = Instantiate(cell, parent).transform;

                var texts = t.GetComponentsInChildren<TMP_Text>();
                var number = texts.First(x => x.CompareTag("NumberText"));
                var value = texts.First(x => x.CompareTag("ValueText"));

                number.text = $"{j + i * cellsCount}";
                t.GetComponent<RectTransform>().sizeDelta = new Vector3(w, h);
                t.localPosition = new Vector3(xPos + w / 2, yPos + h / 2);

                array[j + i * cellsCount] = value;

                yPos -= h;
            }

            yPos = rect.y + h * (cellsCount - 1);
            xPos += w;
        }

        cells = new List<TMP_Text>(array);

        OnGenerated?.Invoke(this);
    }


    public void UpdateRam(List<int> ram)
    {
        for (var i = 0; i < cells.Count; i++)
            cells[i].text = ram[i].ToString();
    }

    public void Clear()
    {
        foreach (var t in cells)
            t.text = "0";
    }
}