using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class TranslationSettings
{
    public TranslationPattern SelectedTranslationPattern { get; set; }
    public float IntervalTime { get; set; }
    public float IntervalDistance { get; set; }
    public float IntervalDelay { get; set; }
    public bool IsOneWay { get; set; }
    public bool UseStartDirection { get; set; }
}
