using System;
using System.Linq;
using Inputs;
using Interactables;
using UnityEngine;

namespace PlayerAndMovement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private float interactDistance;
        [SerializeField] private GameStateManager gameStateManager;
        [SerializeField] private GameObject footstepAudio;

        private SpriteRenderer _spriteRenderer;
        private MoveController _moveController;
        private Rigidbody2D _rigidBody2D;
        private Vector2 _moveDir;
        private IInteractable[] _selectedInteractables;
        private int _interactablesLayer;
        private int _minigameLayer;
        private Animator _animator;
        private bool _isMovingUp;
        private bool _isMovingDown;
        private bool _canMove = true;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsFacingUp = Animator.StringToHash("IsFacingUp");
        private static readonly int IsFacingDown = Animator.StringToHash("IsFacingDown");

        private void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _moveController = GetComponent<MoveController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            playerInput.OnInteract += OnInteract;
            gameStateManager.OnRoaming += ActivatePlayerInputs;
            gameStateManager.OnInDialog += DeActivatePlayerInputs;
            gameStateManager.OnMinigame += DeActivatePlayerInputs;
            _interactablesLayer = LayerMask.GetMask("Interactables");
            _minigameLayer = LayerMask.GetMask("Minigame");
            StopFootsteps();
            //To revert to default player prefs
            //PlayerPrefs.DeleteAll();
        }

        public void ActivatePlayerInputs()
        {
            playerInput.OnInteract += OnInteract;
        }

        public void DeActivatePlayerInputs()
        {
            playerInput.OnInteract -= OnInteract;
            _animator.SetBool(IsMoving, false);
        }

        public void FixedUpdate()
        {
            if (gameStateManager.State != GameState.Roaming) return;

            var inputVector = playerInput.GetMovementVectorNormalized();
            if (inputVector != Vector2.zero && _canMove)
            {
                _moveDir = inputVector;
                _isMovingDown = false;
                _isMovingUp = false;

                if (inputVector.x < 0)
                {
                    _spriteRenderer.flipX = true;
                }
                else if (inputVector.x > 0)
                {
                    _spriteRenderer.flipX = false;
                }

                if (inputVector.y > 0 && inputVector.x == 0)
                {
                    _isMovingUp = true;
                }
                else if (inputVector.y < 0 && inputVector.x == 0)
                {
                    _isMovingDown = true;
                }

                _animator.SetBool(IsFacingUp, _isMovingUp);
                _animator.SetBool(IsFacingDown, _isMovingDown);
                _animator.SetBool(IsMoving, true);

                _moveController.HandleMovement(_moveDir);
                StartFootsteps();
            } else {
                _animator.SetBool(IsMoving, false);
                StopFootsteps();
            }

            HandleSelections();
        }

        private void OnInteract(object sender, EventArgs e)
        {
            if (_selectedInteractables == null) return;
            foreach (var interactable in _selectedInteractables)
            {
                interactable.Interact();
            }
        }

        private void HandleSelections()
        {
            IInteractable[] currentlySelectedInteractables = null;
            
            var collidedInteractables = Physics2D.OverlapCircleAll(_rigidBody2D.position, interactDistance, _interactablesLayer);
            var collidedMinigames = Physics2D.OverlapCircleAll(_rigidBody2D.position, interactDistance, _minigameLayer);
            
            var minigames = collidedMinigames.Select(ob =>
            {
                var distance =
                    HelperFunctions.GetDistanceToVector(gameObject.transform.position, ob.transform.position);
                return (distance, ob);
            });
            
            var interactables = collidedInteractables.Select(ob =>
            {
                //Interactables are "further away" than minigames.
                var distance =
                    HelperFunctions.GetDistanceToVector(gameObject.transform.position, ob.transform.position) + 0.1f;
                return (distance, ob);
            });
            
            var allObjects = interactables.Concat(minigames).ToArray();
            if (allObjects.Length != 0)
            {
                var closestObject = allObjects.Length == 1 ? allObjects.First().ob : allObjects.Min().ob;
                currentlySelectedInteractables = closestObject.GetComponents<IInteractable>();
            }

            if (currentlySelectedInteractables != _selectedInteractables)
            {
                SetSelectedInteractables(currentlySelectedInteractables);
            }
        }

        private void SetSelectedInteractables(IInteractable[] selectedInteractables)
        {

            if (selectedInteractables == null)
            {
                foreach (var interactable in _selectedInteractables)
                {
                    interactable?.Deselect();
                }
            }

            if (selectedInteractables != null)
            {
                foreach (var interactable in selectedInteractables)
                {
                    interactable?.Select();
                }
            }

            _selectedInteractables = selectedInteractables;

        }

        private void OnDestroy()
        {
            playerInput.OnInteract -= OnInteract;
            gameStateManager.OnRoaming -= ActivatePlayerInputs;
            gameStateManager.OnInDialog -= DeActivatePlayerInputs;
            gameStateManager.OnMinigame -= DeActivatePlayerInputs;
        }

        private void StartFootsteps()
        {
            footstepAudio.SetActive(true);
        }

        private void StopFootsteps()
        {
            footstepAudio.SetActive(false);
        }

        public void LockMovement()
        {
            _canMove = false;
        }

        public void UnlockMovement()
        {
            _canMove = true;
        }

    }
}
