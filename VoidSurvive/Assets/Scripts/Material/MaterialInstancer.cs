using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialInstancer : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField]
    private Color color;

    // Start is called before the first frame update
    private void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = Instantiate(skinnedMeshRenderer.material);
        skinnedMeshRenderer.material.color = color;
    }
}
