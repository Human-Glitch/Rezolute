using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class ScannerSettings
{
    public MovementPattern SelectedMovementPattern {get; set;}
    public Vector3 SpawnPoint {get; set;}
    public float HashTime {get; set; }
	public float HashDelay {get; set;}
}
