namespace Ciphers.Tests

open Expecto
open FsCheck
open CommonTypes
open Common
open Common.Generators
open Ciphers.MonoAlphabeticCipher

module MonoAlphabeticCipherTests =

    let config = { FsCheckConfig.defaultConfig with 
                    maxTest = 10000
                    arbitrary = [typeof<Generators>]}

    let encodeWithAlphabet = encode Constants.Alphabet
    let decodeWithAlphabet = decode Constants.Alphabet

    [<Tests>]
    let tests =
        testList "Monoalphabetic Substitution Cipher Tests" [
        
            testPropertyWithConfig config "Identity" <|
                fun (alphaStr: AlphaString) ->
                    let (AlphaString str) = alphaStr
                    encodeWithAlphabet Constants.Alphabet str = str

            testPropertyWithConfig config "Inverse" <|
                fun (str, key) ->
                    let (AlphaString s, Alphabet k) = str, key
                    encodeWithAlphabet k s |> decodeWithAlphabet k = s

            testPropertyWithConfig config "Associative" <|
                fun (aStrA, aStrB, key) ->
                    let (Alphabet k) = key
                    let (AlphaString a, AlphaString b) = aStrA, aStrB
                    let left = encodeWithAlphabet k (a + b)
                    let right = (encodeWithAlphabet k a) + (encodeWithAlphabet k b)

                    left = right

            // This might fail when key is NOT a derangement of the alphabet
            testPropertyWithConfig config "Ciphered text is not original" <|
                fun (str, key) ->
                    let (AlphaString s, Alphabet k) = str, key
                    let ciphered = encodeWithAlphabet k s

                    if k = Constants.Alphabet then
                        ciphered = s
                    else
                        ciphered <> s

        ]
