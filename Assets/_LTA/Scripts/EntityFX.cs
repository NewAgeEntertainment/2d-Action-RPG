using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Hit FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float FlashDuration;
    private Material originalMat;


    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(FlashDuration);
        sr.material = originalMat;
    }
}

