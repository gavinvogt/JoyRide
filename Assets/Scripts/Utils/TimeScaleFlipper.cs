using UnityEngine;

namespace Utils
{
    public class TimeScaleFlipper
    {
        public float PreviousTimeScale { get; private set; }
        private bool _hasSavedScale = false;
        public bool IsFlipped
        {
            get { return _hasSavedScale; }
        }

        public void UpdateTimeScale(float newTimeScale)
        {
            if (_hasSavedScale)
            {
                Debug.LogError($"[TimeScaleFlipper] Updated time scale without reverting to {PreviousTimeScale} first");
            }
            PreviousTimeScale = Time.timeScale;
            Time.timeScale = newTimeScale;
            _hasSavedScale = true;
        }

        public void RevertTimeScale()
        {
            if (_hasSavedScale)
            {
                Time.timeScale = PreviousTimeScale;
            }
            else
            {
                Debug.LogError("[TimeScaleFlipper] No time scale to revert to");
            }
            _hasSavedScale = false;
        }
    }
}
