using UnityEngine;
using UnityEngine.Events;

public class ColorRainbow : MonoBehaviour
{
    private Material localMaterial;

    void Start()
    {
        // Obtener el material local (instancia �nica para este objeto)
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Se clona el material autom�ticamente al usar .material (no .sharedMaterial)
            localMaterial = renderer.material;
        }
    }


    public void CambiarColorAleatorio()
    {
        if (localMaterial != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            localMaterial.color = randomColor;
        }
    }
}
