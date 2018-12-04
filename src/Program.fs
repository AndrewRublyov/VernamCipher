open System
open BinaryString

[<EntryPoint>]
let main argv =
    let key = VernamEncryptor.generateKey 5
    let input = "Hello" |> stringToBinary
    let encoded = VernamEncryptor.encrypt key input
    let decoded = VernamEncryptor.decrypt key encoded

    binaryToString input   |> printfn "Original: \t%s"
    binaryToString key     |> printfn "Key: \t\t%s" 
    binaryToString encoded |> printfn "Encoded: \t%s"
    binaryToString decoded |> printfn "Decoded: \t%s"
    
    0 // return an integer exit code
