using Modules.Entities.SharedComponents;
using TriInspector;
using UnityEngine;

public class GeneratorShadow : MonoBehaviour
{
    [Required] [SerializeField] private ShadowScript Prefab;

    private void Start()
    {
        var shadow = Instantiate(Prefab);
        shadow.target = transform;
    }
}