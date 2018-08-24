using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SteeringBehaviours
{

    public class AIAgent : MonoBehaviour
    {
        public Vector3 point = Vector3.zero;
        
        public NavMeshAgent agent;
        
        // Update is called once per frame
        void Update()
        {
            if (point.magnitude > 0)
            {
                agent.SetDestination(point);
            }
        }
        // Use this for initialization
        public void SetTarget(Vector3 point)
        {
            this.point = point;
        }
    }
}