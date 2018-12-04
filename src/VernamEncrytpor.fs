module VernamEncryptor

open BinaryString

let private random = System.Random()

let generateKey length = 
    (Seq.init length (fun _ -> random.Next (256) |> char |> string))
    |> Seq.reduce (+)
    |> stringToBinary

let encrypt key input = (key <^> input)
let decrypt = encrypt