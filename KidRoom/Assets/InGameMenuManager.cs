using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InGameMenuManager : MonoBehaviour {


    public Canvas canavas;

    private Animator animController;

    public CanvasGroup cavasGroup;

    void Awake()
    {
        canavas = GetComponent<Canvas>();
        canavas.enabled = false;
        animController = GetComponent<Animator>();
    }


    /// <summary>
    /// register to show In game Menu
    /// </summary>
    void OnEnable()
    {
        MenuEventDispatcher.OnShowInGameMenu += this.ShowMenu;
    }


    /// <summary>
    /// to show In game Menu
    /// </summary>
    void OnDisable()
    {

        MenuEventDispatcher.OnShowInGameMenu -= this.ShowMenu;
    }

    public void ShowMenu(bool isShowMenu)
    {

        canavas.enabled = isShowMenu;
        animController.SetTrigger("inGameMenuTrigger");
        cavasGroup.interactable = true;
    }




}
