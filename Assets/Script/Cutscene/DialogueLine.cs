using UnityEngine;
using System;
using NUnit.Framework;

[System.Serializable]
public class DialogueLine
{
    [Header("Dialogue Event")]
    public bool isDialogueEvent;
    public GameObject dialogueBoxPrefab;
    public GameObject dialoguePosition;

    [Header("Movement Event")]
    public bool isMovementEvent;
    public GameObject actor;
    public Vector2 endPosition;

}
