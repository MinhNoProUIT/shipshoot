using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using DG.Tweening;
using System.Threading;

public class ResetPassword : BaseMonoBehaviour
{
    [SerializeField] protected TMP_InputField emailInput;
    [SerializeField] protected Button sendButton;
    [SerializeField] protected Button closeButton;
    [SerializeField] protected Transform loginManager;
    [SerializeField] protected Transform resetPasswordSuccessful;
    [SerializeField] protected Transform resetPasswordFailed;
    private FirebaseAuth auth;
    private SynchronizationContext mainThreadContext;


    //[SerializeField] private TextMeshProUGUI feedbackText;

    protected override void OnEnable()
    {
        base.OnEnable();
        AppearanceEffect();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CloseEffect();
    }

    protected virtual void AppearanceEffect(){
        transform.localScale = Vector3.zero; // Đặt scale ban đầu là 0,0,0
        transform.DOScale(new Vector3(6,9,1), 0.5f).SetEase(Ease.OutBack);
    }

    protected virtual void CloseEffect()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack); // Scale xuống 0,0,0 trong 0.5s
    }

    protected override void Start()
    {
        base.Start();

        // Initialize Firebase
        auth = FirebaseAuth.DefaultInstance;
        mainThreadContext = SynchronizationContext.Current;


        // Attach the listener to the button
        sendButton.onClick.AddListener(SendClickListener);
        closeButton.onClick.AddListener(CloseResetPassword);
    }

    protected virtual void CloseResetPassword(){
        Debug.Log("Chuyển về màn hình đăng nhập");
        loginManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    protected virtual void SendClickListener()
    {
        string email = emailInput.text;

        if (string.IsNullOrEmpty(email))
        {
            //feedbackText.text = "Please enter a valid email address.";
            return;
        }

        SendPasswordResetEmail(email);
    }

    private void SendPasswordResetEmail(string email)
    {
        auth.SendPasswordResetEmailAsync(email).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogWarning("SendPasswordResetEmailAsync was canceled.");
                mainThreadContext.Post(_=>{
                    resetPasswordFailed.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                },null);
                
                //feedbackText.text = "Request canceled. Please try again.";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogWarning("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                resetPasswordFailed.gameObject.SetActive(true);
                gameObject.SetActive(false);
                //feedbackText.text = "Error: Unable to send reset email. Check your email and try again.";
                return;
            }

            Debug.Log("Password reset email sent successfully.");
            mainThreadContext.Post(_=>{
                    resetPasswordSuccessful.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                },null);
            //feedbackText.text = "Password reset email sent. Please check your inbox.";
        });
    }
}
