using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetForwardMovement(Vector2 movement)
    {
        bool isMoving = movement != Vector2.zero;
        animator.SetBool("FWalk", isMoving);

    }
     
     public void Jump()
     {
         animator.SetTrigger("Jump");
     } 

   
}
