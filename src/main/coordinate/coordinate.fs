namespace org.bizzle.pathfinding.coordinate

  type coordinate =
    | Coord of x: int * y: int
    | BadCoord

  module Coords =

    let isValid = function
      | BadCoord -> false
      | _        -> true

    let overlaps = function
      | (BadCoord, _) | (_, BadCoord) -> false
      | (a, b)                        -> a = b


  type priorityCoordinate =
    | PCoord of priority: int * coord: coordinate

  module PCoords =

    let compare = function
      | (PCoord(p1, _), PCoord(p2, _)) -> p1 < p2


  type breadcrumb = {
    coordTo:   coordinate;
    coordFrom: coordinate
  }

