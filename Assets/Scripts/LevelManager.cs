using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public float levelDuration = 30f;
    private float timer = 0f;
    private bool levelCompleted = false;
    private bool levelFailed = false;

    [Header("Player Settings")]
    public PlayerMovement player;
    private float startSpeed = 5f;
    private float endSpeed = 10f;
    public Transform playerStartPoint;

    [Header("UI Elements")]
    public GameObject failUI;
    public GameObject completeUI;
    public TextMeshProUGUI deathCounterText;
    public TextMeshProUGUI timeTMP;

    [Header("Failure Sounds")]
    public AudioSource audioSource;
    public AudioClip[] failureClips;

    [Header("Fall Fail Condition")]
    public float failHeightY = -10f;  // Player fails if they fall below this

    private int deathCount = 0;

    void Start()
    {
        if(!PlayerPrefs.HasKey("DeathCounter"))
        {
            SaveDeathCounter();
        }
        startSpeed = player.Data.runMaxSpeed;
        endSpeed = 2 * player.Data.runMaxSpeed;
        LoadDeathCounter();
        UpdateDeathCounterUI();
        ResetLevelState();
    }

    void Update()
    {
        if (levelCompleted || levelFailed)
            return;

        // Timer
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / levelDuration);
        player.Data.runMaxSpeed = Mathf.Lerp(startSpeed, endSpeed, t);

        UpdateTimerUI();

        if (timer >= levelDuration)
            FailLevel();

        // FALL FAIL CHECK
        if (player.transform.position.y < failHeightY)
            FailLevel();
    }

    // ---------------------------
    // LEVEL COMPLETE
    // ---------------------------
    public void CompleteLevel()
    {
        if (levelCompleted) return;

        levelCompleted = true;
        completeUI.SetActive(true);
        player.Data.runMaxSpeed = 0;
    }

    // ---------------------------
    // LEVEL FAIL
    // ---------------------------
    public void FailLevel()
    {
        if (levelFailed) return;

        levelFailed = true;
        failUI.SetActive(true);
        player.Data.runMaxSpeed = 0;

        // Increment and save deaths
        deathCount++;
        SaveDeathCounter();
        UpdateDeathCounterUI();

        // Play fail sound if none is playing
        if (audioSource != null && !audioSource.isPlaying && failureClips.Length > 0)
        {
            audioSource.clip = failureClips[Random.Range(0, failureClips.Length)];
            audioSource.Play();
        }
    }

    // ---------------------------
    // RETRY level (no scene reset)
    // ---------------------------
    public void RetryLevel()
    {
        ResetLevelState();
        failUI.SetActive(false);
    }

    private void ResetLevelState()
    {
        timer = 0f;
        levelFailed = false;
        levelCompleted = false;

        DropPlatform[] dropPlatforms  = FindObjectsByType(typeof(DropPlatform), FindObjectsInactive.Include, FindObjectsSortMode.None) as DropPlatform[];

        foreach (DropPlatform platform in dropPlatforms)
        {
            platform.ResetPlatform();
            Debug.Log("Reset " + platform.name);
        }

        // Reset the player
        if (playerStartPoint != null)
            player.transform.position = playerStartPoint.position;

        player.Data.runMaxSpeed = startSpeed;
    }

    public void NextLevel()
    {
        Debug.Log("Next level (placeholder)");
    }

    private void UpdateTimerUI()
    {
        float remaining = Mathf.Max(0, levelDuration - timer);
        string formatted = remaining.ToString("F1"); // 1 decimal place

        if (timeTMP != null)
            timeTMP.text = formatted;
    }

    private void SaveDeathCounter()
    {
        PlayerPrefs.SetInt("DeathCounter", deathCount);
        PlayerPrefs.Save();
    }

    private void LoadDeathCounter()
    {
        deathCount = PlayerPrefs.GetInt("DeathCounter", 0);
    }

    private void UpdateDeathCounterUI()
    {
        if (deathCounterText != null)
            deathCounterText.text = "Deaths: " + deathCount;
    }

    // ---------------------------
    // GIZMO FOR FAIL HEIGHT
    // ---------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw a wide horizontal line at fail height
        Gizmos.DrawLine(new Vector3(-1000, failHeightY, -1000),
                        new Vector3(-1000, failHeightY, 1000));

        Gizmos.DrawLine(new Vector3(1000, failHeightY, -1000),
                        new Vector3(1000, failHeightY, 1000));

        Gizmos.DrawLine(new Vector3(-1000, failHeightY, -1000),
                        new Vector3(1000, failHeightY, -1000));

        Gizmos.DrawLine(new Vector3(-1000, failHeightY, 1000),
                        new Vector3(1000, failHeightY, 1000));
    }

    private void OnApplicationQuit()
    {
        player.Data.runMaxSpeed = startSpeed;
    }
}

