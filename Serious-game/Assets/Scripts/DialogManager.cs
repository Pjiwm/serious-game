using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private DialogueInput dialogueInput;
    [SerializeField] private int lettersPerSecond;
    [SerializeField] private GameStateManager gameStateManager;
    
    public Action OnShowDialog;
    public Action OnCloseDialog;
    
    private TMPro.TextMeshProUGUI _dialogText;
    private Dialog _dialog;
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

    public IEnumerator ShowDialog(Dialog dialog)
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
            yield return new WaitForSeconds(1f / lettersPerSecond);
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
        dialogueInput.OnSkip += OnSkip;
        OnShowDialog?.Invoke();
        dialogBox.SetActive(true);
    }
    private void CloseDialog()
    {
        dialogueInput.OnSkip -= OnSkip;
        OnCloseDialog?.Invoke();
        dialogBox.SetActive(false);
        
        _dialogText.text = "";
        _currentLine = 0;
    }
}
