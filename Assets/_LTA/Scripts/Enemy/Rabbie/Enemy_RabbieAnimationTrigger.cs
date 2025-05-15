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

                if (hit.GetComponent<Player>() != null)
                    hit.GetComponent<Player>().Damage(); // Call the Damage method of the Enemy component if it exists.

                //if (hit.CompareTag("Enemy")) // Check if the collider has the "Enemy" tag.
                //{
                //    Enemy enemy = hit.GetComponent<Enemy>(); // Get the Enemy component from the collider.
                //    if (enemy != null)
                //    {
                //        enemy.Damage(); // Call the Damage method of the Enemy component.
                //    }
                //}
            }
        }
    }
}
