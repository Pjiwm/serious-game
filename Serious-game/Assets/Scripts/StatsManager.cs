using UnityEngine;
using TMPro;
public class StatsManager : MonoBehaviour
{
    public TMP_Text friends;
    public TMP_Text swordPieces;

    private int _friendsCount = 0;
    private int _swordPiecesCount = 0;


    void Start() {
        _friendsCount = PlayerPrefs.GetInt(PlayerPrefKeys.Friends, 0);
        _swordPiecesCount = PlayerPrefs.GetInt(PlayerPrefKeys.SwordPieces, 0);
    }

    private void Update() {
        friends.text = $"Vrienden {_friendsCount}/3";
        swordPieces.text = $"Zwaardstukken {_swordPiecesCount}/3";
    }

    public void RequestUpdate() {
        _friendsCount = PlayerPrefs.GetInt(PlayerPrefKeys.Friends, 0);
        _swordPiecesCount = PlayerPrefs.GetInt(PlayerPrefKeys.SwordPieces, 0);
    }

    public static void UpdatePref(string pref, int value) {
        PlayerPrefs.SetInt(pref, value);
        PlayerPrefs.Save();
    }


}
