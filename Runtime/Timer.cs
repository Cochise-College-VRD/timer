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
        private bool isCountDown = false;
        [SerializeField]
        private float duration = 1f;
        [SerializeField]
        private UnityEvent onTimerStart;
        [SerializeField]
        private FloatEvent onTimerUpdate;
        [SerializeField]
        private UnityEvent onTimerEnd;


        private IEnumerator _timer = null;
        private float _elapsedTime = 0f;

        public float ElapsedTime { get => _elapsedTime; }
        public float PercentComplete { get { return _elapsedTime / duration; } }

        private void Start()
        {
            ResetTimer();
        }

        private IEnumerator Countdown()
        {
            onTimerStart?.Invoke();

            while (_elapsedTime < duration)
            {
                _elapsedTime += Time.deltaTime;
                onTimerUpdate?.Invoke(duration - _elapsedTime);
                yield return null;
            }
            onTimerEnd?.Invoke();

        }

        private IEnumerator Countup()
        {
            onTimerStart?.Invoke();

            while (_elapsedTime < duration)
            {
                _elapsedTime += Time.deltaTime;
                onTimerUpdate?.Invoke(_elapsedTime);
                yield return null;
            }

            onTimerEnd?.Invoke();
        }

        public void ResetTimer()
        {
            _timer = isCountDown ? Countdown() : Countup();
            _elapsedTime = 0f;
        }

        public void StartTimer()
        {
            StartCoroutine(_timer);
        }

        public void StopTimer()
        {
            StopCoroutine(_timer);
        }

    }
}