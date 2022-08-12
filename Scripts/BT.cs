using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using TMPro;
public enum NodeStates{
        RUNNING,SUCCESS,FAILURE
    }

//public enum Color{
    //    RED, GREEN ,YELLOW 
 //    }

public class BT : MonoBehaviour
{
    public Color m_evaluating;
    public Color m_succeeded;
    public Color m_failed;


    public Selector LeaderWeight_rootNode;// tier 4 node 
    public Selector Left_ft;//tier 3 node
    public Selector Right_ft;///tier 3 node
     //tier 2 node 
    public Sequence Forward_step_Right;
    public Sequence Side_step_Right;
    public Sequence Changeweight_Right;
    //tier 2 node
    public Sequence Forward_step_Left;
    public Sequence Side_step_Left;
    public Sequence Changeweight_Left;

   


    //playing the retargeted animation clip tier 1 leaf nodes 
    public ActionNode dance1_fl;
    public ActionNode dance2_fl;
    public ActionNode dance3_fl;
    public ActionNode dance4_sl;
    public ActionNode dance5_sl;
    public ActionNode dance6_sl;
    public ActionNode dance7_cwl;
    public ActionNode dance8_fr;
    public ActionNode dance9_fr;
    public ActionNode dance10_fr;
    public ActionNode dance11_sr;
    public ActionNode dance12_sr;
    public ActionNode dance13_sr;
    public ActionNode dance14_cwr;


    //game objects 
    public GameObject LeaderWeight_rootNodeBox;
    public GameObject Left_ftBox;
    public GameObject Right_ftBox;
    public GameObject Forward_step_LeftBox;
    public GameObject Side_step_LeftBox;
    public GameObject Changeweight_LeftBox;
    public GameObject Forward_step_RightBox;
    public GameObject Side_step_RightBox;
    public GameObject Changeweight_RightBox;
    //leaf nodes will be added here at some point

    public int m_targetValue;
    private int m_currentValue = 0;
    public int LeaderWeight_currentValue=0;
    [SerializeField]
    private TMP_Text m_valueLabel;
   // public Text LeaderWeight_valueLabel;
    //instantiation of nodes from the bottom up and assign the children
    void Start()
    {
        //the deepest level nodes will be instantaiated from buttom up

        //level 4 action nodes which contains the tango dance seq(FL)
        dance1_fl = new ActionNode(NotEqualToTarget); //FL-FR-FL-BR-BL-BR-CL
        dance2_fl = new ActionNode(AddFive); //FL-FR-SL-BR-BL-SR
        dance3_fl = new ActionNode(AddFive);
        dance4_sl = new ActionNode(AddFive);
        dance5_sl = new ActionNode(NotEqualToTarget);
        dance6_sl= new ActionNode(NotEqualToTarget);
        dance7_cwl = new ActionNode(NotEqualToTarget);
        dance8_fr= new ActionNode(AddFive);
        dance9_fr= new ActionNode(AddFive);
        dance10_fr = new ActionNode(NotEqualToTarget);
        dance11_sr = new ActionNode(NotEqualToTarget);
        dance12_sr = new ActionNode(NotEqualToTarget);
        dance13_sr = new ActionNode(NotEqualToTarget);
        dance14_cwr = new ActionNode(NotEqualToTarget);


        //level 3 action nodes //it had to co
            
       /* Forward_step_Left= new ActionNode(dance1_fl,dance2_fl,dance3_fl);
        Side_step_Left = new ActionNode(dance4_fl,dance5_fl,dance6_fl);
        Changeweight_Left= new ActionNode(dance7_fl);

        Forward_step_right= new ActionNode(dance8_fl,dance9_fl,dance10_fl);
        Side_step_right = new ActionNode(dance11_fl,dance12_fl,dance13_fl);
        Changeweight_right= new ActionNode(dance14_fl);*/
        /***************************** Arman modify**********************************************/
        Forward_step_Right= new Sequence(dance8_fr,dance9_fr,dance10_fr);
        Side_step_Right = new Sequence(dance11_sr,dance12_sr,dance13_sr);
        Changeweight_Right= new Sequence(dance14_cwr);

        Forward_step_Left= new Sequence(dance1_fl,dance2_fl,dance3_fl);
        Side_step_Left = new Sequence(dance4_sl,dance5_sl,dance6_sl);
        Changeweight_Left= new Sequence(dance7_cwl);

       
        /******************************************************************************************/
      
        //level 2 action Nodes passing thier parent node
       /* Left_ft= new ActionNode(Forward_step_Left,Side_step_Left,Changeweight_Left);
        Right_ft= new ActionNode(Forward_step_Right,Side_step_Right,Changeweight_Right);*/
        /***************************** Arman modify**********************************************/
        Left_ft= new Selector(Forward_step_Left,Side_step_Left,Changeweight_Left);
        Right_ft= new Selector(Forward_step_Right,Side_step_Right,Changeweight_Right);
        /******************************************************************************************/
      
       // Forward_step_Left= new ActionNode(left_FT)


        //lastly we add out root node ,and list of children(level one)
        List<Node> rootChildren = new List<Node>();
        rootChildren.Add(Left_ft);
        rootChildren.Add(Right_ft);

        //create root node object and pass the list 

        LeaderWeight_rootNode = new Selector(rootChildren);
       // LeaderWeight_valueLabel.text = LeaderWeight_currentValue.ToString();
        LeaderWeight_rootNode.Evaluate();

        //tickfunction??
        UpdateBoxes();
    }


    //some tick function after some miliseconds lets say 16??
    private void UpdateBoxes()
    {
        //update root node box 
        if(LeaderWeight_rootNode.nodeState == NodeStates.SUCCESS) {
            SetSucceeded(LeaderWeight_rootNodeBox);
        }else if (LeaderWeight_rootNode.nodeState == NodeStates.FAILURE) {
            SetFailed(LeaderWeight_rootNodeBox);
        }
        else if (LeaderWeight_rootNode.nodeState == NodeStates.RUNNING) {
            SetRUNNING(LeaderWeight_rootNodeBox);
        }
        //update for LF node box
        if(Left_ft.nodeState == NodeStates.SUCCESS){
            SetSucceeded(Left_ftBox);      
        }else if(Left_ft.nodeState == NodeStates.FAILURE){
            SetFailed(Left_ftBox);
        }
        else if(Left_ft.nodeState == NodeStates.RUNNING){
            SetRUNNING(Left_ftBox);
        }
        //update for RF node box
        if(Right_ft.nodeState == NodeStates.SUCCESS){
            SetSucceeded(Right_ftBox);      
        }else if(Right_ft.nodeState == NodeStates.FAILURE){
            SetFailed(Right_ftBox);
        }
        else if(Right_ft.nodeState == NodeStates.RUNNING){
            SetRUNNING(Right_ftBox);
        }
        //update for Forward_step_Left
        if(Forward_step_Left.nodeState == NodeStates.SUCCESS){
            SetSucceeded(Forward_step_LeftBox);      
        }else if(Forward_step_Left.nodeState == NodeStates.FAILURE){
            SetFailed(Forward_step_LeftBox);
        }
        else if(Forward_step_Left.nodeState == NodeStates.RUNNING){
            SetRUNNING(Forward_step_LeftBox);
        }
        //update for Side_step_Left
        if(Side_step_Left.nodeState == NodeStates.SUCCESS){
            SetSucceeded(Side_step_LeftBox);      
        }else if(Side_step_Left.nodeState == NodeStates.FAILURE){
            SetFailed(Side_step_LeftBox);
        }
        else if(Side_step_Left.nodeState == NodeStates.RUNNING){
            SetRUNNING(Side_step_LeftBox);
        }
        //update for Changeweight_Left box

        if(Changeweight_Left.nodeState == NodeStates.SUCCESS){
            SetSucceeded(Changeweight_LeftBox);      
        }else if(Changeweight_Left.nodeState == NodeStates.FAILURE){
            SetFailed(Changeweight_LeftBox);
        }
        else if(Changeweight_Left.nodeState == NodeStates.RUNNING){
            SetRUNNING(Changeweight_LeftBox);
        }
         //update for Forward_step_Right
        if(Forward_step_Right.nodeState == NodeStates.SUCCESS){
            SetSucceeded(Forward_step_RightBox);      
        }else if(Forward_step_Right.nodeState == NodeStates.FAILURE){
            SetFailed(Forward_step_RightBox);
        }
        else if(Forward_step_Right.nodeState == NodeStates.RUNNING){
            SetRUNNING(Forward_step_RightBox);
        }
        //update for Side_step_Right
        if(Side_step_Right.nodeState == NodeStates.SUCCESS){
            SetSucceeded(Side_step_RightBox);      
        }else if(Side_step_Right.nodeState == NodeStates.FAILURE){
            SetFailed(Side_step_RightBox);
        }
        else if(Side_step_Right.nodeState == NodeStates.RUNNING){
            SetRUNNING(Side_step_RightBox);
        }
        //update for Changeweight_Right box
        if(Changeweight_Right.nodeState == NodeStates.SUCCESS){
            SetSucceeded(Changeweight_RightBox);      
        }else if(Changeweight_Right.nodeState == NodeStates.FAILURE){
            SetFailed(Changeweight_RightBox);
        }
        else if(Changeweight_Right.nodeState == NodeStates.RUNNING){
            SetRUNNING(Changeweight_RightBox);
        }

    }
    private Color SetSucceeded(GameObject obj){
       // if(node.nodeState == NodeStates.SUCCESS ){
           // obj.Color = Color.GREEN;
        Debug.Log(obj);
        Debug.Log("Green");
        obj.GetComponent<Renderer>().material.SetColor("_Color", m_succeeded);
        return m_succeeded;
       // }
    }
    
    private Color SetFailed(GameObject obj){
        //if(node.nodeState == NodeStates.FAILURE ){
        Debug.Log(obj);
        Debug.Log("RED");
          
        obj.GetComponent<Renderer>().material.SetColor("_Color", m_failed);
        return m_failed;
            
       // }         
    }  
    private Color SetRUNNING(GameObject obj){
        //if(node.nodeState == NodeStates.FAILURE ){
        
        obj.GetComponent<Renderer>().material.SetColor("_Color", m_evaluating);
        return m_evaluating;
            
       // }         
    }        
     private NodeStates NotEqualToTarget(){
        if(m_currentValue != m_targetValue){
            return NodeStates.SUCCESS;
        }else{
            return NodeStates.FAILURE;
        }
    }

    private NodeStates AddFive() {
        m_currentValue += 5;
        m_valueLabel.text = m_currentValue.ToString();
        if(m_currentValue == m_targetValue){
            return NodeStates.SUCCESS;
        }else{
            return NodeStates.FAILURE;
        }

    }


}



