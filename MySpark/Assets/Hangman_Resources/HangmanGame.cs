using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HangmanGame : MonoBehaviour
{
    public TextMeshProUGUI[] letterTexts; // 각 글자를 표시할 TextMeshProUGUI 배열 (최대 12개)
    public GameObject[] hangmanSprites; // 행맨 단계별 스프라이트
    public Button[] alphabetButtons; // 알파벳 버튼 배열
    public GameObject result; // Result 팝업 오브젝트
    public GameObject black;  // Black 배경 오브젝트


    private string selectedWord;
    private int maxErrors = 9;
    private int currentErrors = 0;
    private List<char> guessedLetters = new List<char>();

    // 레벨 범위별 단어 하드코딩
    private string[] level1to5Words = {
        "banana", "chair", "desk", "elephant", "frog", "grass", "house", "igloo",
        "juice", "kangaroo", "lion", "monkey", "notebook", "ocean", "pig", "queen",
        "robot", "star", "turtle", "violin", "window", "xylophone", "yellow", "tiger", "door"
    };

    private string[] level6to10Words = {
        "airport", "bicycle", "concert", "decision", "economy", "finance", "grocery",
        "hospital", "interview", "journey", "kindness", "library", "moment", "neighbor",
        "opportunity", "promise", "question", "respect", "success", "traffic",
        "university", "victory", "wisdom", "youth", "museum"
    };

    private string[] level11to15Words = {
        "advantage", "border", "citizen", "disaster", "estimate", "factor", "goal",
        "history", "industry", "justice", "kingdom", "labor", "material", "nation",
        "object", "process", "result", "system", "transport", "vision",
        "warning", "examine", "influence", "doubt", "future"
    };

    private string[] level16to20Words = {
        "agreement", "budget", "client", "demand", "export", "forecast", "guarantee",
        "headquarters", "investment", "market", "negotiate", "opportunity", "profit",
        "report", "schedule", "target", "upgrade", "version", "warranty",
        "year-end", "account", "balance", "credit", "distribution", "evaluation"
    };

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // 현재 레벨 가져오기
        SelectRandomWord(currentLevel);
        SetupWordDisplay();
        SetupAlphabetButtons();

        Debug.Log("정답 단어: " + selectedWord);
    }

    private void SelectRandomWord(int level)
    {
        // 레벨 범위에 따라 단어 선택
        if (level >= 1 && level <= 5)
        {
            selectedWord = level1to5Words[Random.Range(0, level1to5Words.Length)];
        }
        else if (level >= 6 && level <= 10)
        {
            selectedWord = level6to10Words[Random.Range(0, level6to10Words.Length)];
        }
        else if (level >= 11 && level <= 15)
        {
            selectedWord = level11to15Words[Random.Range(0, level11to15Words.Length)];
        }
        else if (level >= 16 && level <= 20)
        {
            selectedWord = level16to20Words[Random.Range(0, level16to20Words.Length)];
        }
        else
        {
            // 기본값으로 level1to5Words에서 선택
            selectedWord = level1to5Words[Random.Range(0, level1to5Words.Length)];
        }
    }

    private void SetupWordDisplay()
    {
        int wordLength = selectedWord.Length;
        int startIndex = (letterTexts.Length - wordLength) / 2; // 중앙 정렬 시작 인덱스

        // 모든 TextMeshProUGUI를 초기화
        for (int i = 0; i < letterTexts.Length; i++)
        {
            if (i >= startIndex && i < startIndex + wordLength)
            {
                letterTexts[i].text = "-"; // 단어 글자 수에 맞춰 "-"로 초기화
                letterTexts[i].gameObject.SetActive(true);
            }
            else
            {
                letterTexts[i].text = ""; // 나머지는 빈칸으로 설정
                letterTexts[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetupAlphabetButtons()
    {
        foreach (Button button in alphabetButtons)
        {
            if (button == null)
            {
                Debug.LogError("alphabetButtons 배열에 null 버튼이 포함되어 있습니다.");
                continue;
            }

            // TextMeshProUGUI 컴포넌트가 있는지 확인하고 가져옴
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

            // TextMeshProUGUI가 없으면 에러 메시지를 출력하고 넘어감
            if (buttonText == null)
            {
                Debug.LogError($"Button '{button.name}'에 TextMeshProUGUI 컴포넌트가 없습니다.");
                continue;
            }

            char letter = buttonText.text[0]; // 버튼 텍스트에서 첫 번째 문자 가져오기
            button.onClick.AddListener(() => OnLetterGuess(button, letter));
        }
    }

    public void OnLetterGuess(Button button, char guessedLetter)
    {
        // 버튼 비활성화
        button.interactable = false;

        // 이미 추측한 글자라면 무시
        if (guessedLetters.Contains(guessedLetter))
            return;

        guessedLetters.Add(guessedLetter);

        // 정답에 포함된 글자인지 확인 (대소문자 구분 없이)
        if (selectedWord.ToLower().Contains(guessedLetter.ToString().ToLower()))
        {
            RevealLetters(char.ToLower(guessedLetter));
        }
        else
        {
            ShowNextHangmanSprite(); // 정답이 아닌 경우에만 행맨 스프라이트 표시
        }

        CheckGameOver();
    }


    private void RevealLetters(char letter)
    {
        int startIndex = (letterTexts.Length - selectedWord.Length) / 2;

        // 맞춘 글자를 TextMeshProUGUI에 표시 (대소문자 구분 없이)
        for (int i = 0; i < selectedWord.Length; i++)
        {
            if (char.ToLower(selectedWord[i]) == letter)
            {
                letterTexts[startIndex + i].text = letter.ToString().ToUpper(); // 맞춘 글자 표시
            }
        }
    }

    private void ShowNextHangmanSprite()
    {
        // 틀린 경우에만 다음 스프라이트 표시
        if (currentErrors < maxErrors)
        {
            hangmanSprites[currentErrors].SetActive(true);
            currentErrors++;
        }
    }

    private void CheckGameOver()
    {
        if (currentErrors >= maxErrors)
        {
            DisableAlphabetButtons();
        }
        else if (AllLettersRevealed())
        {          
            DisableAlphabetButtons();
            ShowResultPopup(); // 클리어 시 팝업 및 스탯 조정
        }
    }

    private void ShowResultPopup()
    {
        // result와 black 오브젝트 활성화
        if (result != null) result.SetActive(true);
        if (black != null) black.SetActive(true);

        // 스탯 조정
        StatManager.Instance.AdjustStat("피로도", -1);
        StatManager.Instance.AdjustStat("허기", 1);
        StatManager.Instance.AdjustStat("지혜사탕", 1);
    }

    private bool AllLettersRevealed()
    {
        foreach (var text in letterTexts)
        {
            if (text.text == "-") // 아직 표시되지 않은 글자가 있으면 false 반환
                return false;
        }
        return true;
    }

    private void DisableAlphabetButtons()
    {
        // 모든 알파벳 버튼을 비활성화
        foreach (Button button in alphabetButtons)
        {
            button.interactable = false;
        }
    }
}
