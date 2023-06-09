# Serious Game Project
This is the repository for the Serious Game Project of the 2022-2023 school year.

# Start checklist:
Add cinemachine camera for easier camera effects
Use new input system

# Clean Code patterns checklist:

Seperate game objects from logic (player gameobject, shouldn't be attached to logic). Construct: Parent object with logic, child object with physics

Use new input system (you can use the legacy system when prototyping, but do refactor it later on)

Always use private and public accessors for methods.

If you want to make variables accessible to the editor, don't make them public. Use the [SerializeField] attribute instead.

One exception is Scriptable objects. They may use public fields

Never write to a Scriptable object from an outside class

Animations should be pascalcase:
correct: IsWalking
incorrect: isWalking

Always use const strings for "stringly typed" things:
private const string IS_WALKING = "isWalking"

Use private hash lookups for animations:
private static readonly int IsWalking = Animator.StringToHash("IsWalking");

Always put serialised/public attributes first.

Never use Unity tags. Use TryGetComponent instead (or LayerMasks):
raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)

Use the Input Events and C# eventhandler class when dealing with events.

When using a singleton, use Awake to initialise it and Start to consume it.

Each script that is used in a scriptable object needs to have SO as a postfix:
KitchenObjectSO


# Info
Getkey: returns true as long as the button is pressed
GetKeyDown: returns true once  (one frame) when the button is pressed

Je kan public variables tijdens het draaien van de game aanpassen.

Vector3.Lerp is for vectors, Slerp is for rotation

Animation component is legacy, always use animator

Make sure that an animation is modern. To check it, use debug inspector and untoggle legacy options.