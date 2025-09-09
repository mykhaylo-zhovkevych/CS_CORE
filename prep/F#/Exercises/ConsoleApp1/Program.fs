open System 


[<EntryPoint>]
let main argv =
    // rec is recursive keyword
    // tail recursion is not using a stack, it is a f# feature, it clears the stack before each call
    let rec innerFunction () = 

        // Functions dont require parenthesses and commas betwenn paramaters

        printf "Enter a phrase, must be greater than 6 characters long"

        // line is value
        let line = Console.ReadLine()

        if line.Length > 6 then
            printf "Correct length: '%s'" line
        else
            printf "I said the phrase must be greaer than 6 characters"
            // wehn it is wrong repeat itslef
            innerFunction ()


    innerFunction ()


    printf "Done: press any key to exit"
    Console.ReadKey() |> ignore
    0