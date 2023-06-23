using System.Collections.Generic;
using UnityEngine;

public class DiggerNPCController : NPCController
{
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private readonly string _name = "sword";
    [SerializeField] private Dialog allowedToPassDialog;
    [SerializeField] private Dialog friendsWithoutSwordDialog;
    [SerializeField] private Dialog friendsNotMadeDialog;
    [SerializeField] private Dialog canAlreadyPassDialog;

    protected override void OnInteract()
    {
        if (PlayerPrefs.GetInt(StatsManager.FRIENDPREF, 0) == 3)
        {
            if(PlayerPrefs.HasKey("canPass"))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(new Dialog() { lines = new List<string> { canAlreadyPassDialog.lines[UnityEngine.Random.Range(0, 3)] } }));
                DisableCones();
                return;
            }

            if (PlayerPrefs.HasKey(_name))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(allowedToPassDialog));
                PlayerPrefs.SetInt("canPass", 1);
                PlayerPrefs.Save();
                DisableCones();
            }
            else
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(friendsWithoutSwordDialog));
            }
        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(friendsNotMadeDialog));
        }
    }

    void DisableCones()
    {
        GameObject[] cones = GameObject.FindGameObjectsWithTag("Cone");
        foreach (GameObject cone in cones)
        {
            cone.SetActive(false);
        }
    }
}