using Oxtail.ProjectFears;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxtail.Utils
{
    public class SetAnimatorBooleanSMB : StateMachineBehaviour
    {
        [SerializeField] private bool m_OnEnter;
        [SerializeField, ConditionalHide(nameof(m_OnEnter), true)] private string m_EnterParamName;
        [SerializeField, ConditionalHide(nameof(m_OnEnter), true)] private bool m_EnterState;

        [Space]
        [SerializeField] private bool m_OnExit;
        [SerializeField, ConditionalHide(nameof(m_OnExit), true)] private string m_ExitParamName;
        [SerializeField, ConditionalHide(nameof(m_OnExit), true)] private bool m_ExitState;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            if (!m_OnEnter)
                return;

            animator.SetBool(m_EnterParamName, m_EnterState);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            if (!m_OnExit)
                return;

            animator.SetBool(m_ExitParamName, m_ExitState);
        }
    }
}

