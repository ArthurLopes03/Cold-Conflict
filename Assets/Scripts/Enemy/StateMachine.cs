using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialise()
    {

        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //Check if activestate != null
        if(activeState != null)
        {
            //Run clean up on active state
            activeState.Exit();
        }
        //Change to a new state
        activeState = newState;

        if(activeState != null)
        {
            //setup new state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();

            activeState.Enter();
        }
    }
}
