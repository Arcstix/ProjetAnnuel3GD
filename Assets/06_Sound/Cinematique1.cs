using UnityEngine;
using UnityEngine.Playables;

public class Cinematique1 : MonoBehaviour
{
    public PlayableDirector timeline;
    public MonoBehaviour controlScript; // Ton script de mouvement (ex: PlayerMovement)
    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;

            // Désactiver le script de contrôle du joueur
            if (controlScript != null)
                controlScript.enabled = false;

            // Jouer la cinématique
            if (timeline != null)
            {
                timeline.Play();
                timeline.stopped += OnTimelineStopped;
            }
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        // Réactiver les contrôles du joueur
        if (controlScript != null)
            controlScript.enabled = true;

        // Nettoyage
        timeline.stopped -= OnTimelineStopped;
    }
}