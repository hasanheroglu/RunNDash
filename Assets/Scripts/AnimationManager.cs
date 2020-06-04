using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationManager
{
    public static IEnumerator Scale(GameObject animatedObject, Vector3 startScale, Vector3 endScale, float duration)
    {
        var startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            animatedObject.transform.localScale = Vector3.Lerp(startScale, endScale, ((Time.time - startTime)/duration));
            yield return null;
        }

        animatedObject.transform.localScale = endScale;
    }
}
