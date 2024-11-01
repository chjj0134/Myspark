using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HangmanGame : MonoBehaviour
{
    public TextMeshProUGUI[] letterTexts; // �� ���ڸ� ǥ���� TextMeshProUGUI �迭 (�ִ� 12��)
    public GameObject[] hangmanSprites; // ��� �ܰ躰 ��������Ʈ
    public Button[] alphabetButtons; // ���ĺ� ��ư �迭
    public GameObject result; // Result �˾� ������Ʈ
    public GameObject black;  // Black ��� ������Ʈ


    private string selectedWord;
    private int maxErrors = 9;
    private int currentErrors = 0;
    private List<char> guessedLetters = new List<char>();

    // ���� ������ �ܾ� �ϵ��ڵ�
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
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // ���� ���� ��������
        SelectRandomWord(currentLevel);
        SetupWordDisplay();
        SetupAlphabetButtons();

        Debug.Log("���� �ܾ�: " + selectedWord);
    }

    private void SelectRandomWord(int level)
    {
        // ���� ������ ���� �ܾ� ����
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
            // �⺻������ level1to5Words���� ����
            selectedWord = level1to5Words[Random.Range(0, level1to5Words.Length)];
        }
    }

    private void SetupWordDisplay()
    {
        int wordLength = selectedWord.Length;
        int startIndex = (letterTexts.Length - wordLength) / 2; // �߾� ���� ���� �ε���

        // ��� TextMeshProUGUI�� �ʱ�ȭ
        for (int i = 0; i < letterTexts.Length; i++)
        {
            if (i >= startIndex && i < startIndex + wordLength)
            {
                letterTexts[i].text = "-"; // �ܾ� ���� ���� ���� "-"�� �ʱ�ȭ
                letterTexts[i].gameObject.SetActive(true);
            }
            else
            {
                letterTexts[i].text = ""; // �������� ��ĭ���� ����
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
                Debug.LogError("alphabetButtons �迭�� null ��ư�� ���ԵǾ� �ֽ��ϴ�.");
                continue;
            }

            // TextMeshProUGUI ������Ʈ�� �ִ��� Ȯ���ϰ� ������
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

            // TextMeshProUGUI�� ������ ���� �޽����� ����ϰ� �Ѿ
            if (buttonText == null)
            {
                Debug.LogError($"Button '{button.name}'�� TextMeshProUGUI ������Ʈ�� �����ϴ�.");
                continue;
            }

            char letter = buttonText.text[0]; // ��ư �ؽ�Ʈ���� ù ��° ���� ��������
            button.onClick.AddListener(() => OnLetterGuess(button, letter));
        }
    }

    public void OnLetterGuess(Button button, char guessedLetter)
    {
        // ��ư ��Ȱ��ȭ
        button.interactable = false;

        // �̹� ������ ���ڶ�� ����
        if (guessedLetters.Contains(guessedLetter))
            return;

        guessedLetters.Add(guessedLetter);

        // ���信 ���Ե� �������� Ȯ�� (��ҹ��� ���� ����)
        if (selectedWord.ToLower().Contains(guessedLetter.ToString().ToLower()))
        {
            RevealLetters(char.ToLower(guessedLetter));
        }
        else
        {
            ShowNextHangmanSprite(); // ������ �ƴ� ��쿡�� ��� ��������Ʈ ǥ��
        }

        CheckGameOver();
    }


    private void RevealLetters(char letter)
    {
        int startIndex = (letterTexts.Length - selectedWord.Length) / 2;

        // ���� ���ڸ� TextMeshProUGUI�� ǥ�� (��ҹ��� ���� ����)
        for (int i = 0; i < selectedWord.Length; i++)
        {
            if (char.ToLower(selectedWord[i]) == letter)
            {
                letterTexts[startIndex + i].text = letter.ToString().ToUpper(); // ���� ���� ǥ��
            }
        }
    }

    private void ShowNextHangmanSprite()
    {
        // Ʋ�� ��쿡�� ���� ��������Ʈ ǥ��
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
            ShowResultPopup(); // Ŭ���� �� �˾� �� ���� ����
        }
    }

    private void ShowResultPopup()
    {
        // result�� black ������Ʈ Ȱ��ȭ
        if (result != null) result.SetActive(true);
        if (black != null) black.SetActive(true);

        // ���� ����
        StatManager.Instance.AdjustStat("�Ƿε�", -1);
        StatManager.Instance.AdjustStat("���", 1);
        StatManager.Instance.AdjustStat("��������", 1);
    }

    private bool AllLettersRevealed()
    {
        foreach (var text in letterTexts)
        {
            if (text.text == "-") // ���� ǥ�õ��� ���� ���ڰ� ������ false ��ȯ
                return false;
        }
        return true;
    }

    private void DisableAlphabetButtons()
    {
        // ��� ���ĺ� ��ư�� ��Ȱ��ȭ
        foreach (Button button in alphabetButtons)
        {
            button.interactable = false;
        }
    }
}
