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
        private UnityEvent onLoop;

        private int currentCount = 0;

        [ContextMenu("Start Loop")]
        private void BeginLoop()
        {
            if(_repeatCount < 0)
            {
                StartCoroutine(InfiniteLoop());
            }
            else
            {
                StartCoroutine(DefiniteLoop());
            }
        }

        private IEnumerator DefiniteLoop()
        {
            Debug.Log("Event count: " + currentCount);
            onLoop?.Invoke();
            currentCount += 1;
            if(currentCount < _repeatCount)
            {
                yield return new WaitForSeconds(_waitTime);
                yield return StartCoroutine(DefiniteLoop());
            }
        }

        private IEnumerator InfiniteLoop()
        {
            Debug.Log("Event count: " + currentCount);
            onLoop?.Invoke();
            yield return new WaitForSeconds(_waitTime);
            yield return StartCoroutine(InfiniteLoop());
        }
    }
}
