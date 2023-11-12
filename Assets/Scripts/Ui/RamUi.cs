using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public sealed class RamUi : MonoBehaviour
    {
        [SerializeField] private int cellsCount;
        [SerializeField] private GameObject cell;
        [SerializeField] private Image separator;
        [SerializeField] private RectTransform parent;

        private List<TMP_Text> cells = new();

        public IReadOnlyList<TMP_Text> Cells => cells;

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            var array = DrawCells(parent.rect);
            cells = new List<TMP_Text>(array);

            DrawSeparators(parent.rect);
        }

        private IEnumerable<TMP_Text> DrawCells(Rect rect)
        {
            var w = rect.size.x / cellsCount;
            var h = rect.size.y / cellsCount;

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

            return array;
        }

        private void DrawSeparators(Rect rect)
        {
            const int size = 5;

            // horizontals
            var curX = rect.x;
            for (var i = 0; i < cellsCount - 1; i++)
            {
                curX += rect.size.x / cellsCount;

                var sep = Instantiate(separator, parent);

                sep.transform.localPosition = new Vector2(curX, rect.y + rect.size.y / 2);
                sep.GetComponent<RectTransform>().sizeDelta = new Vector2(size, rect.size.y);
            }

            // verticals
            var curY = rect.y;
            for (var i = 0; i < cellsCount - 1; i++)
            {
                curY += rect.size.y / cellsCount;

                var sep = Instantiate(separator, parent);

                sep.transform.localPosition = new Vector2(rect.x + rect.size.x / 2, curY);
                sep.GetComponent<RectTransform>().sizeDelta = new Vector2(rect.size.x, size);
            }
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
}