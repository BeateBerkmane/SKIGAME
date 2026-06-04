using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup overlay;
    [SerializeField] private float fadeSpeed = 0.5f;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private int nextLevelIndex;

    
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private TMP_Text finalTimeText;

    public void ShowFinalTime(float time)
    {
        finalTimeText.text = "TIME " + time.ToString("F2");
    }

    void Start()
    {
        gameOverMenu.SetActive(false);
        overlay.gameObject.SetActive(true);
        overlay.alpha = 1f;
        StartCoroutine(FadeOutOverlay());
        

    }

    private void OnEnable()
    {
        FinishGate.FinishRace += FinishRaceUI;
    }

    private void OnDisable()
    {
        FinishGate.FinishRace -= FinishRaceUI;
    }

    private void FinishRaceUI()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; 
    }

    private IEnumerator FadeInOverlay()
    {
        while (overlay.alpha < 1.0f)
        {
            overlay.alpha += Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeOutOverlay()
    {
        while (overlay.alpha > 0f)
        {
            overlay.alpha -= Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }
    }

    public void Retry()

    {
        Time.timeScale = 1f;
        StartCoroutine(RetryCoroutine());
    }

    private IEnumerator RetryCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Quit()
    {
        Time.timeScale = 1f;
        StartCoroutine(QuitCoroutine());
    }

    private IEnumerator QuitCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        Application.Quit();
    }


    public void NextLevel()
    {
        Time.timeScale = 1f;
        StartCoroutine(NextLevelCoroutine());
    }

    private IEnumerator NextLevelCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(nextLevelIndex);
    }
}
