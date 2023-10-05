using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cochise.Timers
{
    public class RepeatEvent : MonoBehaviour
    {
        [SerializeField]
        private float _waitTime = 1f;
        [SerializeField]
        private int _repeatCount = -1;
        [SerializeField]
        private UnityEvent _onLoop;

        private int _currentCount = 0;
        private IEnumerator _loop;

        [ContextMenu("Reset Loop")]
        private void ResetLoop()
        {
            _loop = _repeatCount < 0 ? InfiniteLoop() : DefiniteLoop();
            _currentCount = 0;
        }
        [ContextMenu("Start Loop")]
        private void BeginLoop()
        {
            
            StartCoroutine(_loop);
        }

        [ContextMenu("Stop Loop")]
        private void Stop()
        {
            StopCoroutine(_loop);
        }

        private IEnumerator DefiniteLoop()
        {
            _onLoop?.Invoke();
            _currentCount += 1;
            if(_currentCount < _repeatCount)
            {
                yield return new WaitForSeconds(_waitTime);
                yield return StartCoroutine(DefiniteLoop());
            }
        }

        private IEnumerator InfiniteLoop()
        {
            _onLoop?.Invoke();
            _currentCount += 1;
            yield return new WaitForSeconds(_waitTime);
            yield return StartCoroutine(InfiniteLoop());
        }
    }
}
