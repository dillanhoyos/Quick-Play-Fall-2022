namespace DuncanUtilities
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    class Timer : MonoBehaviour
    {
        public float Target;
        public float Time { get; private set; } = 0f;
        public delegate void TimerReachedDelegate();
        TimerReachedDelegate onTimerReached;

        public Timer(float target)
        {
            Target = target;
        }

        public void Reset(float newTarget)
        {
            Time = 0f;
            Target = newTarget;
        }

        private void Update()
        {
            Time += UnityEngine.Time.deltaTime;

            if (Time >= Target)
            {
                onTimerReached?.Invoke();
            }
        }
    }

    class Tools : MonoBehaviour
    {
        static public Vector3 randomVector3(float range)
        {
            return new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
        }
    }
}


