using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera m_mainCamera;
    private PlayerInput m_playerInput;

    private InputAction m_touchPositionAction;
    private InputAction m_touchPressAction;
    private InputAction m_touchHoldAction;
    private Interactable m_interactedPawn;
    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_touchPressAction = m_playerInput.actions.FindAction("TouchPress");
        m_touchHoldAction = m_playerInput.actions.FindAction("TouchHold");
        m_touchPositionAction = m_playerInput.actions.FindAction("TouchPosition");
    }

    private void OnEnable()
    {
        m_touchPressAction.performed += TouchPressed;
		//m_touchPressAction.canceled += TouchCancelled;
        m_touchHoldAction.performed += TouchHold;
        m_touchHoldAction.canceled += TouchCancelled;
    }
    
    private void OnDisable()
    {
        m_touchPressAction.performed -= TouchPressed;
       // m_touchPressAction.canceled -= TouchCancelled;
        m_touchHoldAction.performed += TouchHold;
        m_touchHoldAction.canceled += TouchCancelled;
    }
    private Vector3 GetTouchPosition(InputAction input)
    {
        Vector3 position = new Vector3(
            input.ReadValue<Vector2>().x, input.ReadValue<Vector2>().y, 0.0f);
        return position;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Touch");

        if (context.phase == InputActionPhase.Performed && 
            Physics.Raycast(m_mainCamera.ScreenPointToRay(m_touchPositionAction.ReadValue<Vector2>() ),
            out RaycastHit hit ))
        {
            Debug.Log(hit.collider.name);
            m_interactedPawn = hit.collider.GetComponent<Interactable>();
            if(m_interactedPawn)
                m_interactedPawn.TouchPerformedInput(GetTouchPosition(m_touchPositionAction));
        }
        
    }
    private void TouchHold(InputAction.CallbackContext context)
    {
        Debug.Log("Hold");
        if (context.phase == InputActionPhase.Performed &&
            Physics.Raycast(m_mainCamera.ScreenPointToRay(m_touchPositionAction.ReadValue<Vector2>()),
            out RaycastHit hit))
        {
            Debug.Log(hit.collider.name);
            m_interactedPawn = hit.collider.GetComponent<Interactable>();
            if (m_interactedPawn)
                m_interactedPawn.HoldPerformedInput(GetTouchPosition(m_touchPositionAction));
        }
    }

    private void TouchCancelled(InputAction.CallbackContext context)
    {
        Debug.Log("Let go");
        if(m_interactedPawn)
            m_interactedPawn.TouchCancelledInput();
        m_interactedPawn = null;
    }

}
