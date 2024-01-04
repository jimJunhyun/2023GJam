using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Transform deathVcam;
    public Transform miniMapUI;
    public Transform rhythmUI;
    public void Death()
    {
        deathVcam.gameObject.SetActive(true);

        miniMapUI.gameObject.SetActive(false);
        rhythmUI.gameObject.SetActive(false);
    }

}
