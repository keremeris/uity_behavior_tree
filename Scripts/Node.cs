using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public abstract class Node
{   
    //delegate trhat returns the state of a node 
    public delegate NodeStates NodeReturn();
    protected NodeStates m_nodeState;
    
    public NodeStates nodeState {
        get { return m_nodeState; } 
    
    }
    public Node(){
  
    }
    

    public abstract NodeStates Evaluate();
        
}
  
