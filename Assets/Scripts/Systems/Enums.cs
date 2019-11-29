using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
	public enum MovementPattern
	{
		Patrol,
		Active
	}

	public enum ScannerType
	{
		RedScanner,
		BlueScanner
	}

    public enum TranslationPattern
	{
		UpDown,
		LeftRight,
        DoNothing
	}
}
