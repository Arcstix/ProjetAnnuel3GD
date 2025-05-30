using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasToShow;

    void Start()
    {
        canvasToShow.SetActive(false);
    }

    public void ShowCanvas()
    {
        canvasToShow.SetActive(true);
    }
}

//FindObjectOfType<CanvasController>().ShowCanvas(); = a mettre dans le script qui doit declencher ce canevas
