namespace Ciphers

module MonoAlphabeticCipher = 

    let lookupChar (alphabet:string) (key : string) (c:char) : char =
        match Seq.tryFindIndex ((=) c) alphabet with
        | None -> failwith "character is outside of alphabet"
        | Some idx -> 
            match Seq.tryItem idx key with
            | None -> failwith "character is outside of key"
            | Some c -> c

    let encode (alphabet: string) (key : string) (str : string) : string = 
        String.map (lookupChar alphabet key) str

    let decode (alphabet: string) (key : string) (str : string) : string = 
        String.map (lookupChar key alphabet) str

