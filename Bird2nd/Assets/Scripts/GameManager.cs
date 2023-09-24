using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    private int hiScore;
    
    public Player player;
    private Spawner spawner;
    public Text scoreText;
    public Text hiScoreText;
    private Parallax bg_para;

    public GameObject playButton;

    public Text gameOver;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        bg_para = FindObjectOfType<Parallax>();
        Pause();      
    }

    public void Play()
   {
        score = 0;
        
        if(PlayerPrefs.HasKey("hiScore"))
        {
            hiScore = PlayerPrefs.GetInt("hiScore");
        }
        scoreText.text = score.ToString();
        hiScoreText.text = $"Hi Score: {hiScore.ToString()}";
        playButton.SetActive(false);
        gameOver.gameObject.SetActive(false);
       
        Time.timeScale = 1f;
        player.enabled = true;

        Trunks[] trunks = FindObjectsOfType<Trunks>();

        for (int i = 0; i < trunks.Length; i++) 
        {
            Destroy(trunks[i].gameObject);
        }

        Trunks.speed = 5f;
        Trunks.tmpspeed = 0f;
        spawner.SetSpawnRate(-1f, 1f);
        bg_para.Re();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.gameObject.SetActive(true);
       
        Pause();
    }

    bool isMultipleof(int n, int of)
    {
        while ( n > 0 )
            n = n - of;
    
        if ( n == 0 )
            return true;
    
        return false;
    }

    public void IncreaseScore()
    {
        score++;
        if (PlayerPrefs.HasKey("hiScore"))
        {
            if (score > PlayerPrefs.GetInt("hiScore"))
            {
                hiScore = score;
                PlayerPrefs.SetInt("hiScore", hiScore);
                PlayerPrefs.Save();
            }
        }
        else
        {   
            if (score > hiScore)
            {
               hiScore = score;
               PlayerPrefs.SetInt("hiScore", hiScore);
               PlayerPrefs.Save();
            }
        }
        if (isMultipleof(score, 10))
        {
            player.hasSpeedUp += 1;
            if (Trunks.tmpspeed == 0)
            {
                var old_speed = Trunks.speed;
                Trunks.speed += 0.5f;
                spawner.SetSpawnRate(old_speed + Trunks.tmpspeed, Trunks.speed + Trunks.tmpspeed);
            }
        }
        scoreText.text = score.ToString();
        hiScoreText.text = $"Hi Score: {hiScore.ToString()}";
    }

}
