using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Vitesse de rotation du joueur
    public float rotationSpeed = 5f;

    void Update()
    {
        // Récupère la position de la souris dans l'écran
        Vector3 mousePos = Input.mousePosition;
        // Convertit la position de la souris en un rayon dans l'espace du jeu
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        // Déclaration d'une variable pour stocker l'endroit où le rayon rencontre un objet dans le jeu
        RaycastHit hit;

        // Si le rayon touche un objet
        if (Physics.Raycast(ray, out hit))
        {
            // Calcule la direction à laquelle le joueur doit faire face
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f; // Garde le joueur sur le même plan que le sol (s'il y a)
            // Tourne le joueur pour regarder dans cette direction
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);
        }
    }
}
