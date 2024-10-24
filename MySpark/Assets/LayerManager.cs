using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // 레이어 이름과 순서
    public string sortingLayerName = "Default";  // 기본 레이어 이름
    public int sortingOrder = 0;  // 렌더 순서 (작을수록 먼저 렌더링됨)

    void Start()
    {
        // SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();

        // SpriteRenderer의 레이어 설정
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = sortingLayerName;
            spriteRenderer.sortingOrder = sortingOrder;
        }
        else
        {
            Debug.LogError("SpriteRenderer를 찾을 수 없습니다.");
        }
    }
}
