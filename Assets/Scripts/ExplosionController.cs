using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Esto destruye el objeto de explosión cuando la animación termine
    void Start()
    {
        // Obtener la duración de la animación
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            // Destruir el objeto de explosión después de la duración de la animación
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
