using UnityEngine;
using UnityEngine.UI;

public class RegistrationUI : MonoBehaviour
{
    [SerializeField] private Registration _registration;
    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;
    [SerializeField] private InputField _confirmPassword;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Button _authorizationButton;
    [SerializeField] private GameObject _authorizationCanvas;
    [SerializeField] private GameObject _registrationCanvas;

    private void Awake()
    {
        _login.onEndEdit.AddListener(_registration.SetLogin);
        _password.onEndEdit.AddListener(_registration.SetPassword);
        _confirmPassword.onEndEdit.AddListener(_registration.SetConfirmPassword);

        _applyButton.onClick.AddListener(SignUpClick);
        
        _authorizationButton.onClick.AddListener(AuthorizationClick);

        _registration.Error += () =>
        {
            _authorizationButton.gameObject.SetActive(true);
            _applyButton.gameObject.SetActive(true);
        };

        _registration.Success += () =>
        {
            _authorizationButton.gameObject.SetActive(true);
        };
    }
    
    private void AuthorizationClick()
    {
        _registrationCanvas.SetActive(false);
        _authorizationCanvas.SetActive(true);
    }

    private void SignUpClick()
    {
        _authorizationButton.gameObject.SetActive(false);
        _applyButton.gameObject.SetActive(false);
        _registration.SignUp();
    }
}