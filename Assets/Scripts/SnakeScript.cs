using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SnakeScript : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    public Transform snake_body;
    public GameObject GameOver,GameStart;
    public static int score = 0;
    public TextMeshProUGUI scoreText1, scoreText2;
    public int multiplier;
    public static bool isShielded,scoreMultiplier;
    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_direction == Vector2.down)
            { return; }
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_direction == Vector2.right)
            { return; }
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (_direction == Vector2.up)
            { return; }
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_direction == Vector2.left)
            { return; }
            _direction = Vector2.right;
        }
       
        
            scoreText1.text = "Score - " + score;
            scoreText2.text = "Score - " + score;
        
    }
    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x, Mathf.Round(this.transform.position.y) + _direction.y, 0.0f);

    }
    IEnumerator Shieldon()
    {
        isShielded = true;
        this.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(5f);
        isShielded = false;
        this.GetComponent<SpriteRenderer>().color = Color.green;
    }
    IEnumerator Score()
    {
        scoreMultiplier = true;
        this.GetComponent<SpriteRenderer>().color = Color.cyan;
        yield return new WaitForSeconds(10f);
        scoreMultiplier = false;
        this.GetComponent<SpriteRenderer>().color = Color.green;
    }
    private void Grow()
    {
        Transform segment = Instantiate(this.snake_body);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        if(scoreMultiplier)
        {
            score +=  (multiplier * 1);
        }
        else if(!scoreMultiplier)
        {
            score++;
        }
        
    }
    private void SnakePoison()
    {
        _segments[_segments.Count - 1].gameObject.SetActive(false);
        _segments.Remove(_segments[_segments.Count - 1]);
        score--;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Food>())
        {
            Grow();
        }
        else if (other.tag == "Rightwall")
        {
            this.transform.position = new Vector3(-(this.transform.position.x - 1), this.transform.position.y, 0.0f);
        }
        else if (other.tag == "Topwall")
        {
            this.transform.position = new Vector3(this.transform.position.x, -(this.transform.position.y - 1), 0.0f);
        }
        else if (other.tag == "Bottomwall")
        {
            this.transform.position = new Vector3(this.transform.position.x, -(this.transform.position.y + 1), 0.0f);
        }
        else if (other.tag == "Leftwall")
        {
            this.transform.position = new Vector3(-(this.transform.position.x + 1), this.transform.position.y, 0.0f);
        }
        else if (other.tag == "Shield")
        {
            StartCoroutine(Shieldon());
            Debug.Log("Shield called!");
        }
        else if (other.tag == "Multiplier")
        {
            StartCoroutine(Score());
            Debug.Log("Shield called!");
        }
        else if (other.gameObject.GetComponent<Poison>())
        {
            SnakePoison();
        }
        else if (other.tag == "obstacle")
        {
            if (!isShielded)
            {
                ResetState();
                Time.timeScale = 0;
                GameOver.SetActive(true);
            }
               
        }
    }
    public void Playagain()
    {
        score = 0;
        GameOver.SetActive(false);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        GameOver.SetActive(false);
        GameStart.SetActive(true);
    }
    
    public void Play()
    {
        GameStart.SetActive(false);
        Time.timeScale = 1;
        score = 0;
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void ResetState()
    {
        isShielded = false;
        scoreMultiplier = false;
        for(int i =1; i< _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        this.transform.position = Vector3.zero;
    }

}