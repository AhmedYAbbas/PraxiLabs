using System.Collections.Generic;
using UnityEngine;

public static class WaitFor 
{
    public static WaitForFixedUpdate FixedUpdate => _fixedUpdate;
    
    public static WaitForEndOfFrame EndOfFrame => _endOfFrame;
    
    private static readonly WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    private static readonly WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();
    private static readonly Dictionary<float, WaitForSeconds> _waitForSecondsDict = new(100, new FloatComparer());

    public static WaitForSeconds Seconds(float seconds)
    {
        if (seconds < 1f / Application.targetFrameRate)
            return null;

        if (!_waitForSecondsDict.TryGetValue(seconds, out var forSeconds))
        {
            forSeconds = new WaitForSeconds(seconds);
            _waitForSecondsDict[seconds] = forSeconds;
        }

        return forSeconds;
    }

    class FloatComparer : IEqualityComparer<float>
    {
        public bool Equals(float x, float y) => Mathf.Abs(x - y) <= Mathf.Epsilon;
        public int GetHashCode(float obj) => obj.GetHashCode();
    }
}