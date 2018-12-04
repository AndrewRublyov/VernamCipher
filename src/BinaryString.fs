module BinaryString

open System

type BinaryString = 
    | BinaryString of bool array
    override x.ToString() = 
        match x with
        | BinaryString arr -> 
            arr 
            |> Array.map (fun x -> if x then "1" else "0") 
            |> Array.reduce (+)


let xor (BinaryString a) (BinaryString b) =
    Array.zip a b
    |> Array.map (fun (first, second) -> first <> second)
    |> BinaryString

let concat (BinaryString a) (BinaryString b) =
    [| a; b |] |> Array.concat |> BinaryString

let (<^>) = xor 
let (<+>) = concat

let private align length (BinaryString input) =
    let zerosCount = length - (Seq.length input)
    [| false |> Array.replicate zerosCount; input |] 
    |> Array.concat
    |> BinaryString

let intToBinary (value:int)  =
    (Convert.ToString (value, 2)
    |> Seq.map (fun ch -> ch = '1'))
    |> Seq.toArray
    |> BinaryString

let stringToBinary (value:string) =
    value
    |> Seq.map (int >> intToBinary >> (align Constants.CHAR_SIZE))
    |> Seq.reduce (<+>)

let binaryToString (binaryString:BinaryString) = 
    let charsChunks = seq {
        let pos = ref 0
        let buffer = Array.zeroCreate<'t> Constants.CHAR_SIZE

        for x in binaryString.ToString() do
            buffer.[!pos] <- x
            if !pos = Constants.CHAR_SIZE - 1 then
                yield buffer |> Array.copy
                pos := 0
            else
                incr pos

        if !pos > 0 then
            yield Array.sub buffer 0 !pos
    }

    charsChunks 
    |> Seq.map (Seq.map string >> Seq.reduce (+))
    |> Seq.map (fun str -> Convert.ToByte(str, 2) |> char |> string)
    |> Seq.reduce (+)