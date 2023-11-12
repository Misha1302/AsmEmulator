namespace Ui
{
    using System;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public sealed class InOutUi : MonoBehaviour
    {
        [SerializeField] private TMP_InputField codeField;

        [SerializeField] private TMP_InputField input;

        [SerializeField] private GameObject popupGroup;
        [SerializeField] private GameObject popup;

        private List<GameObject> popups = new();

        public TMP_InputField CodeField => codeField;


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

        public void In(Action<string> callback)
        {
            input.gameObject.SetActive(true);

            input.onSubmit.AddListener(Call);

            void Call(string s)
            {
                callback(s);
                input.onSubmit.RemoveListener(Call);
                input.gameObject.SetActive(false);
            }
        }
    }
}