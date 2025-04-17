# UI scripts

This folder contains UI scripts to attach to our Unity UI Toolkit document.

## Adding a new UI Toolkit scene

1. Create the new UI toolkit document in the `/UI` folder through Unity.
    1. Create > UI Toolkit > UI Document
    1. Create > UI Toolkit > Style Sheet
1. Double click the document and make changes, giving reasonable IDs that may be referenced in code.
1. Create a new `.cs` file in this directory
    1. Include `using UnityEngine.UIElements;` at the top of the file.
    1. Include `[RequireComponent(typeof(UIDocument))]` to ensure that this script is only ever attached to a
       GameObject containing a UIDocument
1. Inside of the relevant GameObject in Unity (likely a top-level object called `UI Documents` in the scene),
   create a new UI document and name it `{Name}Document`.
1. Drag your new UI document file into the "Source Asset" property of the object, and drag the new `.cs`
   behavior script onto the object. Set the "Sort Order" property if applicable (behaves like CSS z-index).
1. Make the game object static (not necessary, but might improve performance).
1. In the script's `Awake()` handler, find references to each relevant object by querying the document
   with  `document.rootVisualElement.Q<ElType>(id)` and set up event listeners. Be sure to un-register
   any listeners in the `OnDestroy()` handler.

Recommended: Store common UI element IDs inside of the `UIElementIds` class.