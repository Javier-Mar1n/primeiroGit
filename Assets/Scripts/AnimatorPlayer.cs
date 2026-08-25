using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovimentoJogador))]
[RequireComponent(typeof(PuloJogador))]
public class AnimadorJogador : MonoBehaviour
{
    private enum EstadoDoJogador
    {
        Parado,
        Correndo,
        Caindo,
        Pulando
    }
    private Animator animator;
    private MovimentoJogador movimento;
    private PuloJogador pulo;
    private EstadoDoJogador estadoAtual;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movimento = GetComponent<MovimentoJogador>();
        pulo = GetComponent<PuloJogador>();
    }
    private void Update()
    {
        EstadoDoJogador novoEstado = DecidirEstado();

        if (novoEstado != estadoAtual)
        {
            estadoAtual = novoEstado;
            animator.Play(estadoAtual.ToString());
        }
    }
    private EstadoDoJogador DecidirEstado()
    {

        if (movimento.EstaAndando && pulo.EstaNoCha)
        {
            return EstadoDoJogador.Correndo;
        }
        if (pulo.EstaCaindo)
        {
            return EstadoDoJogador.Caindo;
        }
        if (pulo.EstaSubindo)
        {
            return EstadoDoJogador.Pulando;
        }

        return EstadoDoJogador.Parado;

    }
}
