namespace Ciphers.Tests

open Expecto
open CommonTypes
open Common.Generators
open Ciphers.Caesar

module CaesarTests =

    let config = { FsCheckConfig.defaultConfig with 
                    maxTest = 100; 
                    arbitrary = [typeof<Generators>] }

    [<Tests>]
    let tests =
        testList "Caesar Cipher Tests" [

            testPropertyWithConfig config "Identity is shift by zero" <|
                fun (str : AlphaString) ->
                    encode 0 str = str

            testPropertyWithConfig config "Inverse is original" <|
                fun (str : AlphaString, n) ->
                    encode n str |> decode n = str

            testPropertyWithConfig config "Associative" <|
                fun (strA, strB, n) ->
                    let left = encode n (strA + strB)
                    let right = (encode n strA) + (encode n strB)
                    left = right

            testPropertyWithConfig config "Commutative" <|
                fun (str, n, m) ->
                    let left = encode n str |> encode m
                    let right = encode m str |> encode n
                    left = right

            testPropertyWithConfig config "Ciphered text is not original" <|
                fun (str, n) ->
                    let ciphered = encode n str
                    if n % 26 <> 0 then
                        ciphered <> str
                    else
                        ciphered = str

            testPropertyWithConfig config "Cipher preserves length" <|
                fun (str : AlphaString, n) ->
                    str.Length = (encode n str).Length

            testPropertyWithConfig config "Different args return different ciphers" <|
                fun str ->
                    seq {
                        for m in [1..25] do
                            for n in [(m+1)..25] do
                                encode n str <> encode m str
                    }
                    |> Seq.forall id
        ]