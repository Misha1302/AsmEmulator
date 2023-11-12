using System;
using System.Collections;
using UnityEngine;

namespace Ui
{
    public sealed class BlinkUi : MonoBehaviour
    {
        private static readonly int _start = Animator.StringToHash("start");

        [SerializeField] private Animator blinkAnimator;
        [SerializeField] private float blinkTime;

        private void Start()
        {
            blinkAnimator.gameObject.SetActive(false);
        }

        public void Blink(Action callback)
        {
            blinkAnimator.gameObject.SetActive(true);
            blinkAnimator.SetTrigger(_start);

            StartCoroutine(ExecuteAfter(callback, blinkTime / 2));
            StartCoroutine(ExecuteAfter(() => blinkAnimator.gameObject.SetActive(false), blinkTime));
        }

        private static IEnumerator ExecuteAfter(Action callback, float f)
        {
            yield return new WaitForSeconds(f);
            callback();
        }
    }
}