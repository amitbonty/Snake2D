using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyScore : MonoBehaviour
{

    public BoxCollider2D Grid;
    public float Invoketime, repeatRate;
    public static bool  scoreMultiplier;
    
    private void Start()
    {
        Destroy(this.gameObject, 8f);
    }
    private void RandomizePos()
    {
        Bounds bounds = this.Grid.bounds;
        float x = Random.Range(bounds.min.y, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SnakeScript>())
        {
            StartCoroutine(Score());
            Destroy(this.gameObject);
        }
    }
    IEnumerator Score()
    {
        scoreMultiplier = true;
        yield return new WaitForSeconds(10f);
        scoreMultiplier = false;
    }
  
}
