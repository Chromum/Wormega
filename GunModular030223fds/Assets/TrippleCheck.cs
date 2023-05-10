using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Transitions/3Transition")]
public class TrippleCheck :  AndTransition
{
    public AIDecision ThirdDecision;
    public override void Execute(AIBase aiBase)
    {



        if (base.Decision.Decide(aiBase) && SecondDecision.Decide(aiBase) && ThirdDecision.Decide(aiBase) && !(TrueState is RemainInState))
            aiBase.CurrentState = TrueState;
        else if (!(FalseState is RemainInState))
            aiBase.CurrentState = FalseState;
    }

}
