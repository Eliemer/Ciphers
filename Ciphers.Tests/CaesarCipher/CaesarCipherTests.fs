namespace Ciphers.Tests

open Expecto
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
                fun (alphaStr : AlphaString) ->
                    let (AlphaString str) = alphaStr
                    encode 0 str = str

            testPropertyWithConfig config "Inverse is original" <|
                fun (alphaStr : AlphaString, n) ->
                    let (AlphaString str) = alphaStr
                    encode n str |> decode n = str

            testPropertyWithConfig config "Associative" <|
                fun (alphaStrA, alphaStrB, n) ->
                    let (AlphaString strA) = alphaStrA
                    let (AlphaString strB) = alphaStrB
                    let left = encode n (strA + strB)
                    let right = (encode n strA) + (encode n strB)
                    left = right

            testPropertyWithConfig config "Commutative" <|
                fun (alphaStr, n, m) ->
                    let (AlphaString str) = alphaStr
                    let left = encode n str |> encode m
                    let right = encode m str |> encode n
                    left = right

            testPropertyWithConfig config "Ciphered text is not original" <|
                fun (alphaStr, n) ->
                    let (AlphaString str) = alphaStr
                    let ciphered = encode n str
                    if n % 26 <> 0 then
                        ciphered <> str
                    else
                        ciphered = str

            testPropertyWithConfig config "Cipher preserves length" <|
                fun (alphaStr, n) ->
                    let (AlphaString str) = alphaStr
                    str.Length = (encode n str).Length

            testPropertyWithConfig config "Different args return different ciphers" <|
                fun alphaStr ->
                    let (AlphaString str) = alphaStr
                    seq {
                        for m in [1..25] do
                            for n in [(m+1)..25] do
                                encode n str <> encode m str
                    }
                    |> Seq.forall id
        ]