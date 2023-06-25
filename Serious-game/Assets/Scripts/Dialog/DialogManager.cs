using System;
using System.Collections;
using Inputs;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    /// <summary>
    /// The dialogmanager, the gameobject and its children is a singleton. This means that there will always be one instance
    /// of it and the canvas with the dialogbox
    /// </summary>
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private DialogInput dialogInput;
    [SerializeField] private int lettersPerSecond;
    [SerializeField] private GameStateManager gameStateManager;
    
    public Action OnShowDialog;
    public Action OnCloseDialog;
    
    private TMPro.TextMeshProUGUI _dialogText;
    private Dialog.Dialog _dialog;
    private int _currentLine = 0;
    private bool _isTyping;

    private void Start()
    {
        _dialogText = dialogBox.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        
        OnShowDialog += () =>
        {
            gameStateManager.ChangeToState(GameState.InDialogue);
            
        };
        
        OnCloseDialog += () =>
        {
            gameStateManager.ChangeToState(GameState.Roaming);
        };
    }

    private void OnSkip(object sender, EventArgs e)
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            TypeDialogInstant(_dialog.lines[_currentLine]);
            return;
        }
        
        _currentLine++;
        
        if (_currentLine < _dialog.lines.Count)
        {
            StartCoroutine(TypeDialog(_dialog.lines[_currentLine]));
        }
        else
        {
            CloseDialog();
        }
    }

    public IEnumerator ShowDialog(Dialog.Dialog dialog)
    {
        if (dialog.lines == null || dialog.lines.Count <= 0)
        {
            Debug.LogError("Dialog manager error: Dialog has no lines");
            yield break;
        }
        yield return new WaitForEndOfFrame();
        
        _dialog = dialog;
        ActivateDialog();
       
        StartCoroutine(TypeDialog(_dialog.lines[_currentLine]));
    }

    private IEnumerator TypeDialog(string text)
    {
        _isTyping = true;
        
        _dialogText.text = "";
        foreach (var letter in text.ToCharArray())
        {
            _dialogText.text += letter;
            yield return new WaitForSecondsRealtime(1f / lettersPerSecond);
        }
        
        _isTyping = false;
    }

    private void TypeDialogInstant(string text)
    {
        _isTyping = true;
        
        _dialogText.text = text;
        
        _isTyping = false;
    }
    
    private void ActivateDialog()
    {
        dialogInput.OnSkip += OnSkip;
        OnShowDialog?.Invoke();
        dialogBox.SetActive(true);
    }
    private void CloseDialog()
    {
        dialogInput.OnSkip -= OnSkip;
        OnCloseDialog?.Invoke();
        dialogBox.SetActive(false);
        
        _dialogText.text = "";
        _currentLine = 0;
    }
}
