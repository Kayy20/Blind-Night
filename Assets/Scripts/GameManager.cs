using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public bool Paused { get; private set; }

    public GameObject player;

    public GameObject pauseScreen;
    public GameObject eventSys;
    public GameObject devConsole;
    public bool dev;

    int currentLevel = 0;

    [SerializeField] private TMP_Text levelText;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseScreen);
        DontDestroyOnLoad(eventSys);
        DontDestroyOnLoad(player);
    }

    private void Start()
    {
        pauseScreen.SetActive(false);
        //devConsole.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Title");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "Title")
            {
                if (Time.timeScale == 0)
                {
                    pauseScreen.SetActive(false);
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        if (dev)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentLevel++;
                LoadLevel("Level " + currentLevel);
            }
        }
    }

    void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        StopAllCoroutines();
        if (s.name != "Title")
        {
            player.SetActive(true);
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            // Display on screen what level they are on with fading text
            levelText.gameObject.SetActive(true);

            currentLevel = int.Parse(SceneManager.GetActiveScene().name.Split(' ')[1]);

            if (s.name == "Tutorial")
            {
                StartCoroutine(TutorialFadeText());
            } else
            {

                levelText.text = s.name;
                StartCoroutine(FadeText());
            }
        }
    }

    // coroutine to fade text out
    private IEnumerator FadeText()
    {
        levelText.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.gameObject.SetActive(false);
    }

    private IEnumerator TutorialFadeText()
    {
        levelText.text = "Tutorial";
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "WASD to move";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Mouse to look around";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Left Click to sense, hold to throw further";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Find your way to the exit";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Each Exit is pulsing with Blue";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Good Luck!";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);


        levelText.gameObject.SetActive(false);
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        Paused = true;
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.SetActive(true);
    }


    public void EndGame()
    {
        Debug.Log("Game Over");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
