namespace org.bizzle.pathfinding.pathingmap

open org.bizzle.pathfinding.coordinate

  type pathingMap (cols: int, rows: int, inMap: terrain [,]) =

    member this.colCount = cols
    member this.rowCount = rows
    member this.grid     = inMap

    member this.getTerrain (coord: coordinate) =
      match coord with
        | Coord(x, y) ->
            let isInBounds = (((x >= 0) && (x < this.colCount)) && ((y >= 0) && (y < this.rowCount)))
            if isInBounds then this.grid.[x, y]
            else               Invalid
        | BadCoord -> failwith "Cannot get terrain of invalid coordinate"

    member this.neighborsOf (loc: coordinate) =
      let filterFunc (x: direction) =
        let coord           = pathingMap.findNeighborCoord (loc, x)
        let neighborTerrain = this.getTerrain coord
        Terrain.isPassable neighborTerrain
      Seq.filter filterFunc direction.toSeq

    member this.step = function
      | (Coord(x1, y1), Coord(x2, y2)) ->
          this.grid.[x1, y1] <- Query
          this.grid.[x2, y2] <- Self
      | _ -> failwith "Cannot step to/from an invalid coordinate"

    member this.markAsGoal = function
      | Coord(x, y) -> this.grid.[x, y] <- Goal
      | BadCoord    -> failwith "Cannot mark invalid coordinate as goal"

    override this.ToString() =
      let xMax                = this.grid.GetLength(0) - 1
      let terrainsToStr x _ t = (string (Terrain.terrainToChar t)) + (if x = xMax then "\n" else "")
      let strings2D           = Array2D.mapi terrainsToStr this.grid
      let strings1D           = strings2D |> Seq.cast<string>
      Seq.fold (fun acc x -> acc + x) "" strings1D

    member this.clone () =
      let newGrid = Array2D.copy this.grid
      new pathingMap(this.colCount, this.rowCount, newGrid)

    member this.generateCloneWithPath (path: coordinate list) =
      let outMap = this.clone()
      for coord in path do (function | Coord(x, y) -> outMap.grid.[x, y] <- Path) coord
      outMap

    static member fromMapString (mapString: pathingMapString) =
      let data = PathingMapStringOps.fromPathingMapString(mapString)
      match data with
        | PathingMapData(start, goal, cols, rows, arr) -> (start, goal, new pathingMap(cols, rows, arr))

    static member findNeighborCoord = function
      | (Coord(x, y), dir) ->
          match dir with
          | North -> Coord(x,     y + 1)
          | South -> Coord(x,     y - 1)
          | East  -> Coord(x + 1, y)
          | West  -> Coord(x - 1, y)
      | (BadCoord, _) -> failwith "Cannot find neighbor of invalid coordinate"

    static member findDirection = function
      | (Coord(x1, y1) as startCoord, Coord(x2, y2) as endCoord) ->
          if   y2 = y1 + 1 then North
          elif y2 = y1 - 1 then South
          elif x2 = x1 + 1 then East
          elif x2 = x1 - 1 then West
          else
            let errorMessage = sprintf "%O or %O is/are invalid" startCoord endCoord
            raise (System.ArgumentException(errorMessage))
      | _ -> failwith "Cannot calculate direction to/from invalid coordinate"
