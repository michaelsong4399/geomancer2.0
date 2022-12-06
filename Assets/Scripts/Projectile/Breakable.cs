using UnityEngine;
public class Breakable : MonoBehaviour {
    [SerializeField] private GameObject _replacement;
    [SerializeField] private float _breakForce = 2;
    [SerializeField] private float _collisionMultiplier = 100;
    [SerializeField] private bool _broken;
 
    void OnCollisionEnter(Collision collision) {
        if(_broken) return;
        if (collision.relativeVelocity.magnitude >= _breakForce) {
            _broken = true;
            GameObject replacement = Instantiate(_replacement, transform.position, transform.rotation);
            // Set the scale of replacement to be the same as the original
            replacement.transform.localScale = transform.localScale;
 
            var rbs = replacement.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rbs) {
                rb.AddExplosionForce(collision.relativeVelocity.magnitude * _collisionMultiplier,collision.contacts[0].point,2);
            }
 
            Destroy(gameObject);
            Destroy(replacement, 2.0f);
            

        }
    }
}