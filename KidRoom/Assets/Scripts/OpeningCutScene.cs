using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpeningCutScene : MonoBehaviour {

    public ParticleSystem particles;

    public GameObject gameTitle;

    public CanvasGroup openSceneButtonGroup;

    public Animator buttonContainerController;

    private GameObject cameraGameObject;

    public void Start()
    {

        buttonContainerController = openSceneButtonGroup.GetComponent<Animator>();
        cameraGameObject = Camera.main.gameObject;

    }


    void OnEnable()
    {
        MenuEventDispatcher.OnStartGame += this.StartGame;
    }


    void OnDisable()
    {
        MenuEventDispatcher.OnStartGame -= this.StartGame;
    }



    public void StartGame()
    {
        buttonContainerController.SetTrigger("FadeOut");

        iTween.MoveTo(cameraGameObject, iTween.Hash("path", iTweenPath.GetPath("cutScene"),
            "time", 4.5f, "easetype", iTween.EaseType.easeInOutSine, "oncompletetarget" ,gameObject, "oncomplete", "ShowGameMenu"));

        iTween.RotateTo(cameraGameObject, iTween.Hash("rotation", new Vector3(24.68277f, 230.0f, 360), "easetype", iTween.EaseType.easeInOutSine, "time", 4.5f));

        particles.Stop();
        gameTitle.SetActive(false);
        openSceneButtonGroup.interactable = false;
    }


    public void ShowGameMenu()
    {
        MenuEventDispatcher.Instance.ShowInGameMenu(true);
    }



}
