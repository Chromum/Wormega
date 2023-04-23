using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    public Enemy Enemy;
    [SerializeField] private BaseAIState initialState;

    private void Awake()
    {
        CurrentState = initialState;
    }

    public BaseAIState CurrentState;

    public void Update()
    {
        CurrentState.Execute(this);
    }
}
