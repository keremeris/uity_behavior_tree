using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector: Node
{
  //the child nodes for this selector
  protected List<Node> m_nodes = new List<Node>();
  
  //the constructor requires a list of child nodes to be passed in 
  public Selector(List<Node> nodes){
    m_nodes = nodes;
  }
  public Selector(Node node1){
    m_nodes.Add(node1);  
  }
  public Selector(Node node1,Node node2,Node node3){
    m_nodes.Add(node1);
    m_nodes.Add(node2);
    m_nodes.Add(node3);
  }
  ///if any of the children retun sucess the selector will immediately return success. if all the children fail then it will return fail 
  public override NodeStates Evaluate(){
    foreach(Node node in m_nodes){
        switch (node.Evaluate()){
            case NodeStates.FAILURE:
                continue;
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
            default:
                continue;           
        }
    }
    m_nodeState = NodeStates.FAILURE;
    return m_nodeState;
  }
}
