using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MovimentoJogador : MonoBehaviour
{
    [SerializeField] private float velocidade = 10f;
    private Rigidbody2D corpo;

    private SpriteRenderer visual;
    private float direcaoHorizontal;
    public float VelocidadeAtual => Mathf.Abs(corpo.linearVelocity.x);
    public bool EstaAndando => direcaoHorizontal != 0f;
    private void Awake()
    {
        visual = GetComponent<SpriteRenderer>();
        corpo = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direcaoHorizontal = Input.GetAxisRaw("Horizontal");
        VirarSpriteParaOLadoCerto();
    }
    private void FixedUpdate()
    {
        Mover();
    }
    private void Mover()
    {
        corpo.linearVelocity = new Vector2(direcaoHorizontal * velocidade, corpo.linearVelocity.y);
    }
    private void VirarSpriteParaOLadoCerto()
    {
        if (direcaoHorizontal == 0f)
        {
            return;
        }
        visual.flipX = direcaoHorizontal < 0f;
    }
}
