using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera m_mainCamera;
    private PlayerInput m_playerInput;

    private InputAction m_touchPositionAction;
    private InputAction m_touchPressAction;

    private Interactable m_interactedPawn;
    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_touchPressAction = m_playerInput.actions.FindAction("TouchPress");
        m_touchPositionAction = m_playerInput.actions.FindAction("TouchPosition");
    }

    private void OnEnable()
    {
        m_touchPressAction.performed += TouchPressed;
		m_touchPressAction.canceled += TouchCancelled;
    }
    
    private void OnDisable()
    {
        m_touchPressAction.performed -= TouchPressed;
        m_touchPressAction.canceled -= TouchCancelled;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector3 position = new Vector3(
            m_touchPositionAction.ReadValue<Vector2>().x,m_touchPositionAction.ReadValue<Vector2>().y,0.0f
            );
        if (context.phase == InputActionPhase.Performed && 
            Physics.Raycast(m_mainCamera.ScreenPointToRay(m_touchPositionAction.ReadValue<Vector2>() ), out RaycastHit hit ))
        {
            Debug.Log(hit.collider.name);
            m_interactedPawn = hit.collider.GetComponent<Interactable>();
            if(m_interactedPawn)
                m_interactedPawn.TouchPerformedInput(position);
        }
        
    }

    private void TouchCancelled(InputAction.CallbackContext context)
    {
        if(m_interactedPawn)
            m_interactedPawn.TouchCancelledInput();
        m_interactedPawn = null;
    }
}
