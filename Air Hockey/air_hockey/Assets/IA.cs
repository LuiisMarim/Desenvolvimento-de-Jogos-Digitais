using UnityEngine;

public class IA : MonoBehaviour
{
    // Limite máximo e mínimo no eixo X (horizontal)
    public float boundX = 8.0f;

    // Limite máximo e mínimo no eixo Y (vertical)
    public float boundY = 2.25f;

    // Velocidade de movimento da IA
    public float moveSpeed = 3.0f;

    // Direção atual do movimento (um vetor 2D com X e Y)
    private Vector2 direction;

    // Start é chamado uma vez quando o objeto é iniciado
    void Start()
    {
        // Escolhe uma direção inicial aleatória
        EscolherNovaDirecao();
    }

    // Update é chamado uma vez por frame (60+ vezes por segundo)
    void Update()
    {
        // Move o objeto na direção escolhida
        // Multiplicado por moveSpeed para controlar a velocidade
        // Multiplicado por Time.deltaTime para que o movimento seja igual em todos os PCs (independente do FPS)
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Guarda a posição atual
        var pos = transform.position;

        // Se passar do limite no eixo X...
        if (pos.x > boundX || pos.x < -boundX)
        {
            // Inverte a direção no eixo X (faz a IA "quicar" na parede)
            direction.x *= -1;

            // Garante que a posição não passe do limite
            pos.x = Mathf.Clamp(pos.x, -boundX, boundX);
        }

        // Se passar do limite no eixo Y...
        if (pos.y > boundY || pos.y < 0)
        {
            // Inverte a direção no eixo Y
            direction.y *= -1;

            // Corrige a posição dentro do limite
            pos.y = Mathf.Clamp(pos.y, -boundY, boundY);
        }

        // Atualiza a posição do objeto com os valores corrigidos
        transform.position = pos;
    }

    // Função para escolher uma direção aleatória
    void EscolherNovaDirecao()
    {
        // Cria um vetor aleatório com valores entre -1 e 1
        // Normalizado (normalized) garante que a velocidade seja sempre a mesma,
        // mesmo se estiver indo na diagonal
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
