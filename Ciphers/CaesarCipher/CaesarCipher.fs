namespace Ciphers

module Caesar = 

    /// Euclidean remainder, the proper modulo operation
    let inline private (%!) a b = (a % b + b) % b

    /// <summary>
    /// Shift characters [A-Z] in a circular manner
    /// </summary>
    /// <param name="n">Amount to shift by</param>
    /// <param name="c">Character to shift. Valid characters are [A-Z]</param>
    let shiftChar n c = 
        if Seq.contains c ['A'..'Z'] then
            int c
            |> (fun i -> i - int 'A') // center around 0
            |> (fun i -> (i + n) %! 26) // shift circularly
            |> (fun i -> i + int 'A') // offset to 'A'
            |> char
        else
            failwith $"{c} is not a valid character"

    let encode (key:int) (str:string) = 
        str
        |> String.map (shiftChar key)


    let decode (key:int) (str:string) = 
        str
        |> String.map (shiftChar -key)