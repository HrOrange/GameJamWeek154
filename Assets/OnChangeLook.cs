using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangeLook : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    public Sprite RockSprite;
    public Sprite ScissorSprite;
    public Sprite PaperSprite;
    public Sprite NoGoSprite;

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.tag == "Scissor") animator.transform.GetChild(animator.transform.childCount - 1).GetComponent<SpriteRenderer>().sprite = ScissorSprite;
        else if (animator.gameObject.tag == "Paper") animator.transform.GetChild(animator.transform.childCount - 1).GetComponent<SpriteRenderer>().sprite = PaperSprite;
        else if (animator.gameObject.tag == "Rock") animator.transform.GetChild(animator.transform.childCount - 1).GetComponent<SpriteRenderer>().sprite = RockSprite;
        else if (animator.gameObject.tag == "Field") animator.transform.GetChild(animator.transform.childCount - 1).GetComponent<SpriteRenderer>().sprite = null;
        else animator.transform.GetChild(animator.transform.childCount - 1).GetComponent<SpriteRenderer>().sprite = NoGoSprite;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
