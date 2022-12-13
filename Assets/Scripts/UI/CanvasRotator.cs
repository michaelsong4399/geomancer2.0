using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion target = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0);
        transform.rotation = target;
    }
}
