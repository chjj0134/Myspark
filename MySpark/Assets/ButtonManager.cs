using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // ��ư�� �Ҵ��� �� �̸��� �迭�� ����
    public string[] sceneNames = { "Button/1", "Button/2", "Button/4", "Button/5" };

    // ������ UI ��ư�� �Ҵ�
    public Button button1;
    public Button button2;
    public Button button4;
    public Button button5;

    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ ����
        button1.onClick.AddListener(() => LoadScene(0)); // Button/1 ������ �̵�
        button2.onClick.AddListener(() => LoadScene(1)); // Button/2 ������ �̵�
        button4.onClick.AddListener(() => LoadScene(2)); // Button/4 ������ �̵�
        button5.onClick.AddListener(() => LoadScene(3)); // Button/5 ������ �̵�
    }

    // ���� �ε��ϴ� �޼ҵ�
    void LoadScene(int index)
    {
        if (index >= 0 && index < sceneNames.Length)
        {
            SceneManager.LoadScene(sceneNames[index]);
        }
        else
        {
            Debug.LogError("�߸��� �� �ε����Դϴ�.");
        }
    }
}
