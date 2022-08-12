using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence: Node 
{
  //the child nodes for this sequence
  private List<Node> m_nodes = new List<Node>();
  
  //the constructor requires a list of child nodes to be passed in 
  public Sequence(List<Node> nodes){
    m_nodes = nodes;
  }
  public Sequence(Node node1){
    m_nodes.Add(node1);
  }
  public Sequence(Node node1,Node node2,Node node3){
    m_nodes.Add(node1);
    m_nodes.Add(node2);
    m_nodes.Add(node3);
  }
  ///if any of the children retun sucess the selector will immediately return success. if all the children fail then it will return fail 
  public override NodeStates Evaluate(){
  
    bool anyChildRunning = false;
    
    foreach(Node node in m_nodes){
        switch (node.Evaluate()){
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
            case NodeStates.SUCCESS:
                continue;
            case NodeStates.RUNNING:
                anyChildRunning= true;
                continue;
            default:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;           
        }
    }
    m_nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
    return m_nodeState;
  }
}

