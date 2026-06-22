using UnityEngine;

public class MouseWorldPosition : MonoBehaviour
{
    public static MouseWorldPosition Instance { get; private set; } // Déclare un singleton pour accéder à cette instance depuis d'autres scripts

    private void Awake() // Méthode appelée au démarrage de l'objet, avant le premier frame
    {
        Instance = this; // Assigne cette instance au singleton, permettant un accès global
    }

    public Vector3 GetPosition() // Méthode qui retourne la position 3D du curseur dans le monde
    {
        Ray mouseCameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); // Crée un rayon depuis la caméra principale, partant de la position du curseur à l'écran

        Plane plane = new Plane(Vector3.up, Vector3.zero); // Définit un plan horizontal (axe Y vers le haut) centré à l'origine (0, 0, 0)

        if (plane.Raycast(mouseCameraRay, out float distance)) // Vérifie si le rayon intersecte le plan, et récupère la distance de l'intersection
        {
            return mouseCameraRay.GetPoint(distance); // Retourne la position 3D dans le monde où le rayon intersecte le plan
        }
        else
        {
            return Vector3.zero; // Si aucune intersection n'est trouvée, retourne la position (0, 0, 0)
        }
    }
}
