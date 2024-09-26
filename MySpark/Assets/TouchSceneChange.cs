using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchSceneChange : MonoBehaviour
{
    // ��ȯ�� ���� �̸��� �Է�
    public string sceneName= "Select/Select";

    void Update()
    {
        // ȭ�鿡 ��ġ �Է��� ������ ���� ��ȯ
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // �� ��ȯ
            SceneManager.LoadScene(sceneName);
        }
    }
}
