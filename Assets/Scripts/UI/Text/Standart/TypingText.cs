using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Template.UI.Text
{
    public class TypingText : BaseTypingText
    {
        public event Action onEnd;

        Coroutine coroutine;

        void Start()
        {
            Init("Hello world, It's Standart implementation of TypingText!");
            Launch();
            onEnd += () => { Debug.Log("StandartOnEnd"); };
        }

        public override void Launch()
        {
            if (string.IsNullOrEmpty(typingStr)) return;

            StopCoroutine();
            coroutine = StartCoroutine(StartTypingText());
        }
        public override void Break()
        {
            StopCoroutine();
            base.Break();            
            onEnd?.Invoke();
        }

        protected override void Reset()
        {
            base.Reset();
            StopCoroutine();
        }

        void StopCoroutine()
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}