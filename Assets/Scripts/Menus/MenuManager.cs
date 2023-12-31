﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages navigation through the menu system
/// </summary>
public static class MenuManager
{
	/// <summary>
	/// Goes to the menu with the given name
	/// </summary>
	/// <param name="name">name of the menu to go to</param>
	public static void GoToMenu(MenuName name)
    {
        switch (name)
        {
            case MenuName.HighScore:

                // deactivate MainMenuCanvas and instantiate prefab
                GameObject Canvas = GameObject.Find("Canvas");
                if (Canvas != null)
                {
                    Canvas.SetActive(false);
                }
                Object.Instantiate(Resources.Load("HighScore"));
                break;

            case MenuName.Play:
                SceneManager.LoadScene("Play");
                break;
            case MenuName.Main:

                // go to MainMenu scene
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.Pause:

                // instantiate prefab
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;
        }
	}
}
