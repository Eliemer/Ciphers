



module CommonTypes
type AlphaString =
  | AlphaString of string
  with
    static member ( + ) : a:AlphaString * b:AlphaString -> AlphaString
    static member map : f:(string -> string) -> x:AlphaString -> AlphaString
    member Length : int
  end
type Alphabet = | Alphabet of string

namespace Ciphers
  module MonoAlphabeticCipher = begin
    val lookupChar : alphabet:string -> key:string -> c:char -> char
    val encode : alphabet:string -> key:string -> str:string -> string
    val decode : alphabet:string -> key:string -> str:string -> string
  end

namespace Ciphers
  module Caesar = begin
    /// Euclidean remainder, the proper modulo operation
    val inline private ( %! ) :
      a: ^a -> b: ^b ->  ^e
        when ( ^a or  ^b) : (static member ( % ) :  ^a *  ^b ->  ^c) and
             ( ^c or  ^b) : (static member ( + ) :  ^c *  ^b ->  ^d) and
             ( ^d or  ^b) : (static member ( % ) :  ^d *  ^b ->  ^e)
    /// <summary>
    /// Shift characters [A-Z] in a circular manner
    /// </summary>
    /// <param name="n">Amount to shift by</param>
    /// <param name="c">Character to shift. Valid characters are [A-Z]</param>
    val shiftChar : n:int -> c:char -> char
    val encode :
      key:int -> str:CommonTypes.AlphaString -> CommonTypes.AlphaString
    val decode :
      key:int -> str:CommonTypes.AlphaString -> CommonTypes.AlphaString
  end

namespace Ciphers
  module Main = begin
    val main : argv:string [] -> int
  end

