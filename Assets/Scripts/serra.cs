using UnityEngine;

public class serra : MonoBehaviour
{
    [SerializeField] private float Rotacao = 10f;
    void Update()
    {
       Serrando();
    }
    void Serrando()
    {
        if (gameObject.GetComponentInParent<MovimentoVaiEVolta>().enabled == true)
        {
            Rotacao = 10f;
        }
        else
        {
            Rotacao = 0f;
        }
        transform.Rotate(0, 0, Rotacao);
    }
}
