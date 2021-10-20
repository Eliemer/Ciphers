module CommonTypes

type AlphaString = 
    | AlphaString of string

    static member (+) (a, b) =
        let (AlphaString strA) = a
        let (AlphaString strB) = b
        AlphaString (strA + strB)

    static member map (f : string -> string) (x: AlphaString) : AlphaString = 
        let (AlphaString s) = x
        AlphaString (f s)

    member this.Length =
        let (AlphaString x) = this
        x.Length

type Alphabet =
    | Alphabet of string
        
