using System;
using Template.UI.Text;

using UniRx;
using UniRx.Diagnostics;
using UnityEngine;

namespace Template.UniRx.UI.Text
{
    public class TypingText : BaseTypingText
    {
        protected Subject<Unit> onEnd = new Subject<Unit>(); 
        public IObservable<Unit> OnEnd => onEnd.AsObservable();
        
        IDisposable typingStream;
        void Start()
        {
            Init("Hello world, It's UniRx implementation of TypingText!");
            Launch();
            PrintDebug();
        }

        void PrintDebug()
        {
            //A Simple emitted version of stream (stream is a Subscribe(...))
            //IDisposable onEndStream = OnEnd.TakeUntilDisable(this).Subscribe(_ => { Debug.Log("OnEnd"); });

            //A Endless emitted version of stream
            //IDisposable onEndEndlessStream = OnEnd.TakeUntilDisable(this)
            //    .Debug("UniRx Endless TypingText")
            //    .DoOnCancel(() => Debug.Log("Call Endless OnCancel"))
            //    .DoOnCompleted(() => Debug.Log("Call Endless OnComplete"))
            //    .Subscribe(_ => Debug.Log("Call Endless OnNext"));

            //A Once emitted version of stream
            IDisposable onEndOnceStream = OnEnd.TakeUntilDisable(this)  //Call onEndOnceStream.Dispose() when called OnDisable() of gameObject
                .First()                                                //Take only first OnNext()
            //    .Debug("UniRx Once TypingText")                         //print all information about stream
                .DoOnCancel(() => Debug.Log("Call Once OnCancel"))      //print debug.log(...) if early call .Dispose()
                .DoOnCompleted(() => Debug.Log("Call Once OnComplete")) //print if stream succeeded completed
                .Subscribe(_ => Debug.Log("Call Once OnNext"));         // do on catch OnNext()
        }

        public override void Launch()
        {
            typingStream?.Dispose();
            typingStream = StartTypingText().ToObservable().Subscribe();
        }
        public override void Break()
        {
            typingStream?.Dispose();
            base.Break();
            onEnd?.OnNext(new Unit());
        }

        protected override void Reset()
        {
            typingStream?.Dispose();
            text.text = string.Empty;
        }
    }
}