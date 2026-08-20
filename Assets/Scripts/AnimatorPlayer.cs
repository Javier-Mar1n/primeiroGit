using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovimentoJogador))]
[RequireComponent(typeof(PuloJogador))]
public class AnimadorJogador : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Run,
        jumping,
        falling,
    }

    private Animator animator;
    private MovimentoJogador movimento;
    private PuloJogador pulo;
    private PlayerState currentState;

    private void Awake()
    {
        movimento = GetComponent<MovimentoJogador>();
        animator = GetComponent<Animator>();
        pulo = GetComponent<PuloJogador>();
    }

    private void Update()
    {
        PlayerState newState = SetState();

        if (newState != currentState)
        {
            currentState = newState;
            animator.Play(currentState.ToString());
        }


    }

    private PlayerState SetState()
    {
        if (movimento.EstaAndando && pulo.EstaNoCha)
        {
            return PlayerState.Run;
        }

        if (!pulo.EstaNoCha)
        {
            return PlayerState.jumping;
        }


        if (pulo.EstaSubindo)
        {
            return PlayerState.jumping;
        }
        if (pulo.EstaCaindo)
        {
            return PlayerState.falling;
        }

        return PlayerState.Idle;
    }

}