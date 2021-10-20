namespace Common

open FsCheck
open CommonTypes

module Generators =

    type Generators =
        static member AlphaString () : Arbitrary<AlphaString> =
            Arb.fromGen (gen {
                let! str = 
                    Gen.elements Constants.Alphabet
                    |> Gen.nonEmptyListOf
                    |> Gen.map List.toArray
                    |> Gen.map System.String
                return AlphaString str
            })

        static member Alphabet () : Arbitrary<Alphabet> =
            Arb.fromGen (gen {
                return! 
                    Gen.shuffle Constants.Alphabet
                    |> Gen.map (System.String >> Alphabet)
            })

