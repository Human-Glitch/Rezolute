using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class TranslationSettings
{
    public TranslationPattern SelectedTranslationPattern { get; set; }
    public float Time { get; set; }
    public float Distance { get; set; }
    public float Delay { get; set; }
    public bool IsOneWay { get; set; }
    public bool UseStartDirection { get; set; }
}
