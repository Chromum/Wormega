using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Transitions/Transition")]
public class AITransition : ScriptableObject
{
    public AIDecision Decision;
    public BaseAIState TrueState;
    public BaseAIState FalseState;

    public virtual void Execute(AIBase aiBase)
    {
        if (Decision.Decide(aiBase) && !(TrueState is RemainInState))
            aiBase.CurrentState = TrueState;
        else if (!(FalseState is RemainInState))
            aiBase.CurrentState = FalseState;
    }
}
