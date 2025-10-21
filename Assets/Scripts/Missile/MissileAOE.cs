using UnityEngine;

public class MissileAOE : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int explosionDamage = 25;
    [SerializeField] private float explosionDuration = 0.1f;
    [SerializeField] private LayerMask damageableLayers = -1;
    
    private bool hasExploded = false;

    public void Explode(Vector3 explosionPosition)
    {
        if (hasExploded) return;
        
        hasExploded = true;
        
        transform.position = explosionPosition;
        
        DealAreaDamage();
        
        Destroy(gameObject, explosionDuration);
    }


    private void DealAreaDamage()
    {
        // Trouver tous les colliders dans le rayon d'explosion
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, damageableLayers);
        
        foreach (Collider hitCollider in hitColliders)
        {
            // Vérifier si l'objet peut subir des dégâts
            IHurtable hurtable = hitCollider.GetComponent<IHurtable>();
            if (hurtable != null)
            {
                hurtable.Hurt();
            }
        }
    }

    // Méthode pour visualiser la zone d'explosion dans l'éditeur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    // Propriétés publiques pour accéder aux paramètres depuis d'autres scripts
    public float ExplosionRadius => explosionRadius;
    public int ExplosionDamage => explosionDamage;
}