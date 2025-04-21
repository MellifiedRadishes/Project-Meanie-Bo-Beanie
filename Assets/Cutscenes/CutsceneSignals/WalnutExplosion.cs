using UnityEngine;

public class WalnutExplosion : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void Explode() {
        animator.SetTrigger("Explode");
    }
}
