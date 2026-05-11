using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup overlay;

    [SerializeField] private float fadeSpeed = 0.5f;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private int nextLevelIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverMenu.SetActive(false);
        overlay.gameObject.SetActive(true);
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

    private IEnumerator FadeInOverlay()
    {
        while (overlay.alpha < 1.0f)
        {
            overlay.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeOutOverlay()
    {
        while (overlay.alpha > 0f)
        {
            overlay.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    public void Retry()

    {
        StartCoroutine(RetryCoroutine());
    }

    private IEnumerator RetryCourutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void Quit()


    {
        StartCoroutine(QuitCoroutine());
    }
    
    private IEnumerator QuitCourutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(NextLevelIndex);

    public void NextLevel()

    {
        StartCoroutine(NextLevelCoroutine());
    }
    private IEnumerator NextLevelCourutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
