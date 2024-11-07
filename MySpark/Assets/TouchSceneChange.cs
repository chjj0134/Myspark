using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchSceneChange : MonoBehaviour
{
    public string sceneName = "Select/Select";  // ��ȯ�� �� �̸�
    private bool canChangeScene = true;         // �� ��ȯ ���� ����
    public float delayBeforeNextTouch = 0.5f;     // �� ��ȯ �� ��ġ �Է��� ���� ������ �ð�

    private void Start()
    {
        // Ÿ��Ʋ ������ ���� ������ �ʱ�ȭ
        PlayerPrefs.DeleteAll();

        // ó�� ���� �� �����̸� �ɾ� ���� �ð� ���� ��ġ �Է��� ���� �ʵ��� ����
        canChangeScene = false;
        Invoke("EnableSceneChange", delayBeforeNextTouch);  // ���� �ð� �Ŀ� ��ġ �����ϰ� ����
    }

    void Update()
    {
        // �� ��ȯ�� �����ϰ� ��ġ �Է��� ���� ���� ���� ��ȯ
        if (canChangeScene && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // �� ��ȯ
            SceneManager.LoadScene(sceneName);

            // ��ġ �Է��� ���� �ð� ���� ����
            canChangeScene = false;
            Invoke("EnableSceneChange", delayBeforeNextTouch);  // ���� �ð� �� �ٽ� ��ġ �Է��� ����
        }
    }

    // �� ��ȯ �����ϰ� �ٽ� �����ϴ� �Լ�
    void EnableSceneChange()
    {
        canChangeScene = true;
    }
}
