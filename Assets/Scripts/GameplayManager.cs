using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Style Gameplay")]
    [SerializeField] private bool _defaultThrowBall = true;
    [SerializeField] private bool _multipleThrowMode = false;
    [SerializeField] private bool _spidermanMode = false;

    public bool DefaultThrowBall { get => _defaultThrowBall; }
    public bool MultipleThrowMode { get => _multipleThrowMode; }
    public bool SpidermanMode { get => _spidermanMode; }
}
