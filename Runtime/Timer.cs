using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Cochise.Timers
{
    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private float duration = 1f;
        [SerializeField]
        private UnityEvent onTimerStart;
        [SerializeField]
        private FloatEvent onTimerUpdate;
        [SerializeField]
        private FloatEvent onProgressUpdate;
        [SerializeField]
        private UnityEvent onTimerEnd;


        private IEnumerator _timer = null;
        private float _elapsedTime = 0f;

        public float ElapsedTime { get => _elapsedTime; }
        public float PercentComplete { get { return _elapsedTime / duration; } }
        public float RemainingTime { get { return duration - _elapsedTime; } }

        private void Start()
        {
            ResetTimer();
        }

        private IEnumerator SimpleTimer()
        {
            onTimerStart?.Invoke();

            while (_elapsedTime < duration)
            {
                _elapsedTime += Time.deltaTime;
                onTimerUpdate?.Invoke(_elapsedTime);
                onProgressUpdate?.Invoke(_elapsedTime / duration);

                yield return null;
            }
            onTimerEnd?.Invoke();

        }


        [ContextMenu("Reset Timer")]
        public void ResetTimer()
        {
            _timer = SimpleTimer();
            _elapsedTime = 0f;
        }

        [ContextMenu("Start Timer")]
        public void StartTimer()
        {
            StartCoroutine(_timer);
        }

        [ContextMenu("Stop Timer")]
        public void StopTimer()
        {
            StopCoroutine(_timer);
        }

        private string FormatTime(float timeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);
            return string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        }

        public string GetFormattedTimeRemaining()
        {
            return FormatTime(RemainingTime);
        }

        public string GetFormattedTimeElapsed()
        {
            return FormatTime(ElapsedTime);
        }
    }
}