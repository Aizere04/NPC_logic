using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Скорость движения игрока
    public float rotationSpeed = 700.0f; // Скорость поворота игрока

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Проверяем наличие компонента NavMeshAgent
        if (navMeshAgent == null)
        {
            Debug.LogError("Missing NavMeshAgent component on " + gameObject.name);
        }
    }

    void Update()
    {
        if (navMeshAgent != null)
        {
            MovePlayer();
        }
        UpdateAnimation();
    }

    void MovePlayer()
    {
        // Получаем ввод от пользователя
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Двигаем игрока вперед-назад и влево-вправо
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            Vector3 targetPosition = transform.position + direction * movementSpeed * Time.deltaTime;
            navMeshAgent.SetDestination(targetPosition);

            // Поворачиваем игрока в сторону движения
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void UpdateAnimation()
    {
        // Обновляем параметры анимации для Blend Tree
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speed = new Vector2(horizontal, vertical).magnitude;
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }
}
