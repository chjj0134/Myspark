using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public SpriteRenderer characterSpriteRenderer;

    public Sprite[] sprite1Levels = new Sprite[4];
    public Sprite[] sprite2Levels = new Sprite[4];
    public Sprite[] dirtySprite1Levels = new Sprite[4];
    public Sprite[] dirtySprite2Levels = new Sprite[4];
    public Sprite[] hungrySprite1Levels = new Sprite[4];
    public Sprite[] hungrySprite2Levels = new Sprite[4];

    private bool isDirty = false;
    private bool isHungry = false;

    private void Start()
    {
        UpdateCharacterSprite();

        // 이벤트 연결
        StatManager.Instance.OnDirtyState.AddListener(SetDirtySprite);
        StatManager.Instance.OnHungryState.AddListener(SetHungrySprite);
        StatManager.Instance.OnNormalFatigueState.AddListener(ResetFatigueSprite);
        StatManager.Instance.OnNormalHungerState.AddListener(ResetHungerSprite);
        StatManager.Instance.OnDateChanged.AddListener(UpdateCharacterSprite);
    }

    private void UpdateCharacterSprite()
    {
        int dayLevel = GetSpriteLevelByDay();
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");

        if (selectedSprite == "Sprite1")
        {
            characterSpriteRenderer.sprite = sprite1Levels[dayLevel];
        }
        else if (selectedSprite == "Sprite2")
        {
            characterSpriteRenderer.sprite = sprite2Levels[dayLevel];
        }
    }

    private int GetSpriteLevelByDay()
    {
        return StatManager.Instance.GetSpriteLevelByDay();
    }

    private void SetDirtySprite()
    {
        if (!isDirty)
        {
            int dayLevel = GetSpriteLevelByDay();
            string selectedSprite = PlayerPrefs.GetString("SelectedSprite");

            if (selectedSprite == "Sprite1")
            {
                characterSpriteRenderer.sprite = dirtySprite1Levels[dayLevel];
            }
            else if (selectedSprite == "Sprite2")
            {
                characterSpriteRenderer.sprite = dirtySprite2Levels[dayLevel];
            }
            isDirty = true;
        }
    }

    private void SetHungrySprite()
    {
        if (!isHungry)
        {
            int dayLevel = GetSpriteLevelByDay();
            string selectedSprite = PlayerPrefs.GetString("SelectedSprite");

            if (selectedSprite == "Sprite1")
            {
                characterSpriteRenderer.sprite = hungrySprite1Levels[dayLevel];
            }
            else if (selectedSprite == "Sprite2")
            {
                characterSpriteRenderer.sprite = hungrySprite2Levels[dayLevel];
            }
            isHungry = true;
        }
    }

    private void ResetFatigueSprite()
    {
        if (isDirty && !isHungry)
        {
            UpdateCharacterSprite();
        }
        isDirty = false;
    }

    private void ResetHungerSprite()
    {
        if (isHungry && !isDirty)
        {
            UpdateCharacterSprite();
        }
        isHungry = false;
    }
}
