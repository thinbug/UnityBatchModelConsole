using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    public static UIRoot inst;
    [SerializeField] Transform mainMenu;
    [SerializeField] Button btnOpenServerUI;

    [SerializeField] Transform serverLogin;
    [SerializeField] InputField txtIp;
    [SerializeField] InputField txtPort;
    [SerializeField] Button btnLogin;

    [SerializeField] Transform Avatars;
    [SerializeField] Button btnAddOpenUI;

    [SerializeField] Transform AvatarNew;
    [SerializeField] InputField txtName;
    [SerializeField] Button btnAddChar;


    private void Awake()
    {
        inst = this;
        btnOpenServerUI.onClick.AddListener(OnOpenServerUI);
        btnLogin.onClick.AddListener(OnLogin);
        btnAddOpenUI.onClick.AddListener(OnOpenAddCharUI);
        btnAddChar.onClick.AddListener(OnAddChar);
    }

    private void OnAddChar()
    {
        throw new NotImplementedException();
    }

    private void OnOpenAddCharUI()
    {
        throw new NotImplementedException();
    }

    private void OnLogin()
    {
        ClientRoot.inst.LoginServer(txtIp.text, int.Parse(txtPort.text));
    }

    private void OnOpenServerUI()
    {
        mainMenu.gameObject.SetActive(false);
        serverLogin.gameObject.SetActive(true);
    }

    public void OpenUI()
    { 
        gameObject.SetActive(true);

        mainMenu.gameObject.SetActive(true);
        serverLogin.gameObject.SetActive(false);
        Avatars.gameObject.SetActive(false);
        AvatarNew.gameObject.SetActive(false);
    }

    public void CloseUI() 
    {
        gameObject.SetActive(false);

    }

    void Clear()
    { }
}
