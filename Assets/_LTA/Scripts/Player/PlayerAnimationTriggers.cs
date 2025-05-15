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

    private void AttackTrigger()
    {
        foreach (Transform attackCheck in player.attackCheck) // Iterate through each Transform in the attackCheck array.  
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, player.attackCheckRadius); // Check for colliders within the attack range.  

            foreach (var hit in colliders)
            {
                if (hit.CompareTag("Enemy")) // Check if the collider has the "Enemy" tag.  
                {
                    Enemy enemy = hit.GetComponent<Enemy>(); // Get the Enemy component from the collider.  
                    if (enemy != null)
                    {
                        enemy.Knockback(transform, player.knockbackForce); // Call the Knockback method of the Enemy component.
                        enemy.Damage(); // Call the Damage method of the Enemy component.  
                    }
                }
            }
        }
    }
}
