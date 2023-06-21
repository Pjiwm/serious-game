using UnityEngine;
using TMPro;
public class StatsManager : MonoBehaviour
{
    public static string FRIENDPREF = "friends";
    public static string SWORDPREF = "swordPieces";
    public TMP_Text friends;
    public TMP_Text swordPieces;

    private int friendsCount = 0;
    private int swordPiecesCount = 0;


    void Start() {
        friendsCount = PlayerPrefs.GetInt(FRIENDPREF, 0);
        swordPiecesCount = PlayerPrefs.GetInt(SWORDPREF, 0);
    }

    void Update() {
        friends.text = $"Vrienden git a{friendsCount}/3";
        swordPieces.text = $"Zwaardstukken {swordPiecesCount}/3";
    }

    void requestUpdate() {
        friendsCount = PlayerPrefs.GetInt(FRIENDPREF, 0);
        swordPiecesCount = PlayerPrefs.GetInt(SWORDPREF, 0);
    }

    public static void updatePref(string pref, int value) {
        PlayerPrefs.SetInt(pref, value);
        PlayerPrefs.Save();
    }


}
