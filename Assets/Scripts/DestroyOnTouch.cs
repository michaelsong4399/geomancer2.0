using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When this script is attached to an object, it will be destroyed when touched by an object with tag
/// tagToCollideWith.
/// For this script to work, the object that it is attached to must have a trigger collider and
/// tagToCollideWith must be set in the Inspector
/// </summary>
public class DestroyOnTouch : MonoBehaviour
{
    public string tagToCollideWith = "Rock"; //this is public, so it can be set in the Inspector
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == tagToCollideWith && other.transform.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
        {
            Destroy(gameObject); //destroy self
        }
    }
}
