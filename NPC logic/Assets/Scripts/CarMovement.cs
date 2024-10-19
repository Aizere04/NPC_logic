using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 5.0f; // Скорость движения машины
    public Transform[] waypoints; // Точки маршрута для движения машины
    private int currentWaypointIndex = 0;

    void Update()
    {
        MoveAlongWaypoints();
    }

    void MoveAlongWaypoints()
    {
        if (waypoints.Length == 0)
            return;

        // Перемещаем машину к текущей точке маршрута
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Проверяем, достигли ли мы текущей точки маршрута
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.5f)
        {
            // Переходим к следующей точке маршрута
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
