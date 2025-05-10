using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{

    private Player player => GetComponentInParent<Player>(); // Get the Player component from the parent GameObject.

    private void AnimationTrigger() // This method is called when the animation trigger is activated.
    {
        player.AnimationTrigger(); // Call the AnimationTrigger method of the Player component.
    }
}
