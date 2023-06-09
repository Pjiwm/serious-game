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
    [SerializeField] TMPro.TextMeshProUGUI dialogText;
    [SerializeField] private DialogueInput dialogueInput;
    [SerializeField] int lettersPerSecond;
    public Action OnShowDialog;
    public Action OnCloseDialog;
    private Dialog _dialog;
    private int _currentLine = 0;
    private bool _isTyping;

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
        yield return new WaitForEndOfFrame();
        
        _dialog = dialog;
        ActivateDialog();
       
        StartCoroutine(TypeDialog(_dialog.Lines[_currentLine]));
    }

    private IEnumerator TypeDialog(string text)
    {
        _isTyping = true;
        
        dialogText.text = "";
        foreach (var letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        
        _isTyping = false;
    }

    private void TypeDialogInstant(string text)
    {
        _isTyping = true;
        
        dialogText.text = text;
        
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
        
        dialogText.text = "";
        _currentLine = 0;
    }
}