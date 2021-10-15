namespace Common

open FsCheck

module Generators =

    type AlphaString = AlphaString of string
    
        // TODO : create string generator for [A-Z]
        type Generators =
            static member AlphaString () : Arbitrary<AlphaString> =
                Arb.fromGen (gen {
                    let! str = 
                        Gen.elements ['A'..'Z'] 
                        |> Gen.nonEmptyListOf
                        |> Gen.map List.toArray
                        |> Gen.map System.String
                    return AlphaString str
                })

