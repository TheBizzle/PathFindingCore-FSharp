namespace org.bizzle.pathfinding.pathingmap

open Microsoft.FSharp.Reflection

  type direction =
    | North
    | East
    | South
    | West
    static member toSeq =
      let cases = FSharpType.GetUnionCases(typeof<direction>)
      [
        for c in cases do
          let d = FSharpValue.MakeUnion(c, [| |]) :?> direction
          yield d
      ]
