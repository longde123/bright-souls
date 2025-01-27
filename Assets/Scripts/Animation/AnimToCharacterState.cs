﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrightSouls
{
    public class AnimToCharacterState : StateMachineBehaviour
    {
        public enum Condition
        {
            StateEnter,
            StateExit,
            TransitionTo
        }

        private bool IsNotTransitionCondition
        {
            get { return condition != Condition.TransitionTo; }
        }

        private Character animatorOwner = null;

        public States targetState = States.Default;
        public StateMachineType stateMachine = StateMachineType.Generic;
        public Condition condition = Condition.StateEnter;
        public string nextState = string.Empty;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (condition == Condition.StateEnter)
            {
                UpdateState(animator);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (condition == Condition.StateExit)
                UpdateState(animator);
            if (condition == Condition.TransitionTo)
            {
                if (animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(nextState))
                {
                    UpdateState(animator);
                }
            }
        }

        private void UpdateState(Animator animator)
        {
            if (animatorOwner == null)
            {
                animatorOwner = animator.GetComponent<Character>();
            }
            else
            {
                animatorOwner.SetState(stateMachine, targetState);
            }
        }
    }
}