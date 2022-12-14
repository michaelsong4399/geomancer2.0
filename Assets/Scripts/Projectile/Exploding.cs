using UnityEngine;
using UnityEditor;
public class Exploding : MonoBehaviour
{
    [SerializeField] private float _breakForce = 2;
    [SerializeField] private bool _broken;
    private ParticleSystem explosionParticle;
    public float explosionRadius;

    void Start()
    { 
        //need to add code to get target object so it does not need to be set in Inspector
        explosionParticle = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Particle_Explosion.prefab", typeof(ParticleSystem)) as ParticleSystem;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (_broken) return;
        if (collision.relativeVelocity.magnitude >= _breakForce)
        {
            _broken = true;
            ParticleSystem newParticle = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            newParticle.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject, 0.2f);
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius, Physics.AllLayers);
            foreach (var hitCollider in hitColliders)
            {
                //Debug.Log(hitCollider.gameObject.name);
                if (hitCollider.gameObject.tag == "Slime_Base" || hitCollider.gameObject.tag == "Slime_Silver" || hitCollider.gameObject.tag == "Slime_Gold" || hitCollider.gameObject.tag == "Bat_Base")
                {
                    // hitCollider.gameObject.GetComponent<Slime>().applyDamage(1f + 9f*(explosionRadius - Vector3.Distance(gameObject.transform.position, hitCollider.gameObject.transform.position)));
                    // Apply damage but disregard y
                    Vector3 pos = hitCollider.gameObject.transform.position;
                    pos.y = gameObject.transform.position.y;
                    hitCollider.gameObject.GetComponent<Slime>().applyDamage(1f + 3f*(explosionRadius - Vector3.Distance(gameObject.transform.position, pos)));
                }
            }
            //Destroy(replacement, 2.0f);
        }
    }
}