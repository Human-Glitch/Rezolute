using UnityEngine;
using static Enums;

/// <summary>
/// Create scanner type.
/// </summary>
public class CreateScannerType : MonoBehaviour 
{
	private float time;
	private bool stopSpawns = false;
	private bool timeStarted = false;
    private readonly Vector3 ActivePositionOffSet = new Vector3(20f, 6f, 0);
    private readonly Vector3 PatrolPositionOffSet = new Vector3(20f, 20f, 0);

    public float waitTime = 0;

    [Header("Scanner Prefabs")]
	public GameObject redScanner;
	public GameObject blueScanner;

	[Header("Scanner Settings")]
	public ScannerType selectedScannerType;
	public MovementPattern selectedMovementPattern;
    public TranslationPattern selectedTranslationPattern;
    public bool isOneWay = false;

	[Header("Initialize Movement Attributes")]
	public float hashTime;
	public float hashDelay;

	private Vector3 spawnPoint;
    private GameObject scanner;

	void Start () 
	{ 
		time = 0;
		spawnPoint = gameObject.transform.position;
	}

	void Update()
	{
		if (timeStarted) 
		{
			time += Time.deltaTime;
		}
	}

    #region TRIGGERS

    void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag != "Player") return;
		
		//Do this once
		if (time <= waitTime && timeStarted == false) 
		{
			timeStarted = true; 
			SpawnScannerWithSettings();
		}

		//Only do this once wait time has passed;
		if (time > waitTime) 
		{
			SpawnScannerWithSettings();
			time = 0;
		}
	}

    #endregion TRIGGERS

    #region FUNCTIONS

    private void SpawnScannerWithSettings()
	{
        if(stopSpawns) return;

        scanner = new GameObject();

        var scannerSettings = new ScannerSettings
        {
            SpawnPoint = spawnPoint,
            SelectedMovementPattern = selectedMovementPattern,
            HashTime = hashTime,
            HashDelay = hashDelay
        };

        var translationSettings = new TranslationSettings
        {
            SelectedTranslationPattern = selectedTranslationPattern,
            IsOneWay = isOneWay
        };

        switch (selectedScannerType)
        {
            case(ScannerType.RedScanner):
                scanner = Instantiate(redScanner,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    scanner.transform.rotation = transform.rotation);
                break;

            case(ScannerType.BlueScanner):
                scanner = Instantiate(blueScanner,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    scanner.transform.rotation = transform.rotation);
                break;
        }

        switch (selectedMovementPattern)
        {
            case(MovementPattern.Active):
                scanner.transform.position += ActivePositionOffSet;
                break;

            case(MovementPattern.Patrol):
                scanner.transform.position += PatrolPositionOffSet;
                stopSpawns = true;
                break;
        }

        scanner.GetComponent<ScannerPattern>().Initialize(scannerSettings);
		scanner.GetComponent<IntervalTranslate>().Initialize(translationSettings);
	}

    #endregion FUNCTIONS
}
