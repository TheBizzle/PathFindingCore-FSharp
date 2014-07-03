namespace org.bizzle.pathfinding.pathingmap

  type terrain =
    | Ant
    | Empty
    | Food
    | Goal
    | Mound
    | Invalid
    | Path
    | Query
    | Self
    | Wall
    | Water

  module Terrain =

    let isPassable = function
      | Ant     -> true
      | Empty   -> true
      | Food    -> true
      | Goal    -> true
      | Mound   -> true
      | Invalid -> false
      | Path    -> false
      | Query   -> false
      | Self    -> false
      | Wall    -> false
      | Water   -> false

    let charToTerrain = function
      | 'a' -> Ant
      | '_' -> Empty
      | 'f' -> Food
      | 'G' -> Goal
      | 'O' -> Mound
      | 'x' -> Path
      | '.' -> Query
      | '*' -> Self
      | 'D' -> Wall
      | '%' -> Water
      | _   -> Invalid

    let terrainToChar = function
      | Ant     -> 'a'
      | Empty   -> '_'
      | Food    -> 'f'
      | Goal    -> 'G'
      | Mound   -> 'O'
      | Path    -> 'x'
      | Query   -> '.'
      | Self    -> '*'
      | Wall    -> 'D'
      | Water   -> '%'
      | Invalid -> failwith "`Invalid` is not representable terrain"
