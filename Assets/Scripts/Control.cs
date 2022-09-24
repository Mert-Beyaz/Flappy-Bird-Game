using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Control : MonoBehaviour
{
    public Sprite[] BirdSprite;
    SpriteRenderer spriteRenderer;
    bool flyControl = true;
    int SpriteCounter;
    float GameTime = 0;
    Rigidbody2D rb;
    int Score = 0;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;
    bool GameEnd = true;
    public GameController GameController;
    AudioSource []BirdSource;
    int BestScore = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        BirdSource = GetComponents<AudioSource>();
        BestScore = PlayerPrefs.GetInt("BestScore");
        BestScoreText.text = "Best Score = " + BestScore;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameEnd)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, 200));
            BirdSource[0].Play();
        }
        if (rb.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
        FlyAnimation();
    }
    void FlyAnimation()
    {
        GameTime += Time.deltaTime;
        if (GameTime > 0.2f)
        {
            GameTime = 0;
            if (flyControl)
            {
                spriteRenderer.sprite = BirdSprite[SpriteCounter];
                SpriteCounter++;
                if (SpriteCounter == BirdSprite.Length)
                {
                    SpriteCounter--;
                    flyControl = false;
                }
            }
            else
            {
                SpriteCounter--;
                spriteRenderer.sprite = BirdSprite[SpriteCounter];
                if (SpriteCounter == 0)
                {
                    SpriteCounter++;
                    flyControl = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Score")
        {
            Score++;
            ScoreText.text = "Score = " + Score;
            BirdSource[1].Play();
        }
        if (other.gameObject.tag == "Pipe")
        {
            BirdSource[2].Play();
            GameEnd = false;
            GameController.GameOver();
            GetComponent<CircleCollider2D>().enabled = false;
            PlayerPrefs.SetInt("Score", Score);

            if (Score > BestScore)
            {
                BestScore = Score;
                PlayerPrefs.SetInt("BestScore", BestScore);
            }
            Invoke("GoToMenu", 2);
        }
    }

    void GoToMenu() => SceneManager.LoadScene(0);

    
}
