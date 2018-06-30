using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

//    public StateBase currentState;
//    // Use this for initialization
//    void Start () {
//        Idle idleInstance = new Idle();
//        idleInstance.OnExitState();
//        Debug.Log(idleInstance.yo); //prints 30

//        StateBase sometate = new StateBase();
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//        if(currentState != null)
//        {
//            currentState.OnUpdateState();
//        }
//	}

//    public void SwitchToState(StateBase newState)
//    {
//        if(currentState != null)
//        {
//            currentState.OnExitState();
//        }
//        currentState = newState;
//        currentState.OnEnterState();
//    }
//}


//public abstract class StateBase : MonoBehaviour
//{
//    protected FSM fsm;


//    public int yo = 1;
//    public abstract void OnEnterState();

//    public abstract void OnExitState();

//    public abstract void OnUpdateState();
//}

//public class Idle : StateBase
//{
//    public override void OnEnterState()
//    {
//        yo = 5;
//    }
//    public override void OnUpdateState()
//    {
//        fsm.SwitchToState(new Aggro());
//    }
//    public override void OnExitState()
//    {
//        yo *= 3;
//    }
//}

//public class Aggro : StateBase
//{
//    public override void OnEnterState()
//    {

//    }       
}