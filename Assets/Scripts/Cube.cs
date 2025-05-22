using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;

    public float ChanceDuplication { get; private set; } = 100f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Vector3 oldScale, float newChance)
    {
        RandomizeColor();
        SetScale(oldScale);
        SetChanceDuplication(newChance);
    }

    private void SetChanceDuplication(float newChance)
    {
        float chanceDuplicationModifier = 0.5f;
        ChanceDuplication = newChance;
        ChanceDuplication *= chanceDuplicationModifier;
    }

    private void SetScale(Vector3 oldScale)
    {
        float scaleModifier = 0.5f;
        transform.localScale = oldScale * scaleModifier;
    }

    private void RandomizeColor()
    {
        Material newMaterial = new Material(_renderer.sharedMaterial);
        Color randomColor = Random.ColorHSV();
        newMaterial.color = randomColor;
        _renderer.material = newMaterial;
    }
}