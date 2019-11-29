using System.Collections;
using UnityEngine;

/// <summary>
/// This script translates an object at an interval with a periodic delay between translations.
/// </summary>
public class IntervalTranslate : MonoBehaviour 
{
	private bool hasStopped;
	private float translation = 0; 
	private float translationAmt = 0;
	private float originalTarget = 0;

    public float intervalTime = 0f;
    public float intervalDistance = 0f;
    public float intervalDelay = 0f;
    public bool useStartDirection = true;
    private bool readyToReverse = false;

    private bool goUpFirst;
	private bool goSidewaysFirst;
	public enum TranslationPattern
	{
		UpDown,
		LeftRight
	}
    public TranslationPattern selectedTranslationPattern;

	public float targetTranslation = 0f;
	public float translationDelay = 0f;
	public float translationSpeed = 1f;

    void Start()
	{
        StartCoroutine(Translate());
	}

    #region COROUTINES

    /// <summary>
    /// Translate an object between two points at an interval.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Translate()
    {
        // Set the distance and direction for the current interval
        Vector3 intervalDistance;
        if(readyToReverse)
        {
            intervalDistance = ConfigureDistance().ReverseDirection();
        }
        else
        { 
            intervalDistance = ConfigureDistance(); 
        }

        Vector3 fromPosition = transform.position;
        Vector3 toPosition = fromPosition + intervalDistance;

        // Translate the object per frame by interpolating between two points
        float time = 0f;
        while (time != 1)
        {
            time += Time.deltaTime / intervalTime;
            time = Mathf.Clamp(time, 0, 1);

            transform.position = Vector3.Lerp(fromPosition, toPosition, time);
            yield return null;
        }

        // Delay motion between intervals
        yield return new WaitForSecondsRealtime(intervalDelay);
        
        // Set the bit to indicate to reverse direction at during the next internval.
        readyToReverse = readyToReverse ? false : true;
        StartCoroutine(Translate());
    }
    #endregion COROUTINES

    #region FUNCTIONS
    /// <summary>
    /// Initialize inspector settings into the script.
    /// </summary>
    private Vector3 ConfigureDistance()
	{
        int direction = useStartDirection ? 1 : -1;
        
        switch (selectedTranslationPattern)
        {
            case(TranslationPattern.UpDown):
                return new Vector3(0, direction, 0) * intervalDistance;

            case(TranslationPattern.LeftRight):
                return new Vector3(direction, 0, 0) * intervalDistance;

            default:
                return new Vector3();
        }
	}

    /// <summary>
    /// Passes in initialized values from <CreateScannerType>.
    /// </summary>
    /// <param name="goUpFirst"></param>
    /// <param name="goSidewaysFirst"></param>
    /// <param name="targetTranslation"></param>
    /// <param name="translationSpeed"></param>
    /// <param name="translationDelay"></param>
	public void InitializeScannerTranslationPattern(bool goUpFirst, bool goSidewaysFirst, 
		float targetTranslation, float translationSpeed, float translationDelay)
	{
		this.targetTranslation = targetTranslation;
		this.translationSpeed = translationSpeed;
		this.translationDelay = translationDelay;

        return;
	}
    #endregion FUNCTIONS
}

public static class TranslationExtensions
{
    /// <summary>
    /// If the translation pattern setting is positive or negative, then reverse values.
    /// </summary>
    public static Vector3 ReverseDirection(this Vector3 setting)
    {
        const int REVERSE = -1;
        return setting *= REVERSE;
    }
};
