using System;
using UnityEngine;

namespace Ui
{

    public abstract class MyStateMachineBehaviour
    {
        public abstract void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public abstract void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }
    

    public abstract class State :
#if UNITY_NEW       
    StateMachineBehaviour
#else
    MyStateMachineBehaviour
#endif



    {
        [SerializeField]
        private string stateName;

        public string StateName { get { return stateName; } set { stateName = value;} }


        private void Enter()
        {
            Debug.Log("State.Enter() - " + StateName);

            OnEnter();
        }

        private void Leave()
        {
            Debug.Log("State.Leave() - " + StateName);

            OnLeave();
        }

        public virtual void OnEnter()
        { }
        
        public virtual void OnLeave()
        { }

        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Enter();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Leave();
        }
    
    }

}
