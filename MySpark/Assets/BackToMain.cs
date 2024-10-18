using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMain : MonoBehaviour
{
    public Button backButton;

    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ �߰�
        backButton.onClick.AddListener(LoadMainScene);
    }

    // "Main/Main" ���� �ε��ϴ� �Լ�
    void LoadMainScene()
    {
        SceneManager.LoadScene("Main/Main");
    }
}
