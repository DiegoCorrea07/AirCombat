using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Esto destruye el objeto de explosi�n cuando la animaci�n termine
    void Start()
    {
        // Obtener la duraci�n de la animaci�n
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            // Destruir el objeto de explosi�n despu�s de la duraci�n de la animaci�n
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
