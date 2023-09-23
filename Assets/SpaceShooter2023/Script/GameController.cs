using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;

public class GameController : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private float gameTime;

    private float sliderCurrentFillAmount = 1f;
    [Header("Score Components")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int playerScore;

    [Header("Game Over Components")]
    [SerializeField] private GameObject gameOverScreen;

    [Header("Gameplay audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] gameplayAudio;

    public enum GameState
    {
        Waiting,
        Playing,
        GameOver,
    }

    public static GameState currentGameStatus;

    private void Awake()
    {
        currentGameStatus = GameState.Waiting;
    }


    private void Update()
    {
        if(currentGameStatus == GameState.Playing)
            AdjustTimer();
    }

    private void AdjustTimer()
    {
        timerImage.fillAmount = sliderCurrentFillAmount - (Time.deltaTime / gameTime);
        sliderCurrentFillAmount = timerImage.fillAmount;
        if (sliderCurrentFillAmount <= 0f)
        {
            GameOver();
        }
    }

    public void UpdatePlayerScore(int asteroidHitPoints)
    {
        if (currentGameStatus != GameState.Playing)
            return;
        playerScore += asteroidHitPoints;
        scoreText.text = playerScore.ToString();
    }

    public void StartGame()
    {
        currentGameStatus = GameState.Playing;
        PlayGameAudio(gameplayAudio[1], true);
    }

    public void ResetGame()
    {
        currentGameStatus=GameState.Waiting;

        //put time to 1
        sliderCurrentFillAmount = 1f;
        timerImage.fillAmount = 1f;

        //reset score to 1
        playerScore = 0;
        scoreText.text = "0";

        //play intro music
        PlayGameAudio(gameplayAudio[0], true);

    }

    public void GameOver()
    {
        currentGameStatus = GameState.GameOver;

        //show the Game Over Screen
        gameOverScreen.SetActive(true);

        //change the audio
        PlayGameAudio(gameplayAudio[2], false);

    }

    private void PlayGameAudio(AudioClip clipToPlay, bool shouldLoop)
    {
        audioSource.clip = clipToPlay;
        audioSource.loop = shouldLoop;
        audioSource.Play();
    }


}
