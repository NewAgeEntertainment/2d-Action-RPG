using UnityEngine;

public class Enemy_RabbieAnimationTrigger : MonoBehaviour
{
    private Enemy_Rabbie enemy => GetComponentInParent<Enemy_Rabbie>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
