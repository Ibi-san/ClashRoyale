using UnityEngine;
using UnityEngine.UI;

public class AuthorizationUI : MonoBehaviour
{
    [SerializeField] private Authorization _authorization;
    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;
    [SerializeField] private Button _authorizationButton;
    [SerializeField] private Button _registrationButton;
    [SerializeField] private GameObject _authorizationCanvas;
    [SerializeField] private GameObject _registrationCanvas;

    private void Awake()
    {
        _login.onEndEdit.AddListener(_authorization.SetLogin);
        _password.onEndEdit.AddListener(_authorization.SetPassword);

        _authorizationButton.onClick.AddListener(SignInClick);
        
        _registrationButton.onClick.AddListener(RegistrationClick);

        _authorization.Error += () =>
        {
            _authorizationButton.gameObject.SetActive(true);
            _registrationButton.gameObject.SetActive(true);
        };
    }

    private void RegistrationClick()
    {
        _authorizationCanvas.SetActive(false);
        _registrationCanvas.SetActive(true);
    }

    private void SignInClick()
    {
        _authorizationButton.gameObject.SetActive(false);
        _registrationButton.gameObject.SetActive(false);
        _authorization.SignIn();
    }
}