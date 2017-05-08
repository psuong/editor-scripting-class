using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Ally, Enemy, Neutral, Prop }

[System.Serializable]
// Our struct is a MonoBehaviour, so we must serialize the struct to show in
// the inspector.
public struct EnemyInfo {
    public string name;
    public State state;
}

public class SampleClass : MonoBehaviour {

    // Non decorated
    public float uSlider = 120;
    public string uComment;

    // Decorators
    // The following decoratored code provides some neat and good spacing for our Inspector
    [Header("Decorators")]
    [Range(0f, 360f)]
    public float slider = 120;
    [Tooltip("Showing some Unity basic decorators")]
    [TextArea]
    public string comment;
    
    [Space]
    [HideInInspector] // We're going to hide our enemyInfo array for when we do editor scripting.
    public EnemyInfo[] enemyInfo;
}
