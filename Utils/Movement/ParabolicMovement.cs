using System;
using UnityEngine;

namespace Oxtail.Utils
{
    public class ParabolicMovement : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private float m_Height;
        [SerializeField] private float m_Speed;
        [SerializeField] private bool m_FixedSpeed;
        [SerializeField] private bool m_LookAtTarget;
        [SerializeField] private float m_InitialAcceleration;

        private Vector3 m_StartPosition;
        private Vector3 m_TargetPosition;
        private float m_StepScale;
        private float m_CurrentStepScale;
        private float m_MovementProgress;
        private bool m_Moving;
        private float m_InitialRotation;
        private float m_CurrentAcceleration;

        public float Progress => m_MovementProgress;

        public Action OnMovementComplete;

        private void Awake()
        {
            SetTarget(m_Target);
        }

        public void SetTarget(Transform target)
        {
            m_Target = target;

            if (m_Target != null)
            {
                m_TargetPosition = m_Target.position;
                StartMovement();
            }
        }

        public void SetTargetPosition(Vector3 position)
        {
            m_TargetPosition = position;

            if (m_TargetPosition != null)
                StartMovement();
        }

        public void SetHeight(float height) => m_Height = height;

        public void SetSpeed(float speed) => m_Speed = speed;

        public void SetFixedSpeed(bool isFixed) => m_FixedSpeed = isFixed;

        public void InvertHeight() => m_Height *= -1;

        public void SetLookAt(bool look) => m_LookAtTarget = look;

        void Update()
        {
            if (!m_Moving)
                return;

            m_CurrentStepScale = m_StepScale + m_CurrentAcceleration;
            m_CurrentAcceleration = Mathf.Clamp(m_CurrentAcceleration - Time.deltaTime, 0, m_InitialAcceleration);
            m_MovementProgress = Mathf.Min(m_MovementProgress + Time.deltaTime * m_CurrentStepScale, 1.0f);
            float parabola = 1.0f - 4.0f * (m_MovementProgress - 0.5f) * (m_MovementProgress - 0.5f);
            Vector3 nextPos = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_MovementProgress);
            nextPos.y += parabola * m_Height;
            //if (m_Height != 0f)

            //else
            //    nextPos.x += parabola;

            if (m_LookAtTarget)
                transform.LookAt2D(nextPos, m_InitialRotation);
            transform.position = nextPos;

            if (m_MovementProgress == 1.0f)
            {
                m_Moving = false;
                OnMovementComplete?.Invoke();
            }
        }

        void StartMovement()
        {
            m_StartPosition = transform.position;

            float distance = Vector3.Distance(m_StartPosition, m_TargetPosition);
            if (m_FixedSpeed)
                m_StepScale = m_Speed;
            else
                m_StepScale = m_Speed / distance;

            m_InitialRotation = transform.eulerAngles.z;
            m_CurrentAcceleration = m_InitialAcceleration;
            m_MovementProgress = 0f;
            m_Moving = true;
        }
    }
}

