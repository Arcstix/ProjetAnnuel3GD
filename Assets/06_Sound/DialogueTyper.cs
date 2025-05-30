using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTyper : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Référence au composant TextMeshProUGUI
    public GameObject dialogueCanvas;    // Référence au Canvas contenant le texte
    public CanvasGroup fadeCanvasGroup;  // Référence au CanvasGroup de l'image noire
    public float charactersPerSecond = 30f; // Vitesse d'affichage des caractères
    public int maxLines = 4;             // Nombre maximum de lignes affichées simultanément
    public float fadeDuration = 1f;      // Durée du fondu en secondes

    private Queue<string> dialogueLines = new Queue<string>(); // File des lignes de dialogue
    private List<string> currentDisplayLines = new List<string>(); // Lignes actuellement affichées
    private Coroutine typingCoroutine;

    // Méthode publique sans paramètres pour être appelée via un Signal
    public void TriggerDialogue()
    {
        string[] lines = new string[]
        {
            "Bienvenue dans notre jeu d'aventure.",
            "Vous incarnez un héros courageux.",
            "Votre mission est de sauver le royaume.",
            "Préparez-vous à affronter de nombreux défis.",
            "Bonne chance dans votre quête !"
        };

        StartCoroutine(PlayIntro(lines));
    }

    IEnumerator PlayIntro(string[] lines)
    {
        // Assurez-vous que l'image noire est active et opaque au début
        fadeCanvasGroup.gameObject.SetActive(true);
        fadeCanvasGroup.alpha = 1f;

        // Effectuer le fondu
        yield return StartCoroutine(FadeIn());

        // Démarrer le dialogue après le fondu
        StartDialogue(lines);
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.gameObject.SetActive(false); // Désactive l'image noire après le fondu
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines.Clear();
        foreach (string line in lines)
        {
            dialogueLines.Enqueue(line);
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueCanvas.SetActive(true); // S'assurer que le Canvas est actif au début
        typingCoroutine = StartCoroutine(TypeDialogue());
    }

    IEnumerator TypeDialogue()
    {
        while (dialogueLines.Count > 0)
        {
            string line = dialogueLines.Dequeue();

            if (currentDisplayLines.Count >= maxLines)
            {
                currentDisplayLines.RemoveAt(0); // Supprimer la première ligne pour faire de la place
            }

            currentDisplayLines.Add(""); // Ajouter une nouvelle ligne vide
            int lineIndex = currentDisplayLines.Count - 1;

            foreach (char c in line)
            {
                currentDisplayLines[lineIndex] += c;
                dialogueText.text = string.Join("\n", currentDisplayLines);
                yield return new WaitForSeconds(1f / charactersPerSecond);
            }

            // Attendre un court instant avant d'afficher la ligne suivante
            yield return new WaitForSeconds(0.5f);
        }

        // Tout le texte a été affiché, désactivation du Canvas
        dialogueCanvas.SetActive(false);
    }
}