using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public BoxCollider2D Grid;
    public float Invoketime, repeatRate;
    public static bool isShielded;
    public GameObject shields1, shields2;
    private void Start()
    {
        Destroy(this.gameObject, 8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SnakeScript>())
        {
            StartCoroutine(Shieldon());
            Destroy(this.gameObject);
        }
    }
    IEnumerator Shieldon()
    {
        isShielded = true;
        yield return new WaitForSeconds(15f);
        isShielded = false;
    }
    private void Shields()
    {
        if(isShielded)
        {
            shields1.GetComponent<SpriteRenderer>().color = Color.blue;
            shields2.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(!isShielded)
        {
            shields1.GetComponent<SpriteRenderer>().color = Color.green;
            shields2.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    private void Update()
    {
        Shields();
    }
}
