// In F#, files are indeed compiled from top to bottom, and order matters
// C# does not compile files top to bottom in the same way F# does.

// Let binding associates a value to a name
// type is inferred enough sharp and determines what the type is
// And whitespace delimitation
// But I can still assign a type to my value, so wehn I need to
let myOne: double = 1.0

let myTwo = 2

let mutable isEnabled = true

// By default bindings are immutable so I cant go and change the value 
// the keyword mutable allows me to mutate the value of the binding

isEnabled <- false