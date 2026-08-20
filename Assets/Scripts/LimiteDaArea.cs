using UnityEngine;

public class LimiteDaArea : MonoBehaviour
{
    [Header("limites no eixo X")]
    [SerializeField] private float limiteEsquerda = -11f;
    [SerializeField] private float limiteDireita = 13f;
    void Start()
    {

    }


    void LateUpdate()
    {
        SegurarDentroDaArea();
    }

    private void SegurarDentroDaArea()
    {
        Vector3 posicao = transform.position;

        posicao.x = Mathf.Clamp(posicao.x, limiteEsquerda, limiteDireita);
        transform.position = posicao;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float altura = 10f;
        float y = transform.position.y;
        Gizmos.DrawLine(new Vector3(limiteEsquerda, y - altura, 0f),
            new Vector3(limiteEsquerda, y + altura, 0f));
        Gizmos.DrawLine(new Vector3(limiteDireita, y - altura, 0f),
            new Vector3(limiteDireita, y + altura, 0f));
    }
}