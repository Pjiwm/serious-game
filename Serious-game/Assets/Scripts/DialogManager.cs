using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] GameObject dialogBox;
    private TMPro.TextMeshProUGUI _dialogText;
    [SerializeField] private DialogueInput dialogueInput;
    [SerializeField] int lettersPerSecond;
    public Action OnShowDialog;
    public Action OnCloseDialog;
    private Dialog _dialog;
    private int _currentLine = 0;
    private bool _isTyping;

    private void Start()
    {
        _dialogText = dialogBox.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void OnSkip(object sender, EventArgs e)
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            TypeDialogInstant(_dialog.Lines[_currentLine]);
            return;
        }
        
        _currentLine++;
        
        if (_currentLine < _dialog.Lines.Count)
        {
            StartCoroutine(TypeDialog(_dialog.Lines[_currentLine]));
        }
        else
        {
            CloseDialog();
        }
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        if (dialog.Lines == null || dialog.Lines.Count <= 0)
        {
            Debug.LogError("Dialog manager error: Dialog has no lines");
            yield break;
        }
        yield return new WaitForEndOfFrame();
        
        _dialog = dialog;
        ActivateDialog();
       
        StartCoroutine(TypeDialog(_dialog.Lines[_currentLine]));
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
