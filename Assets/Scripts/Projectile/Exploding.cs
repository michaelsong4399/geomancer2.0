using UnityEngine;
using UnityEditor;
public class Exploding : MonoBehaviour
{
    [SerializeField] private float _breakForce = 2;
    [SerializeField] private bool _broken;
    private ParticleSystem explosionParticle;

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
            //Destroy(replacement, 2.0f);
        }
    }
}