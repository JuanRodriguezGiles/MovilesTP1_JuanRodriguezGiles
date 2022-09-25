using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private Transform holderMainMenu;
    [SerializeField] private Transform holderGameModes;
    [SerializeField] private Transform holderCredits;
    [SerializeField] private Transform holderDifficulty;

    [SerializeField] private Button easyButton;
    [SerializeField] private Button normalButton;
    [SerializeField] private Button hardButton;

    [SerializeField] private Button singleplayerButton;
    [SerializeField] private Button multiplayerButton;
    void Start()
    {
        easyButton.onClick.AddListener((() =>
        {
            GameManager.Instance.SetDifficulty(0);
            holderGameModes.gameObject.SetActive(true);
            holderDifficulty.gameObject.SetActive(false);
        }));
        normalButton.onClick.AddListener((() =>
        {
            GameManager.Instance.SetDifficulty(1);
            holderGameModes.gameObject.SetActive(true);
            holderDifficulty.gameObject.SetActive(false);
        }));
        hardButton.onClick.AddListener((() =>
        {
            GameManager.Instance.SetDifficulty(2);
            holderGameModes.gameObject.SetActive(true);
            holderDifficulty.gameObject.SetActive(false);
        }));
        singleplayerButton.onClick.AddListener((() =>
        {
            GameManager.Instance.StartGame(1);
        }));
        multiplayerButton.onClick.AddListener((() =>
        {
            GameManager.Instance.StartGame(2);
        }));
    }
}