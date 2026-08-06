using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PuloJogador : MonoBehaviour
{
    [Header("Pulo")]
    [SerializeField] private float focaDoPulo = 16f;

    [Header("Deteccao do Chao")]
    [SerializeField] private Transform pontoDosPes;
    [SerializeField] private LayerMask camadaDoChao;
    [SerializeField] private float raioDaChecagem = 0.15f;
    private Rigidbody2D corpo;
    private bool pedidoDePulo;

    public bool EstaNoCha { get; private set; }

    private void Awake()
    {
        corpo = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        VerificaSeEstaNoChao();
        if (pedidoDePulo && EstaNoCha)
        {
            Pular();
        }
        pedidoDePulo = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            pedidoDePulo = true;
        }
    }

    private void Pular()
    {
        corpo.linearVelocity = new Vector2(corpo.linearVelocity.x, focaDoPulo);
    }
    private void VerificaSeEstaNoChao()
    {
        EstaNoCha = Physics2D.OverlapCircle(pontoDosPes.position, raioDaChecagem, camadaDoChao);
    }
    private void OnDrawGizmosSelected()
    {
        if (pontoDosPes == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pontoDosPes.position, raioDaChecagem);
    }

}
