using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;  // ������ ǥ���� Text ������Ʈ
    public Button[] answerButtons;  // 4���� ��ư�� �迭�� ����
    private List<Question> askedQuestions = new List<Question>(); // �̹� ���� ������ ������ ����Ʈ
    private Dictionary<int, List<Question>> levelQuestions = new Dictionary<int, List<Question>>();  // ������ ������ ������ ��ųʸ�
    private Question currentQuestion;
    private int currentQuestionIndex = 0;
    private int maxQuestions = 10;  // �� 10����
    private int correctAnswersCount = 0;  // ���� ���� Ƚ��
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultColor = Color.white;

    public GameObject resultPopup;  // ��� �˾� â
    public TextMeshProUGUI resultMessageText;  // ��� �˾�â�� �޽��� �ؽ�Ʈ
    public TextMeshProUGUI wisdomText;  // �����ο� ��ġ �ؽ�Ʈ
    public TextMeshProUGUI experienceText;  // ����ġ ��ġ �ؽ�Ʈ
    public TextMeshProUGUI candyText;  // �������� ��ġ �ؽ�Ʈ (���� �߰��� �κ�)
    public int baseRewardWisdom = 4;  // �⺻ �����ο� ����
    public int baseRewardExperience = 10;  // �⺻ ����ġ ����
    public int bonusWisdom = 1;  // 7�� �̻� ���߸� �߰��� �ִ� ��������
    public int bonusExperience = 10;  // 7�� �̻� ���߸� �߰��� �ִ� ����ġ
    public GameObject blackOverlay;  // Black ������Ʈ

    private string correctMessage;
    private string wrongMessage;

    public TextMeshProUGUI answerMessageText;
    private int currentLevel;
    private string selectedSprite; // ���õ� ��������Ʈ


    void Start()
    {
        LoadQuestions();

        if (ExperienceManager.Instance != null)
        {
            currentLevel = ExperienceManager.Instance.����;
        }
        else
        {
            currentLevel = 1;  // ExperienceManager�� ������ �⺻ ���� ����
        }

        // ��������Ʈ�� �´� ����/���� �޽��� ����
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        if (selectedSprite == "Sprite1")
        {
            correctMessage = "�����̴ٿ�!";
            wrongMessage = "Ʋ�ȴٿ�!";
        }
        else if (selectedSprite == "Sprite2")
        {
            correctMessage = "�����̴ٸ�!";
            wrongMessage = "Ʋ�ȴٸ�!";
        }

        SetQuestionForLevel(currentLevel);
    }

    private void SetResultPopupMessage()
    {
        if (selectedSprite == "Sprite1")
        {
            resultMessageText.text = "�����ߴٿ�!";
        }
        else if (selectedSprite == "Sprite2")
        {
            resultMessageText.text = "�����ߴٸ�!";
        }
    }


    // ������ �ε��ϴ� �Լ� (�� ������ �´� ������ �ϵ��ڵ�)
    void LoadQuestions()
    {
        // �ʱ� (Lv 1-5)
        AddQuestionForLevel(1, "What is your name?", "�� �̸��� ����?", new string[] { "�� ���̰� ���̾�?", "�� ��� ���?", "�� ������ ����?" });
        AddQuestionForLevel(1, "I like apples.", "���� ����� ������.", new string[] { "���� �ٳ����� ������.", "���� ������ ������.", "���� ���⸦ ������." });
        AddQuestionForLevel(1, "Where is my book?", "�� å�� ��� �־�?", new string[] { "�� ���� ��� �־�?", "�� ��å�� ��� �־�?", "�� ������ ��� �־�?" });
        AddQuestionForLevel(1, "Can you help me?", "���� ������ �� �ִ�?", new string[] { "�� ���� ����÷�?", "�� ���� �?", "�� ������ �����ٷ�?" });  
        AddQuestionForLevel(1, "I want to play.", "���� ��� �;�.", new string[] { "���� �ڰ� �;�.", "���� �����ϰ� �;�.", "���� ���� �;�." });
        AddQuestionForLevel(1, "It's sunny today.", "������ ������ ����.", new string[] { "������ �� ��.", "������ �ٶ��� �Ҿ�.", "������ ���� ��." });
        AddQuestionForLevel(1, "Let's go!", "����!", new string[] { "��ٷ�!", "����!", "���ѷ�!" });
        AddQuestionForLevel(1, "This is fun!", "�̰� ��վ�!", new string[] { "�̰� �����!", "�̰� ������!", "�̰� ������!" });
        AddQuestionForLevel(1, "I can run fast.", "���� ���� �޸� �� �־�.", new string[] { "���� õõ�� ���� �� �־�.", "���� �ָ� �� �� �־�.", "���� ���� �� �� �־�." });
        AddQuestionForLevel(1, "I like ice cream.", "���� ���̽�ũ���� ������.", new string[] { "���� ���ݸ��� ������.", "���� ������ ������.", "���� ����ũ�� ������." });

        // �߱� (Lv 6-10)
        AddQuestionForLevel(6, "What time is it?", "���� �� �þ�?", new string[] { "���� ��¥�� ����?", "�� ��� ��?", "���� �� �� �ž�?" });
        AddQuestionForLevel(6, "Can I ask you a question?", "���� �ϳ� �ص� �ɱ�?", new string[] { "�� ��� ��?", "�� �̰� �˾�?", "�� �̰� �� �� �־�?" });
        AddQuestionForLevel(6, "How was your day?", "���� �Ϸ� ���?", new string[] { "���� �� �� �ž�?", "���� ��� �?", "������ ���?" });
        AddQuestionForLevel(6, "Do you like sports?", "�� � ������?", new string[] { "�� å ������?", "�� �丮 ������?", "�� ���� ������?" });
        AddQuestionForLevel(6, "I was just at the park.", "��� ������ �־���.", new string[] { "��� ���� �־���.", "��� �б��� �־���.", "��� �������� �־���." });
        AddQuestionForLevel(6, "I'm feeling better now.", "���� ����� ��������.", new string[] { "����� �� ��������.", "������ ����.", "���°� �Ȱ���." });  
        AddQuestionForLevel(6, "Let's have lunch together.", "���� ���� ����.", new string[] { "���� ���� ����.", "���� ��ħ ����.", "���� ���� ����." }); 
        AddQuestionForLevel(6, "I'll handle the situation.", "���� ��Ȳ�� ó���Ұ�.", new string[] { "�ʰ� ��Ȳ�� ó����.", "�츮�� ��Ȳ�� ó������.", "��Ȳ�� ��� ó���ұ�?" });
        AddQuestionForLevel(6, "Could you repeat that, please?", "�ٽ� �� �� �����ٷ�?", new string[] { "�װ� �ٽ� ���ٷ�?", "�װ� ���� ���ٱ�?", "�װ� ���� �ٸ� �� ���ٷ�?" });
        AddQuestionForLevel(6, "I'm sorry I'm late.", "�ʾ �̾���.", new string[] { "�ʸ� ��ٸ��� �ؼ� �̾���.", "���� �ʾƼ� �̾���.", "���� ���� �ͼ� �̾���." });

        // ��� (Lv 11-15)
        AddQuestionForLevel(11, "What's your plan for tomorrow?", "���� ��ȹ�� ����?", new string[] { "���� ��ȹ�� ����?", "���� �� ��ȹ�� ����?", "�̹� �ָ� ��ȹ�� ����?" });
        AddQuestionForLevel(11, "Please let me know if you need anything.", "�ʿ��� ���� ������ �˷��ּ���.", new string[] { "���ϴ� ���� ������ �˷��ּ���.", "������ ������ �˷��ּ���.", "������ �ʿ��ϸ� �˷��ּ���." });
        AddQuestionForLevel(11, "I'm available at 3 PM.", "���� 3�ÿ� �ð��� �˴ϴ�.", new string[] { "���� 4�ÿ� �ð��� �˴ϴ�.", "���� 11�ÿ� �ð��� �˴ϴ�.", "���� 6�ÿ� �ð��� �˴ϴ�." });
        AddQuestionForLevel(11, "Let me check my schedule.", "�� ������ Ȯ���غ��Կ�.", new string[] { "�� �ð��� Ȯ���غ��Կ�.", "�� ��ȹ�� Ȯ���غ��Կ�.", "�� ������ �ٲ㺼�Կ�." });
        AddQuestionForLevel(11, "Please feel free to contact me.", "�������� �����ϼ���.", new string[] { "�������� ������ ���ϼ���.", "�ð� ���߸� �����ϼ���.", "���ϰ� �� �ɾ��ּ���." });
        AddQuestionForLevel(11, "We have a problem with the shipment.", "��ۿ� ������ �ֽ��ϴ�.", new string[] { "����� �������ϴ�.", "����� ������� ����˴ϴ�.", "����� ��ҵǾ����ϴ�." });
        AddQuestionForLevel(11, "I think it's a good idea.", "���� ���� ����.", new string[] { "���� ���� ����.", "���� ���� ���� ����.", "�۽�, Ȯ���� �� ��." });
        AddQuestionForLevel(11, "Let's grab some coffee.", "Ŀ�� ���÷� ����.", new string[] { "�� ���÷� ����.", "�Ļ��Ϸ� ����.", "��ȭ ���� ����." });
        AddQuestionForLevel(11, "Better late than never.", "�ʾ �� �ϴ� �ͺ��� ����.", new string[] { "�� �ϴ� �ͺ��� �� ����.", "�ʴ� �� �� ����.", "�̸� �ϴ� �� ����." });
        AddQuestionForLevel(11, "Could you send me the report?", "������ �����ֽðھ��?", new string[] { "�̸����� �����ֽðھ��?", "�ڷḦ �����ֽðھ��?", "������ �����ֽðھ��?" });

        // �ǻ�Ȱ - ���� (Lv 16-20)
        AddQuestionForLevel(16, "When is the deadline?", "�������� �����ΰ���?", new string[] { "�������� �����ΰ���?", "ȸ�Ǵ� �����ΰ���?", "������ �����ΰ���?" });
        AddQuestionForLevel(16, "Please let me know if you need anything.", "�ʿ��� ���� ������ �˷��ּ���.", new string[] { "������ �ʿ��ϸ� �˷��ּ���.", "������ ������ �˷��ּ���.", "�������ֽðھ��?" });
        AddQuestionForLevel(16, "I'm available at 3 PM.", "���� 3�ÿ� �ð��� �˴ϴ�.", new string[] { "���� 2�ÿ� �ð��� �˴ϴ�.", "���� 4�ÿ� �ð��� �˴ϴ�.", "���� 5�ÿ� �ð��� �˴ϴ�." });
        AddQuestionForLevel(16, "Let me check my schedule.", "�� ������ Ȯ���غ��Կ�.", new string[] { "�� ��ȹ�� Ȯ���غ��Կ�.", "������ �����غ��Կ�.", "�ٽ� Ȯ���غ��Կ�." });
        AddQuestionForLevel(16, "Please feel free to contact me.", "�������� �����ϼ���.", new string[] { "�������� ��ȭ�ϼ���.", "�ʿ��� �� ������ ���ϼ���.", "������ ���� ������ �˷��ּ���." });
        AddQuestionForLevel(16, "We have a problem with the shipment.", "��ۿ� ������ �ֽ��ϴ�.", new string[] { "����� �������ϴ�.", "����� �����Ǿ����ϴ�.", "����� ��ҵǾ����ϴ�." });
        AddQuestionForLevel(16, "I think it's a good idea.", "���� ���� ����.", new string[] { "���� ���� ����.", "�۽�, Ȯ���� �� ��.", "���� ���� �����̾�." });
        AddQuestionForLevel(16, "Let's grab some coffee.", "Ŀ�� ���÷� ����.", new string[] { "�� ���÷� ����.", "�� ������ ����.", "� ����." });
        AddQuestionForLevel(16, "Better late than never.", "�ʾ �� �ϴ� �ͺ��� ����.", new string[] { "�� �ϴ� �� ����.", "������ �� ��.", "�̸� �ϴ� �� ����." });
        AddQuestionForLevel(16, "Could you send me the report?", "������ �����ֽðھ��?", new string[] { "�̸����� �����ֽðھ��?", "������ �����ֽðھ��?", "�ڷḦ �����ֽðھ��?" });
    }

    // Ư�� ������ �ش��ϴ� ������ �߰��ϴ� �Լ�
    void AddQuestionForLevel(int level, string question, string correctAnswer, string[] wrongAnswers)
    {
        if (!levelQuestions.ContainsKey(level))
        {
            levelQuestions[level] = new List<Question>();
        }
        levelQuestions[level].Add(new Question(question, correctAnswer, wrongAnswers));
    }

    // Ư�� ������ �ش��ϴ� ������ �����ϴ� �Լ�
    public void SetQuestionForLevel(int level)
    {

        // �Ƿε��� ��Ⱑ Ȱ���� ������ Ȯ��
        if (StatManager.Instance != null)
        {
            int currentFatigue = StatManager.Instance.�Ƿε�;
            int currentHunger = StatManager.Instance.���;

            if (currentFatigue >= 10)
            {
                questionText.text = "�ʹ� �ǰ��ؼ� ������ �� �� �����ϴ�.";
                return;
            }

            if (currentHunger >= 10)
            {
                questionText.text = "�谡 �ʹ� ���ļ� ������ �� �� �����ϴ�.";
                return;
            }
        }

        if (level >= 1 && level <= 5)
        {
            level = 1; // 1~5 ���� ������ ������ ���
        }
        else if (level >= 6 && level <= 10)
        {
            level = 6; // 6~10 ���� ������ ������ ���
        }
        else if (level >= 11 && level <= 15)
        {
            level = 11; // 11~15 ���� ������ ������ ���
        }
        else if (level >= 16 && level <= 20)
        {
            level = 16; // 16~20 ���� ������ ������ ���
        }

        currentLevel = level;  // ���� ���� ����

        // ������ 10�� �ʰ��ϸ� ���� ó��
        if (currentQuestionIndex >= maxQuestions)
        {
            if (correctAnswersCount >= 7)
            {
                GiveRewards();  // ���� ����
            }

            ShowResultPopup();  // ��� �˾� â ǥ��
            Debug.Log("ShowResultPopup ȣ�� �Ϸ�!");

            questionText.text = "��� ��ȭ�� �Ϸ��Ͽ����ϴ�!";

            foreach (Button button in answerButtons)
            {
                button.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                button.interactable = false;
            }

            if (StatManager.Instance != null)
            {
                StatManager.Instance.AdjustStat("���", 1);
                StatManager.Instance.AdjustStat("���ɵ�", 2);
                StatManager.Instance.SaveStatsToPlayerPrefs();
            }

            return;
        }

        if (!levelQuestions.ContainsKey(level))
        {
            Debug.LogError("�ش� ������ ���� ������ �����ϴ�.");
            return;
        }

        List<Question> questionsForLevel = levelQuestions[level];
        List<Question> availableQuestions = questionsForLevel.FindAll(q => !askedQuestions.Contains(q));

        if (availableQuestions.Count == 0)
        {
            Debug.Log("�� �̻� ���ο� ������ �����ϴ�.");
            return;
        }

        currentQuestion = availableQuestions[Random.Range(0, availableQuestions.Count)];
        askedQuestions.Add(currentQuestion);
        currentQuestionIndex++;

        questionText.text = currentQuestion.question;

        List<string> allAnswers = new List<string>(currentQuestion.wrongAnswers);
        allAnswers.Add(currentQuestion.correctAnswer);
        ShuffleAnswers(allAnswers);

        int answerCount = Mathf.Min(allAnswers.Count, answerButtons.Length);

        for (int i = 0; i < answerCount; i++)
        {
            int index = i;
            answerButtons[index].gameObject.SetActive(true);
            answerButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[index];
            answerButtons[index].onClick.RemoveAllListeners();
            answerButtons[index].GetComponent<Image>().color = defaultColor;

            if (allAnswers[index] == currentQuestion.correctAnswer)
            {
                answerButtons[index].onClick.AddListener(() => StartCoroutine(OnCorrectAnswer(answerButtons[index])));
            }
            else
            {
                answerButtons[index].onClick.AddListener(() => StartCoroutine(OnWrongAnswer(answerButtons[index])));
            }
        }

        for (int i = answerCount; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(false);
        }
    }



    // �亯�� �������� ���� �Լ�
    private void ShuffleAnswers(List<string> answers)
    {
        for (int i = 0; i < answers.Count; i++)
        {
            int randomIndex = Random.Range(0, answers.Count);
            string temp = answers[i];
            answers[i] = answers[randomIndex];
            answers[randomIndex] = temp;
        }
    }

    private IEnumerator OnCorrectAnswer(Button button)
    {
        correctAnswersCount++;  // ���� Ƚ�� ����

        // ���õ� ��������Ʈ�� �´� �޽��� ���
        questionText.text = correctMessage;

        button.GetComponent<Image>().color = correctColor;
        yield return new WaitForSeconds(0.5f);
        SetQuestionForLevel(currentLevel);  // ���� ���� ���� �� ���� ���� ����
    }

    private IEnumerator OnWrongAnswer(Button button)
    {
        questionText.text = wrongMessage;

        button.GetComponent<Image>().color = wrongColor;
        foreach (var btn in answerButtons)
        {
            if (btn.GetComponentInChildren<TextMeshProUGUI>().text == currentQuestion.correctAnswer)
            {
                btn.GetComponent<Image>().color = correctColor;
                break;
            }
        }
        yield return new WaitForSeconds(0.5f);
        SetQuestionForLevel(currentLevel);  // ���� ���� ���� �� ���� ���� ����
    }


    private void GiveRewards()
    {
        int totalWisdom = baseRewardWisdom;  // �⺻ �����ο� ����
        int totalExperience = baseRewardExperience;  // �⺻ ����ġ ����
        int totalCandies = 0;  // �������� ���� (�⺻�� 0)

        if (correctAnswersCount >= 7)
        {
            totalCandies = bonusWisdom;  // �߰� �������� ���� (7�� �̻� ���߸�)
            totalExperience += bonusExperience;  // �߰� ����ġ ���� + ����ġ 20 �߰�
        }

        // StatManager�� �����ο� �� �������� ������ �ݿ�
        if (StatManager.Instance != null)
        {
            StatManager.Instance.AdjustStat("�����ο�", totalWisdom);  // �����ο� �߰�
            StatManager.Instance.AdjustStat("��������", totalCandies);  // �������� �߰�
            StatManager.Instance.SaveStatsToPlayerPrefs();  // ���� ����
        }

        // ExperienceManager���� ����ġ �߰�
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.AddExperience(totalExperience);  // ����ġ �߰�
        }

        // ��� �˾��� ���� ǥ��
        wisdomText.text = $"�����ο� +{totalWisdom}";
        experienceText.text = $"����ġ +{totalExperience}";

        // �������� �ؽ�Ʈ�� �������� �� ǥ��
        if (totalCandies > 0)
        {
            candyText.text = $"�������� +{totalCandies}";
        }
        else
        {
            candyText.text = "0";  // �������� ������ ���� ��� �� ���ڿ��� ����
        }
    }

    private void ShowResultPopup()
    {
        if (resultPopup != null)
        {
            resultPopup.SetActive(true);  // ��� �˾� â Ȱ��ȭ

            if (blackOverlay != null)
            {
                blackOverlay.SetActive(true);  // Black ������Ʈ Ȱ��ȭ
            }

            Debug.Log("��� �˾��� Ȱ��ȭ�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError("resultPopup�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }
}

// ���� Ŭ���� ����
[System.Serializable]
public class Question
{
    public string question;  // ����
    public string correctAnswer;  // ����
    public string[] wrongAnswers;  // ����

    public Question(string q, string correct, string[] wrong)
    {
        question = q;
        correctAnswer = correct;
        wrongAnswers = wrong;
    }
}
