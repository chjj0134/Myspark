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
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultColor = Color.white;

    void Start()
    {
        LoadQuestions();
        SetQuestionForLevel(1);
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
        // ������ 10�� �ʰ��ϸ� ���� ó��
        if (currentQuestionIndex >= maxQuestions)
        {
            // ���� �ؽ�Ʈ�� "��� ��ȭ�� �Ϸ��Ͽ����ϴ�!"�� ����
            questionText.text = "��� ��ȭ�� �Ϸ��Ͽ����ϴ�!";

            // ��� ��ư�� ȸ������ ����� ��Ȱ��ȭ
            foreach (Button button in answerButtons)
            {
                button.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);  // ȸ������ ����
                button.interactable = false;  // ��ư ��Ȱ��ȭ
            }

            // ��� +1, ���ɵ� +2 ����
            if (StatManager.Instance != null)
            {
                StatManager.Instance.AdjustStat("���", 1);
                StatManager.Instance.AdjustStat("���ɵ�", 2);
                StatManager.Instance.SaveStatsToPlayerPrefs();  // ���� ����
            }

             return;
        }

        // �ش� ������ ������ �ִ��� Ȯ��
        if (!levelQuestions.ContainsKey(level))
        {
            Debug.LogError("�ش� ������ ���� ������ �����ϴ�.");
            return;
        }

        // �ߺ� ���� ���� - �̹� ���� ������ ������ ���� ����
        List<Question> questionsForLevel = levelQuestions[level];
        List<Question> availableQuestions = questionsForLevel.FindAll(q => !askedQuestions.Contains(q));

        if (availableQuestions.Count == 0)
        {
            //Debug.Log("�� �̻� ���ο� ������ �����ϴ�.");
            return;
        }

        // ���ο� ���� ���� �� ����
        currentQuestion = availableQuestions[Random.Range(0, availableQuestions.Count)];
        askedQuestions.Add(currentQuestion);  // �̹� ���� ������ ����
        currentQuestionIndex++;  // ���� �� ī��Ʈ ����

        // ���� �ؽ�Ʈ ����
        questionText.text = currentQuestion.question;

        // �亯 �迭�� �������� ���� ��ư�� ����
        List<string> allAnswers = new List<string>(currentQuestion.wrongAnswers);
        allAnswers.Add(currentQuestion.correctAnswer);  // ������ ����
        ShuffleAnswers(allAnswers);  // �亯�� �������� ����

        // `answerButtons` �迭�� ���� �亯 ���� (answerButtons ũ��� allAnswers ũ�� ��)
        int answerCount = Mathf.Min(allAnswers.Count, answerButtons.Length);  // �� ���� ������ ����

        // �� ��ư�� �亯 �ؽ�Ʈ�� �����ϰ�, Ŭ�� �̺�Ʈ�� ���� �� �߰�
        for (int i = 0; i < answerCount; i++)
        {
            int index = i;  // ���� ������ ���� �ε����� ����
            answerButtons[index].gameObject.SetActive(true);  // ��Ȱ��ȭ�� ��ư�� ������ �ٽ� Ȱ��ȭ
            answerButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[index];
            answerButtons[index].onClick.RemoveAllListeners();  // ���� �̺�Ʈ ����
            answerButtons[index].GetComponent<Image>().color = defaultColor;  // ��ư ���� �ʱ�ȭ

            if (allAnswers[index] == currentQuestion.correctAnswer)
            {
                answerButtons[index].onClick.AddListener(() => StartCoroutine(OnCorrectAnswer(answerButtons[index])));
            }
            else
            {
                answerButtons[index].onClick.AddListener(() => StartCoroutine(OnWrongAnswer(answerButtons[index])));
            }
        }

        // ������� �ʴ� ������ ��ư���� ��Ȱ��ȭ
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

    // ������ �������� �� ȣ��� �Լ�
    private System.Collections.IEnumerator OnCorrectAnswer(Button button)
    {
        button.GetComponent<Image>().color = correctColor;  // ���� ��ư�� �ʷϻ����� ����
        yield return new WaitForSeconds(0.5f);  // 0.5�� ��
        SetQuestionForLevel(1);  // ���� ���� ���� (������ �°� ���� ����)
    }

    // ������ �������� �� ȣ��� �Լ�
    private System.Collections.IEnumerator OnWrongAnswer(Button button)
    {
        button.GetComponent<Image>().color = wrongColor;  // ���� ��ư�� ���������� ����
        foreach (var btn in answerButtons)
        {
            if (btn.GetComponentInChildren<TextMeshProUGUI>().text == currentQuestion.correctAnswer)
            {
                btn.GetComponent<Image>().color = correctColor;  // ������ �ʷϻ����� ǥ��
                break;
            }
        }
        yield return new WaitForSeconds(0.5f);  // 0.5�� ��
        SetQuestionForLevel(1);  // ���� ���� ���� (������ �°� ���� ����)
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
