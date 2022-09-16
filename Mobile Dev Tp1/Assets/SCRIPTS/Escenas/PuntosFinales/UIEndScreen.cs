using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIEndScreen : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float displayTime = 0;
    [SerializeField] private float fadeTime = 0;
    [SerializeField] private float blinkTime = 0;

    [Header("UI")]
    [SerializeField] private Image backgroundImage = null;
    [SerializeField] private Image player1WinImage = null;
    [SerializeField] private Image player2WinImage = null;
    [SerializeField] private Image player1ScoreImage = null;
    [SerializeField] private Image player2ScoreImage = null;
    [SerializeField] private Text player1ScoreText = null;
    [SerializeField] private Text player2ScoreText = null;

    private int players;
    
    public void Start()
    {
        StartCoroutine(Timer());
        
        StartCoroutine(FadeImage(true, backgroundImage, onFadeFinished: () =>
        {
            players = GameManager.Instance.players;

            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq || players == 1) 
            {
                player1WinImage.gameObject.SetActive(true);
            }
            else
            {
                player2WinImage.gameObject.SetActive(true);
            }
            player1ScoreImage.gameObject.SetActive(true);
            
            if (players == 2)
            {
                player2ScoreImage.gameObject.SetActive(true);
            }
            
            SetWinner();
        }));
    }

    IEnumerator Timer()
    {
        float currentTime = 0;

        while (currentTime <= displayTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(SceneConstants.mainMenu);
    }

    IEnumerator FadeImage(bool fadeIn, Image img, Action onFadeFinished)
    {
        if (!fadeIn)
        {
            for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
            {
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            onFadeFinished?.Invoke();
        }
        else
        {
            for (float i = 0; i <= fadeTime; i += Time.deltaTime)
            {
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            onFadeFinished?.Invoke();
        }
    }

    IEnumerator Blink(Text winnerScore)
    {
        while (true)
        {
            winnerScore.enabled = !winnerScore.enabled;
            yield return new WaitForSeconds(blinkTime);
        }
    }
    
    private void SetWinner()
    {
        switch (DatosPartida.LadoGanadaor)
        {
            case DatosPartida.Lados.Izq:
                player1ScoreText.text = "$" + GameManager.Instance.PrepararNumeros(DatosPartida.PtsGanador);
                if (players == 2)
                {
                    player2ScoreText.text = "$" + GameManager.Instance.PrepararNumeros(DatosPartida.PtsPerdedor);
                }
                StartCoroutine(Blink(player1ScoreText));
                break;
            case DatosPartida.Lados.Der:
                player1ScoreText.text = "$" + GameManager.Instance.PrepararNumeros(DatosPartida.PtsPerdedor);
                if (players == 2)
                {
                    player2ScoreText.text = "$" + GameManager.Instance.PrepararNumeros(DatosPartida.PtsGanador);
                    StartCoroutine(Blink(player2ScoreText));
                }
                break;
        }
    }
}