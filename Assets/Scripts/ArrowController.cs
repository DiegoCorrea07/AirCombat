using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform personaje; // Referencia al personaje
    public float limiteRotacion = 22.5f; // Límite de rotación en grados (45 grados totales)

    void Update()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ignorar la profundidad

        // Calcular la dirección desde el personaje hacia el mouse
        Vector3 direccion = (mousePos - personaje.position).normalized;

        // Hacer que la flecha siga al personaje
        transform.position = personaje.position;

        // Calcular el ángulo en grados (desde el eje derecho del personaje)
        float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Limitar el ángulo al rango permitido (-22.5 a +22.5 grados)
        angle = Mathf.Clamp(angle, -limiteRotacion, limiteRotacion);

        // Aplicar la rotación limitada al objeto
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
