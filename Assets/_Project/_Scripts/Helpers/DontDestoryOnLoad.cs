using UnityEngine;

public class DontDestoryOnLoad : MonoBehaviour
{
    private void Start() => DontDestroyOnLoad(gameObject);
}