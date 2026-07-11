using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject promptPanel;

    private void Start()
    {
        promptPanel.SetActive(false);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                promptPanel.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            else
            {
                promptPanel.SetActive(false);
            }
        }
        else
        {
            promptPanel.SetActive(false);
        }
    }
}