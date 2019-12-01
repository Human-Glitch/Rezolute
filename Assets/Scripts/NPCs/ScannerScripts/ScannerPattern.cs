using System.Collections;
using UnityEngine;
using static Enums;

/// <summary>
/// This script determines the behavior of the spawned scanner 
/// based off the settings initialized from <CreateScanner>.
/// </summary>
public class ScannerPattern : MonoBehaviour 
{
	//private bool reachedTarget = false;
    public ScannerSettings scannerSettings;
    
    public Vector3 fromPosition;
    public Vector3 toPosition;

    void Start()
    {
        switch (scannerSettings.SelectedMovementPattern)
        {
            case(MovementPattern.Active):
                fromPosition = scannerSettings.SpawnPoint + new Vector3(30f, 6f, 0f);
                toPosition = fromPosition - new Vector3(scannerSettings.Distance, 0f, 0f);
                break;

            case(MovementPattern.Patrol):
                fromPosition = scannerSettings.SpawnPoint + new Vector3(15f, 4f, 0f);
                toPosition = fromPosition + new Vector3(scannerSettings.Distance, 0, 0);
                break;

            default:
                fromPosition = new Vector3();
                toPosition = new Vector3();
                break;
        }

        StartCoroutine(ActivateMovementPattern());
    }

 //   void Update () {

	//	if (scannerSettings.SelectedMovementPattern == MovementPattern.Patrol && reachedTarget == false) 
	//	{
	//		iTween.MoveUpdate(gameObject, 
	//			iTween.Hash("x", (scannerSettings.SpawnPoint.x + 20), 
	//				"y", scannerSettings.SpawnPoint.y + 2.5, 
	//				"time", scannerSettings.HashTime,
	//				"delay", scannerSettings.HashDelay, 
	//				"onupdate", "myUpdateFunction"
	//			)
	//		);	
	//		StartCoroutine(CompleteITween());
	//	}

	//	if (scannerSettings.SelectedMovementPattern == MovementPattern.Active) 
	//	{
	//		iTween.MoveUpdate(gameObject, 
	//			iTween.Hash("x", (scannerSettings.SpawnPoint.x - 40), 
	//				"y", scannerSettings.SpawnPoint.y + 6, 
	//				"time", scannerSettings.HashTime,
	//				"delay", scannerSettings.HashDelay, 
	//				"onupdate", "myUpdateFunction"
	//			)
	//		);
	//		StartCoroutine(TimedDeath());
	//	}
	//}

    #region FUNCTIONS
    public void Initialize(ScannerSettings scannerSettings)
	{
		this.scannerSettings = scannerSettings;
	}

	//private void StopITween(){ reachedTarget = true; }
    #endregion FUNCTIONS

    #region COROUTINES

    private IEnumerator ActivateMovementPattern()
    {
        float time = 0f;
        while (time != 1)
        {
            time += Time.deltaTime / scannerSettings.Time;
            time = Mathf.Clamp(time, 0, 1);

            transform.position = Vector3.Lerp(fromPosition, toPosition, time);
            yield return null;
        }

        switch (scannerSettings.SelectedMovementPattern)
        {
            case(MovementPattern.Active):
                StartCoroutine(TimedDeath());
                break;

            case(MovementPattern.Patrol):
                //yield return new WaitForSecondsRealtime(scannerSettings.Delay);

                Vector3 tempPosition = new Vector3(toPosition.x, toPosition.y, toPosition.z);
                toPosition = fromPosition;
                fromPosition = tempPosition;

                StartCoroutine(ActivateMovementPattern());
                break;
        }
    }
	private IEnumerator TimedDeath()
	{
		Destroy(this.gameObject, scannerSettings.Delay + 1f);
		yield return new WaitForSecondsRealtime(scannerSettings.Delay);
		//gameObject.GetComponent<Fade>().enabled = true;
	}

	//private IEnumerator CompleteITween()
	//{
	//	yield return new WaitForSecondsRealtime(scannerSettings.HashTime - .5f);
	//	StopITween();
	//}
    #endregion COROUTINES
}
