using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    public Enemy Enemy;
    public Damageable Damageable;
    public BaseAIState initialState;

    private void Awake()
    {
        CurrentState = initialState;
        Damageable = GetComponent<Damageable>();
}

    public BaseAIState CurrentState;

    public void Update()
    {
        CurrentState.Execute(this);
    }
}
