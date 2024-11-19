/*
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandGenerator : MonoBehaviour
{
    public Image landImage;
    public Image pondImage;
    public AudioClip pondSoundClip;
    public AudioClip landSoundClip;
    public GameObject splash;
    public AudioClip waterSplashSoundClip;
    private AudioSource audioSource;
    public TMP_Text scoreText;
    private Collider pondCollider;

    private WaitForSeconds interval = new WaitForSeconds(6f);

    private int score = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pondCollider = GameObject.Find("Pond_FBX").GetComponent<Collider>();

        if (pondCollider == null)
        {
            Debug.LogError("Pond collider not found!");
        }
        else
        {
            StartCoroutine(StartCommandGeneration());
        }
    }

    private IEnumerator StartCommandGeneration()
    {
        yield return new WaitForSeconds(13f);
        StartCoroutine(GenerateCommands());
    }

    private IEnumerator GenerateCommands()
    {
        bool isLastLand = false;
        float totalTime = 0f;

        while (totalTime < 120f)
        {
           

            if (isLastLand)
            {
                landImage.gameObject.SetActive(true);
                pondImage.gameObject.SetActive(false);
                audioSource.PlayOneShot(landSoundClip);
            }
            else
            {
                landImage.gameObject.SetActive(false);
                pondImage.gameObject.SetActive(true);
                audioSource.PlayOneShot(pondSoundClip);
                Check_for_Score(isLastLand);
            }

            

            yield return new WaitForSeconds(6f);

            landImage.gameObject.SetActive(false);
            pondImage.gameObject.SetActive(false);

            yield return new WaitForSeconds(2f);

            totalTime += 5f;
        }
    }
    void Check_for_Score(bool isLastLand)
    {
        if (!isLastLand && pondCollider.bounds.Contains(transform.position))
        {
            score++;
            UpdateScoreText();
            ShowWaterSplash();
            Debug.Log("Player is inside the pond! Score incremented.");
        }
    }
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void ShowWaterSplash()
    {
        // Enable the water splash image
        //splash.gameObject.SetActive(true);
        // Play water splash sound
        audioSource.PlayOneShot(waterSplashSoundClip);
    }
}
*/
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandGenerator : MonoBehaviour
{
    public Image landImage;
    public Image pondImage;
    public AudioClip pondSoundClip;
    public AudioClip landSoundClip;
    public GameObject splash;
    public AudioClip waterSplashSoundClip;
    public AudioClip rightClip; // Audio clip for the correct action
    public AudioClip wrongClip; // Audio clip for the wrong action
    private AudioSource audioSource;
    public TMP_Text scoreText;
    private Collider pondCollider;

    private WaitForSeconds interval = new WaitForSeconds(6f);

    private int score = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pondCollider = GameObject.Find("Pond_FBX").GetComponent<Collider>();

        if (pondCollider == null)
        {
            Debug.LogError("Pond collider not found!");
        }
        else
        {
            StartCoroutine(StartCommandGeneration());
        }
    }

    private IEnumerator StartCommandGeneration()
    {
        yield return new WaitForSeconds(13f);
        StartCoroutine(GenerateCommands());
    }

    private IEnumerator GenerateCommands()
    {
        bool isLastLand = false;
        float totalTime = 0f;

        while (totalTime < 120f)
        {
            isLastLand = !isLastLand;

            if (isLastLand)
            {
                landImage.gameObject.SetActive(true);
                pondImage.gameObject.SetActive(false);
                audioSource.PlayOneShot(landSoundClip);
            }
            else
            {
                landImage.gameObject.SetActive(false);
                pondImage.gameObject.SetActive(true);
                audioSource.PlayOneShot(pondSoundClip);
            }

            yield return new WaitForSeconds(5f); // Wait for 5 seconds

            // Check player's position and update score/audio based on the current state
            CheckPlayerPosition(isLastLand);

            yield return new WaitForSeconds(1f); // Additional wait time to complete the 6 seconds

            landImage.gameObject.SetActive(false);
            pondImage.gameObject.SetActive(false);

            yield return new WaitForSeconds(2f);

            totalTime += 8f; // Updated to reflect total time spent in each loop iteration
        }
    }

    private void CheckPlayerPosition(bool isLastLand)
    {
        if (isLastLand)
        {
            // Land state: Check if the player is out of the pond collider
            if (!pondCollider.bounds.Contains(transform.position))
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
            // Pond state: Check if the player is inside the pond collider
            if (pondCollider.bounds.Contains(transform.position))
            {
                score++;
                UpdateScoreText();
                ShowWaterSplash();
                PlayAudioClip(rightClip);
                Debug.Log("Player is inside the pond! Score incremented.");
            }
            else
            {
                PlayAudioClip(wrongClip);
                Debug.Log("Player is not in the pond! No score increment.");
            }
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void ShowWaterSplash()
    {
        // Enable the water splash image
        // splash.gameObject.SetActive(true); // If you have a splash image to show, uncomment this line
        // Play water splash sound
        audioSource.PlayOneShot(waterSplashSoundClip);
    }
}
