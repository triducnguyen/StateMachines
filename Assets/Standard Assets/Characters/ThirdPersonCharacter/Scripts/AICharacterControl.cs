using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        private bool jump;
        private bool crounching;


        public bool isCrying = false;
        private void Start()
        {
            //get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);

            if (!DestinationReached())
            {
                if (!jump && !crounching)
                    character.Move(agent.desiredVelocity, false, false);
                else if(jump && !crounching)
                    character.Move(agent.desiredVelocity, false, true);
                else if(!jump && crounching)
                    character.Move(agent.desiredVelocity, true, false);

            }
            else
                character.Move(Vector3.zero, false, false);
            
            if (isCrying)
                character.Cry(true);
            else
                character.Cry(false);
            
        }
        public bool DestinationReached()
        {
            return agent.remainingDistance < agent.stoppingDistance;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
            jump = false;
            crounching = false;
        }

        public void SetTarget(Transform target, bool jump)
        {
            this.target = target;
            this.jump = jump;
            crounching = false;
        }

        public void SetTarget(Transform target, bool jump, bool crounching)
        {
            this.target = target;
            this.jump = jump;
            this.crounching = crounching;
        }
    }
}
