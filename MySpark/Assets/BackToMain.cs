using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMain : MonoBehaviour
{
    public Button backButton;  // �������� ���ư��� ��ư
    public GameObject resultPopup;  // ��� �˾� ������Ʈ
    public GameObject blackOverlay;  // Black �������� ������Ʈ

    void Start()
    {
        // ���� ��ư Ŭ�� �̺�Ʈ�� ����
        backButton.onClick.AddListener(LoadMainScene);
    }

    // "Main/Main" ���� �ε��ϴ� ���� �Լ�
    void LoadMainScene()
    {
        SceneManager.LoadScene("Main/Main");
    }

    // ��� �˾��� �������̸� ��Ȱ��ȭ�ϰ� ���� ������ ���ư��� ���ο� �Լ�
    public void ClosePopupAndReturnToMain()
    {
        // ��� �˾��� Black ������Ʈ�� Ȱ��ȭ�Ǿ� ���� ���� ��Ȱ��ȭ
        if (resultPopup != null && resultPopup.activeSelf)
        {
            resultPopup.SetActive(false);  // ��� �˾� ��Ȱ��ȭ
        }

        if (blackOverlay != null && blackOverlay.activeSelf)
        {
            blackOverlay.SetActive(false);  // Black �������� ��Ȱ��ȭ
        }

        // "Main/Main" �� �ε�
        SceneManager.LoadScene("Main/Main");
    }
}
