using System.Collections;
using UnityEngine;

/// <summary>
/// This script rotates an object at an interval with a periodic delay between rotations
/// </summary>
public class IntervalRotate : MonoBehaviour 
{
    public float rotationTime = 0f;
    public float angleOfRotation = 0f;
    public float rotationDelay = 0f;
    public bool isClockwise = true;

    #region PROPERTIES
    public int RotationCount { get; set; } = 0;
    #endregion PROPERTIES

    void Start()
	{
        StartCoroutine(RotateMe(Vector3.back * angleOfRotation, rotationTime));
	}

    #region COROUTINES
    IEnumerator RotateMe(Vector3 byAngles, float inTime) {
        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        Vector3 direction = isClockwise ? Vector3.back : Vector3.forward;
        float time = 0f;

        while(time != 1)
        {
            time += Time.deltaTime / inTime;
            time = Mathf.Clamp(time, 0, 1);

            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, time);
            yield return null;
        }

        RotationCount++;
        yield return new WaitForSecondsRealtime (rotationDelay);
        StartCoroutine(RotateMe(direction * angleOfRotation, rotationTime));
      }

    #endregion COROUTINES

}
