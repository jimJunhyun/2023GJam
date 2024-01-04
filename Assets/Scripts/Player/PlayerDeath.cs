using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private TransitionSettings _setted;
    public Transform deathVcam;
    public Transform miniMapUI;
    public Transform rhythmUI;
    public Transform deathUI;
    private void OnEnable()
    {
        deathVcam.gameObject.SetActive(false);
        miniMapUI.gameObject.SetActive(true);
        rhythmUI.gameObject.SetActive(true);
        deathUI.gameObject.SetActive(false);
    }
    public void Death()
    {
        deathVcam.gameObject.SetActive(true);

        miniMapUI.gameObject.SetActive(false);
        rhythmUI.gameObject.SetActive(false);

        deathUI.gameObject.SetActive(true);
        TransitionManager.Instance.Transition("StartScene", _setted, 7f);

    }


}
