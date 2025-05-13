using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public float ChanceDuplication { get; private set; } = 100f;

    public void Init(Vector3 oldScale, float newChance)
    {
        RandomizeColor();
        SetScale(oldScale);
        SetChanceDuplication(newChance);
        ReduceChance();
    }

    private void ReduceChance()
    {
        ChanceDuplication = ChanceDuplication * 0.5f;
        Debug.Log("Разделен с шансом: " + ChanceDuplication);
    }

    private void SetChanceDuplication(float newChance)
    {
        ChanceDuplication = newChance;
    }

    private void SetScale(Vector3 oldScale)
    {
        float scaleModifier = 0.5f;
        transform.localScale = oldScale * scaleModifier;
    }

    private void RandomizeColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material newMaterial = new Material(renderer.sharedMaterial);
        Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        newMaterial.color = randomColor;
        renderer.material = newMaterial;
    }
}