using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // ���̾� �̸��� ����
    public string sortingLayerName = "Default";  // �⺻ ���̾� �̸�
    public int sortingOrder = 0;  // ���� ���� (�������� ���� ��������)

    void Start()
    {
        // SpriteRenderer ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>();

        // SpriteRenderer�� ���̾� ����
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = sortingLayerName;
            spriteRenderer.sortingOrder = sortingOrder;
        }
        else
        {
            Debug.LogError("SpriteRenderer�� ã�� �� �����ϴ�.");
        }
    }
}
