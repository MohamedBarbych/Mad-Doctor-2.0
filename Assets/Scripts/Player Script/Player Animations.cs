using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator animat;

    private Vector3 tempScale;

    private int currentAnimation;

    private void Awake()
    {
        animat = GetComponent<Animator>();//Attached object
    }

    public void PlayAnimation(string animationName)
    {
        //Test if the current animation is bieng played
        if (currentAnimation == Animator.StringToHash(animationName)) //converting animations into integers
            return;

        animat.Play(animationName);

        currentAnimation = Animator.StringToHash(animationName);
    }

    public void setFacingDirection(bool faceRight)
    {
        tempScale = transform.localScale;

        if (faceRight)
            tempScale.x = 1f;
        else
            tempScale.x = -1f;

        transform.localScale = tempScale;
    }

}
