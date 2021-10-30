using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Template.UI.Text.UniTask {
	public class TypingText : BaseTypingText {
		CancellationTokenSource _cancellationTokenSource;

		public event Action OnEnd;

		void Start()
		{
			Init("Hello world, It's UniTask implementation of TypingText!");
			Launch();
			OnEnd += () => { Debug.Log("UniTasksOnEnd"); };
		}
		
		
		public override void Launch() 
		{
			_cancellationTokenSource = new CancellationTokenSource();
			StartTypingText().ToUniTask(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
		}

		public override void Break() 
		{
			base.Break();
			_cancellationTokenSource?.Cancel();
			OnEnd?.Invoke();
		}

		protected override void Reset() 
		{
			base.Reset();
			_cancellationTokenSource?.Cancel();
		}
	}
}