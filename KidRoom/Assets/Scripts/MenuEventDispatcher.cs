using UnityEngine;
using System.Collections;

/// <summary>
/// This class encapsulate all menuRelated callbacks when user press a button in the game
/// since there a not a lot of button I encapsulted it in one class or other related menu only events
/// </summary>
public class MenuEventDispatcher : MonoBehaviour
{

    private static MenuEventDispatcher instance;

    public static MenuEventDispatcher Instance
    {

        get
        {
            return instance;
        }

    }

    void Awake()
    {

        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }



    #region EVENTS

    public delegate void StartGameHandler();
    public static event StartGameHandler OnStartGame;

    public delegate void ShowInGameMenuHandler(bool showGameMenu);
    public static event ShowInGameMenuHandler OnShowInGameMenu;

    public delegate void ThrowDiceHandler();
    public static event ThrowDiceHandler OnThrowDice;

    #endregion



    #region BUTTONS CALLBACKS


    public void StartGame()
    {
        if (OnStartGame != null)
        {
            OnStartGame();
        }

    }


    public void ChooseLevel()
    {

    }


    public void RollDice()
    {
        if (OnThrowDice != null)
        {
            OnThrowDice();
        }

    }


    public void ShowInGameMenu(bool toShow)
    {
        if (OnShowInGameMenu != null)
        {
            OnShowInGameMenu(toShow);
        }
    }


    #endregion




}
