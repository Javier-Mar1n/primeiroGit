using UnityEngine;


public class MovimentoVaiEVolta : MonoBehaviour
{

    private enum EixoDoMovimento
    {
        Horizontal,
        Vertical
    }


    [SerializeField] private EixoDoMovimento eixo = EixoDoMovimento.Horizontal;

    [SerializeField] private float distancia = 3f;


    [SerializeField] private float velocidade = 2f;


    [SerializeField] private bool comecarAoContrario = false;

    [Header("Ajuda visual")]


    [SerializeField] private bool mostrarCaminhoSempre = true;


    private float limiteMinimo;
    private float limiteMaximo;


    private float sentidoAtual;


    private void Awake()
    {

        float posicaoInicial = eixo == EixoDoMovimento.Horizontal
            ? transform.position.x
            : transform.position.y;

        limiteMinimo = posicaoInicial - distancia;
        limiteMaximo = posicaoInicial + distancia;


        sentidoAtual = comecarAoContrario ? -1f : 1f;
    }

    private void Update()
    {
        VaiEVolta();
    }

    private void VaiEVolta()
    {
        float passo = velocidade * sentidoAtual * Time.deltaTime;

        Vector3 posicao = transform.position;

        if (eixo == EixoDoMovimento.Horizontal)
        {
            posicao.x = Mathf.Clamp(posicao.x + passo, limiteMinimo, limiteMaximo);
        }
        else
        {
            posicao.y = Mathf.Clamp(posicao.y + passo, limiteMinimo, limiteMaximo);
        }

        transform.position = posicao;

        VerificarSeChegouNaPonta();
    }

    private void VerificarSeChegouNaPonta()
    {
        float posicaoNoEixo = eixo == EixoDoMovimento.Horizontal
            ? transform.position.x
            : transform.position.y;

        if (posicaoNoEixo == limiteMaximo || posicaoNoEixo == limiteMinimo)
        {
            sentidoAtual *= -1f;
        }
    }

    private void OnDrawGizmos()
    {
        if (!mostrarCaminhoSempre)
        {
            return;
        }

        DescobrirPontas(out Vector3 pontaIda, out Vector3 pontaVolta);

        Gizmos.color = new Color(1f, 1f, 1f, 0.35f);
        Gizmos.DrawLine(pontaIda, pontaVolta);
    }

    private void OnDrawGizmosSelected()
    {
        DescobrirPontas(out Vector3 pontaIda, out Vector3 pontaVolta);

        Vector3 centro = (pontaIda + pontaVolta) / 2f;
        Vector3 tamanhoDoObjeto = DescobrirTamanhoDoObjeto();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pontaIda, pontaVolta);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pontaIda, tamanhoDoObjeto);
        Gizmos.DrawWireSphere(pontaIda, 0.12f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pontaVolta, tamanhoDoObjeto);
        Gizmos.DrawWireSphere(pontaVolta, 0.12f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(centro, 0.08f);

        Gizmos.color = Color.green;
        DesenharSeta(centro, pontaIda);
    }

    private void DescobrirPontas(out Vector3 pontaIda, out Vector3 pontaVolta)
    {
        Vector3 direcao = eixo == EixoDoMovimento.Horizontal
            ? Vector3.right
            : Vector3.up;

        Vector3 sentidoDaIda = comecarAoContrario ? -direcao : direcao;

        if (Application.isPlaying)
        {
            float meio = (limiteMinimo + limiteMaximo) / 2f;

            Vector3 centro = transform.position;

            if (eixo == EixoDoMovimento.Horizontal)
            {
                centro.x = meio;
            }
            else
            {
                centro.y = meio;
            }

            pontaIda = centro + sentidoDaIda * distancia;
            pontaVolta = centro - sentidoDaIda * distancia;
            return;
        }

        pontaIda = transform.position + sentidoDaIda * distancia;
        pontaVolta = transform.position - sentidoDaIda * distancia;
    }

    private Vector3 DescobrirTamanhoDoObjeto()
    {
        SpriteRenderer visual = GetComponent<SpriteRenderer>();

        if (visual == null || visual.sprite == null)
        {
            return Vector3.one * 0.5f;
        }

        return Vector3.Scale(visual.sprite.bounds.size, transform.lossyScale);
    }

    private void DesenharSeta(Vector3 origem, Vector3 destino)
    {
        Gizmos.DrawLine(origem, destino);

        Vector3 direcao = (destino - origem).normalized;

        if (direcao == Vector3.zero)
        {
            return;
        }
        Vector3 lado = new Vector3(-direcao.y, direcao.x, 0f);

        float tamanhoDaPonta = 0.2f;

        Gizmos.DrawLine(destino, destino - direcao * tamanhoDaPonta + lado * tamanhoDaPonta);
        Gizmos.DrawLine(destino, destino - direcao * tamanhoDaPonta - lado * tamanhoDaPonta);
    }
}