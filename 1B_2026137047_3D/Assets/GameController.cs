using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 씬 재시작 필수

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;  // 점수 텍스트 (아이템 먹으면 실시간 연동)
    public TextMeshProUGUI timerText;  // 타이머 텍스트
    public GameObject clearPanel;      // 게임 클리어 UI 패널 (버튼 없음)
    public GameObject gameOverPanel;   // 게임 오버 UI 패널 (버튼 없음)

    [Header("Game Settings")]
    public float maxTime = 60f;        // 제한 시간 (1분)
    public float autoRestartDelay = 3f; // 화면 보여줄 대기 시간 (3초)

    private int score = 0;
    private float currentTime;
    private bool isGameFinished = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        score = 0;
        currentTime = maxTime;
        isGameFinished = false;

        UpdateScoreUI();
        UpdateTimerUI();

        if (clearPanel != null) clearPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // 플레이 중 마우스 숨김
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (isGameFinished) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            UpdateTimerUI();
            GameOver();
        }
        UpdateTimerUI();
    }

    // 아이템(Item.cs) 획득 시 정상 작동하는 점수 추가 함수
    public void AddScore(int value)
    {
        if (isGameFinished) return;
        score += value;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Score : " + score.ToString();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null) timerText.text = "Time : " + Mathf.CeilToInt(currentTime).ToString() + "s";
    }

    // 골인 지점(Goal.cs) 도달 시 호출
    public void ClearGame()
    {
        if (isGameFinished) return;
        isGameFinished = true;
        if (clearPanel != null) clearPanel.SetActive(true);

        StartCoroutine(AutoRestartRoutine());
    }

    // 추락 또는 시간 초과 시 호출
    public void GameOver()
    {
        if (isGameFinished) return;
        isGameFinished = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        StartCoroutine(AutoRestartRoutine());
    }

    // ⭐ [100% 에러 차단] 오직 유니티 순정 기능으로만 3초 대기 후 씬을 리셋하는 완벽한 루틴
    private IEnumerator AutoRestartRoutine()
    {
        // 1. 대기하는 동안 맵이 멈추도록 유니티 물리 시간 일시정지
        Time.timeScale = 0f;

        // 2. 현실 시간 기준으로 딱 3초 대기
        yield return new WaitForSecondsRealtime(autoRestartDelay);

        // 3. 정지했던 물리 시간 원상복구
        Time.timeScale = 1f;

        // 4. KCC 명령어고 뭐고 다 치우고, 유니티 순정 엔진 기능으로 현재 씬을 완전히 처음 상태로 강제 재로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}