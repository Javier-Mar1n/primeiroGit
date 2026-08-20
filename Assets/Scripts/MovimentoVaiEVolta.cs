using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEditor.ShaderData;

public class MovimentoAniSerra : MonoBehaviour
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
    [SerializeField] private bool mostrarCaminho = true;

    private float limiteMinimo;
    private float limiteMaximo;

    private float sentidoAtual;

    private void Awake()
    {

        float posicaoInicial = eixo == EixoDoMovimento.Horizontal ? transform.position.x : transform.position.y;
        limiteMinimo = posicaoInicial - distancia;
        limiteMaximo = posicaoInicial + distancia;
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

    }
    private void VerificarSeChegouNaPonta()
    {
        float posicaoEixo = eixo == EixoDoMovimento.Horizontal ? transform.position.x : transform.position.y;
        if (posicaoEixo == limiteMaximo || posicaoEixo == limiteMinimo)
        {
            sentidoAtual *= -1f;
        }
    }


    private void OnDrawGizmosSelected()
        {
        if (!mostrarCaminho)
            {
                return;
            }
        
        }
    private void Descobrirpontas(out Vector3 pontaIda, out Vector3 pontaVolta)
    {
        Vector3 direcao = eixo == EixoDoMovimento.Horizontal ? Vector3.right : Vector3.up; //condiçao ternaria

    }
    }



