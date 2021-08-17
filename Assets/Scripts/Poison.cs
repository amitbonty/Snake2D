using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    
    public BoxCollider2D Grid;
    public float Invoketime, repeatRate;
    private void Start()
    {
        Destroy(this.gameObject, 8f);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SnakeScript>())
        {
            Destroy(this.gameObject);
        }
    }
}

