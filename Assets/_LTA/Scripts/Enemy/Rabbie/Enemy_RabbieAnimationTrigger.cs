using UnityEngine;

public class Enemy_RabbieAnimationTrigger : MonoBehaviour
{
    private Enemy_Rabbie enemy => GetComponentInParent<Enemy_Rabbie>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }


    private void AttackTrigger()
    {
        foreach (Transform attackCheck in enemy.attackCheck)
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, enemy.attackCheckRadius); // Check for colliders within the attack range.

            foreach (var hit in colliders)
            {

                //if (hit.GetComponent<Player>() != null)
                //    hit.GetComponent<Player>().Damage(); // Call the Damage method of the Enemy component if it exists.
                if (hit.CompareTag("Player")) // Check if the collider has the "Enemy" tag.
                {
                    Player player = hit.GetComponent<Player>(); // Get the Enemy component from the collider.
                    if (enemy != null)
                    {
                        player.KnockBack(transform, enemy.knockbackForce); // Call the Knockback method of the Player component if it exists.
                        player.Damage(); // Call the Damage method of the Enemy component.
                    }
                }
            }
        }
    }

    private void ShowAttackIndicator() => enemy.ShowAttackIndicator(); // Call the OpenCounterWindow method of the Enemy_Rabbie component.
    private void HideAttackIndicator() => enemy.HideAttackIndicator(); // Call the CloseCounterWindow method of the Enemy_Rabbie component.
}
