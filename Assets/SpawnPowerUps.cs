using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    public BoxCollider2D Grid;
    public GameObject[] prefabs;
    void Start()
    {
        InvokeRepeating("RandomizePos", 15f, 25f);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void RandomizePos()
    {
        Bounds bounds = this.Grid.bounds;
        float x = Random.Range(bounds.min.y, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        int Randomindex = Random.Range(0, prefabs.Length );
        Vector3 pos = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        Instantiate(prefabs[Randomindex].gameObject, pos, Quaternion.identity);
        Debug.Log(Randomindex);
    }
}
