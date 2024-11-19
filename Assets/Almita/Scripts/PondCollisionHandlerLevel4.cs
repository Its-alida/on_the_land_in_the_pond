using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PondCollisionHandlerLevel4 : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public GameObject imageToShow; // Reference to the image to show when score reaches 8
    public AudioClip audioToPlay; // Audio clip to play when score reaches 8
    public AudioClip rightClip; // Audio clip to play for correct action
    public AudioClip wrongClip; // Audio clip to play for wrong action
    private int score = 0;
    private float timerDuration = 90f;
    private float timer = 0f;
    private bool timerStarted = false;
    private bool timerFinished = false;
    private float delayBeforeTimerStarts = 13f;
    public Image landImage;
    public Image pondImage;
    public AudioClip pondSoundClip;
    public AudioClip landSoundClip;
    public GameObject splash;
    public GameObject splash2;
    public AudioClip waterSplashSoundClip;
    private AudioSource audioSource;
    private bool isPond = false;
    private bool playerInsidePond = false;

    private void Start()
    {
        Invoke("StartTimer", delayBeforeTimerStarts);
        StartCoroutine(StartCommandGeneration());
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator StartCommandGeneration()
    {
        yield return new WaitForSeconds(13f);
        StartCoroutine(GenerateCommands());
    }

    private IEnumerator GenerateCommands()
    {
        float totalTime = 0f;

        while (totalTime < timerDuration)
        {
            bool isLastLand = Random.Range(0, 2) == 0; // Randomly choose between land and pond

            if (isLastLand)
            {
                landImage.gameObject.SetActive(true);
                pondImage.gameObject.SetActive(false);
                audioSource.PlayOneShot(landSoundClip);
                isPond = false;
            }
            else
            {
                landImage.gameObject.SetActive(false);
                pondImage.gameObject.SetActive(true);
                audioSource.PlayOneShot(pondSoundClip);
                isPond = true;
            }

            yield return new WaitForSeconds(3f); // Wait for 3 seconds to check player's position

            CheckPlayerPosition(isLastLand);

            yield return new WaitForSeconds(1f); // Additional wait time

            landImage.gameObject.SetActive(false);
            pondImage.gameObject.SetActive(false);

            yield return new WaitForSeconds(2f); // Additional wait time

            totalTime += 6f; // Increment total time by 6 seconds
        }
    }

    private void CheckPlayerPosition(bool isLastLand)
    {
        if (isLastLand)
        {
            // Land state: Check if the player is out of the pond trigger
            if (!playerInsidePond)
            {
                score++;
                UpdateScoreText();
                PlayAudioClip(rightClip);
                Debug.Log("Player is out of the pond! Score incremented.");
            }
            else
            {
                PlayAudioClip(wrongClip);
                Debug.Log("Player is still in the pond! No score increment.");
            }
        }
        else
        {
            // Pond state: Check if the player is inside the pond trigger
            if (playerInsidePond)
            {
                score++;
                UpdateScoreText();
                PlayAudioClip(rightClip);
                Debug.Log("Player is inside the pond! Score incremented.");
            }
            else
            {
                PlayAudioClip(wrongClip);
                Debug.Log("Player is not in the pond! No score increment.");
            }
        }

        if (score == 8)
        {
            // Play the audio
            audioSource.PlayOneShot(audioToPlay);
            // Show the image
            if (imageToShow != null)
                imageToShow.SetActive(true);

            // Start the coroutine to load the MainMenu scene after 3 seconds
            StartCoroutine(LoadMainMenuAfterDelay(3f));
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private IEnumerator DeactivateSplashAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for specified delay
        splash.SetActive(false); // Deactivate splash
        splash2.SetActive(false); // Deactivate splash2
    }

    private void StartTimer()
    {
        timerStarted = true;
    }

    private void Update()
    {
        if (timerStarted && !timerFinished)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
            if (timer >= timerDuration)
            {
                timerFinished = true;
                Debug.Log("Timer finished!");
            }
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            float remainingTime = Mathf.Max(timerDuration - timer, 0);
            string minutes = Mathf.Floor(remainingTime / 60).ToString("00");
            string seconds = Mathf.Floor(remainingTime % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MyPlayer"))
        {
            Debug.Log("Player entered the pond.");
            audioSource.PlayOneShot(waterSplashSoundClip);
            splash.SetActive(true);
            splash2.SetActive(true);
            StartCoroutine(DeactivateSplashAfterDelay(2f));
            playerInsidePond = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MyPlayer"))
        {
            Debug.Log("Player exited the pond.");
            playerInsidePond = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MyPlayer"))
        {
            // This will be called every frame while the player is inside the pond trigger.
            Debug.Log("Player is staying in the pond.");
            playerInsidePond = true;
        }
    }

    private IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
}
