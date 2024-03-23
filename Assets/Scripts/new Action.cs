using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class MoveToClick : MonoBehaviour
{
    private bool isMoving = false; // Variable para controlar si el objeto está siendo movido
    private float moveSpeed = 5f; // Velocidad de movimiento del objeto

    void Update()
    {
        // Comprueba si se ha hecho clic y si el objeto no está siendo movido
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Convierte la posición del clic en el mundo a la posición del objeto
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = transform.position.z; // Mantén la coordenada Z del objeto

            // Mueve el objeto gradualmente hacia la posición del clic
            StartCoroutine(MoveObject(clickPosition));
        }
    }

    // Método para mover gradualmente el objeto hacia una posición objetivo
    private IEnumerator MoveObject(Vector3 targetPosition)
    {
        isMoving = true; // Marca el objeto como en movimiento

        while (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPosition.x, targetPosition.y)) > 0.01f)
        {
            // Calcula la nueva posición del objeto (solo en los ejes X e Y)
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            newPosition.z = transform.position.z; // Mantén la coordenada Z del objeto
            transform.position = newPosition;

            yield return null; // Espera un frame antes de continuar el bucle
        }

        // Ajusta la posición del objeto a la posición objetivo para evitar pequeños desajustes
        transform.position = targetPosition;

        isMoving = false; // Marca el objeto como no en movimiento
    }
}