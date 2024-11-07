using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;  // 질문을 표시할 Text 컴포넌트
    public Button[] answerButtons;  // 4개의 버튼을 배열로 설정
    private List<Question> askedQuestions = new List<Question>(); // 이미 나온 질문을 저장할 리스트
    private Dictionary<int, List<Question>> levelQuestions = new Dictionary<int, List<Question>>();  // 레벨별 질문을 저장할 딕셔너리
    private Question currentQuestion;
    private int currentQuestionIndex = 0;
    private int maxQuestions = 10;  // 총 10문제
    private int correctAnswersCount = 0;  // 정답 맞춘 횟수
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultColor = Color.white;

    public GameObject resultPopup;  // 결과 팝업 창
    public TextMeshProUGUI resultMessageText;  // 결과 팝업창의 메시지 텍스트
    public TextMeshProUGUI wisdomText;  // 지혜로움 수치 텍스트
    public TextMeshProUGUI experienceText;  // 경험치 수치 텍스트
    public TextMeshProUGUI candyText;  // 지혜사탕 수치 텍스트 (새로 추가된 부분)
    public int baseRewardWisdom = 4;  // 기본 지혜로움 보상
    public int baseRewardExperience = 10;  // 기본 경험치 보상
    public int bonusWisdom = 1;  // 7개 이상 맞추면 추가로 주는 지혜사탕
    public int bonusExperience = 10;  // 7개 이상 맞추면 추가로 주는 경험치
    public GameObject blackOverlay;  // Black 오브젝트

    private string correctMessage;
    private string wrongMessage;

    public TextMeshProUGUI answerMessageText;
    private int currentLevel;
    private string selectedSprite; // 선택된 스프라이트


    void Start()
    {
        LoadQuestions();

        if (ExperienceManager.Instance != null)
        {
            currentLevel = ExperienceManager.Instance.레벨;
        }
        else
        {
            currentLevel = 1;  // ExperienceManager가 없으면 기본 레벨 설정
        }

        // 스프라이트에 맞는 정답/오답 메시지 설정
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        if (selectedSprite == "Sprite1")
        {
            correctMessage = "정답이다옹!";
            wrongMessage = "틀렸다옹!";
        }
        else if (selectedSprite == "Sprite2")
        {
            correctMessage = "정답이다멍!";
            wrongMessage = "틀렸다멍!";
        }

        SetQuestionForLevel(currentLevel);
    }

    private void SetResultPopupMessage()
    {
        if (selectedSprite == "Sprite1")
        {
            resultMessageText.text = "수고했다옹!";
        }
        else if (selectedSprite == "Sprite2")
        {
            resultMessageText.text = "수고했다멍!";
        }
    }


    // 질문을 로드하는 함수 (각 레벨에 맞는 질문을 하드코딩)
    void LoadQuestions()
    {
        // 초급 (Lv 1-5)
        AddQuestionForLevel(1, "What is your name?", "네 이름이 뭐야?", new string[] { "네 나이가 몇이야?", "넌 어디 살아?", "너 직업이 뭐야?" });
        AddQuestionForLevel(1, "I like apples.", "나는 사과를 좋아해.", new string[] { "나는 바나나를 좋아해.", "나는 포도를 좋아해.", "나는 딸기를 좋아해." });
        AddQuestionForLevel(1, "Where is my book?", "내 책은 어디에 있어?", new string[] { "내 펜은 어디에 있어?", "내 공책은 어디에 있어?", "내 가방은 어디에 있어?" });
        AddQuestionForLevel(1, "Can you help me?", "나를 도와줄 수 있니?", new string[] { "넌 나를 따라올래?", "너 같이 놀래?", "내 질문에 답해줄래?" });  
        AddQuestionForLevel(1, "I want to play.", "나는 놀고 싶어.", new string[] { "나는 자고 싶어.", "나는 공부하고 싶어.", "나는 쉬고 싶어." });
        AddQuestionForLevel(1, "It's sunny today.", "오늘은 날씨가 맑아.", new string[] { "오늘은 비가 와.", "오늘은 바람이 불어.", "오늘은 눈이 와." });
        AddQuestionForLevel(1, "Let's go!", "가자!", new string[] { "기다려!", "멈춰!", "서둘러!" });
        AddQuestionForLevel(1, "This is fun!", "이거 재밌어!", new string[] { "이거 어려워!", "이거 지루해!", "이거 귀찮아!" });
        AddQuestionForLevel(1, "I can run fast.", "나는 빨리 달릴 수 있어.", new string[] { "나는 천천히 걸을 수 있어.", "나는 멀리 갈 수 있어.", "나는 높이 뛸 수 있어." });
        AddQuestionForLevel(1, "I like ice cream.", "나는 아이스크림을 좋아해.", new string[] { "나는 초콜릿을 좋아해.", "나는 사탕을 좋아해.", "나는 케이크를 좋아해." });

        // 중급 (Lv 6-10)
        AddQuestionForLevel(6, "What time is it?", "지금 몇 시야?", new string[] { "오늘 날짜가 뭐야?", "너 어디 가?", "내일 뭐 할 거야?" });
        AddQuestionForLevel(6, "Can I ask you a question?", "질문 하나 해도 될까?", new string[] { "너 어디 가?", "너 이거 알아?", "너 이거 할 수 있어?" });
        AddQuestionForLevel(6, "How was your day?", "오늘 하루 어땠어?", new string[] { "내일 뭐 할 거야?", "지금 기분 어때?", "어제는 어땠어?" });
        AddQuestionForLevel(6, "Do you like sports?", "너 운동 좋아해?", new string[] { "너 책 좋아해?", "너 요리 좋아해?", "너 게임 좋아해?" });
        AddQuestionForLevel(6, "I was just at the park.", "방금 공원에 있었어.", new string[] { "방금 집에 있었어.", "방금 학교에 있었어.", "방금 도서관에 있었어." });
        AddQuestionForLevel(6, "I'm feeling better now.", "지금 기분이 나아졌어.", new string[] { "기분이 더 나빠졌어.", "아직도 아파.", "상태가 똑같아." });  
        AddQuestionForLevel(6, "Let's have lunch together.", "같이 점심 먹자.", new string[] { "같이 저녁 먹자.", "같이 아침 먹자.", "같이 간식 먹자." }); 
        AddQuestionForLevel(6, "I'll handle the situation.", "내가 상황을 처리할게.", new string[] { "너가 상황을 처리해.", "우리가 상황을 처리하자.", "상황을 어떻게 처리할까?" });
        AddQuestionForLevel(6, "Could you repeat that, please?", "다시 한 번 말해줄래?", new string[] { "그거 다시 해줄래?", "그거 내가 해줄까?", "그거 말고 다른 거 해줄래?" });
        AddQuestionForLevel(6, "I'm sorry I'm late.", "늦어서 미안해.", new string[] { "너를 기다리게 해서 미안해.", "늦지 않아서 미안해.", "내가 먼저 와서 미안해." });

        // 고급 (Lv 11-15)
        AddQuestionForLevel(11, "What's your plan for tomorrow?", "내일 계획이 뭐야?", new string[] { "오늘 계획이 뭐야?", "다음 주 계획이 뭐야?", "이번 주말 계획이 뭐야?" });
        AddQuestionForLevel(11, "Please let me know if you need anything.", "필요한 것이 있으면 알려주세요.", new string[] { "원하는 것이 있으면 알려주세요.", "질문이 있으면 알려주세요.", "도움이 필요하면 알려주세요." });
        AddQuestionForLevel(11, "I'm available at 3 PM.", "오후 3시에 시간이 됩니다.", new string[] { "오후 4시에 시간이 됩니다.", "오전 11시에 시간이 됩니다.", "저녁 6시에 시간이 됩니다." });
        AddQuestionForLevel(11, "Let me check my schedule.", "제 일정을 확인해볼게요.", new string[] { "제 시간을 확인해볼게요.", "제 계획을 확인해볼게요.", "제 일정을 바꿔볼게요." });
        AddQuestionForLevel(11, "Please feel free to contact me.", "언제든지 연락하세요.", new string[] { "언제든지 나한테 말하세요.", "시간 맞추면 연락하세요.", "편하게 말 걸어주세요." });
        AddQuestionForLevel(11, "We have a problem with the shipment.", "배송에 문제가 있습니다.", new string[] { "배송이 끝났습니다.", "배송이 예정대로 진행됩니다.", "배송이 취소되었습니다." });
        AddQuestionForLevel(11, "I think it's a good idea.", "좋은 생각 같아.", new string[] { "나쁜 생각 같아.", "좋지 않은 생각 같아.", "글쎄, 확신이 안 서." });
        AddQuestionForLevel(11, "Let's grab some coffee.", "커피 마시러 가자.", new string[] { "차 마시러 가자.", "식사하러 가자.", "영화 보러 가자." });
        AddQuestionForLevel(11, "Better late than never.", "늦어도 안 하는 것보단 나아.", new string[] { "안 하는 것보다 더 나빠.", "늦는 게 더 나빠.", "미리 하는 게 나아." });
        AddQuestionForLevel(11, "Could you send me the report?", "보고서를 보내주시겠어요?", new string[] { "이메일을 보내주시겠어요?", "자료를 보내주시겠어요?", "파일을 보내주시겠어요?" });

        // 실생활 - 토익 (Lv 16-20)
        AddQuestionForLevel(16, "When is the deadline?", "마감일은 언제인가요?", new string[] { "시작일은 언제인가요?", "회의는 언제인가요?", "미팅은 언제인가요?" });
        AddQuestionForLevel(16, "Please let me know if you need anything.", "필요한 것이 있으면 알려주세요.", new string[] { "도움이 필요하면 알려주세요.", "질문이 있으면 알려주세요.", "말씀해주시겠어요?" });
        AddQuestionForLevel(16, "I'm available at 3 PM.", "오후 3시에 시간이 됩니다.", new string[] { "오후 2시에 시간이 됩니다.", "오후 4시에 시간이 됩니다.", "오후 5시에 시간이 됩니다." });
        AddQuestionForLevel(16, "Let me check my schedule.", "제 일정을 확인해볼게요.", new string[] { "제 계획을 확인해볼게요.", "일정을 조정해볼게요.", "다시 확인해볼게요." });
        AddQuestionForLevel(16, "Please feel free to contact me.", "언제든지 연락하세요.", new string[] { "언제든지 전화하세요.", "필요한 것 있으면 말하세요.", "도와줄 것이 있으면 알려주세요." });
        AddQuestionForLevel(16, "We have a problem with the shipment.", "배송에 문제가 있습니다.", new string[] { "배송이 끝났습니다.", "배송이 지연되었습니다.", "배송이 취소되었습니다." });
        AddQuestionForLevel(16, "I think it's a good idea.", "좋은 생각 같아.", new string[] { "나쁜 생각 같아.", "글쎄, 확신이 안 서.", "좋지 않은 생각이야." });
        AddQuestionForLevel(16, "Let's grab some coffee.", "커피 마시러 가자.", new string[] { "차 마시러 가자.", "밥 먹으러 가자.", "놀러 가자." });
        AddQuestionForLevel(16, "Better late than never.", "늦어도 안 하는 것보단 나아.", new string[] { "안 하는 게 나아.", "늦으면 안 돼.", "미리 하는 게 나아." });
        AddQuestionForLevel(16, "Could you send me the report?", "보고서를 보내주시겠어요?", new string[] { "이메일을 보내주시겠어요?", "파일을 보내주시겠어요?", "자료를 보내주시겠어요?" });
    }

    // 특정 레벨에 해당하는 질문을 추가하는 함수
    void AddQuestionForLevel(int level, string question, string correctAnswer, string[] wrongAnswers)
    {
        if (!levelQuestions.ContainsKey(level))
        {
            levelQuestions[level] = new List<Question>();
        }
        levelQuestions[level].Add(new Question(question, correctAnswer, wrongAnswers));
    }

    // 특정 레벨에 해당하는 질문을 설정하는 함수
    public void SetQuestionForLevel(int level)
    {

        // 피로도와 허기가 활동을 막는지 확인
        if (StatManager.Instance != null)
        {
            int currentFatigue = StatManager.Instance.피로도;
            int currentHunger = StatManager.Instance.허기;

            if (currentFatigue >= 10)
            {
                questionText.text = "너무 피곤해서 질문을 할 수 없습니다.";
                return;
            }

            if (currentHunger >= 10)
            {
                questionText.text = "배가 너무 고파서 질문을 할 수 없습니다.";
                return;
            }
        }

        if (level >= 1 && level <= 5)
        {
            level = 1; // 1~5 레벨 범위의 질문을 사용
        }
        else if (level >= 6 && level <= 10)
        {
            level = 6; // 6~10 레벨 범위의 질문을 사용
        }
        else if (level >= 11 && level <= 15)
        {
            level = 11; // 11~15 레벨 범위의 질문을 사용
        }
        else if (level >= 16 && level <= 20)
        {
            level = 16; // 16~20 레벨 범위의 질문을 사용
        }

        currentLevel = level;  // 현재 레벨 저장

        // 질문을 10번 초과하면 종료 처리
        if (currentQuestionIndex >= maxQuestions)
        {
            if (correctAnswersCount >= 7)
            {
                GiveRewards();  // 보상 지급
            }

            ShowResultPopup();  // 결과 팝업 창 표시
            Debug.Log("ShowResultPopup 호출 완료!");

            questionText.text = "모든 대화를 완료하였습니다!";

            foreach (Button button in answerButtons)
            {
                button.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                button.interactable = false;
            }

            if (StatManager.Instance != null)
            {
                StatManager.Instance.AdjustStat("허기", 1);
                StatManager.Instance.AdjustStat("관심도", 2);
                StatManager.Instance.SaveStatsToPlayerPrefs();
            }

            return;
        }

        if (!levelQuestions.ContainsKey(level))
        {
            Debug.LogError("해당 레벨에 대한 질문이 없습니다.");
            return;
        }

        List<Question> questionsForLevel = levelQuestions[level];
        List<Question> availableQuestions = questionsForLevel.FindAll(q => !askedQuestions.Contains(q));

        if (availableQuestions.Count == 0)
        {
            Debug.Log("더 이상 새로운 질문이 없습니다.");
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



    // 답변을 무작위로 섞는 함수
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
        correctAnswersCount++;  // 정답 횟수 증가

        // 선택된 스프라이트에 맞는 메시지 출력
        questionText.text = correctMessage;

        button.GetComponent<Image>().color = correctColor;
        yield return new WaitForSeconds(0.5f);
        SetQuestionForLevel(currentLevel);  // 다음 질문 설정 시 현재 레벨 유지
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
        SetQuestionForLevel(currentLevel);  // 다음 질문 설정 시 현재 레벨 유지
    }


    private void GiveRewards()
    {
        int totalWisdom = baseRewardWisdom;  // 기본 지혜로움 보상
        int totalExperience = baseRewardExperience;  // 기본 경험치 보상
        int totalCandies = 0;  // 지혜사탕 개수 (기본값 0)

        if (correctAnswersCount >= 7)
        {
            totalCandies = bonusWisdom;  // 추가 지혜사탕 보상 (7개 이상 맞추면)
            totalExperience += bonusExperience;  // 추가 경험치 보상 + 경험치 20 추가
        }

        // StatManager에 지혜로움 및 지혜사탕 보상을 반영
        if (StatManager.Instance != null)
        {
            StatManager.Instance.AdjustStat("지혜로움", totalWisdom);  // 지혜로움 추가
            StatManager.Instance.AdjustStat("지혜사탕", totalCandies);  // 지혜사탕 추가
            StatManager.Instance.SaveStatsToPlayerPrefs();  // 스탯 저장
        }

        // ExperienceManager에서 경험치 추가
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.AddExperience(totalExperience);  // 경험치 추가
        }

        // 결과 팝업에 보상 표시
        wisdomText.text = $"지혜로움 +{totalWisdom}";
        experienceText.text = $"경험치 +{totalExperience}";

        // 지혜사탕 텍스트에 지혜사탕 값 표시
        if (totalCandies > 0)
        {
            candyText.text = $"지혜사탕 +{totalCandies}";
        }
        else
        {
            candyText.text = "0";  // 지혜사탕 보상이 없을 경우 빈 문자열로 설정
        }
    }

    private void ShowResultPopup()
    {
        if (resultPopup != null)
        {
            resultPopup.SetActive(true);  // 결과 팝업 창 활성화

            if (blackOverlay != null)
            {
                blackOverlay.SetActive(true);  // Black 오브젝트 활성화
            }

            Debug.Log("결과 팝업이 활성화되었습니다.");
        }
        else
        {
            Debug.LogError("resultPopup이 할당되지 않았습니다!");
        }
    }
}

// 질문 클래스 정의
[System.Serializable]
public class Question
{
    public string question;  // 질문
    public string correctAnswer;  // 정답
    public string[] wrongAnswers;  // 오답

    public Question(string q, string correct, string[] wrong)
    {
        question = q;
        correctAnswer = correct;
        wrongAnswers = wrong;
    }
}
