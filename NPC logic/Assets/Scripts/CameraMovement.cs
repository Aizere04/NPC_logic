using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 10.0f; // Скорость перемещения камеры
    public float sprintMultiplier = 2.0f; // Множитель для ускорения
    public float rotationSpeed = 100.0f; // Скорость вращения камеры

    void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    void MoveCamera()
    {
        // Получаем ввод от пользователя для направления движения
        float horizontal = Input.GetAxis("Horizontal"); // Влево и вправо (A и D)
        float vertical = Input.GetAxis("Vertical");   // Вперед и назад (W и S)

        // Определяем направление перемещения
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Учитываем ускорение, если зажата клавиша Shift
        float speed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= sprintMultiplier;
        }

        // Перемещаем камеру относительно её направления
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }

    void RotateCamera()
    {
        // Получаем ввод от пользователя для вращения камеры
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Вращаем камеру по горизонтали
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);

        // Вращаем камеру по вертикали
        transform.Rotate(Vector3.right, -mouseY * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
